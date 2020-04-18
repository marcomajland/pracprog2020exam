using System;
using static System.Console;
using static System.Math;
public partial class integrator{
	public static double integrate(Func<double,double> f, double a, double b, double delta, double eps, int n=999){
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
		else{return integrate(f,a,(a+b)/2,delta/Sqrt(2),eps) + integrate(f,(a+b)/2,b,delta/Sqrt(2),eps);}
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

