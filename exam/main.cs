using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){	
	int dim = 30;
		double tol = 1e-6;
		int updates = 0;
		int n_max = 3000;
		var rnd = new Random(); int i = rnd.Next(dim);
		double deviation = 1.05;

		matrix A = misc.gen_matrix(dim);
		matrix Ac = A.copy();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues(); 
		matrix V = jacobi.get_eigenvectors(); 

		double e_0 = e[i]*deviation;
		vector v_0 = V[i]/V[i].norm();
		for(int j=0;j<v_0.size;j++){v_0[j] = v_0[j]*deviation;}

		double[] s = power_method.inverse_iteration(Ac, e_0, v_0, tol, n_max, updates, false);


		var outfile = new System.IO.StreamWriter($"test_out.txt",append:false);
		outfile.WriteLine($"--------------------------------------------");
		outfile.WriteLine($"Inverse iteration method (Jacobi comparison)");
		outfile.WriteLine($"--------------------------------------------");
		outfile.WriteLine($"To test the implementation of the inverse iteration method, the algorithm is compared to the Jacobi diagonalization procedure which we implemented in problems 4.");
		outfile.WriteLine($"A random real symmetric matrix of dimensions {dim}x{dim} is generated and diagonalized using Jacobi diagonalization (implementation may be found in matlib). A random eigenvalue of the Jacobi procedure is chosen as initial guess for the inverse iteration. The initial guess is defined as e_0 = delta*e_i where delta is the deviation from the Jacobi eigenvalue, e_i. The Jacobi eigenvalues are ordered in increasing order such that e_{i-1}<e_{i}<e_{i+1}.\n");
		outfile.WriteLine($"Random eigenvalue index:     {i}\n");
		outfile.WriteLine($"Jacobi eigenvalues:");
		outfile.WriteLine($"e_{i-1}:                         {e[i-1]}");
		outfile.WriteLine($"e_{i}:                         {e[i]}");
		outfile.WriteLine($"e_{i+1}:                         {e[i+1]}\n");
		outfile.WriteLine($"Inverse iteration method:");
		outfile.WriteLine($"Deviation:                    {deviation}");
		outfile.WriteLine($"Initial eigenvalue:           {e_0}");
		outfile.WriteLine($"Algorithm result:             {s[0]}");
		outfile.WriteLine($"Error:                        {s[2]}");
		outfile.WriteLine($"Iterations:                   {s[1]}");
		outfile.WriteLine($"Rayleigh updates:             {updates}\n");
		outfile.WriteLine($"Comparison to Jacobi diagonalization:");
		outfile.WriteLine($"Abs(e_{i-1} - s):                {Abs(e[i-1]-s[0])}");
		outfile.WriteLine($"Abs(e_{i} - s):                {Abs(e[i]-s[0])}");
		outfile.WriteLine($"Abs(e_{i+1} - s):                {Abs(e[i+1]-s[0])}\n");
		outfile.WriteLine($"Eigenvectors of Jacobi diagonalization:");
		outfile.WriteLine("");
		outfile.Close();	


/*		int dim = 20; int iterations = 300;
		double min_dev = 0.80; double max_dev = 1.20; double step_dev = 0.01;
		generate_deviations(dim, iterations, min_dev, max_dev, step_dev);
*/
		return 0;
	}

/*	public static double[] generate_s(double deviation, int dim, double tol = 1e-6, int max_qrs = 0, int n_max = 999){
		var rnd = new Random(); int i = rnd.Next(dim);
		matrix A = misc.gen_matrix(dim); matrix Ac = A.copy();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues(); 
		matrix V = jacobi.get_eigenvectors(); 

		double e_0 = e[i]*deviation;
		vector v_0 = V[i]/V[i].norm();
		for(int j=0;j<v_0.size;j++){v_0[j] = v_0[j]*deviation;}

		double s = power_method.inverse_iteration(Ac, e_0, v_0, tol, n_max, max_qrs);
		return new double[] {s[0], e[i]};		
	}
	public static int sample_convergences(int dim, double deviation, int iterations, double tol = 1e-3){
		int convergences = 0;
		for(int j=0;j<iterations;j++){
			double[] s = generate_s(deviation, dim);
			if(Abs(s[0] - s[1]) < tol){convergences++;}
		}
		return convergences;
	}
	public static void dev_distribution(int dim, int iterations, double min_dev, double max_dev, double step_dev){
		var dev_dist_out = new System.IO.StreamWriter($"./plot_files/dev_distribution.txt",append:false);
		int steps = Convert.ToInt32((max_dev-min_dev)/step_dev); double dev=min_dev;
		for(int j=0;j<=steps;j++){
			dev_dist_out.WriteLine($"{dev} {sample_convergences(dim,dev,iterations)/(iterations*1.0)}");
			dev += step_dev;
		}
		dev_dist_out.Close();
	}
	public static void convergence(){
		


	}


/*	public static void convergence(){
		int dim = 50; double tol = 1e-6;
		int max_qrs = 999; int n_max = 999;

		var rnd = new Random(); int i = rnd.Next(dim);
		matrix A = misc.gen_matrix(dim); matrix Ac = A.copy();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues(); 
		matrix V = jacobi.get_eigenvectors(); 

		double[] s; double e_0; vector v_0;

		double[] deviations = new double[4] {1.00, 1.01, 1.02, 1.03};
		var convergence_out = new System.IO.StreamWriter($"./plot_files/convergence.txt",append:false);
		for(int j=0;j<deviations.Length;j++){
			e_0 = e[i]*deviations[j];
			v_0 = V[i]/V[i].norm();
			for(int k=0;k<v_0.size;k++){v_0[k] = v_0[k]*deviations[j];}
			s = power_method.inverse_iteration(Ac, e_0, v_0, tol, n_max, max_qrs);
			convergence_out.WriteLine($"{deviations[j]} {s[1]}");
		}
		convergence_out.Close();
		}
*/
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






