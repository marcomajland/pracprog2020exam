using System;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
class ode{
	public static int Main(){
		double a = 0; double b = 100; // Integration limits
		double N = 5.8; double T_c = 0; double T_r = 7.0; // Model parameters
		double acc = 1e-3; double eps = 1e-3; double h = 0.01; int max_steps = 2000;
		vector ya = new vector(3); ya[0] = N-ya[1]; ya[1] = N*0.05; ya[2] = 0; // Initial value conditions for ODEs
		// Construction of ODEs
		Func<double,vector,vector> sir_ODE = delegate(double x, vector y){
			return new vector(-1/(N*T_c)*y[0]*y[1], 1/(N*T_c)*y[0]*y[1] - 1/T_r*y[1], 1/T_r*y[1]);
		};
		// ODE routine which returns x values and y values
		Tuple<List<double>, List<vector>> sir_res;
		for(T_c=0.25;T_c<2;T_c+=0.5){
			sir_res = ode_solver.rk12(sir_ODE, ya, a, b, acc, eps, h, max_steps);
			var sir_out = new System.IO.StreamWriter($"./plot_files/sir_out_{T_c}.txt",append:false);
			for(int i=0;i<sir_res.Item1.Count;i++){
				sir_out.WriteLine($"{sir_res.Item1[i]} {sir_res.Item2[i][0]} {sir_res.Item2[i][1]} {sir_res.Item2[i][2]}");
			}sir_out.Close();
		}
		var outfile = new System.IO.StreamWriter($"outfile.txt",append:false);
		outfile.WriteLine("-------------");
		outfile.WriteLine("The SIR model");
		outfile.WriteLine("-------------");
		outfile.WriteLine("The SIR model of the covid-19 epidemic development of Denmark is solved numerically for the following initial value conditions:");
		outfile.WriteLine("Population (N): 5.8 million");
		outfile.WriteLine("Average recovery time (T_r): 7 days\n");
		outfile.WriteLine("As can be seen in figure SIRPlot.svg, the average contact time (T_c) is varied throughout the figure to investigate the effect of social distancing. Clearly, the effect of larger T_c yields a more flat green curve.");
		outfile.Close();
		return 0;
	}
}

