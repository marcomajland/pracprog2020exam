using System;
using static System.Console;
public class qspline{
	double[] x;
	double[] y;
	double[] b;
	double[] c;
	public qspline(double[] xs, double[] ys){
		x = xs; y = ys;
		int n = x.Length;
		b = new double[n-1];
		c = new double[n-1];
		Func<int,double> p = (i) => (y[i+1]-y[i])/(x[i+1]-x[i]);
		c[0] = 0;
		for(int i=0;i<n-2;i++){
			c[i+1] = 1/(x[i+2] - x[i+1])*(p(i+1) - p(i) - c[i]*(x[i+1]-x[i]));
		}
		for(int i=0;i<n-1;i++){
			b[i] = p(i) - c[i]*(x[i+1]-x[i]);
		}		
	}
	public double spline(double z){
		int i = misc.binary_search(x, z);
		return y[i] + b[i]*(z - x[i]) + c[i]*(z-x[i])*(z-x[i]);
	}
	public double derivative(double z){
		int i = misc.binary_search(x, z);
		double dx = z - x[i];
		return b[i] + 2*c[i]*dx;
	}
	public double integral(double z){
		int i = misc.binary_search(x, z);
		double integral = 0;
		Func<int,double,double> F = delegate(int j, double dz){return y[j]*dz + 1.0/2.0*b[j]*dz*dz + 1.0/3.0*c[j]*dz*dz*dz;};
		for(int j=0;j<i;j++){integral += F(j, x[j+1] - x[j]);}
		integral += F(i, z - x[i]);
		return integral;
	}
}



