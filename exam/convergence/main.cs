using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){	
		int dim = 50;
		double tol = 1e-6; int n_max = 999; int max_qrs = 999;

		var rnd = new Random(); int i = rnd.Next(dim);
		matrix A = misc.gen_matrix(dim); matrix Ac = A.copy();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues(); 
		matrix V = jacobi.get_eigenvectors(); 

		double e_0; vector v_0;

		double[] deviations = new double[3] {1.01, 1.02, 1.03};
		for(int j=0;j<deviations.Length;j++){
			e_0 = e[i]*deviations[j];
			v_0 = V[i]/V[i].norm();
			for(int k=0;k<v_0.size;k++){v_0[k] = v_0[k]*deviations[j];}
			generate_convergences(j, Ac, e_0, v_0, e[i], tol, n_max, max_qrs);
		}
		return 0;
	}	
	public static List<double> generate_convergences(int iteration, matrix A, double e_0, vector v_0, double e_J, double tol = 1e-6, int n_max = 999, int max_qrs = 5){
		matrix As; matrix I;
		I = new matrix(A.size1,A.size1); I.set_identity();
		double s = e_0;
		As = A - s*I;
		qr As_QR = new qr(As);
		
		List<double> errors = new List<double>();
		List<double> rayleigh_errors = new List<double>();
		
		generate_errors(A, As_QR, ref errors, 999, e_J, v_0, tol);
		generate_errors(A, As_QR, ref rayleigh_errors, 2, e_J, v_0, tol);

		var outfile = new System.IO.StreamWriter($"./plotfiles/convergence_{iteration}.txt",append:false);
		for(int k=0;k<errors.Count;k++){outfile.WriteLine($"{k} {errors[k]}");}
		outfile.Close();	
		var rayleigh_out = new System.IO.StreamWriter($"./plotfiles/rayleigh_convergence_{iteration}.txt",append:false);
		for(int k=0;k<rayleigh_errors.Count;k++){rayleigh_out.WriteLine($"{k} {rayleigh_errors[k]}");}
		rayleigh_out.Close();

		return errors;
	}
	public static void generate_errors(matrix A, qr As_QR, ref List<double> errors, int m_max, double e_J, vector v_0, double tol, double n_max = 999){
		int n = 0; int m = 0;
		matrix I = new matrix(A.size1,A.size1); I.set_identity();
		matrix As;
		double s; vector u; vector v;
		u = v_0/v_0.norm();
		double error = 1.0;
		while(error > tol && n < n_max){
			v = As_QR.solve(u);
			v = v/v.norm();
			error = (v - u).norm();
			u = v/v.norm();
			if(m > m_max){
				m = 0;
				s = u.dot(A*u)/(u.dot(u));
				As = A - s*I;
				As_QR = new qr(As);
			}
			n++; m++; errors.Add(error);
		}
		s = u.dot(A*u)/(u.norm()*u.norm());
		if(s - e_J > tol){WriteLine($"Error: Wrong eigenvalue");}
	}
}





