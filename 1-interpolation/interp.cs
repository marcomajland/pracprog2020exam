using System;
using static System.Console;
using System.Collections.Generic;
class main{
	public static int Main(){
		List<double[]> data = misc.load_data("data.txt");
		double[] x = data[0];
		double[] y = data[1];
		int n = x.Length;

		var lspline_out = new System.IO.StreamWriter("lspline_out.txt",append:false);
		var qspline_out = new System.IO.StreamWriter("qspline_out.txt",append:false);
//		var cspline_out = new System.IO.StreamWriter("cspline_out.txt",append:false);
		double zmin = 0;
		double zmax = n-1;
		double dz = 0.001;

		var res1 = new linterp(x,y);
		for(double z=zmin;z<=zmax;z+=dz){
			lspline_out.WriteLine($"{z} {res1.spline(z)} {res1.integral(z)}");
		}
		lspline_out.Close();			

		var res = new qspline(x,y);
		for(double z=zmin;z<=zmax;z+=dz){
			qspline_out.WriteLine($"{z} {res.spline(z)}");
		}
		qspline_out.Close();
/*		Tuple<double[], double[], double[]> cres = spline.cinterp(x,y);
		for(double z=zmin;z<=zmax;z+=dz){
			cspline_out.WriteLine($"{z} {spline.cevaluate(x, y, cres.Item1, cres.Item2, cres.Item3, z)}");
		}
		cspline_out.Close();
*/
		return 0;
	}
}





