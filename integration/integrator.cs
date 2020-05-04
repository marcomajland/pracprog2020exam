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
/*	public static double trap_int(Func<double,double> f, double a, double b, double delta, double eps, int n = 999){
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
*/
	public static double trap_int(Func<double,double> f, double a, double b, double delta, double eps, vector fs_old = null){
		integrator.i++;
		vector xs = new vector(a + (b-a)*1.0/6.0, a + (b-a)*2.0/6.0, a + (b-a)*4.0/6.0, a + (b-a)*5.0/6.0);
		vector fs = new vector(xs.size);
		for(int j=0;j<xs.size;j++){fs[j] = f(xs[j]);}
		double Q = trap(xs, fs, a, b);
		double q = rect(xs, fs, a, b);
		double err = Abs(Q-q);
		double tol = delta + eps*Abs(Q);
		if(err < tol){return Q;}
		else{return trap_int(f,a,(a+b)/2.0,delta/Sqrt(2.0),eps) + trap_int(f,(a+b)/2.0,b,delta/Sqrt(2.0),eps);}
	}
/*	public static Tuple<double,int> clenshaw_curtis(Func<double,double> f, double a, double b, double delta, double eps){
		double ta = Acos(a); double tb = Acos(b);
		Func<double,double> fcc = delegate(double t){return -f(Cos(t))*Sin(t);};
		return integrate(fcc,ta,tb,delta,eps);
	}
*/
	public static Tuple<double,int> clenshaw_curtis(Func<double,double> f, double a, double b, double delta, double eps){
		double alpha = (b-a)/2;
		Func<double,double> fu = delegate(double u){return alpha*f(alpha*u + (a+b)/2);};
		Func<double,double> fcc = delegate(double t){return fu(Cos(t))*Sin(t);};
		return integrate(fcc,0,PI,delta,eps);
	}
	public static double rect(vector xs, vector fs, double a, double b){
		double integral=0;
		vector ws = new vector((b-a)*1.0/4.0, (b-a)*1.0/4.0, (b-a)*1.0/4.0, (b-a)*1.0/4.0);
		for(int j=0;j<xs.size;j++){integral += fs[j]*ws[j];}
		return integral;
	}
	public static double trap(vector xs, vector fs, double a, double b){
		double integral=0;
		vector vs = new vector((b-a)*2.0/6.0, (b-a)*1.0/6.0, (b-a)*1.0/6.0, (b-a)*2.0/6.0);
		for(int j=0;j<xs.size;j++){integral += fs[j]*vs[j];}
		return integral;
	}
}
