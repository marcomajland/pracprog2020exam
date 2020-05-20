using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
public partial class ode_solver{
	// Initializing method with default accuracy, epsilon and stepsize
	public static Tuple<List<double>, List<vector>> solve(Func<double,vector,vector> f, vector ya, double a, double b, 
								double acc=1e-3, double eps=1e-3, double h=0.01,
								int step_limit = 999,string stepper="rk12"){
		if(stepper=="rk12"){return driver(f, ya, a, b, acc, eps, h, step_limit,rkstep12);}
		if(stepper=="rk45"){return driver(f, ya, a, b, acc, eps, h, step_limit,rkstep45);}
		else{return driver(f, ya, a, b, acc, eps, h, step_limit,rkstep12);}
	}
	// Adaptive step size driver routine which utilizes the Runge-Kutta stepper with the Euler midpoint method

	public static Tuple<List<double>, List<vector>> driver(Func<double,vector,vector> f, vector ya, double a, double b, 								double acc, double eps, double h, int step_limit,
							Func<Func<double,vector,vector>, double, vector,double,vector[]> stepper){
		List<double> xs = new List<double>(); // List to contain x-values
		List<vector> ys = new List<vector>(); // List to contain y-values
		double x = a; vector y = ya; vector yh; vector dyh; vector tau; int n=0; vector ratios;
		xs.Add(x); // Add initial x-value to list
		ys.Add(y); // Add initial y-value to list
		do{
			if(x>=b){return new Tuple<List<double>, List<vector>>(xs, ys);}
			if(n>step_limit){return new Tuple<List<double>, List<vector>>(xs, ys);}
			if(x+h > b){h = b-x;}
			vector[] step = stepper(f,x,y,h);
			yh = step[0]; dyh = step[1];
			bool accept_step = true;
			tau = new vector(dyh.size);
			ratios = new vector(dyh.size);
			for(int i=0;i<tau.size;i++){;
				tau[i] = (eps*Abs(yh[i]) + acc)*Sqrt(h/(b-a));
				if(dyh[i]==0)dyh[i]=tau[i]/4;
				if(Abs(dyh[i]) > tau[i]){accept_step = false;}
				ratios[i] = Abs(tau[i]/dyh[i]);
			}
			double fac = ratios[0];
			for(int i=1;i<tau.size;i++){
				fac = Min(fac,ratios[i]);
			}
			if(accept_step){
				n++;
				x += h;
				y = yh;
				xs.Add(x);
				ys.Add(yh);
			}
			double h_fac = Pow(fac,0.25)*0.95;
			if(h_fac < 2){h*=h_fac;}else{h*=2;}
		}while(true);
	}
	// Runge-Kutta stepper utilizing the Euler midpoint method which returns updated y-values and estimated error
	public static vector[] rkstep12(Func<double, vector, vector> f, double x, vector y, double h){
		vector k_0 = f(x,y); // Zeroth order Euler method
		vector k_12 = f(x + 0.5*h, y + 0.5*h*k_0); // First order
		vector yh = y + h*k_12; // Updated y-values
		vector err = (k_12 - k_0)*h; // Error estimate
		return new vector[] {yh, err};
	}
	public static vector[] rkstep45(Func<double,vector,vector> f, double x, vector y, double h){
		vector k_0 = h*f(x,y);
		vector k_1 = h*f(x + 1.0/4*h, y + 1.0/4*h*k_0);
		vector k_2 = h*f(x + 3.0/8*h, y + 3.0/32*k_0 + 9.0/32*k_1);
		vector k_3 = h*f(x + 12.0/13*h, y + 1932.0/2197*k_0 - 7200.0/2197*k_1 + 7296.0/2197*k_2);
		vector k_4 = h*f(x + h, y + 439.0/216*k_0 - 8.0*k_1 + 3680.0/513*k_2 - 845.0/4104*k_3);
		vector k_5 = h*f(x + 0.5*h, y - 8.0/27*k_0 + 2.0*k_1 - 3544.0/2565*k_2 + 1859.0/4104*k_3 - 11.0/40*k_4);
		vector[] ks = new vector[] {k_0,k_1,k_2,k_3,k_4,k_5};
		double[] coeff_4 = new double[] {16.0/135, 0, 6656.0/12825, 28561.0/56430, -9.0/50, 2.0/55};
		double[] coeff_5 = new double[] {25.0/216, 0, 1408.0/2565, 2197.0/4104, -1.0/5, 0};
		vector yh = y;
		vector err = new vector(y.size);
		for(int i=0;i<coeff_5.Length;i++){
			yh += coeff_5[i]*ks[i];
			err += (coeff_5[i] - coeff_4[i])*ks[i];
		}
		return new vector[] {yh, err};
	}


}











