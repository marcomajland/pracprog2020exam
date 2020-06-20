using System;
using static System.Console;
using static System.Math;
using static System.Double;
using System.Collections.Generic;
public partial class power_method{
	public static double[] inverse_iteration(matrix A, double e_0, vector v_0, double tol = 1e-6, int n_max = 999, int updates = 5, bool error_msg = false, double eps = 1e-6){
		int n = 0; int m = 0; double error;
		double s; vector u; vector v;
		matrix As; matrix I;
		I = new matrix(A.size1,A.size1); I.set_identity();
		u = v_0/v_0.norm();
		s = e_0;
		As = A - s*I;
		qr As_QR = new qr(As);
		do{
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
		}while(nequal(u,v,tol,eps) && n < n_max);
		if(n == n_max && error_msg){WriteLine($"Warning: maximum iterations reached, maybe wrong eigenvector");}
		else if(error_msg){WriteLine($"Iteration results:");
			WriteLine($"Iterations:          {n}");
			WriteLine($"Error:               {error}");
			WriteLine($"Rayleigh:            {updates}");
		}
		s = u.dot(A*u)/(u.norm()*u.norm());
		return new double[] {s, n, error};
	}
	public static bool nequal(vector u, vector v, double tol, double eps){
		double diff; double rel_diff;
		for(int i=0;i<u.size;i++){
			diff = Abs(u[i] - v[i]);
			rel_diff = diff/Max(Abs(u[i]), Abs(v[i]));
			if(tol < diff){return false;}
			if(eps < rel_diff){return false;}
		}
		return true;				
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


