using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
class roots{
	public static int Main(){
		double eps = 1e-3;
		double dx = 1e-6;

		vector x1 = new vector(1.0,-1.0);
		vector x2 = new vector(-0.5,3.0);
		vector x_rosenbrock = new vector(0.5, 1.5);
		vector res1 = root_finder.newton(f1,x1,eps,dx);
		vector res2 = root_finder.newton(f2,x2,eps,dx);
		vector res_rosenbrock = root_finder.newton(rosenbrock,x_rosenbrock,eps,dx);

		var outfile = new System.IO.StreamWriter($"../A_out.txt",append:false);
		outfile.WriteLine($"--------------------------------------------------------------------");
		outfile.WriteLine($"Newton's method with numerical Jacobian and back-tracking linesearch");
		outfile.WriteLine($"--------------------------------------------------------------------");
		outfile.WriteLine($"The root finder routine is tested using the functions:");
		outfile.WriteLine($"f11(x) = x*x + 4*y*y - 9, f12(x) = 18*y - 14*x*x + 45");
		outfile.WriteLine($"and");
		outfile.WriteLine($"f21(x) = 2*(x-1) + 400*x*(x*x - y), f22(x) = 200*(y-x*x).\n");
		outfile.WriteLine($"Root finder results:\n");
		outfile.WriteLine($"f11(x)=f12(x)=0:");
		outfile.WriteLine($"Initial (x0,y0):              {x1[0]},{x1[1]}");		
		outfile.WriteLine($"Root (x,y):                   {res1[0]},{res1[1]}");
		outfile.WriteLine($"Error (f11(x),f12(x)):        {f1(res1)[0]},{f1(res1)[1]}\n");
		outfile.WriteLine($"f21(x)=f22(x)=0:");
		outfile.WriteLine($"Initial (x0,y0):              {x2[0]},{x2[1]}");		
		outfile.WriteLine($"Root (x,y):                   {res2[0]},{res2[1]}");
		outfile.WriteLine($"Error (f21(x),f22(x)):        {f2(res2)[0]},{f2(res2)[1]}\n");
		outfile.WriteLine($"Root finder results for Rosenbrock valley function:\n");
		outfile.WriteLine($"Initial (x0,y0):              {x_rosenbrock[0]},{x_rosenbrock[1]}");		
		outfile.WriteLine($"Root (x,y):                   {res_rosenbrock[0]},{res_rosenbrock[1]}");
		outfile.WriteLine($"Error (f21(x),f22(x)):        {rosenbrock(res_rosenbrock)[0]},{rosenbrock(res_rosenbrock)[1]}\n");
		outfile.Close();

		return 0;
	}
	public static Func<vector,vector> f1 = delegate(vector x){
		return new vector(x[0]*x[0] + 4*x[1]*x[1] - 9, 18*x[1] - 14*x[0]*x[0] + 45);};	
	public static Func<vector,vector> f2 = delegate(vector x){
		return new vector(2*(x[0]-1) + 400*x[0]*(x[0]*x[0] - x[1]), 200*(x[1]-x[0]*x[0]));};	
	public static Func<vector,vector> rosenbrock = delegate(vector x){
		return new vector(2*(200*x[0]*x[0]*x[0] - 200*x[0]*x[1] + x[0] - 1),200*(x[1] - x[0]*x[0]));};
}



