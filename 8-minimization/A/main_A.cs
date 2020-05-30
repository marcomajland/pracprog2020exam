using System;
using static System.Console;
class main_A{
	public static int Main(){
		// Rosenbrock function
		Func<vector,double> rosenbrock = delegate(vector x){
			return (1-x[0])*(1-x[0]) + 100*(x[1] - x[0]*x[0])*(x[1] - x[0]*x[0]);
		};
		// Himmelblau function
		Func<vector,double> himmelblau = delegate(vector x){
			return (x[0]*x[0] + x[1] - 11)*(x[0]*x[0] + x[1] - 11) + (x[0] + x[1]*x[1] - 7)*(x[0] + x[1]*x[1] - 7);
		};
		
		vector xi_rosenbrock = new vector(3.0,3.0);
		vector xi_himmelblau = new vector(3.1,2.1);	

		int steps_rosenbrock = qnewton.sr1(rosenbrock, ref xi_rosenbrock, 1e-4);
		int steps_himmelblau = qnewton.minimize(himmelblau, ref xi_himmelblau, 1e-6);

		var outfile = new System.IO.StreamWriter($"Outfile.txt",append:false);
		outfile.WriteLine($"----------------------------------------------------------------------------------------");
		outfile.WriteLine($"Quasi-Newton method with numerical gradient, back-tracking linesearch, and rank-1 update");
		outfile.WriteLine($"----------------------------------------------------------------------------------------");
		outfile.WriteLine($"Minimization of the Rosenbrock function (steps: {steps_rosenbrock}):");
		outfile.WriteLine($"(x,y):          {xi_rosenbrock[0]},{xi_rosenbrock[1]}");
		outfile.WriteLine($"Error:          {1.0 - xi_rosenbrock[0]},{1.0 - xi_rosenbrock[1]}\n");
		outfile.WriteLine($"Minimization of the Himmelblau function (steps: {steps_himmelblau}):");
		outfile.WriteLine($"(x,y):          {xi_himmelblau[0]},{xi_himmelblau[1]}");
		outfile.WriteLine($"Error:          {3.0 - xi_himmelblau[0]},{2.0 - xi_himmelblau[1]}");
		outfile.Close();
		return 0;
	}
}
