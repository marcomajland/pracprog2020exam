using System;
using static System.Console;
class main_C{
	public static int Main(){
		// Rosenbrock function
		Func<vector,double> rosenbrock = delegate(vector x){
			return (1-x[0])*(1-x[0]) + 100*(x[1] - x[0]*x[0])*(x[1] - x[0]*x[0]);
		};
		// Himmelblau function
		Func<vector,double> himmelblau = delegate(vector x){
			return (x[0]*x[0] + x[1] - 11)*(x[0]*x[0] + x[1] - 11) + (x[0] + x[1]*x[1] - 7)*(x[0] + x[1]*x[1] - 7);
		};
		
		vector xi_qn_rosenbrock = new vector(3.0,3.0);
		vector xi_qn_himmelblau = new vector(3.1,2.1);	
		vector xi_ds_rosenbrock = new vector(3.0,3.0);
		vector xi_ds_himmelblau = new vector(3.1,2.1);	
		double tol = 1e-8;

		int qn_rosenbrock = qnewton.minimize(rosenbrock, ref xi_qn_rosenbrock, tol);
		int qn_himmelblau = qnewton.minimize(himmelblau, ref xi_qn_himmelblau, tol);
		int ds_rosenbrock = simplex.downhill(rosenbrock, ref xi_ds_rosenbrock, tol:tol);
		int ds_himmelblau = simplex.downhill(himmelblau, ref xi_ds_himmelblau, tol:tol);

		var outfile = new System.IO.StreamWriter($"Outfile.txt",append:false);
		outfile.WriteLine($"-------------------------------------");
		outfile.WriteLine($"Downhill simplex minimization routine");
		outfile.WriteLine($"-------------------------------------");
		outfile.WriteLine($"The downhill simplex minimization routine is implemented and compared to the quasi-Newton method in exercise 8A with tolerance = {tol}.\n");
		outfile.WriteLine($"Minimization of the Rosenbrock function:\n");
		outfile.WriteLine($"Quasi-Newton minimization routine:");
		outfile.WriteLine($"(x,y):          {xi_qn_rosenbrock[0]},{xi_qn_rosenbrock[1]}");
		outfile.WriteLine($"Error:          {1.0 - xi_qn_rosenbrock[0]},{1.0 - xi_qn_rosenbrock[1]}");
		outfile.WriteLine($"Steps:          {qn_rosenbrock}\n");
		outfile.WriteLine($"Downhill simplex minimization routine:");
		outfile.WriteLine($"(x,y):          {xi_ds_rosenbrock[0]},{xi_ds_rosenbrock[1]}");
		outfile.WriteLine($"Error:          {1.0 - xi_ds_rosenbrock[0]},{1.0 - xi_ds_rosenbrock[1]}");
		outfile.WriteLine($"Steps:          {ds_rosenbrock}\n");
		outfile.WriteLine($"Minimization of the Himmelblau function:\n");
		outfile.WriteLine($"Quasi-Newton minimization routine:");
		outfile.WriteLine($"(x,y):          {xi_qn_himmelblau[0]},{xi_qn_himmelblau[1]}");
		outfile.WriteLine($"Error:          {3.0 - xi_qn_himmelblau[0]},{2.0 - xi_qn_himmelblau[1]}");
		outfile.WriteLine($"Steps:          {qn_himmelblau}\n");
		outfile.WriteLine($"Downhill simplex minimization routine:");
		outfile.WriteLine($"(x,y):          {xi_ds_himmelblau[0]},{xi_ds_himmelblau[1]}");
		outfile.WriteLine($"Error:          {3.0 - xi_ds_himmelblau[0]},{2.0 - xi_ds_himmelblau[1]}");
		outfile.WriteLine($"Steps:          {ds_himmelblau}\n");
		outfile.WriteLine($"The downhill simplex method seems to achieve better accuracy as compared to the quasi-Newton method. Although, for the Rosenbrock function, the downhill simplex method performs quite a few more minimization steps as compared to the quasi-Newton method.");


/*
		outfile.WriteLine($"Minimization of the Himmelblau function (steps: {steps_himmelblau}):");
		outfile.WriteLine($"(x,y):          {xi_himmelblau[0]},{xi_himmelblau[1]}");
		outfile.WriteLine($"Error:          {3.0 - xi_himmelblau[0]},{2.0 - xi_himmelblau[1]}");
*/		outfile.Close();
		return 0;
	}

}
