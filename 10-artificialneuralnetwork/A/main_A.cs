using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main_A{
	public static int Main(){	
		double a = 0.0; double b = 3*PI;
//		double a = -1.0; double b = 1.0;
		misc.generate_data(f1, a, b, 0.1, "./datafiles/data.txt");
		List<double[]> data = misc.load_data("./datafiles/data.txt");
		double[] x = data[0]; double[] y = data[1];
		vector xs = new vector(x.Length); for(int i=0;i<xs.size;i++){xs[i] = x[i];}
		vector ys = new vector(y.Length); for(int i=0;i<ys.size;i++){ys[i] = y[i];}

		var ann1 = new ann(gaussian_wavelet,5);
		ann1.train(xs,ys);

		var plot = new System.IO.StreamWriter("./datafiles/plot.txt",append:false);
		for(double z=a;z<=b;z+=1.0/64){
			plot.WriteLine($"{z} {ann1.feedforward(z)}");
		}
		plot.Close();
		return 0;
	}
	public static Func<double,double> gaussian_wavelet = delegate(double x){return x*Exp(-x*x);};
	public static Func<double,double> gaussian = delegate(double x){return Exp(-x*x);};
	public static Func<double,double> wavelet = delegate(double x){return Cos(5*x)*Exp(-x*x);};
	public static Func<double,double> g=(x)=>Cos(5*x-1)*Exp(-x*x); // function to fit
	public static Func<double,double> f1 = delegate(double x){return Sin(x);};
	public static Func<double,double> f2 = delegate(double x){return -Cos(x)+1;};
	public static Func<double,double> f3 = delegate(double x){return Cos(x);};
}



		/*var errors = new System.IO.StreamWriter($"./plot_files/errors.txt",append:false);
		for(int N=N_min;N<=N_max;N+=dN){
			monte_carlo.plain(f,a,b,N,ref result,ref error);
			errors.WriteLine($"{N} {error} {factor/Sqrt(N)} {analytical-result}");
		}
		errors.Close();*/
