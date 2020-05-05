using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
class roots{
	public static int Main(){
		double eps = 1e-3;
		double dx = 1e-7;
//		vector x1 = new vector(1.0,-1.0);
//		vector x2 = new vector(-0.5,3.0);
//		vector res1 = root_finder.newton(f1,x1,eps,dx);
//		vector res2 = root_finder.newton(f2,x2,eps,dx);
//		WriteLine($"{res2[0]}");
//		WriteLine($"{res2[1]}");

		Tuple<List<double>, List<vector>> res = radial_schrodinger(-0.5);

		var hydrogen_out = new System.IO.StreamWriter($"hydrogen_out.txt",append:false);
		for(int i=0;i<res.Item1.Count;i++){
			hydrogen_out.WriteLine($"{res.Item1[i]} {res.Item2[i][0]}");
		}
		hydrogen_out.Close();
		return 0;
	}
	public static Tuple<List<double>, List<vector>> radial_schrodinger(double epsilon){
		double a = 1e-3; double b = 8;
		vector ya = new vector(2); ya[0] = a - a*a; ya[1] = 1-2*a;

		Func<double,vector,vector> f = delegate(double x, vector y){
			return new vector(y[1], -2*(epsilon*y[0] + y[0]/x));};

		return ode_solver.rk12(f,ya,a,b);
	}
	public static Func<vector,vector> f1 = delegate(vector x){
		return new vector(x[0]*x[0] + 4*x[1]*x[1] - 9, 18*x[1] - 14*x[0]*x[0] + 45);};	
	public static Func<vector,vector> f2 = delegate(vector x){
		return new vector(2*(x[0]-1) + 400*x[0]*(x[0]*x[0] - x[1]), 200*(x[1]-x[0]*x[0]));};	
}
