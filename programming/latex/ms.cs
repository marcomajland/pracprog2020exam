using System;
using static System.Math;
using static System.Console;
using static System.Double;
class func{
	static double inf = PositiveInfinity;			
	// -------------------------------- //
	// INTEGRATION OF FRESNEL INTEGRALS //
	// -------------------------------- //
	// Integrands of Fresnel integrals
	static Func<double,double> Sint = delegate(double t){return Sin(PI/2*t*t);};
	static Func<double,double> Cint = delegate(double t){return Cos(PI/2*t*t);};
	public static int Main(){
	// S integral
	int ints = 500;
	var output = new System.IO.StreamWriter("output.txt",append:false);
	double xmin = 0;
	double xmax = 5;
	double dx = xmax/ints;

	for(double x=xmin;x<=xmax;x+=dx){
		output.WriteLine($"{x} {quad.o8av(Sint, 0, x)} {quad.o8av(Cint, 0, x)}");
	}
	output.Close();

	// -------------------------------- //
	//           CONVERGENCE    	    //
	// -------------------------------- //

	ints = 500;
	var output2 = new System.IO.StreamWriter("output2.txt",append:false);
	xmin = 10;
	xmax = 50;
	dx = xmax/ints;

	for(double x=xmin;x<=xmax;x+=dx){
		output2.WriteLine($"{x} {quad.o8av(Sint, 0, x)} {quad.o8av(Cint, 0, x)}");
	}
	output2.Close();

	return 0;
	}
}


















