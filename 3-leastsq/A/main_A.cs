using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
class main_A{
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

		var outfile = new System.IO.StreamWriter("../out_A.txt",append:false);
		outfile.WriteLine($"---------------------------------------");
		outfile.WriteLine($"ThX decay fit using least square method");
		outfile.WriteLine($"---------------------------------------");
		outfile.WriteLine("Fitting parameters:");
		outfile.WriteLine($"a:                {a}");
		outfile.WriteLine($"lambda:           {l}");
		outfile.WriteLine($"Fitting t_1/2:    {-Log(2)/l} days");
		outfile.WriteLine($"Modern t_1/2:     3.66 days");
		outfile.Close();

		var expout = new System.IO.StreamWriter("expout.txt",append:false);
		var logout = new System.IO.StreamWriter("logout.txt",append:false);

		for(double i=0;i<20;i+=0.25){
			expout.WriteLine($"{i} {exp(a,l)(i)}");
		}
		expout.Close();
		for(int i=0;i<x.Length;i++){
			logout.WriteLine($"{x[i]} {y[i]} {c[0] + c[1]*x[i]}");
		}
		logout.Close();
		return 0;
	}
	public static Func<double,double> exp(double a, double l){
		Func<double,double> f = delegate(double x){return a*Exp(l*x);};
		return f;
	}
}

