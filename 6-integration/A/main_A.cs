using System;
using static System.Math;
using static System.Console;
class integration{
	public static int Main(){
		double a = 0;
		double b = 1;
		double delta = 1e-3;
		double eps = 1e-3;

		Tuple<double,int> f1_int = integrator.integrate(f1,a,b,delta,eps);
		double f1_err = f1_int.Item1 - 2.0/3.0;
		int f1_counts = f1_int.Item2;
		Tuple<double,int>  f2_int = integrator.integrate(f2,a,b,delta,eps);
		double f2_err = f2_int.Item1 - PI;
		int f2_counts = f2_int.Item2;

		var outfile = new System.IO.StreamWriter($"../out_A.txt",append:false);
		outfile.WriteLine("-----------------------------");
		outfile.WriteLine("Recursive adaptive integrator");
		outfile.WriteLine("-----------------------------");
		outfile.WriteLine($"The recursive adaptive integrator uses the trapezium rules and estimates local errors using embedded lower order rectangular rules. To test the numerical integration routine, the following definite integrals are calculated with absolute error delta = {delta} and relative error eps = {eps}. Errors are calculated as the difference between the numerical integration routine result and the analytical result.\n");
		outfile.WriteLine($"Definite integral of sqrt(x) from {a} to {b}:");
		outfile.WriteLine($"Numerical routine result:       {f1_int.Item1}");
		outfile.WriteLine($"Analytical result:              2/3");
		outfile.WriteLine($"Error:                          {f1_err}");
		outfile.WriteLine($"Integration counts:             {f1_counts}\n");
		outfile.WriteLine($"Definite integral of 4*sqrt(1-x*x) from {a} to {b}:");
		outfile.WriteLine($"Numerical routine result:       {f2_int.Item1}");
		outfile.WriteLine($"Analytical result:              pi");
		outfile.WriteLine($"Error:                          {f2_err}");
		outfile.WriteLine($"Integration counts:             {f2_counts}\n");
		outfile.Close();
		return 0;
	}
	public static Func<double,double> f1 = delegate(double x){		
		return Sqrt(x);
	};
	public static Func<double,double> f2 = delegate(double x){		
		return 4*Sqrt(1-x*x);
	};
}
