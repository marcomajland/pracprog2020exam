using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){
		int dim = 50;
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

		double tol = 1e-4; int max_qrs = 0; int n_max = 999;
		double s = power_method.inverse_iteration(Ac, e_0, v_0, tol, n_max, max_qrs);

		WriteLine($"Jacobi diagonalization eigenvalue:               {e[i]}");
		WriteLine($"Deviation for initial values:                    {deviation}");
		WriteLine($"Error tolerance:                                 {tol}");
		WriteLine($"Initial eigenvalue:                              {e_0}");
		WriteLine($"Inverse iteration method eigenvalue:             {s}");
		WriteLine($"Error (dev. from Jacobi):                        {Abs(s-e[i])}");

		return 0;
	}
}





