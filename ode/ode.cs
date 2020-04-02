using System;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
class ode{
	public static int Main(){
		double a = 0;
		double b = 2*PI;
		vector ya = new vector(1, 0);
		Func<double,vector,vector> ODE = delegate(double x, vector y){		
			return new vector(y[1], 1 - y[0]);
		};			
		matrix ys = ode_solver.rk12(ODE, ya, a, b);

//		for(int i=0;i<n;i++){
//			WriteLine($"{xs[i]} {ys[i][0]}");
//		}
		return 0;
	}
}

