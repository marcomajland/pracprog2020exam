using System;
using static System.Math;
using static System.Console;
class integration{
	public static int Main(){
		double a = 0;
		double b = 1;
		double delta = 1e-3;
		double eps = 1e-3;

		double f1_int = integrator.integrate(f1,a,b,delta,eps);
		double f1_err = f1_int - 2.0/3.0;
		double f2_int = integrator.integrate(f2,a,b,delta,eps);
		double f2_err = f2_int - PI;
		
		WriteLine("Numerical integration");
		WriteLine("--------------------------------------");
		WriteLine("A: Recursive adaptive integrator");
		WriteLine("--------------------------------------");
		WriteLine($"The recursive adaptive integrator uses the trapezium rules and estimates local errors using embedded lower order rectangular rules. To test the numerical integration routine, the following definite integrals are calculated with absolute error delta = {delta} and relative error eps = {eps}. Errors are calculated as the difference between the numerical integration routine result and the analytical result.\n");
		WriteLine($"Definite integral of sqrt(x) from {a} to {b} (analytical result = 2/3):");
		WriteLine($"Numerical routine: {f1_int} with error {f1_err}\n");
		WriteLine($"Definite integral of 4*sqrt(1-x*x) from {a} to {b} (analytical result = pi):");
		WriteLine($"Numerical routine: {f2_int} with error {f2_err}");
	
		return 0;
	}
	public static Func<double,double> f1 = delegate(double x){		
		return Sqrt(x);
	};
	public static Func<double,double> f2 = delegate(double x){		
		return 4*Sqrt(1-x*x);
	};


}
