using System;
using static System.Console;
using static System.Math;
using static System.Double;
public partial class integrator{
	public static int i=0;
	public static Tuple<double,int> integrate(Func<double,double> f, double a, double b, double delta, double eps){
		integrator.i = 0;
		double inf = double.PositiveInfinity;
		double integral;
		if(a == -inf && b == inf){return clenshaw_curtis(f1(f),-1,1,delta,eps);}
		if(a != -inf && b == inf){return clenshaw_curtis(f2(f,a),0,1,delta,eps);}
		if(a == -inf && b != inf){return clenshaw_curtis(f3(f,b),-1,0,delta,eps);}
		integral = trap_int(f,a,b,delta,eps);
		return Tuple.Create(integral,integrator.i);
	}
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
	// The following functions perform variable transformation to account for infinite integration limits
	public static Func<double,double> f1(Func<double,double> f){
		Func<double,double> F = delegate(double t){return f(t/(1-t*t))*(1+t*t)/((1-t*t)*(1-t*t));};
		return F;
	}
	public static Func<double,double> f2(Func<double,double> f, double a){
		Func<double,double> F = delegate(double t){return f(a + t/(1-t))*1/((1-t)*(1-t));};
		return F;
	}
	public static Func<double,double> f3(Func<double,double> f, double b){
		Func<double,double> F = delegate(double t){return f(b + t/(1+t))*1/((1+t)*(1+t));};
		return F;
	}
}
