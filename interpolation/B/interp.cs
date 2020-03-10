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
		Tuple<double[], double[]> res = qspline.qinterp(x,y);
		var table = new System.IO.StreamWriter("table.txt",append:false);
		var lspline_out = new System.IO.StreamWriter("lspline_out.txt",append:false);
		var qspline_out = new System.IO.StreamWriter("qspline_out.txt",append:false);
		double zmin = 0;
		double zmax = n-1;
		double dz = 0.001;
		for(int i = 0;i<n;i++){
			table.WriteLine($"{x[i]} {y[i]}");
		}
		table.Close();		
		double[] p = qspline.linterp(x,y);
		for(double z=zmin;z<=zmax;z+=dz){
			lspline_out.WriteLine($"{z} {qspline.levaluate(x, y, p, z)}");
		}
		lspline_out.Close();			
		for(double z=zmin;z<=zmax;z+=dz){
			qspline_out.WriteLine($"{z} {qspline.qevaluate(x, y, res.Item1, res.Item2, z)}");
		}
		qspline_out.Close();
		return 0;
	}
}
