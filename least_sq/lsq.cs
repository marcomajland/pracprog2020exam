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
		double[] c_unc = res.get_unc();
		double l_unc = Log(2)/(l*l)*c_unc[1]; // Uncertainty in t 1/2 obtained using error of propagation
		WriteLine("Fitting parameters:");
		WriteLine($"a = {a}");
		WriteLine($"lambda = {l}");
		WriteLine($"Fitting t_1/2 = {-Log(2)/l}({l_unc}) days");
		WriteLine($"Modern t_1/2 = 3.66 days");
		WriteLine("The modern value of t_1/2 seems to be within the uncertainty of the fitting t_1/2");

		var expout = new System.IO.StreamWriter("expout.txt",append:false);
		var logout1 = new System.IO.StreamWriter("logout1.txt",append:false);
		var logout2 = new System.IO.StreamWriter("logout2.txt",append:false);

/*		int n = 100;
		double[] t = new double[n];
		int j = 0;
		for(double i=0;i<x[x.Length-1];i+=x[x.Length-1]/n){
			t[j] = i;
			j++;
		}
*/		for(int i=0;i<x.Length;i++){
			expout.WriteLine($"{x[i]} {exp(a,l)(x[i])} {exp(a+Exp(c_unc[0]),l+c_unc[1])(x[i])} {exp(a-Exp(c_unc[0]),l-c_unc[1])(x[i])}");
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
}




