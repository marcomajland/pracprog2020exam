using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){
		int dim = 30;
		double deviation = 1.3;
		var rnd = new Random();
		int i = rnd.Next(20);

		matrix A = misc.gen_matrix(dim);
		matrix Ac = A.copy();

		var res = new jacobi_diagonalization(A);
		vector e = res.get_eigenvalues(); 
		matrix V = res.get_eigenvectors(); 

		double eigenvalue = e[i]*deviation;
		vector v_0 = V[i]/V[i].norm();

		double s = power_method.inverse_iteration(Ac, eigenvalue, v_0);

		WriteLine($"Initial eigenvalue:                         {eigenvalue}");
		WriteLine($"Inverse iteration method eigenvalue:        {s}");
		WriteLine($"Jacobi diagonalization eigenvalue:          {e[i]}");
		return 0;
	}
	public static Func<double,double> f3 = delegate(double x){return Cos(x);};
}
