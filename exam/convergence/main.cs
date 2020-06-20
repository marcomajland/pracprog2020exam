using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main{
	public static int Main(){	
		int dim = 30;
		double tol = 1e-6;
		int updates = 999;
		int n_max = 999;
		var rnd = new Random(); int i = rnd.Next(dim);
		
		matrix A = misc.gen_matrix(dim);
		matrix Ac = A.copy();

		var jacobi = new jacobi_diagonalization(A);
		vector e = jacobi.get_eigenvalues(); 
		matrix V = jacobi.get_eigenvectors(); 

		vector v_0 = new vector(V[i].size);
		for(int j=0;j<v_0.size;j++){v_0[j] = rnd.NextDouble()*dim;}

		double e_0;
		double[] deviations = new double[1] {1.01};
		for(int j=0;j<deviations.Length;j++){
			e_0 = e[i]*deviations[j];
//			v_0 = V[i]/V[i].norm();
//			for(int k=0;k<v_0.size;k++){v_0[k] = v_0[k]*deviations[j];}
			matrix I = new matrix(A.size1,A.size1); I.set_identity();
			generate_convergences(j, ref Ac, ref I, e_0, v_0, e[i], tol, n_max, updates);
		}
		return 0;
	}	
	public static void generate_convergences(int iteration, ref matrix A, ref matrix I, double e_0, vector v_0, double e_J, double tol = 1e-6, int n_max = 999, int updates = 999){
		matrix As; double s = e_0;
		As = A - s*I;
		qr As_QR = new qr(As);
		
		List<double> errors = new List<double>();
		//List<double> rayleigh_errors = new List<double>();
		
		generate_errors(As_QR, ref A, ref I, ref errors, updates, e_J, v_0, tol);

		var outfile = new System.IO.StreamWriter($"./plotfiles/convergence_{iteration}.txt",append:false);
		for(int k=0;k<errors.Count;k++){outfile.WriteLine($"{k} {errors[k]}");}
		outfile.Close();	
	}
	public static void generate_errors(qr As_QR, ref matrix A, ref matrix I, ref List<double> errors, int updates, double e_J, vector v_0, double tol, double n_max = 999){
		int n = 0; int m = 0;
		matrix As;
		double s; vector u; vector v;
		u = v_0/v_0.norm();
		double error = 1.0;
		while(error > tol && n < n_max){
			v = As_QR.solve(u);
			v = v/v.norm();
			error = (v - u).norm();
			errors.Add(error);
			if(error > 1.99){break;}
			u = v/v.norm();
			if(m > updates){
				m = 0;
				s = u.dot(A*u)/(u.dot(u));
				As = A - s*I;
				As_QR = new qr(As);
			}
			n++; m++;
		}
		s = u.dot(A*u)/(u.norm()*u.norm());
	}
}





