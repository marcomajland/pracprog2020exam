using System;
using static System.Console;
using static System.Math;
using static System.Double;
public partial class power_method{
	public static double[] inverse_iteration(matrix A, double e_0, vector v_0, double tol = 1e-6, int n_max = 999, int max_qrs = 5){
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
			if(m > max_qrs){
				m = 0;
				s = u.dot(A*u)/(u.dot(u));
				As = A - s*I;
				As_QR = new qr(As);
			}
			n++; m++;
		}
		s = u.dot(A*u)/(u.norm()*u.norm());
//		WriteLine($"Inverse iteration iterations:    {n}");
		return new double[2] {s,n};
	}
}


