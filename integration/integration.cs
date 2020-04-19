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
		Tuple<double,int> f3_int = integrator.clenshaw_curtis(f3,a,b,delta,eps);
		double f3_err = f3_int.Item1 - 2.0;
		int f3_counts = f3_int.Item2;
		Tuple<double,int>  f4_int = integrator.clenshaw_curtis(f4,a,b,delta,eps);
		double f4_err = f4_int.Item1 + 4.0;
		int f4_counts = f4_int.Item2;
	
		WriteLine("Numerical integration");
		WriteLine("--------------------------------");
		WriteLine("A: Recursive adaptive integrator");
		WriteLine("--------------------------------");
		WriteLine($"The recursive adaptive integrator uses the trapezium rules and estimates local errors using embedded lower order rectangular rules. To test the numerical integration routine, the following definite integrals are calculated with absolute error delta = {delta} and relative error eps = {eps}. Errors are calculated as the difference between the numerical integration routine result and the analytical result.\n");
		WriteLine($"Definite integral of sqrt(x) from {a} to {b} (analytical result = 2/3):");
		WriteLine($"Numerical routine result: {f1_int.Item1}");
		WriteLine($"Error: {f1_err}");
		WriteLine($"Integration counts: {f1_counts}\n");
		WriteLine($"Definite integral of 4*sqrt(1-x*x) from {a} to {b} (analytical result = pi):");
		WriteLine($"Numerical routine result: {f2_int.Item1}");
		WriteLine($"Error: {f2_err}");
		WriteLine($"Integration counts: {f2_counts}\n");
		WriteLine("---------------------------------------------------------------");
		WriteLine("B: Open quadrature with Clenshaw-Curtis variable transformation");
		WriteLine("---------------------------------------------------------------");
		WriteLine($"The Clenshaw-Curtis variable transformation is implemented to improve numerical definite integrals with singularities at the end-points of the integration.\n");
		WriteLine($"Definite integral of 1/sqrt(x) from {a} to {b} (analytical result = 2):");
		WriteLine($"Numerical routine result: {f3_int.Item1}");
		WriteLine($"Error: {f3_err}");
		WriteLine($"Integration counts: {f3_counts}\n");
		WriteLine($"Definite integral of ln(x)/sqrt(x) from {a} to {b} (analytical result = -4):");
		WriteLine($"Numerical routine result: {f4_int.Item1}");
		WriteLine($"Error: {f4_err}");
		WriteLine($"Integration counts: {f4_counts}\n");	
		return 0;
	}
	public static Func<double,double> f1 = delegate(double x){		
		return Sqrt(x);
	};
	public static Func<double,double> f2 = delegate(double x){		
		return 4*Sqrt(1-x*x);
	};
	public static Func<double,double> f3 = delegate(double x){		
		return 1/Sqrt(x);
	};
	public static Func<double,double> f4= delegate(double x){		
		return Log(x)/Sqrt(x);
	};


}
