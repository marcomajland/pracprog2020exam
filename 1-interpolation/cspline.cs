using System;
using static System.Console;
public class cspline{
	double[] x;
	double[] y;
	static int n;
	double[] b = new double[n];
	double[] c = new double[n-1];
	double[] d = new double[n-1];
	double[] D = new double[n];
	double[] Q = new double[n-1];
	double[] B = new double[n];
	public cspline(double[] xs, double[] ys){
		x = xs; y = ys; 
		n = x.Length;
		Func<int,double> p = (i) => (y[i+1]-y[i])/(x[i+1]-x[i]);
		D[0] = 2;
		D[n-1] = 2;
		for(int i=0;i<n-2;i++){
			D[i+1] = 2*(x[i+1] - x[i])/(x[i+2]-x[i+1]) + 2;
		}
		Q[0] = 1;
		for(int i=0;i<n-2;i++){
			Q[i+1] = (x[i+1]-x[i])/(x[i+2]-x[i+1]);
		}
		B[0] = 3*p(0);
		B[n-1] = 3*p(n-2);
		for(int i=0;i<n-2;i++){
			B[i+1] = 3*(p(i) + p(i+1)*(x[i+1]-x[i])/(x[i+2]-x[i+1]));
		}
		for(int i=1;i<n;i++){
			D[i] -= Q[i-1]/D[i-1];
			B[i] -= B[i-1]/D[i-1];		
		}
		b[n-1] = B[n-1]/D[n-1];
		for(int i=n-2;i>=0;i--){
			b[i] = (B[i] - Q[i]*b[i+1])/D[i];
		}
		for(int i=0;i<n-1;i++){
			c[i] = (-2*b[i] - b[i+1] + 3*p(i))/(x[i+1] - x[i]);
			d[i] = (b[i] + b[i+1] - 2*p(i)/((x[i+1] - x[i]))*(x[i+1] - x[i]));
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
		return 0;
	}
}
