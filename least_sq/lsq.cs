using System;
using static System.Console;
using static System.Math;
class lsq{
	public static int Main(){
		string[] xs = System.IO.File.ReadAllLines("x.txt");
		string[] ys = System.IO.File.ReadAllLines("y.txt");
		double[] x = new double[xs.Length];
		double[] y = new double[xs.Length];
		for (int i=0;i<x.Length;i++){
			x[i] = Log(double.Parse(xs[i])); // Logarithmic value
			y[i] = Log(double.Parse(ys[i])); // Logarithmic value
		}
		double[] dy = new double[y.Length];
		for(int i=0;i<dy.Length;i++){dy[i]=(y[i]/20)/y[i];} // MUST BE FIXED
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
		double l = Exp(-c[1]);
		double[] t = new double[100];
		for(int i=0;i<100;i++){t[i] = i;}
		var output = new System.IO.StreamWriter("output.txt",append:true);
		for(int i=0;i<t.Length;i++){
			output.WriteLine($"{t[i]} {exp(a,l)(t[i])}");
		}
		output.Close();
		
		return 0;
	}
	public static Func<double,double> exp(double a, double l){
		Func<double,double> f = delegate(double x){return a*Exp(-l*x);};
		return f;
	}
}




