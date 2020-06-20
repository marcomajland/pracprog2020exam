using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){	
		int dim = 30;
		double tau = 1e-6; double eps = 1e-6;
		int updates = 999; int n_max = 999;
		var rnd = new Random(); int i = rnd.Next(dim);
		double deviation = 1.05;

		matrix A = misc.gen_matrix(dim);
		matrix Ac = A.copy();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues();
		matrix V = jacobi.get_eigenvectors(); 

		double e_0 = e[i]*deviation;
		vector v_01 = misc.gen_vector(dim); // Random vector
		vector v_02 = V[i]/V[i].norm(); // Jacobi eigenvector
		for(int j=0;j<v_02.size;j++){v_02[j] = v_02[j]*deviation;}


		double[] s1 = power_method.inverse_iteration(Ac, e_0, v_01, tau, eps, n_max, updates); // Random
		double[] s2 = power_method.inverse_iteration(Ac, e_0, v_02, tau, eps, n_max, updates); // Jacobi

		var outfile = new System.IO.StreamWriter($"test_out.txt",append:false);
		outfile.WriteLine($"--------------------------------------------");
		outfile.WriteLine($"Inverse iteration method (Jacobi comparison)");
		outfile.WriteLine($"--------------------------------------------");
		outfile.WriteLine($"Matrix dimension:                         {dim}");
		outfile.WriteLine($"Random eigenvalue index:                  {i}");
		outfile.WriteLine($"Maximum iterations:                       {n_max}\n");
		outfile.WriteLine($"Jacobi eigenvalues:");
		outfile.WriteLine($"e_(i-1):                                  {e[i-1]}");
		outfile.WriteLine($"e_(i):                                    {e[i]}");
		outfile.WriteLine($"e_(i+1):                                  {e[i+1]}\n");
		outfile.WriteLine($"Inverse iteration method:\n");
		outfile.WriteLine($"Initial deviation:                        {deviation}");
		outfile.WriteLine($"Initial eigenvalue:                       {e_0}");
		outfile.WriteLine($"Absolute accuracy:                        {tau}");
		outfile.WriteLine($"Relative accuracy:                        {eps}\n");
		outfile.WriteLine($"Inverse iteration method with random initial eigenvector:");
 		outfile.WriteLine($"Algorithm result:                         {s1[0]}");
		outfile.WriteLine($"Iterations:                               {s1[1]}");
		outfile.WriteLine($"Errors compared to Jacobi eigenvalues:");
		outfile.WriteLine($"Abs(e_(i-1) - s):                         {Abs(e[i-1]-s1[0])}");
		outfile.WriteLine($"Abs(e_(i) - s):                           {Abs(e[i]-s1[0])}");
		outfile.WriteLine($"Abs(e_(i+1) - s):                         {Abs(e[i+1]-s1[0])}\n");
		outfile.WriteLine($"Inverse iteration method with deviated Jacobi eigenvector:");
 		outfile.WriteLine($"Algorithm result:                         {s2[0]}");
		outfile.WriteLine($"Iterations:                               {s2[1]}");
		outfile.WriteLine($"Errors compared to Jacobi eigenvalues:");
		outfile.WriteLine($"Abs(e_(i-1) - s):                         {Abs(e[i-1]-s2[0])}");
		outfile.WriteLine($"Abs(e_(i) - s):                           {Abs(e[i]-s2[0])}");
		outfile.WriteLine($"Abs(e_(i+1) - s):                         {Abs(e[i+1]-s2[0])}\n");
		outfile.Close();	
		return 0;
	}
}
/*
		outfile.WriteLine($"To test the implementation of the inverse iteration method, the algorithm is compared to the Jacobi diagonalization procedure which we implemented in problems 4.");
		outfile.WriteLine($"A random real symmetric matrix of dimensions {dim}x{dim} is generated and diagonalized using Jacobi diagonalization (implementation may be found in matlib). A random eigenvalue of the Jacobi procedure is chosen as initial guess for the inverse iteration. The initial guess is defined as e_0 = delta*e_i where delta is the deviation from the Jacobi eigenvalue, e_i. The Jacobi eigenvalues are ordered in increasing order such that e_(i-1)<e_(i)<e_(i+1).\n");
*/
