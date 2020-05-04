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
		Tuple<double,int>  f5_int = integrator.integrate(f5,a,b,delta,eps);
		double f5_err = f5_int.Item1 - PI;
		int f5_counts = f5_int.Item2;
		Tuple<double,int>  f6_int = integrator.clenshaw_curtis(f5,a,b,delta,eps);
		double f6_err = f6_int.Item1 - PI;
		int f6_counts = f6_int.Item2;

		int o8av_count=0;
		Func<double,double> f5_o8av = delegate(double x){
			o8av_count++;
			return 4*Sqrt(1-x*x);
		};
		double f7_int = quad.o8av(f5_o8av,a,b,delta,eps);
		double f7_err = f7_int - PI;
	
		WriteLine("Numerical integration");
		WriteLine("--------------------------------");
		WriteLine("A: Recursive adaptive integrator");
		WriteLine("--------------------------------");
		WriteLine($"The recursive adaptive integrator uses the trapezium rules and estimates local errors using embedded lower order rectangular rules. To test the numerical integration routine, the following definite integrals are calculated with absolute error delta = {delta} and relative error eps = {eps}. Errors are calculated as the difference between the numerical integration routine result and the analytical result.\n");
		WriteLine($"Definite integral of sqrt(x) from {a} to {b}:");
		WriteLine($"Numerical routine result:       {f1_int.Item1}");
		WriteLine($"Analytical result:              2/3");
		WriteLine($"Error:                          {f1_err}");
		WriteLine($"Integration counts:             {f1_counts}\n");
		WriteLine($"Definite integral of 4*sqrt(1-x*x) from {a} to {b}:");
		WriteLine($"Numerical routine result:       {f2_int.Item1}");
		WriteLine($"Analytical result:              pi");
		WriteLine($"Error:                          {f2_err}");
		WriteLine($"Integration counts:             {f2_counts}\n");
		WriteLine("---------------------------------------------------------------");
		WriteLine("B: Open quadrature with Clenshaw-Curtis variable transformation");
		WriteLine("---------------------------------------------------------------");
		WriteLine($"The Clenshaw-Curtis variable transformation is implemented to improve numerical definite integrals with singularities at the end-points of the integration.\n");
		WriteLine($"Definite integral of 1/sqrt(x) from {a} to {b}:");
		WriteLine($"Numerical routine result:       {f3_int.Item1}");
		WriteLine($"Analytical result:              2");
		WriteLine($"Error:                          {f3_err}");
		WriteLine($"Integration counts:             {f3_counts}\n");
		WriteLine($"Definite integral of ln(x)/sqrt(x) from {a} to {b}:");
		WriteLine($"Numerical routine result:       {f4_int.Item1}");
		WriteLine($"Analytical result:              -4");
		WriteLine($"Error:                          {f4_err}");
		WriteLine($"Integration counts:             {f4_counts}\n");	
		WriteLine($"The definite integral of 4*sqrt(1-x*x) from {a} to {b} is calculated with and without the Clenshaw-Curtis variable transformation for comparison. With delta = {delta} and eps = {eps}, the computation results are as follows:\n");
		WriteLine($"Without Clenshaw-Curtis:");
		WriteLine($"Integration result:             {f5_int.Item1}");
		WriteLine($"Analytical result:              pi");
		WriteLine($"Error:                          {f5_err}");
		WriteLine($"Integration counts:             {f5_counts}\n");
		WriteLine($"With Clenshaw-Curtis:");
		WriteLine($"Integration result:             {f6_int.Item1}");
		WriteLine($"Analytical result:              pi");
		WriteLine($"Error:                          {f6_err}");
		WriteLine($"Integration counts:             {f6_counts}\n");
		WriteLine($"Within the given accuracy, the error is substantially different for the two routines.\n");

		WriteLine($"{f7_int}");
		WriteLine($"{f7_err}");
		WriteLine($"{o8av_count}");

		Tuple<double,int> trapint;
		Tuple<double,int> CC;
		double o8av_int;

		double[] deltas = new double[] {1e-0, 1e-1, 1e-2, 1e-3, 1e-4, 1e-5, 1e-6, 1e-7, 1e-8, 1e-9, 1e-10, 1e-11, 1e-12, 1e-13, 1e-14, 1e-15};
		double[] CC_errors = new double[deltas.Length];
		double[] CC_counts = new double[deltas.Length];
		double[] o8av_errors = new double[deltas.Length];
		double[] o8av_counts = new double[deltas.Length];
		double[] trapint_errors = new double[deltas.Length];
		double[] trapint_counts = new double[deltas.Length];
		for(int j=0;j<deltas.Length;j++){
			trapint = integrator.integrate(f5,a,b,deltas[j],0.0);
			trapint_errors[j] = trapint.Item1 - PI;
			trapint_counts[j] = trapint.Item2;		
			CC = integrator.clenshaw_curtis(f5,a,b,deltas[j],0.0);
			CC_errors[j] = CC.Item1 - PI;
			CC_counts[j] = CC.Item2;		
			o8av_count=0;
			o8av_int = quad.o8av(f5_o8av,a,b,deltas[j],0.0);		
			o8av_errors[j] = o8av_int - PI;
			o8av_counts[j] = o8av_count;
		}
		var error_out = new System.IO.StreamWriter($"error_out.txt",append:false);
		for(int j=0;j<deltas.Length;j++){
			error_out.WriteLine($"{deltas[j]} {Abs(trapint_errors[j])} {Abs(CC_errors[j])} {Abs(o8av_errors[j])}");
		}
		error_out.Close();
		var counts_out = new System.IO.StreamWriter($"counts_out.txt",append:false);
		for(int j=0;j<deltas.Length;j++){
			counts_out.WriteLine($"{deltas[j]} {trapint_counts[j]} {CC_counts[j]} {o8av_counts[j]}");
		}
		counts_out.Close();
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
	public static Func<double,double> f5 = delegate(double x){
		return 4*Sqrt(1-x*x);
	};

}
