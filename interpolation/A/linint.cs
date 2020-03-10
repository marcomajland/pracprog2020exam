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
		double[] p = lspline.linterp(x,y);
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
			output2.WriteLine($"{z} {lspline.evaluate(x, y, p, z)}");
		}
		output2.Close();
		double ints = lspline.linterp_int(x, y, p, 4);
		WriteLine($"{ints}");
		return 0;
	}
}
