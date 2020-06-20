using System;
using static System.Console;
using static System.Math;
using static System.Double;
using System.Collections.Generic;
public partial class power_method{
	public static double[] inverse_iteration(matrix A, double e_0, vector v_0, double tau = 1e-6, double eps = 1e-6, int n_max = 999, int updates = 5, bool error_msg = false){
		int n = 0; int m = 0; double error;
		double s; vector u; vector v;
		matrix As; matrix I;
		I = new matrix(A.size1,A.size1); I.set_identity();
		u = v_0/v_0.norm();
		s = e_0;
		As = A - s*I;
		qr As_QR = new qr(As);
		double abs=0; double rel=0;
		while(nconverge(u,A,s,tau,eps,ref abs, ref rel) && n < n_max);{
			v = As_QR.solve(u);
			u = v/v.norm();
			s = u.dot(A*u);
			if(m > updates){
				m = 0;
				s = u.dot(A*u)/(u.dot(u));
				As = A - s*I;
				As_QR = new qr(As);
			}
			n++; m++;
		}
		s = u.dot(A*u)/(u.norm()*u.norm());
		return new double[] {s, n};
	}
	public static bool nconverge(vector v, matrix A, double s, double tau, double eps, ref double abs, ref double rel){
		abs = (A*v - s*v).norm();
		rel = abs/((A*v).norm() + (s*v).norm());
		if(abs <= tau){return false;}
		if(rel <= eps){return false;}
		return true;
	}
	public static void generate_convergences(int iteration, ref matrix A, ref matrix I, double e_0, vector v_0, double e_J, double tau = 1e-6, double eps = 1e-6, int n_max = 999, int updates = 999){
		matrix As; double s = e_0;
		As = A - s*I;
		qr As_QR = new qr(As);
		
		List<double> errors = new List<double>();
		//List<double> rayleigh_errors = new List<double>();
		
		generate_errors(As_QR, ref A, ref I, ref errors, s, updates, e_J, v_0, tau, eps);

		var outfile = new System.IO.StreamWriter($"./plotfiles/convergence_{iteration}.txt",append:false);
		for(int k=0;k<errors.Count;k++){outfile.WriteLine($"{k} {errors[k]}");}
		outfile.Close();	
	}
	public static void generate_errors(qr As_QR, ref matrix A, ref matrix I, ref List<double> errors, double s, int updates, double e_J, vector v_0, double tau = 1e-6, double eps = 1e-6, double n_max = 999){
		int n = 0; int m = 0;
		matrix As;
		vector u; vector v;
		u = v_0/v_0.norm();
		double abs=0; double rel=0;
		while(nconverge(u,A,s,tau,eps,ref abs,ref rel) && n < n_max){
			v = As_QR.solve(u);
			u = v/v.norm();
			s = u.dot(A*u);
			if(m > updates){
				m = 0;
				s = u.dot(A*u)/(u.dot(u));
				As = A - s*I;
				As_QR = new qr(As);
			}
			n++; m++; errors.Add(rel);
		}
		errors.Add(rel);
		s = u.dot(A*u)/(u.norm()*u.norm());
	}
}

