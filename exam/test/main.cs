using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){	
		int dim = 30;
		double tau = 1e-6; double eps = 1e-6;
		int updates = 999; int n_max = 999; // If updates=999, no Rayleigh updates are performed
		var rnd = new Random(); int i = rnd.Next(dim-1);
		double deviation = 1.05;

		matrix A = misc.gen_matrix(dim);
		matrix Ac = A.copy();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues();
		matrix V = jacobi.get_eigenvectors(); 

		double s_0 = e[i]*deviation;
		double s_1 = e[i]*deviation;
		vector v_0 = misc.gen_vector(dim); // Random vector
		vector v_1 = V[i]/V[i].norm(); // Jacobi eigenvector
		for(int j=0;j<v_1.size;j++){v_1[j] = v_1[j]*deviation;}

		double n_0 = power_method.inverse_iteration(Ac, ref s_0, ref v_0, tau, eps, n_max, updates); // Random
		double n_1 = power_method.inverse_iteration(Ac, ref s_1, ref v_1, tau, eps, n_max, updates); // Jacobi
		
		var outfile = new System.IO.StreamWriter($"../test_out.txt",append:false);
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
		outfile.WriteLine($"Initial eigenvalue:                       {e[i]*deviation}");
		outfile.WriteLine($"Absolute accuracy:                        {tau}");
		outfile.WriteLine($"Relative accuracy:                        {eps}\n");
		outfile.WriteLine($"Inverse iteration method with random initial eigenvector:");
 		outfile.WriteLine($"Algorithm result:                         {s_0}");
		outfile.WriteLine($"v^(T)*A*v:                                {v_0.dot(Ac*v_0)}");
		outfile.WriteLine($"Iterations:                               {n_0}");
		outfile.WriteLine($"Errors compared to Jacobi eigenvalues:");
		outfile.WriteLine($"Abs(e_(i-1) - s):                         {Abs(e[i-1]-s_0)}");
		outfile.WriteLine($"Abs(e_(i) - s):                           {Abs(e[i]-s_0)}");
		outfile.WriteLine($"Abs(e_(i+1) - s):                         {Abs(e[i+1]-s_0)}\n");
		outfile.WriteLine($"Inverse iteration method with deviated Jacobi eigenvector:");
 		outfile.WriteLine($"Algorithm result:                         {s_1}");
		outfile.WriteLine($"v^(T)*A*v:                                {v_1.dot(Ac*v_1)}");
		outfile.WriteLine($"Iterations:                               {n_1}");
		outfile.WriteLine($"Errors compared to Jacobi eigenvalues:");
		outfile.WriteLine($"Abs(e_(i-1) - s):                         {Abs(e[i-1]-s_1)}");
		outfile.WriteLine($"Abs(e_(i) - s):                           {Abs(e[i]-s_1)}");
		outfile.WriteLine($"Abs(e_(i+1) - s):                         {Abs(e[i+1]-s_1)}\n");
		outfile.Close();	
		return 0;
	}
}
