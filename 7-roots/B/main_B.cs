using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
class roots{
	public static int Main(){
		double eps = 1e-6;
		double dx = 1e-7;

		vector eps_initial = new vector(-0.40);
		vector epsilon = root_finder.newton(aux_function,eps_initial,eps,dx);

		Tuple<List<double>, List<vector>> hydrogen = radial_schrodinger(epsilon[0]);
		var hydrogen_out = new System.IO.StreamWriter($"hydrogen_out.txt",append:false);
		for(int i=0;i<hydrogen.Item1.Count;i++){
			hydrogen_out.WriteLine($"{hydrogen.Item1[i]} {hydrogen.Item2[i][0]}");
		}
		hydrogen_out.Close();

		var outfile = new System.IO.StreamWriter($"Outfile.txt",append:false);
		outfile.WriteLine($"------------------------------------------------------------------------------");
		outfile.WriteLine($"Bound states of hydrogen atom with shooting method for boundary value problems");
		outfile.WriteLine($"------------------------------------------------------------------------------");
		outfile.WriteLine($"The ground state of the hydrogen atom is calculated using the shooting method for boundary value problems.");
		outfile.WriteLine($"Root finder results of auxiliary function with accuracy eps = {eps}:");
		outfile.WriteLine($"M(eps)=0:");
		outfile.WriteLine($"Initial eps:                  {eps_initial[0]}");		
		outfile.WriteLine($"Root eps:                     {epsilon[0]}");
		outfile.WriteLine($"Error:                        {aux_function(epsilon)[0]}\n");
		outfile.Close();

		return 0;
	}
	public static Func<vector,vector> aux_function = delegate(vector x){
		double epsilon = x[0];
		Tuple<List<double>, List<vector>> res = radial_schrodinger(epsilon);	
		return new vector(res.Item2[res.Item1.Count-1][0]);
	};
	public static Tuple<List<double>, List<vector>> radial_schrodinger(double epsilon){
		double r_min = 1e-6; double r_max = 8;
		vector ya = new vector(2); ya[0] = r_min - r_min*r_min; ya[1] = 1-2*r_min;
		Func<double,vector,vector> f = delegate(double x, vector y){
			return new vector(y[1], -2*(epsilon*y[0] + y[0]/x));};
		return ode_solver.solve(f,ya,r_min,r_max,1e-3,1e-3);
	}
	public static Func<vector,vector> f1 = delegate(vector x){
		return new vector(x[0]*x[0] + 4*x[1]*x[1] - 9, 18*x[1] - 14*x[0]*x[0] + 45);};	
	public static Func<vector,vector> f2 = delegate(vector x){
		return new vector(2*(x[0]-1) + 400*x[0]*(x[0]*x[0] - x[1]), 200*(x[1]-x[0]*x[0]));};	
}



