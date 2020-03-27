using System;
using static System.Math;
using static System.Console;
class eigen{
	public static int Main(){
		matrix A = gen_matrix(2);
		WriteLine("A:");
		A.print();
		matrix B = eigenvalues(A);
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
	public static matrix eigenvalues(matrix A){
		vector e = new vector(A.size1); // Vector to contain eigenvalues
		vector en = new vector(A.size1);
		matrix V = new matrix(A.size1,A.size1); V.set_identity(); // Matrix to contain eigenvectors
		for(int i=0;i<A.size1;i++){e[i] = A[i][i];}
		while(vector.approx(e,en) == false){
			for(int i=0;i<A.size1;i++){e[i] = A[i][i];}
			for(int p=0;p<A.size1;p++){for(int q=p+1;q<A.size1;q++){
				double App = A[p][p], Aqq = A[q][q], Apq = A[q][p]; // Row/column convention interchanged
				double phi = 0.5*Atan2(2*Apq,Aqq - App);
				double c = Cos(phi), s = Sin(phi);
				A[p][p] = c*c*App - 2*s*c*Apq + s*s*Aqq;
				A[q][q] = s*s*App + 2*s*c*Apq + c*c*Aqq;
				A[q][p] = 0.0;
				for(int i=0;i<p;i++){
					A[p][i] = c*A[p][i] - s*A[q][i];
					A[q][i] = s*A[p][i] + c*A[q][i];
				}
				for(int i=p+1;i<q;i++){
					A[i][p] = c*A[i][p] - s*A[q][i];
					A[q][i] = s*A[i][p] + c*A[q][i];
				}
				for(int i=q+1;i<A.size1;i++){
					A[i][p] = c*A[i][p] - s*A[i][q];
					A[i][q] = c*A[i][q] + s*A[i][p];
				}
				for(int i=0;i<A.size1;i++){
					V[p][i] = c*V[p][i] - s*V[q][i];
					V[q][i] = s*V[p][i] + c*V[q][i];
				}				
			}}
			for(int i=0;i<A.size1;i++){en[i] = A[i][i];};
		}
		matrix D = new matrix(A.size1,A.size1);
		for(int i=0;i<D.size1;i++){D[i][i] = e[i];};
		D.print();
		matrix An = V*D*V.transpose();
		An.print();
		return A;
	}
}


