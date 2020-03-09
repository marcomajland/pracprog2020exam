using System;
using static System.Console;

class main{
	public static int Main(){
		//string[] input = System.IO.File.ReadAllLines("xy");
		int n = 10;
		double[] x = new double[n];
		double[] y = new double[n];		
		for(int i=0;i<n;i++){
			x[i] = i;
			y[i] = i*i;		
		}
		double[] p = linterp(x,y);
		var output1 = new System.IO.StreamWriter("output1.txt",append:false);
		var output2 = new System.IO.StreamWriter("output2.txt",append:false);
		double zmin = 0;
		double zmax = n-1;
		double dz = 0.001;
		for(int i = 0;i<n;i++){
			output1.WriteLine($"{x[i]} {y[i]}");
		}
		output1.Close();		
		for(double z=zmin;z<=zmax;z+=dz){
			output2.WriteLine($"{z} {evaluate(x, y, p, z)}");
		}
		output2.Close();
		double ints = linterp_int(x, y, p, 10);
		WriteLine($"{ints}");
		return 0;
	}
	static double[] linterp(double[] x, double[] y){
		double[] p = new double[x.Length-1];
		for(int i=0;i<x.Length-1;i++){
			p[i] = (y[i+1]-y[i])/(x[i+1]-x[i]);
		}
		return p;
	}
	static double linterp_int(double[] x, double[] y, double[] p, double z){
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
	static int binary_search(double[] x, double z){
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
	static double evaluate(double[] x, double[] y, double[] p, double z){
		int i = binary_search(x, z);
		return y[i] + p[i]*(z - x[i]);
	}
}




