using System;
using static System.Console;
using static System.Math;
using static System.Double;
using System.Collections.Generic;
public partial class power_method{
	public static double[] inverse_iteration(matrix A, double e_0, vector v_0, double tol = 1e-6, int n_max = 999, int updates = 5, bool error_msg = false){
		int n = 0; int m = 0; double error = 1.0;
		double s; vector u; vector v;
		matrix As; matrix I;
		I = new matrix(A.size1,A.size1); I.set_identity();
		u = v_0/v_0.norm();
		s = e_0;
		As = A - s*I;
		qr As_QR = new qr(As);
		while(error > tol && n < n_max){
			v = As_QR.solve(u);
			v = v/v.norm();
			error = (v - u).norm();
			u = v/v.norm();
			if(m > updates){
				m = 0;
				s = u.dot(A*u)/(u.dot(u));
				As = A - s*I;
				As_QR = new qr(As);
			}
			n++; m++;
		}
		if(n == n_max && error_msg){WriteLine($"Warning: maximum iterations reached, maybe wrong eigenvector");}
		else if(error_msg){WriteLine($"Iteration results:");
			WriteLine($"Iterations:          {n}");
			WriteLine($"Error:               {error}");
			WriteLine($"Rayleigh:            {updates}");
		}
		s = u.dot(A*u)/(u.norm()*u.norm());
		return new double[] {s, n, error};
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


