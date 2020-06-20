using System;
using static System.Math;
using static System.Console;
using static System.Double;
class func{
	static double inf = PositiveInfinity;			
	static Func<double,double> f1 = delegate(double x){return Log(x)/Sqrt(x);};
	static Func<double,double> f2 = delegate(double x){return Exp(-x*x);};
	static double p = 5;
	static double z = p + 1;
	static Func<double,double> f3 = delegate(double x){return Pow(Log(1/x),p);};
	static double gamma(double z){
		if(z<0) return -PI/Sin(PI*z)/gamma(1+z);
		if(z<1) return gamma(z+1)/z;
		if(z>2) return gamma(z-1)*(z-1);
		Func<double,double> f = delegate(double x){
			return Pow(x,z-1)*Exp(-x);
		};
		return quad.o8av(f,0,inf,acc:1e-6,eps:0);			
	}
	
	static Func<double,double> norm(double a){
		return delegate(double x){return Exp(-a*x*x);};
	}
	static Func<double,double> exp(double a){
		return delegate(double x){return (-a*a*x*x/2 + a/2 + x*x/2)*Exp(-a*x*x);};
	}	
	static double E(double a){
		double norm_int = quad.o8av(norm(a),-inf,inf);
		double exp_int = quad.o8av(exp(a),-inf,inf);
		return exp_int/norm_int;
	}
	public static int Main(){		
		double min = 0;
		double max = 2;
		double dx = 0.1;
		for(double x=min+dx;x<=max;x+=dx){
			double y = E(x);
			WriteLine($"{x}, {y}");
		}
//		double f1_int = quad.o8av(f1, 0, 1);
//		double f2_int = quad.o8av(f2, -inf, inf);
//		double f3_int = quad.o8av(f3, 0, 1);
//		Write($"f1 numerical: {f1_int}\n");
//		Write($"f2 numerical: {f2_int}\n");
//		Write($"f3 numerical: {f3_int}\n");
//		Write($"gamma({z}) = {gamma(z)}\n");

		return 0;
	}
}












