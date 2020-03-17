using System;
using static System.Console;
class lineq{
	public static int Main(){
		Random rnd = new Random();
		int minint = 0;
		int maxint = 10;
		int n = 5;
		int m = 3;
		matrix A = new matrix(n,m); // Row/column convention is interchanged
		vector b = new vector(n);
		for(int i=0;i<m;i++){for(int j=0;j<n;j++){A[i][j] = rnd.Next(minint,maxint);}}
		for(int i=0;i<n;i++){b[i] = rnd.Next(minint,maxint);}
		Tuple<matrix, matrix> QR = qr_gs_decomp(A);
		matrix Q = QR.Item1;
		matrix R = QR.Item2;
		WriteLine($"A:");
		A.print();
		WriteLine($"Q:");
		Q.print();
		WriteLine($"Q.transpose*Q:");
		(Q.transpose()*Q).print();		
		WriteLine($"R:");
		R.print();
		WriteLine($"Q*R:");
		(Q*R).print();
		return 0;
	}
	public static Tuple<matrix, matrix> qr_gs_decomp(matrix A){
		matrix B = A.copy();
		matrix Q = new matrix(A.size1,A.size2);
		matrix R = new matrix(A.size2,A.size2);
		for(int i=0;i<A.size2;i++){
			// In the matrix library, row/column convention is interchanged 
			// such that A[i][j] = A[j][i]
			R[i][i] = B[i].norm();
			Q[i] = B[i]/R[i][i];
			for(int j=i+1;j<A.size2;j++){
				R[j][i] = Q[i].dot(B[j]);
				B[j] = B[j] - Q[i]*R[j][i];
			}
		}
		return Tuple.Create(Q,R);
	}
	public static int qr_gs_solve(matrix Q, matrix R, vector b){
		return 0;

	}
}



















