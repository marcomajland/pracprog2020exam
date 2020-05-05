using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
class roots{
	public static int Main(){
		double eps = 1e-3;
		double dx = 1e-6;
/*		vector x1 = new vector(1.0,-1.0);
		vector x2 = new vector(-0.5,3.0);
		vector res1 = root_finder.newton(f1,x1,eps,dx);
		vector res2 = root_finder.newton(f2,x2,eps,dx);
		WriteLine($"{res2[0]}");
		WriteLine($"{res2[1]}");
*/
		vector eps_initial = new vector(-1.0);
		double epsilon = root_finder.newton(aux_function,eps_initial,eps,dx)[0];
		WriteLine($"Root of auxiliary function: {epsilon}");

		Tuple<List<double>, vector> hydrogen = radial_schrodinger(epsilon);
		var hydrogen_out = new System.IO.StreamWriter($"hydrogen_out.txt",append:false);
		for(int i=0;i<hydrogen.Item2.size;i++){
			hydrogen_out.WriteLine($"{hydrogen.Item1[i]} {hydrogen.Item2[i]}");
		}
		hydrogen_out.Close();

		return 0;
	}
	public static Func<vector,vector> aux_function = delegate(vector x){
		double epsilon = x[0];
		Tuple<List<double>, vector> res = radial_schrodinger(epsilon);
		return new vector(res.Item2[res.Item2.size-1]);
	};
	public static Tuple<List<double>, vector> radial_schrodinger(double epsilon){
		double a = 1e-6; double b = 8;
		vector ya = new vector(2); ya[0] = a - a*a; ya[1] = 1-2*a;
		Func<double,vector,vector> f = delegate(double x, vector y){
			return new vector(y[1], -2*(epsilon*y[0] + y[0]/x));};
		var xs = new List<double>();
		vector res = ode.rk23(f,a,ya,b,1e-3,1e-3,0.01,xlist:xs);
		return new Tuple<List<double>, vector>(xs, res);
//		return ode_solver.rk12(f,ya,a,b);
	}
	public static Func<vector,vector> f1 = delegate(vector x){
		return new vector(x[0]*x[0] + 4*x[1]*x[1] - 9, 18*x[1] - 14*x[0]*x[0] + 45);};	
	public static Func<vector,vector> f2 = delegate(vector x){
		return new vector(2*(x[0]-1) + 400*x[0]*(x[0]*x[0] - x[1]), 200*(x[1]-x[0]*x[0]));};	
}
