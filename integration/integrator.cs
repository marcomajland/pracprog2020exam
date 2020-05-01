using System;
using static System.Console;
using static System.Math;
public partial class integrator{
	public static int i=0;
	public static Tuple<double,int> integrate(Func<double,double> f, double a, double b, double delta, double eps){
		integrator.i = 0;
		double integral = trap_int(f,a,b,delta,eps);
		return Tuple.Create(integral,integrator.i);
	}
	public static double trap_int(Func<double,double> f, double a, double b, double delta, double eps, int n = 999){
		integrator.i++;
		double dx = (b-a)/n;
		vector xs = new vector(n);
		vector fs = new vector(n);
		xs[0] = a; fs[0] = f(a);
		for(int j=1;j<xs.size;j++){
			xs[j] = xs[j-1] + dx;
			fs[j] = f(xs[j]);
		}
		double Q = trap(xs, fs, dx);
		double q = rect(xs, fs, dx);
		double err = Abs(Q-q);
		double tol = delta + eps*Abs(Q);
		if(err < tol){return Q;}
		else{return trap_int(f,a,(a+b)/2.0,delta/Sqrt(2.0),eps) + trap_int(f,(a+b)/2.0,b,delta/Sqrt(2.0),eps);}
	}
	public static Tuple<double,int> clenshaw_curtis(Func<double,double> f, double a, double b, double delta, double eps){
		double ta = Acos(a); double tb = Acos(b);
		Func<double,double> fcc = delegate(double t){return -f(Cos(t))*Sin(t);};
		return integrate(fcc,ta,tb,delta,eps);
	}
	public static double rect(vector xs, vector fs, double dx){
		double integral=0;
		for(int j=0;j<xs.size;j++){integral += fs[j]*dx;}
		return integral;
	}
	public static double trap(vector xs, vector fs, double dx){
		double integral=0;
		for(int j=1;j<xs.size;j++){integral += 0.5*(fs[j-1] + fs[j])*dx;}
		return integral;
	}

}
