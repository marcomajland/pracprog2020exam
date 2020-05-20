using System;
using static System.Console;
class minimization{
	public static int Main(){
		// Rosenbrock function
		Func<vector,double> rosenbrock = delegate(vector x){
			return (1-x[0])*(1-x[0]) + 100*(x[1] - x[0]*x[0])*(x[1] - x[0]*x[0]);
		};
		// Himmelblau function
		Func<vector,double> himmelblau = delegate(vector x){
			return (x[0]*x[0] + x[1] - 11)*(x[0]*x[0] + x[1] - 11) + (x[0] + x[1]*x[1] - 7)*(x[0] + x[1]*x[1] - 7);
		};
		Func<vector,double> f2 = delegate(vector x){
			return 3*x[0]*x[0] - 12*x[0] + 2*x[1]*x[1] + 16*x[1] - 10;
		};
		
		vector xi_rosenbrock = new vector(3.0,3.0);
		vector xi_himmelblau = new vector(3.1,2.1);	

//		int steps_rosenbrock = qnewton.minimize(rosenbrock, ref xi_rosenbrock, 1e-8);
		int steps_rosenbrock = qnewton.sr1(rosenbrock, ref xi_rosenbrock, 1e-4);
		int steps_himmelblau = qnewton.minimize(himmelblau, ref xi_himmelblau, 1e-6);
//		int steps2 = qnewton.sr1(f, ref x_initial2, 1e-3);
		WriteLine($"Rosenbrock function (steps: {steps_rosenbrock}):");
		WriteLine($"x: {xi_rosenbrock[0]}");
		WriteLine($"y: {xi_rosenbrock[1]}\n");
		WriteLine($"Himmelblau function (steps: {steps_himmelblau}):");
		WriteLine($"x: {xi_himmelblau[0]}");
		WriteLine($"y: {xi_himmelblau[1]}\n");

		string[] Ei = System.IO.File.ReadAllLines("E.txt");
		string[] sigmai = System.IO.File.ReadAllLines("sigma.txt");
		string[] errori = System.IO.File.ReadAllLines("error.txt");
		vector E = new vector(Ei.Length);
		vector sigma = new vector(Ei.Length);
		vector error = new vector(Ei.Length);
		for (int i=0;i<Ei.Length;i++){
			E[i] = double.Parse(Ei[i]);
			sigma[i] = double.Parse(sigmai[i]);
			error[i] = double.Parse(errori[i]);
		}
		Func<vector,double> D = delegate(vector x){
			double val = 0;
			for(int i=0;i<E.size;i++){
				val += (F(E[i],x[0],x[1],x[2]) - sigma[i])*(F(E[i],x[0],x[1],x[2]) - sigma[i]);
			}
			return val;
		};
		vector xi_higgs = new vector(125.0,4.0,15.0);	
		int steps_higgs = qnewton.minimize(D, ref xi_higgs, 1e-6);
		WriteLine($"Higgs (steps: {steps_higgs}):");
		WriteLine($"m: {xi_higgs[0]}");
		WriteLine($"gamma: {xi_higgs[1]}");
		WriteLine($"A: {xi_higgs[2]}");
		return 0;
	}
	public static double F(double E, double m, double gamma, double A){
		return A/((E-m)*(E-m) + gamma*gamma/4);
	}


}
