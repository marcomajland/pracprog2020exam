using System;
using static System.Math;
using static System.Console;
class integration{
	public static int Main(){
		double inf = double.PositiveInfinity;
		double a = -inf;
		double b = inf;
		double delta = 1e-6;
		double eps = 1e-6;

		Tuple<double,int> f1_int = integrator.integrate(f1,-inf,inf,delta,eps); 
		Tuple<double,int> f2_int = integrator.integrate(f1,1,inf,delta,eps); 
		Tuple<double,int> f3_int = integrator.integrate(f1,-inf,1,delta,eps); 
		double f1_int_quado8 = quad.o8av(f1,-inf,inf,delta,eps);
		double f2_int_quado8 = quad.o8av(f1,1,inf,delta,eps);
		double f3_int_quado8 = quad.o8av(f1,-inf,1,delta,eps);

		WriteLine($"Integration routine:       {f1_int.Item1}");
		WriteLine($"o8av:                      {f1_int_quado8}");
		WriteLine($"Deviation:                 {f1_int_quado8-f1_int.Item1}\n");
		WriteLine($"Integration routine:       {f2_int.Item1}");
		WriteLine($"o8av:                      {f2_int_quado8}");
		WriteLine($"Deviation:                 {f2_int_quado8-f2_int.Item1}\n");
		WriteLine($"Integration routine:       {f3_int.Item1}");
		WriteLine($"o8av:                      {f3_int_quado8}");
		WriteLine($"Deviation:                 {f3_int_quado8-f3_int.Item1}\n");
		return 0;
	}
	public static Func<double,double> f1 = delegate(double x){		
		return Exp(-x*x);
	};
}
