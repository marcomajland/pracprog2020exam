using System;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
class ode{
	public static int Main(){
		// ODE routine test for ODE u'' = -u
		double a = 0; double b = 4*PI; // Integration limits
		vector ya = new vector(2); ya[0] = 1; ya[1] = 0; // Initial value conditions for u(x)
		Func<double,vector,vector> cos_ODE = delegate(double x, vector y){return new vector(y[1], -y[0]);};
		// ODE routine which returns x values and u,u'
		double acc = 1e-3; double eps = 1e-3; double h = 0.01; int max_steps = 999;
		Tuple<List<double>, List<vector>> cos_res = ode_solver.rk12(cos_ODE, ya, a, b, acc, eps, h, max_steps);

		// Export results to separate text files for plotting using gnuplot
		var cos_out = new System.IO.StreamWriter($"./plot_files/cos_out.txt",append:false);
		for(int i=0;i<cos_res.Item1.Count;i++){
			cos_out.WriteLine($"{cos_res.Item1[i]} {cos_res.Item2[i][0]} {Cos(cos_res.Item1[i])} {Cos(cos_res.Item1[i]) - cos_res.Item2[i][0]}");
		}
		cos_out.Close();
		return 0;
	}
}

