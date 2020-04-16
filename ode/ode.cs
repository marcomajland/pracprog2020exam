using System;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
class ode{
	public static int Main(){
		// A: Embedded Runge-Kutta ODE integrator
		// ODE routine test for ODE u'' = -u
		double a = 0; double b = 2*PI; // Integration limits
		vector ya = new vector(2); ya[0] = 1; ya[1] = 0; // Initial value conditions for u(x)
		Func<double,vector,vector> cos_ODE = delegate(double x, vector y){		
			return new vector(y[1], -y[0]);
		};
		// ODE routine which returns x values and u,u'
		Tuple<List<double>, List<vector>> cos_res = ode_solver.rk12(cos_ODE, ya, a, b);

		// B: The SIR model
		a = 0; b = 100; // Integration limits
		double N = 5.8; double T_c = 3.0; double T_r = 14.0; // Model parameters
		ya = new vector(3); ya[0] = N-ya[1]; ya[1] = N*0.05; ya[2] = 0; // Initial value conditions for ODEs
		// Construction of ODEs
		Func<double,vector,vector> sir_ODE = delegate(double x, vector y){		
			return new vector(-1/(N*T_c)*y[0]*y[1], 1/(N*T_c)*y[0]*y[1] - 1/T_r*y[1], 1/T_r*y[1]);
		};
		// ODE routine which returns x values and y values
		Tuple<List<double>, List<vector>> sir_res = ode_solver.rk12(sir_ODE, ya, a, b);
		// Export results to separate text files for plotting using gnuplot
		var cos_out = new System.IO.StreamWriter($"cos_out.txt",append:false);
		for(int i=0;i<cos_res.Item1.Count;i++){
			cos_out.WriteLine($"{cos_res.Item1[i]} {cos_res.Item2[i][0]} {Cos(cos_res.Item1[i])}");
		}
		cos_out.Close();
		var sir_out = new System.IO.StreamWriter($"sir_out.txt",append:false);
		for(int i=0;i<sir_res.Item1.Count;i++){
			sir_out.WriteLine($"{sir_res.Item1[i]} {sir_res.Item2[i][0]} {sir_res.Item2[i][1]} {sir_res.Item2[i][2]}");
		}
		sir_out.Close();

		// Text to be printed in console with no importance to numerical calculations
		WriteLine("Ordinary differential equations");
		WriteLine("--------------------------------------");
		WriteLine("A: Embedded Runge-Kutta ODE integrator");
		WriteLine("--------------------------------------");
		WriteLine("The embedded Runge-Kutta ODE integrator is implemented using the midpoint Euler method. The routine is tested by solving the differential equation u'' = -u. The numerical solution of the routine is compared to the analytical solution, namely u(x) = cos(x). Furthermore, u'(x) = -sin(x) is also depicted in the figure to test the method. The routine is implemented using an adaptive step size driver which evaluates whether to accept a given step, depending on local errors and tolerances, obtained by using the Euler midpoint method stepper.");
		WriteLine("----------------");
		WriteLine("B: The SIR model");
		WriteLine("----------------");
		WriteLine("The SIR model is solved using the embedded Runge-Kutta ODE integrator routine. The following initial value conditions for the covid-19 epidemic situation in Denmark are as follows:");
		WriteLine("Population (N): 5.8 million");
		WriteLine("Average recovery time (T_r): 14 days");
		WriteLine("As can be seen in the figure, the average contact time (T_c) is varied throughout the figure to investigate the effect of social distancing.");

		return 0;
	}
}

