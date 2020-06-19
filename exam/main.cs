using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){
		int dim = 20; int iterations = 300;
		double min_dev = 0.80; double max_dev = 1.20; double step_dev = 0.01;
		generate_deviations(dim, iterations, min_dev, max_dev, step_dev);
		return 0;
	}
	public static double[] generate_s(double deviation, int dim, double tol = 1e-6, int max_qrs = 0, int n_max = 999){
		var rnd = new Random(); int i = rnd.Next(dim);
		matrix A = misc.gen_matrix(dim); matrix Ac = A.copy();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues(); 
		matrix V = jacobi.get_eigenvectors(); 

		double e_0 = e[i]*deviation;
		vector v_0 = V[i]/V[i].norm();
		for(int j=0;j<v_0.size;j++){v_0[j] = v_0[j]*deviation;}

		double s = power_method.inverse_iteration(Ac, e_0, v_0, tol, n_max, max_qrs);
		return new double[] {s, e[i]};		
	}
	public static int generate_convergences(int dim, double deviation, int iterations, double tol = 1e-3){
		int convergences = 0;
		for(int j=0;j<iterations;j++){
			double[] s = generate_s(deviation, dim);
			if(Abs(s[0] - s[1]) < tol){convergences++;}
		}
		return convergences;
	}
	public static void generate_deviations(int dim, int iterations, double min_dev, double max_dev, double step_dev){
		var deviations_out = new System.IO.StreamWriter($"./plot_files/deviations.txt",append:false);
		int steps = Convert.ToInt32((max_dev-min_dev)/step_dev); double dev=min_dev;
		for(int j=0;j<=steps;j++){
			deviations_out.WriteLine($"{dev} {generate_convergences(dim,dev,iterations)/(iterations*1.0)}");
			WriteLine($"Deviation: {dev}");
			dev += step_dev;
		}
		deviations_out.Close();
	}




/*		int dim = 50;
		double deviation = 1.05;
		var rnd = new Random();
		int i = rnd.Next(dim);

		matrix A = misc.gen_matrix(dim);
		matrix Ac = A.copy();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues(); 
		matrix V = jacobi.get_eigenvectors(); 

		double e_0 = e[i]*deviation;
		vector v_0 = V[i]/V[i].norm();
		for(int j=0;j<v_0.size;j++){v_0[j] = v_0[j]*deviation;}

		double tol = 1e-6; int max_qrs = 0; int n_max = 999;
		double s = power_method.inverse_iteration(Ac, e_0, v_0, tol, n_max, max_qrs);

*/
/*
//		WriteLine($"Jacobi diagonalization eigenvalue i-1:             {e[i-1]}");
		WriteLine($"Jacobi diagonalization eigenvalue i:               {e[i]}");
//		WriteLine($"Jacobi diagonalization eigenvalue i+1:             {e[i+1]}");
		WriteLine($"Deviation for initial values:                      {deviation}");
		WriteLine($"Error tolerance:                                   {tol}");
		WriteLine($"Initial eigenvalue:                                {e_0}");
		WriteLine($"Inverse iteration method eigenvalue:               {s}");
		WriteLine($"Error (dev. from Jacobi):                          {Abs(s-e[i])}");
*/


}



