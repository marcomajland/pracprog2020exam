using System;
using static System.Console;
using static System.Math;
using static System.Double;
using System.Collections.Generic;
public partial class power_method{
	// The following method performs the inverse iteration method on a real symmetric matrix A
	public static int inverse_iteration(matrix A, ref double s, ref vector v, double tau = 1e-6, double eps = 1e-6, int n_max = 999, int updates = 999){
		int n = 0; int m = 0;
		matrix As; matrix I = new matrix(A.size1,A.size1); I.set_identity();
		v = v/v.norm();
		As = A - s*I;
		qr As_QR = new qr(As);
		double abs = 0; double rel = 0;
		while(converge(v,A,s,tau,eps,ref abs,ref rel) && n < n_max){
			v = As_QR.solve(v);
			v = v/v.norm();
			s = v.dot(A*v);
			if(m > updates){ // Update QR decomposition if Rayleigh updates are used (if updates<999)
				m = 0;
				s = v.dot(A*v)/(v.dot(v));
				As = A - s*I;
				As_QR = new qr(As);
			}
			n++; m++;
		}
		s = v.dot(A*v)/(v.norm()*v.norm());
		v = v/v.norm();
		return n;
	}
	// The following method checks for absolute/relative convergence
	public static bool converge(vector v, matrix A, double s, double tau, double eps, ref double abs, ref double rel){
		abs = (A*v - s*v).norm();
		rel = abs/((A*v).norm() + (s*v).norm());
		if(abs <= tau){return false;} // Absolute error
		if(rel <= eps){return false;} // Relative error
		return true;
	}
	// Below are two modified versions of the above algorithm in which the errors are collected to monitor the convergence
	public static void generate_convergences(int iteration, ref matrix A, ref matrix I, double e_0, vector v_0, double e_J, double tau = 1e-6, double eps = 1e-6, int n_max = 999, int updates = 999){
		matrix As; double s = e_0;
		As = A - s*I;
		qr As_QR = new qr(As);
		
		List<double> errors = new List<double>();
		List<double> errors_s = new List<double>();
		generate_errors(As_QR, ref A, ref I, ref errors, ref errors_s, s, updates, e_J, v_0, tau, eps);

		var outfile = new System.IO.StreamWriter($"./plotfiles/convergence_{iteration}.txt",append:false);
		for(int k=0;k<errors.Count;k++){outfile.WriteLine($"{k} {errors[k]} {errors_s[k]}");}
		outfile.Close();	
	}
	public static void generate_errors(qr As_QR, ref matrix A, ref matrix I, ref List<double> errors, ref List<double> errors_s, double s, int updates, double e_J, vector v_0, double tau = 1e-6, double eps = 1e-6, double n_max = 999){
		int n = 0; int m = 0;
		matrix As;
		vector u; vector v;
		u = v_0/v_0.norm();
		double abs=0; double rel=0;
		while(converge(u,A,s,tau,eps,ref abs,ref rel) && n < n_max){
			v = As_QR.solve(u);
			u = v/v.norm();
			s = u.dot(A*u);
			if(m > updates){
				m = 0;
				s = u.dot(A*u)/(u.dot(u));
				As = A - s*I;
				As_QR = new qr(As);
			}
			n++; m++; errors.Add(rel); errors_s.Add(Abs(s-e_J));
		}
		errors.Add(rel);
		errors_s.Add(Abs(s-e_J));
		s = u.dot(A*u)/(u.norm()*u.norm());
	}
}

