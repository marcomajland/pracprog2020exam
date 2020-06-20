using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){	
		int dim = 30;
		double tol = 1e-6;
		int updates = 0;
		int n_max = 999;
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


		var outfile = new System.IO.StreamWriter($"../outfiles/test_out.txt",append:false);
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
		return 0;
	}
}
