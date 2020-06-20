using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){	
		int dim = 30;
		double tau = 1e-6;
		double eps = 1e-6;
		int updates = 999; // If updates=999, no Rayleigh updates are performed
		int n_max = 999;
		var rnd = new Random(); int i = rnd.Next(dim);
		
		matrix A = misc.gen_matrix(dim);
		matrix Ac = A.copy();
		matrix I = new matrix(A.size1,A.size1); I.set_identity();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues(); 

		vector v_0 = misc.gen_vector(dim);

		// The below code monitors the error as function of iterations to investigate convergence for different deviations
		double e_0;
		double[] deviations = new double[3] {1.01, 1.02, 1.03};
		double[] deviations2 = new double[3] {1.05, 1.075, 1.10};
		for(int j=0;j<deviations.Length;j++){
			e_0 = e[i]*deviations[j];
			power_method.generate_convergences(j, ref Ac, ref I, e_0, v_0, e[i], tau, eps, n_max, updates);
		}
		for(int j=0;j<deviations.Length;j++){
			e_0 = e[i]*deviations[j];
			power_method.generate_convergences(j+deviations.Length, ref Ac, ref I, e_0, v_0, e[i], tau, eps, n_max, -1);
		}
		for(int j=0;j<deviations2.Length;j++){
			e_0 = e[i]*deviations2[j];
			power_method.generate_convergences(j+2*deviations.Length, ref Ac, ref I, e_0, v_0, e[i], tau, eps, n_max, updates);
		}
		for(int j=0;j<deviations2.Length;j++){
			e_0 = e[i]*deviations2[j];
			power_method.generate_convergences(j+3*deviations2.Length, ref Ac, ref I, e_0, v_0, e[i], tau, eps, n_max, -1);
		}
		return 0;
	}	
}
