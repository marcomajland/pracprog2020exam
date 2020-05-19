using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
public partial class ode_solver{
	// Initializing method with default accuracy, epsilon and stepsize
	public static Tuple<List<double>, List<vector>> rk12(Func<double,vector,vector> f, vector ya, double a, double b, 
		double acc=1e-1, double eps=1e-1, double h=0.01){
			return driver(f, ya, a, b, acc, eps, h);
	}
	// Runge-Kutta stepper utilizing the Euler midpoint method which returns updated y-values and estimated error
	public static vector[] rkstep12(Func<double, vector, vector> f, double x, vector y, double h){
		vector k_0 = f(x,y); // Zeroth order Euler method
		vector k_12 = f(x + 0.5*h, y + 0.5*h*k_0); // First order

		vector yh = y + h*k_12; // Updated y-values
		vector err = (k_12 - k_0)*h; // Error estimate
	
		return new vector[] {yh, err};
	}
	// Adaptive step size driver routine which utilizes the Runge-Kutta stepper with the Euler midpoint method
	public static Tuple<List<double>, List<vector>> driver(Func<double,vector,vector> f, vector ya, double a, double b, 									double acc, double eps, double h){
		List<double> xs = new List<double>(); // List to contain x-values
		List<vector> ys = new List<vector>(); // List to contain y-values
		double x; vector y; vector yh; vector dyh; vector tau; int n=0; int step_limit = 999;
		vector ratios;
		xs.Add(a); // Add initial x-value to list
		ys.Add(ya); // Add initial y-value to list
		do{
			x = xs[n]; y = ys[n];
			if(n>step_limit){return new Tuple<List<double>, List<vector>>(xs, ys);}
			if(x>=b){return new Tuple<List<double>, List<vector>>(xs, ys);}
			if(a+h > b){h = b-a;}
			vector[] step = rkstep12(f,x,y,h);
			yh = step[0]; dyh = step[1];
			bool accept_step = true;
			tau = new vector(dyh.size);
			ratios = new vector(dyh.size);
			for(int i=0;i<tau.size;i++){;
				tau[i] = Max(acc,Abs(yh[i])*eps)*Sqrt(h/(b-a));
				if(Abs(dyh[i]) > Abs(tau[i])) {accept_step = false;}
				ratios[i] = Abs(tau[i])/Abs(dyh[i]);
			}
			double fac = ratios[0];
			for(int i=1;i<tau.size;i++){
				fac = Min(fac,ratios[i]);
			}
			h *= Min(Pow(fac,0.25)*0.95,2);
			for(int i=0;i<tau.size;i++){if(Abs(dyh[i]) > tau[i]){accept_step = false;}}
			if(accept_step){
				x += h;
				xs.Add(x);
				ys.Add(yh);
				n++;
			}
		}while(true);
	}
}




