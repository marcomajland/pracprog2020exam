using System;
using System.Collections.Generic;
using static System.Console;
using static System.Math;
class main{		
	public static int Main(){
//		Non-relativistic motion
		double e = 0;
		double a = 0;
		double b = 2*PI;
		vector ya = new vector(1, 0);
//		SolveODE(e, a, ya, b);
		ya[0] = 1;
		ya[1] = -0.5;
//		SolveODE(e, a, ya, b);
//		Relativistic motion
		e = 0.01;
		b = 4*PI;
//		SolveODE(e, a, ya, b);

		return 0;
	}
	public static int SolveODE(double e, double a, vector ya, double b){
		var xs = new List<double>();
		var ys = new List<vector>();		
		Func<double,vector,vector> ODE = delegate(double x, vector y){		
			return new vector(y[1], 1 - y[0] + e*y[0]*y[0]);
		};			
		vector result = ode.rk23(ODE, a, ya, b, xlist:xs, ylist:ys);

		for(int i=0;i<xs.Count;i++){
			WriteLine($"{xs[i]} {ys[i][0]}");
		}
		return 0;
	}	
}
