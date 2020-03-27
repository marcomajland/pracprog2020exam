using System;
using static System.Math;
public class jacobi_diagonalization{
	public vector e;
	public matrix V;
	public jacobi_diagonalization(matrix A){
		e = new vector(A.size1); // Vector to contain eigenvalues
		V = new matrix(A.size1,A.size1); V.set_identity(); // Matrix to contain eigenvectors
		int changed;
		for(int i=0;i<A.size1;i++){e[i] = A[i][i];}
		do{changed = 0;
			for(int p=0;p<A.size1;p++){for(int q=p+1;q<A.size1;q++){
				double App = e[p], Aqq = e[q], Apq = A[p][q];
				double phi = 0.5*Atan2(2*Apq,Aqq - App);
				double c = Cos(phi), s = Sin(phi);
				double app = c*c*App - 2*s*c*Apq + s*s*Aqq;
				double aqq = s*s*App + 2*s*c*Apq + c*c*Aqq;
				if(app!=App || aqq!=Aqq){changed=1;
					e[p] = app; e[q] = aqq;
					A[p][p] = app; A[q][q] = aqq; A[p][q] = 0.0;
					for(int i=0;i<p;i++){
						double Aip = A[i][p];
						double Aiq = A[i][q];
						A[i][p] = c*Aip - s*Aiq;
						A[i][q] = s*Aip + c*Aiq;
					}
					for(int i=p+1;i<q;i++){
						double Api = A[p][i];
						double Aiq = A[i][q];
						A[p][i] = c*Api - s*Aiq;
						A[i][q] = s*Api + c*Aiq;
					}
					for(int i=q+1;i<A.size1;i++){
						double Api = A[p][i];
						double Aqi = A[q][i];
						A[p][i] = c*Api - s*Aqi;
						A[q][i] = c*Aqi + s*Api;
					}
					for(int i=0;i<A.size1;i++){
						double Vip = V[i][p];
						double Viq = V[i][q];
						V[i][p] = c*Vip - s*Viq;
						V[i][q] = s*Vip + c*Viq;
					}
				}				
			}}
		}while(changed != 0);
	}
	public vector get_eigenvalues(){
		return e;
	}
	public matrix get_eigenvectors(){
		return V;
	}





}





