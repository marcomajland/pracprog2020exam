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
		double x; vector y; vector yh; double dyh; double tau;
		xs.Add(a); // Add initial x-value to list
		ys.Add(ya); // Add initial y-value to list
		int i=0;
		while(xs[i] < b-h){
			x = xs[i]; y = ys[i];
			vector[] step = rkstep12(f,x,y,h); 
			yh = step[0]; dyh = step[1].norm();
			tau = (eps*yh.norm() + acc)*Sqrt(h/(b-a)); // The tolerance is evaluated according to simple prescription
			if(dyh < tau){ // If the local error is less than tolerance, the step is accepted
				i++;
				x+=h;
				xs.Add(x);
				ys.Add(yh);
			}
			if(dyh > 0){h*=Pow(tau/dyh,0.25)*0.95;} else{h*=2;} // Update step size in adaptive step size routine
		}
		return new Tuple<List<double>, List<vector>>(xs, ys);
	}
}




