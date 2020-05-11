using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
class main{
	public static int Main(){
		double xmin = 0;
		double xmax = 3*PI;
		misc.generate_data(f1, xmin, xmax, 0.5, "./datafiles/data.txt");
		misc.generate_data(f2, xmin, xmax, 0.5, "./datafiles/data2.txt");
		List<double[]> data = misc.load_data("./datafiles/data.txt");
		double[] x = data[0];
		double[] y = data[1];
		int n = x.Length;

		var lspline_out = new System.IO.StreamWriter("./datafiles/lspline_out.txt",append:false);
		var qspline_out = new System.IO.StreamWriter("./datafiles/qspline_out.txt",append:false);
		var cspline_out = new System.IO.StreamWriter("./datafiles/cspline_out.txt",append:false);

		double dz = 0.01;
		var res1 = new lspline(x,y);
		for(double z=xmin;z<=xmax;z+=dz){
			lspline_out.WriteLine($"{z} {res1.spline(z)} {res1.integral(z)}");
		}
		lspline_out.Close();			
		var res2 = new qspline(x,y);
		for(double z=xmin;z<=xmax;z+=dz){
			qspline_out.WriteLine($"{z} {res2.spline(z)} {res2.integral(z)}");
		}
		qspline_out.Close();
		var res3 = new cspline(x,y);
		for(double z=xmin;z<=xmax;z+=dz){
			cspline_out.WriteLine($"{z} {res3.spline(z)} {res3.integral(z)}");
		}
		cspline_out.Close();

		WriteLine($"{res1.integral(2*PI)}");
		WriteLine($"{res2.integral(2*PI)}");
		WriteLine($"{res3.integral(2*PI)}");

		return 0;
	}
	public static Func<double,double> f1 = delegate(double x){return Sin(x);};
	public static Func<double,double> f2 = delegate(double x){return -Cos(x)+1;};
}





