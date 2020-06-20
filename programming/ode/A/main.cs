using System;
using System.Collections.Generic;
using static System.Console;
using static System.Math;
class main{
	static Func<double,vector,vector> F = delegate(double x, vector y){		
		return new vector(y[0]-y[0]*y[0],0);
	};
	static Func<double,double> logistic = delegate(double x){return 1/(1 + Exp(-x));};
	public static int Main(){
		double a = 0;
		double b = 3;
		vector ya = new vector(0.5, 0.25);

		var xs = new List<double>();
		var ys = new List<vector>();		
	
		vector y = ode.rk23(F, a, ya, b, xlist:xs, ylist:ys);
		for(int i=0;i<xs.Count;i++){
			WriteLine($"{xs[i]} {ys[i][0]} {logistic(xs[i])}");
		}
		return 0;
	}
}
//public static vector rk23 // calls driver with rkstep23 stepper
//(Func<double,vector,vector> F, // the ODE system to integrate
//double a, // initial point
//vector ya, // functions at the initial point
//double b, // final point
//List<double> xlist=null, // list of x's, if needed
//List<vector> ylist=null, // list of y's, if needed
//double acc=1e-3, // absolute accuracy goal
//double eps=1e-3, // relative accuracy goal
//double h=0.1, //initial step-size
//int limit=999 // maximal number of steps
//)
//{
