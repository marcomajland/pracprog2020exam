using System;
using static System.Console;
public class linterp{
	double[] x;
	double[] y;
	double[] p;
	public linterp(double[] xs, double[] ys){
		x = xs; y = ys;
		p = new double[xs.Length-1];
		Func<int,double> pf = (i) => (y[i+1]-y[i])/(x[i+1]-x[i]);
		for(int i=0;i<xs.Length-1;i++){p[i] = pf(i);}
	}
	public double spline(double z){
		int i = misc.binary_search(x, z);	
		return y[i] + p[i]*(z - x[i]);
	}
	public double derivative(double z){
		int i = misc.binary_search(x, z);
		return p[i];
	}
	public double integral(double z){
		int i = misc.binary_search(x, z);
//		return (y[i] - p[i]*x[i])*z + 1/2*p[i]*z*z;
		return y[i]*z + 1.0/2.0*p[i]*(z - x[i])*(z - x[i]);
	}
}
