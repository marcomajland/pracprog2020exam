using System;
using static System.Console;
public class lspline{
	double[] x;
	double[] y;
	double[] p;
	public lspline(double[] xs, double[] ys){
		x = xs; y = ys;
		p = new double[xs.Length-1];
		for(int i=0;i<xs.Length-1;i++){p[i] = (y[i+1]-y[i])/(x[i+1]-x[i]);} // Calculate slope values
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
		double integral = 0;
		Func<int,double,double> F = delegate(int j, double dz){return y[j]*dz + 1.0/2.0*p[j]*dz*dz;};
		for(int j=0;j<i;j++){integral += F(j, x[j+1] - x[j]);}
		integral += F(i, z - x[i]);
		return integral;
	}
}
