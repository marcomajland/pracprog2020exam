using System;
using static System.Console;
class main_C{
	public static int Main(){
		// Rosenbrock function
		Func<vector,double> rosenbrock = delegate(vector x){
			return (1-x[0])*(1-x[0]) + 100*(x[1] - x[0]*x[0])*(x[1] - x[0]*x[0]);
		};	
		vector xi_rosenbrock = new vector(3.0,3.0);

		int steps_rosenbrock = simplex.downhill(rosenbrock, ref xi_rosenbrock);

		WriteLine($"Rosenbrock function (steps: {steps_rosenbrock}):");
		WriteLine($"x:              {xi_rosenbrock[0]}");
		WriteLine($"y:              {xi_rosenbrock[1]}\n");
		WriteLine($"Error:          {1.0 - xi_rosenbrock[0]},{1.0 - xi_rosenbrock[1]}\n");
		return 0;
	}

}
