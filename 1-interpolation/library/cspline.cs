using System;
using static System.Console;
public class cspline{
	double[] x;
	double[] y;
	static int n;
	double[] b;
	double[] c;
	double[] d;
	double[] p;
	public cspline(double[] xs, double[] ys){
		x = xs; y = ys; 
		n = x.Length;
		p = new double[n-1];
		for(int i=0;i<n-1;i++){p[i] = (y[i+1]-y[i])/(x[i+1]-x[i]);}

		double[] D = new double[n];
		D[0] = 2;
		D[n-1] = 2;
		for(int i=0;i<n-2;i++){
			D[i+1] = 2*(x[i+1] - x[i])/(x[i+2]-x[i+1]) + 2;
		}
		double[] Q = new double[n-1];
		Q[0] = 1;
		for(int i=0;i<n-2;i++){
			Q[i+1] = (x[i+1]-x[i])/(x[i+2]-x[i+1]);
		}
		double[] B = new double[n];
		B[0] = 3*p[0];
		B[n-1] = 3*p[n-2];
		for(int i=0;i<n-2;i++){
			B[i+1] = 3*(p[i] + p[i+1]*(x[i+1]-x[i])/(x[i+2]-x[i+1]));
		}
		for(int i=1;i<n;i++){
			D[i] -= Q[i-1]/D[i-1];
			B[i] -= B[i-1]/D[i-1];		
		}
		b = new double[n];
		b[n-1] = B[n-1]/D[n-1];
		for(int i=n-2;i>=0;i--){
			b[i] = (B[i] - Q[i]*b[i+1])/D[i];
		}
		c = new double[n-1];
		d = new double[n-1];
		for(int i=0;i<n-1;i++){
			c[i] = (-2*b[i] - b[i+1] + 3*p[i])/(x[i+1] - x[i]);
			d[i] = (b[i] + b[i+1] - 2*p[i])/((x[i+1] - x[i])*(x[i+1] - x[i]));
		}		
	}
	public double spline(double z){
		int i = misc.binary_search(x, z);
		return y[i] + b[i]*(z - x[i]) + c[i]*(z-x[i])*(z-x[i]) + d[i]*(z-x[i])*(z-x[i])*(z-x[i]);
	}
	public double derivative(double z){
		return 0;
	}
	public double integral(double z){
		int i = misc.binary_search(x, z);
		double integral = 0;
		Func<int,double,double> F = delegate(int j, double dz){return y[j]*dz + 1.0/2.0*b[j]*dz*dz + 1.0/3.0*c[j]*dz*dz*dz + 1.0/4.0*d[j]*dz*dz*dz*dz;};
		for(int j=0;j<i;j++){integral += F(j,x[j+1] - x[j]);}
		double dzz = z - x[i];
		integral += y[i]*dzz + 1.0/2.0*b[i]*dzz*dzz + 1.0/3.0*c[i]*dzz*dzz*dzz + 1.0/4.0*d[i]*dzz*dzz*dzz*dzz;
		return integral;
	}
}




