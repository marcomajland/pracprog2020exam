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
		var output = new System.IO.StreamWriter("output.txt",append:false);
		double zmin = 0.1;
		double zmax = n-2;
		double dz = 0.1;
		for(int i = 0;i<p.Length;i++){WriteLine($"{p[i]}");}
		for(double z=zmin;z<=zmax;z+=dz){
			output.WriteLine($"{z} {z*z} {evaluate(x, y, p, z)}");
		}
		output.Close();
		return 0;
	}
	static double[] linterp(double[] x, double[] y){
		double[] p = new double[x.Length];
		for(int i=0;i<x.Length-1;i++){
			p[i] = (y[i+1]-y[i])/(x[i+1]-x[i]);
		}
		return p;
	}
	static double evaluate(double[] x, double[] y, double[] p, double z){
		int i = binary_search(x, z);
		return y[i] + p[i]*(z - x[i]);
	}
	static int binary_search(double[] x, double z){
		int i = 0;
		int len = x.Length-1;
		while(i<=len){
			int mid = (i+len)/2;
			if(x[mid] < z){
				i = mid + 1;
			}
			else{
				len = mid - 1;
			}
		}
		return i;	
	}
}




