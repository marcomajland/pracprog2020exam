using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
class main{
	public static int Main(){
		// The interpolation routines will be tested on the function f(x) = sin(x)
		double xmin = 0;	// Minimum x value
		double xmax = 3*PI;	// Maximum x value
		misc.generate_data(f1, xmin, xmax, 0.5, "./datafiles/data.txt");	// Generate tabulated function values
		misc.generate_data(f2, xmin, xmax, 0.5, "./datafiles/data2.txt");	// Generate tabulated integration values
		misc.generate_data(f3, xmin, xmax, 0.5, "./datafiles/data3.txt");	// Generate tabulated integration values
		// Load tabulated data values into double arrays
		List<double[]> data = misc.load_data("./datafiles/data.txt");
		double[] x = data[0];
		double[] y = data[1];
		int n = x.Length;

		// Preparation of output files for plotting
		var lspline_out = new System.IO.StreamWriter("./datafiles/lspline_out.txt",append:false);
		var qspline_out = new System.IO.StreamWriter("./datafiles/qspline_out.txt",append:false);
		var cspline_out = new System.IO.StreamWriter("./datafiles/cspline_out.txt",append:false);
		var outfile = new System.IO.StreamWriter("./out.txt",append:false);

		double dz = 0.01;
		// Output files for linear interpolation
		var res1 = new lspline(x,y);
		for(double z=xmin;z<=xmax;z+=dz){
			lspline_out.WriteLine($"{z} {res1.spline(z)} {res1.integral(z)}");
		}
		lspline_out.Close();			
		// Output files for quadratic interpolation
		var res2 = new qspline(x,y);
		for(double z=xmin;z<=xmax;z+=dz){
			qspline_out.WriteLine($"{z} {res2.spline(z)} {res2.integral(z)} {res2.derivative(z)}");
		}
		qspline_out.Close();
		// Output files for cubic interpolation
		var res3 = new cspline(x,y);
		for(double z=xmin;z<=xmax;z+=dz){
			cspline_out.WriteLine($"{z} {res3.spline(z)} {res3.integral(z)}");
		}
		cspline_out.Close();
		// Output files for comparsion of interpolation routines in terms of the integration values
		outfile.WriteLine($"In the following, the interpolation routines are compared with their integration values.");
		outfile.WriteLine($"For comparison, f(x) = sin(x) is interpolated and integrated from 0 to 2*pi (analytical value: 0).\n");
		outfile.WriteLine($"Linear interpolation:");
		outfile.WriteLine($"Integration result:       {res1.integral(2*PI)}");
		outfile.WriteLine($"Error:                    {0-res1.integral(2*PI)}\n");
		outfile.WriteLine($"Quadratic interpolation:");
		outfile.WriteLine($"Integration result:       {res2.integral(2*PI)}");	
		outfile.WriteLine($"Error:                    {0-res2.integral(2*PI)}\n");
		outfile.WriteLine($"Cubic interpolation:");
		outfile.WriteLine($"Integration result:       {res3.integral(2*PI)}");
		outfile.WriteLine($"Error:                    {0-res3.integral(2*PI)}\n");
		outfile.Close();
		return 0;
	}
	public static Func<double,double> f1 = delegate(double x){return Sin(x);};
	public static Func<double,double> f2 = delegate(double x){return -Cos(x)+1;};
	public static Func<double,double> f3 = delegate(double x){return Cos(x);};
}





