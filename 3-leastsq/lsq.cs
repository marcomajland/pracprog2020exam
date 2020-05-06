using System;
using static System.Console;
using static System.Math;
class lsq{
	public static int Main(){
		// Loading raw data and converting to logarithmic values 
		string[] xs = System.IO.File.ReadAllLines("x.txt"); // Raw x data
		string[] ys = System.IO.File.ReadAllLines("y.txt"); // Raw y data
		double[] x = new double[xs.Length];
		double[] y = new double[xs.Length];
		for (int i=0;i<x.Length;i++){
			x[i] = double.Parse(xs[i]);
			y[i] = Log(double.Parse(ys[i])); // Logarithmic value
		}
		double[] dy = new double[y.Length]; // Allocate array for y errors
		for(int i=0;i<dy.Length;i++){dy[i]= (Exp(y[i])/20)/Exp(y[i]);} // Logarithmic y errors
		Func<double,double>[] F = new Func<double,double>[] {s => 1, s => s}; // Array of fitting functions
		var res = new lsq_qr(x,y,dy,F); // Variable storing instance of least square class
		vector c = res.get_c(); // Retrieve expansion coefficients of fitting functions
		double a = Exp(c[0]);
		double l = c[1];
		matrix cov = res.get_cov();
		cov.print();
		double[] dc = res.get_dc();
		double dl = Log(2)/(l*l)*dc[1]; // Uncertainty in t 1/2 obtained using error of propagation
		WriteLine("Fitting parameters:");
		WriteLine($"a = {a}");
		WriteLine($"lambda = {l}");
		WriteLine($"Fitting t_1/2 = {-Log(2)/l}({dl}) days");
		WriteLine($"Modern t_1/2 = 3.66 days");
		WriteLine("The modern value of t_1/2 seems to be within the uncertainty of the fitting t_1/2");

		var expout = new System.IO.StreamWriter("expout.txt",append:false);
		var logout1 = new System.IO.StreamWriter("logout1.txt",append:false);
		var logout2 = new System.IO.StreamWriter("logout2.txt",append:false);

		for(double i=0;i<20;i+=0.25){
			expout.WriteLine($"{i} {exp(a,l)(i)} {Exp(Fp(c,dc,F)(i))} {Exp(Fm(c,dc,F)(i))}");
		}
		expout.Close();
		for(int i=0;i<x.Length;i++){
			logout1.WriteLine($"{x[i]} {y[i]}");
		}
		logout1.Close();
		for(int i=0;i<x.Length;i++){
			logout2.WriteLine($"{x[i]} {c[0] + c[1]*x[i]}");
		}
		logout2.Close();
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




