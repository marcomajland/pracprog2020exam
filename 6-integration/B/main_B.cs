using System;
using static System.Math;
using static System.Console;
class integration{
	public static int Main(){
		double a = 0;
		double b = 1;
		double delta = 1e-3;
		double eps = 1e-3;

		Tuple<double,int> f3_int = integrator.integrate(f3,a,b,delta,eps);
		double f3_err = f3_int.Item1 - 2.0;
		int f3_counts = f3_int.Item2;
		Tuple<double,int> f3_int_cc = integrator.clenshaw_curtis(f3,a,b,delta,eps);
		double f3_err_cc = f3_int_cc.Item1 - 2.0;
		int f3_counts_cc = f3_int_cc.Item2;

		Tuple<double,int>  f4_int = integrator.integrate(f4,a,b,delta,eps);
		double f4_err = f4_int.Item1 + 4.0;
		int f4_counts = f4_int.Item2;
		Tuple<double,int>  f4_int_cc = integrator.clenshaw_curtis(f4,a,b,delta,eps);
		double f4_err_cc = f4_int_cc.Item1 + 4.0;
		int f4_counts_cc = f4_int_cc.Item2;
		


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

		var outfile = new System.IO.StreamWriter($"outfile.txt",append:false);
		outfile.WriteLine("------------------------------------------------------------");
		outfile.WriteLine("Open quadrature with Clenshaw-Curtis variable transformation");
		outfile.WriteLine("------------------------------------------------------------");
		outfile.WriteLine($"The Clenshaw-Curtis variable transformation is implemented to improve numerical definite integrals with singularities at the end-points of the integration.\n");
		outfile.WriteLine($"Definite integral of 1/sqrt(x) from {a} to {b}:");
		outfile.WriteLine($"Numerical routine (without CC):       {f3_int.Item1}");
		outfile.WriteLine($"Numerical routine (with CC):          {f3_int_cc.Item1}");
		outfile.WriteLine($"Analytical result:                    2");
		outfile.WriteLine($"Error (without CC):                   {f3_err}");
		outfile.WriteLine($"Error (with CC):                      {f3_err_cc}");
		outfile.WriteLine($"Integration counts (without CC):      {f3_counts}");
		outfile.WriteLine($"Integration counts (with CC):         {f3_counts_cc}\n");
		outfile.WriteLine($"Definite integral of ln(x)/sqrt(x) from {a} to {b}:");
		outfile.WriteLine($"Numerical routine (without CC):       {f4_int.Item1}");
		outfile.WriteLine($"Numerical routine (with CC):          {f4_int_cc.Item1}");
		outfile.WriteLine($"Analytical result:                    -4");
		outfile.WriteLine($"Error (without CC):                   {f4_err}");
		outfile.WriteLine($"Error (with CC):                      {f4_err_cc}");
		outfile.WriteLine($"Integration counts (without CC):      {f4_counts}");
		outfile.WriteLine($"Integration counts (with CC):         {f4_counts_cc}\n");
		outfile.Close();
/*
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
*/		return 0;
	}
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
