using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
class main_C{
	public static int Main(){
		// Loading raw data and converting to logarithmic values 
		List<double[]> data = misc.load_data("../data.txt");
		double[] x = data[0]; double[] y = data[1];
		for (int i=0;i<y.Length;i++){y[i] = Log(y[i]);} // Logarithmic value
		double[] dy = new double[y.Length]; // Allocate array for y errors
		for(int i=0;i<dy.Length;i++){dy[i]= (Exp(y[i])/20)/Exp(y[i]);} // Logarithmic y errors
		Func<double,double>[] F = new Func<double,double>[] {s => 1, s => s}; // Array of fitting functions
	
		var res = new lsq_qr(x,y,dy,F); // Variable storing instance of least square class
		vector c = res.get_c(); // Retrieve expansion coefficients of fitting functions
		double a = Exp(c[0]);
		double l = c[1];

		double[] dc = res.get_dc();
		double dl = Log(2)/(l*l)*dc[1]; // Uncertainty in t 1/2 obtained using error of propagation
		matrix cov = res.get_cov();

		var outfile = new System.IO.StreamWriter("./out_C.txt",append:false);
		outfile.WriteLine($"---------------------------------------");
		outfile.WriteLine($"ThX decay fit using least square method");
		outfile.WriteLine($"---------------------------------------");
		outfile.WriteLine("Fitting parameters:");
		outfile.WriteLine($"a:                {a}");
		outfile.WriteLine($"lambda:           {l}");
		outfile.WriteLine($"Fitting t_1/2:    {-Log(2)/l}({dl}) days");
		outfile.WriteLine($"Modern t_1/2:     3.66 days");
		outfile.WriteLine("The modern value of t_1/2 seems to be within the uncertainty of the fitting t_1/2\n");
		outfile.WriteLine($"Covariance matrix:");
		for(int ir=0;ir<cov.size1;ir++){for(int ic=0;ic<cov.size2;ic++){
			outfile.Write("{0,10:g3} ", cov[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.Close();

		var expout = new System.IO.StreamWriter("expout.txt",append:false);
		var logout = new System.IO.StreamWriter("logout.txt",append:false);

		for(double i=0;i<20;i+=0.25){expout.WriteLine($"{i} {exp(a,l)(i)} {Exp(Fp(c,dc,F)(i))} {Exp(Fm(c,dc,F)(i))}");}
		expout.Close();
		for(int i=0;i<x.Length;i++){logout.WriteLine($"{x[i]} {y[i]} {c[0] + c[1]*x[i]}");}
		logout.Close();
		return 0;
	}
	public static Func<double,double> exp(double a, double l){
		Func<double,double> f = delegate(double x){return a*Exp(l*x);};
		return f;
	}
	public static Func<double,double> Fp(double[] c, double[] dc, Func<double,double>[] F){
		Func<double,double> f = delegate(double x){double sum = 0;
			for(int i=0;i<F.Length;i++){sum += (c[i]+dc[i])*F[i](x);}
			return sum;
			};
		return f;
	}
	public static Func<double,double> Fm(double[] c, double[] dc, Func<double,double>[] F){
		Func<double,double> f = delegate(double x){double sum = 0;
			for(int i=0;i<F.Length;i++){sum += (c[i]-dc[i])*F[i](x);}
			return sum;
			};
		return f;
	}
}
