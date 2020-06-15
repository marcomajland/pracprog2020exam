using System;
using static System.Console;
using static System.Math;
using static System.Double;
public partial class power_method{
	public static double inverse_iteration(matrix A, double eigenvalue, vector eigenvector){
		int n = 0; int m = 0; double s; vector u; vector v; matrix As; matrix I; double error=1.0;
		double tol = 1e-4;
		I = new matrix(A.size1,A.size1); I.set_identity();
		u = eigenvector/eigenvector.norm();
		s = eigenvalue;
		As = A - s*I;
		qr As_QR = new qr(As);
		while(error > tol && n<999){
			v = As_QR.solve(u);
			v = v/v.norm();
			error = (v-u).norm();
			u = v/v.norm();
			if(m>5){
				m = 0;
				s = u.dot(A*u)/(u.dot(u));
				As = A - s*I;
				As_QR = new qr(As);
			}
			n++; m++;
		}
		s = u.dot(A*u)/(u.norm()*u.norm());
		return s;
	}
}



