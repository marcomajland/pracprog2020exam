using System;
using static System.Console;
using static System.Math;
class lsq{
	public static int Main(){
		string[] xs = System.IO.File.ReadAllLines("x.txt"); // Raw x data
		string[] ys = System.IO.File.ReadAllLines("y.txt"); // Raw y data
		double[] x = new double[xs.Length];
		double[] y = new double[xs.Length];
		for (int i=0;i<x.Length;i++){
			x[i] = double.Parse(xs[i]);
			y[i] = Log(double.Parse(ys[i])); // Logarithmic value
		}
		double[] dy = new double[y.Length]; // Allocate array for y errors
		for(int i=0;i<dy.Length;i++){dy[i]= (Exp(y[i])/20)/Exp(y[i]);} // MUST BE FIXED
		Func<double,double>[] F = new Func<double,double>[] {s => 1, s => s};
		matrix A = new matrix(x.Length,F.Length);
		for(int i=0;i<F.Length;i++){
			for(int j=0;j<x.Length;j++){
				A[i][j] = F[i](x[j])/dy[j]; // Row/column convention interchanged
			}
		}
		vector b = new vector(x.Length);
		for(int i=0;i<x.Length;i++){
			b[i] = y[i]/dy[i];
		}
		qr AQR = new qr(A);
		vector c = AQR.solve(b);
		double a = Exp(c[0]);
		double l = c[1];
		WriteLine($"a = {a}");
		WriteLine($"lambda = {l}");
		WriteLine($"t = {-Log(2)/l}");
		var expout = new System.IO.StreamWriter("expout.txt",append:false);
		var logout1 = new System.IO.StreamWriter("logout1.txt",append:false);
		var logout2 = new System.IO.StreamWriter("logout2.txt",append:false);
		for(int i=0;i<x.Length;i++){
			expout.WriteLine($"{x[i]} {exp(a,l)(x[i])}");
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




