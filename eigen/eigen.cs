using System;
using static System.Math;
using static System.Console;
class eigen{
	public static int Main(){
		matrix A = gen_matrix(3);
		matrix Ac = A.copy();
		var res = new jacobi_diagonalization(A);
		vector e = res.get_eigenvalues();
		matrix V = res.get_eigenvectors();
		WriteLine("Random real symmetric matrix A:");
		Ac.print();
		matrix D = new matrix(A.size1,A.size1);
		for(int i=0;i<A.size1;i++){D[i][i] = e[i];}
		WriteLine("Eigenvalues of A:");
		e.print();
		WriteLine("Eigenvectors of V:");
		V.print();
		WriteLine("V*D*V^T:");
		(V.transpose()*D*V).print();
		WriteLine("V^T*D*V:");
		(V*Ac*V.transpose()).print();		
		return 0;
	}
	public static matrix gen_matrix(int n){
		Random rnd = new Random();
		int minint = 0;
		int maxint = 10;
		matrix A = new matrix(n,n); // Row/column convention is interchanged
		for(int i=0;i<n;i++){for(int j=0;j<n;j++){A[i][j] = rnd.Next(minint,maxint);A[j][i]=A[i][j];}}
		return A;
	}
}
