using System;
using static System.Console;
class main{
	public static int Main(){
		string[] input = System.IO.File.ReadAllLines("table.txt");
		int n = input.Length;
		double[] x = new double[n];
		double[] y = new double[n];
		for(int i=0;i<n;i++){
			x[i] = double.Parse(input[i]);
			WriteLine($"{x[i]}");
		}
		var lspline_out = new System.IO.StreamWriter("lspline_out.txt",append:false);
		var qspline_out = new System.IO.StreamWriter("qspline_out.txt",append:false);
		var cspline_out = new System.IO.StreamWriter("cspline_out.txt",append:false);
		double zmin = 0;
		double zmax = n-1;
		double dz = 0.001;
		double[] lres = spline.linterp(x,y);
		for(double z=zmin;z<=zmax;z+=dz){
			lspline_out.WriteLine($"{z} {spline.levaluate(x, y, lres, z)}");
		}
		lspline_out.Close();			
		Tuple<double[], double[]> qres = spline.qinterp(x,y);
		for(double z=zmin;z<=zmax;z+=dz){
			qspline_out.WriteLine($"{z} {spline.qevaluate(x, y, qres.Item1, qres.Item2, z)}");
		}
		qspline_out.Close();
		Tuple<double[], double[], double[]> cres = spline.cinterp(x,y);
		for(double z=zmin;z<=zmax;z+=dz){
			cspline_out.WriteLine($"{z} {spline.cevaluate(x, y, cres.Item1, cres.Item2, cres.Item3, z)}");
		}
		cspline_out.Close();
		return 0;
	}
}





