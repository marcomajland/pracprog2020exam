using System;
using static System.Console;
public class lspline{
	public static double[] linterp(double[] x, double[] y){
		double[] p = new double[x.Length-1];
		for(int i=0;i<x.Length-1;i++){
			p[i] = (y[i+1]-y[i])/(x[i+1]-x[i]);
		}
		return p;
	}
	public static double linterp_int(double[] x, double[] y, double[] p, double z){
		double ints = 0;
		int im = binary_search(x, z);
		for(int i=0;i<im;i++){
			ints += quad.o8av(F(p[i], y[i]), x[i], x[i+1]);
		}
		ints += quad.o8av(F(p[im], y[im]), x[im+1], z);
		return ints;
	}
	public static Func<double,double> F(double a, double b){
		Func<double,double> f = delegate(double x){return a*x + b;};
		return f;
	}			
	public static double evaluate(double[] x, double[] y, double[] p, double z){
		int i = binary_search(x, z);
		return y[i] + p[i]*(z - x[i]);
	}
	public static int binary_search(double[] x, double z){
		int i = 0;
		int len = x.Length-1;
		while(i<len-1){
			int mid = (i+len)/2;
			if(x[mid] < z){
				i = mid;
			}
			else{
				len = mid;
			}
		}
		return i;	
	}
}



