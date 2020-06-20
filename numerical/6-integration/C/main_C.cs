using System;
using static System.Math;
using static System.Console;
class integration{
	public static int Main(){
		double inf = double.PositiveInfinity;
		double delta = 1e-6;
		double eps = 1e-6;

		var outfile = new System.IO.StreamWriter($"../out_C.txt",append:false);	
		outfile.WriteLine("---------------");
		outfile.WriteLine("Infinite limits");
		outfile.WriteLine("---------------");
		outfile.WriteLine($"The following infinite limit integrals are evaluated using the integration routine and compared to the o8av matlib routine with delta = {delta} and eps = {eps}.\n");
		outfile.WriteLine($"Definite integral of exp(-x*x) from -inf to inf:\n");
		calculate_integrals(f1,-inf,inf,delta,eps,Sqrt(PI),ref outfile);	
		outfile.WriteLine($"Definite integral of exp(-x)*cos(x) from 0 to inf:\n");
		calculate_integrals(f2,0,inf,delta,eps,0.5,ref outfile);
		outfile.WriteLine($"Definite integral of exp(-x)*sin(x) from 0 to inf:\n");
		calculate_integrals(f3,0,inf,delta,eps,0.5,ref outfile);
		outfile.WriteLine($"Definite integral of exp(-x*x) from -inf to 0:\n");
		calculate_integrals(f1,-inf,0,delta,eps,Sqrt(PI)*0.5,ref outfile);
		outfile.Close();
		return 0;
	}
	public static void calculate_integrals(Func<double,double> f, double a, double b, double delta, double eps, double analytical, ref System.IO.StreamWriter outfile){
		int o8av_counts = 0;
		Func<double,double> f_o8av = delegate(double x){o8av_counts++; return f(x);};
		Tuple<double,int> integral = integrator.integrate(f,a,b,delta,eps); 
		o8av_counts=0;
		double integral_quado8 = quad.o8av(f_o8av,a,b,delta,eps);

		outfile.WriteLine($"Numerical routine:               {integral.Item1}");
		outfile.WriteLine($"Analytical result:               {analytical}");
		outfile.WriteLine($"Routine tolerance:               {delta-eps*Abs(integral.Item1)}");
		outfile.WriteLine($"Error (analytical-numerical):    {analytical-integral.Item1}");
		outfile.WriteLine($"o8av result:                     {integral_quado8}");
		outfile.WriteLine($"Routine counts:                  {integral.Item2}");
		outfile.WriteLine($"o8av counts:                     {o8av_counts}\n");
	}
	public static Func<double,double> f1 = delegate(double x){
		return Exp(-x*x);
	};
	public static Func<double,double> f2 = delegate(double x){
		return Exp(-x)*Cos(x);
	};
	public static Func<double,double> f3 = delegate(double x){
		return Exp(-x)*Sin(x);
	};
}
