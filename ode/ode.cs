using System;
using System.Collections.Generic;
using static System.Math;
using static System.Console;
class ode{
	public static int Main(){
		// Test of ODE routine for y(x) = cos(x)
		double a = 0; double b = 2*PI;
		vector ya = new vector(2); ya[0] = 1; ya[1] = 0;
		Func<double,vector,vector> cos_ODE = delegate(double x, vector y){		
			return new vector(y[1], -y[0]);
		};
		Tuple<List<double>, List<vector>> cos_res = ode_solver.rk12(cos_ODE, ya, a, b);



		a = 0; b = 10;
		double N = 100; double T_c = 1; double T_r = 1;
		ya = new vector(3); ya[0] = 80; ya[1] = 10; ya[2] = 0;
		Func<double,vector,vector> sir_ODE = delegate(double x, vector y){		
			return new vector(-1/(N*T_c)*y[0]*y[1], 1/(N*T_c)*y[0]*y[1] - 1/T_r*y[1], 1/T_r*y[1]);
		};
		Tuple<List<double>, List<vector>> sir_res = ode_solver.rk12(sir_ODE, ya, a, b);

		WriteLine("A: Jacobi diagonalization with cyclic sweeps and quantum particle in a box:");
		WriteLine("------------------------------------------------");
		WriteLine("Diagonalization of random real symmetric matrix:");
		WriteLine("------------------------------------------------");

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



		return 0;
	}
}
