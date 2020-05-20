using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
public partial class ode_solver{
	// Initializing method with default accuracy, epsilon and stepsize
	public static Tuple<List<double>, List<vector>> rk12(Func<double,vector,vector> f, vector ya, double a, double b, 
		double acc=1e-3, double eps=1e-3, double h=0.01, int step_limit = 999){
			return driver(f, ya, a, b, acc, eps, h, step_limit);
	}
	// Runge-Kutta stepper utilizing the Euler midpoint method which returns updated y-values and estimated error
	public static vector[] rkstep12(Func<double, vector, vector> f, double x, vector y, double h){
		vector k_0 = f(x,y); // Zeroth order Euler method
		vector k_12 = f(x + 0.5*h, y + 0.5*h*k_0); // First order
		vector yh = y + h*k_12; // Updated y-values
		vector err = (k_12 - k_0)*h; // Error estimate
		return new vector[] {yh, err};
	}
	public static vector[] rkstep23(Func<double,vector,vector> f, double x, vector y, double h){
		vector k_0 = f(x,y);
		vector k_1 = f(x + h/2, y + (h/2)*k_0);
		vector k_2 = f(x + 3*h/4, y + (3*h/4)*k_1);
		vector k_a = (2*k_0 + 3*k_1 + 4*k_2)/9;
		vector k_b = k_1;
		vector yh = y + k_a*h;
		vector err = (k_a-k_b)*h;
		return new vector[] {yh, err};
	}
	public static vector[] rkstep45(Func<double,vector,vector> f, double x, vector y, double h){
		vector k_0 = f(x,y);
		vector k_1 = f(x + 0.5*h, y + 0.5*h*k_0);
		vector k_2 = f(x + 0.5*h, y + 0.5*h*k_1);
		vector k_3 = f(x + h, y + h*k_2);
		vector k_a = 1/6.0*k_0 + 1/3.0*k_1 + 1/3.0*k_2 + 1/6.0*k_3;
		vector k_b = k_2;
		vector yh = y + k_a*h;
		vector err = (k_a - k_b)*h;
		return new vector[] {yh, err};
	}
	// Adaptive step size driver routine which utilizes the Runge-Kutta stepper with the Euler midpoint method
	public static Tuple<List<double>, List<vector>> driver(Func<double,vector,vector> f, vector ya, double a, double b, 									double acc, double eps, double h, int step_limit){
		List<double> xs = new List<double>(); // List to contain x-values
		List<vector> ys = new List<vector>(); // List to contain y-values
		double x; vector y; vector yh; vector dyh; vector tau; int n=0; vector ratios;
		xs.Add(a); // Add initial x-value to list
		ys.Add(ya); // Add initial y-value to list
		do{
			x = xs[n]; y = ys[n];
			if(x>=b){return new Tuple<List<double>, List<vector>>(xs, ys);}
			if(n>step_limit){return new Tuple<List<double>, List<vector>>(xs, ys);}
			if(x+h > b){h = b-x;}
			vector[] step = rkstep12(f,x,y,h);
			yh = step[0]; dyh = step[1];
			bool accept_step = true;
			tau = new vector(dyh.size);
			ratios = new vector(dyh.size);
			for(int i=0;i<tau.size;i++){;
				tau[i] = (eps*Abs(yh[i]) + acc)*Sqrt(h/(b-a));
				if(Abs(dyh[i]) > tau[i]){accept_step = false;}
				ratios[i] = Abs(tau[i]/dyh[i]);
			}
			double fac = ratios[0];
			for(int i=1;i<tau.size;i++){
				fac = Min(fac,ratios[i]);
			}
			double h_fac = Pow(fac,0.25)*0.95;
			if(h_fac < 2){h*=h_fac;}else{h*=2;}
			if(accept_step){
				n++;
				x += h;
				xs.Add(x);
				ys.Add(yh);
			}
		}while(true);
	}
}



