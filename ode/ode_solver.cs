using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
public partial class ode_solver{
	public static Tuple<List<double>, List<vector>> rk12(Func<double,vector,vector> f, vector ya, double a, double b, 
		double acc=1e-1, double eps=1e-1, double h=0.01){return driver(f, ya, a, b, acc, eps, h);}
	public static vector[] rkstep12(Func<double, vector, vector> f, double x, vector y, double h){
		vector k_0 = f(x,y);
		vector k_12 = f(x + 0.5*h, y + 0.5*h*k_0);
		vector k = k_12;

		vector yh = y + h*k;
		vector err = (k - k_0)*h;
	
		return new vector[] {yh, err};
	}
	public static Tuple<List<double>, List<vector>> driver(Func<double,vector,vector> f, vector ya, double a, double b, 									double acc, double eps, double h){
		List<double> xs = new List<double>();
		List<vector> ys = new List<vector>();
		double x; vector y; vector yh; double dyh; double tau;
		xs.Add(a);
		ys.Add(ya);
		int i=0;
		while(xs[i] < b-h){
			x = xs[i]; y = ys[i];
			vector[] step = rkstep12(f,x,y,h);
			yh = step[0]; dyh = step[1].norm();
			tau = (eps*yh.norm() + acc)*Sqrt(h/(b-a));
			if(dyh < tau){i++; x+=h; xs.Add(x); ys.Add(yh);}
			if(dyh > 0){h*=Pow(tau/dyh,0.25)*0.95;}else{h*=2;}
		}
		return new Tuple<List<double>, List<vector>>(xs, ys);
	}
}




