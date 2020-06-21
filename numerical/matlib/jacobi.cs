using System;
using static System.Math;
public class jacobi_diagonalization{
	public vector e;
	public matrix V;
	public matrix Ac;
	public int rotations;
	public jacobi_diagonalization(matrix A, string routine="cyclic", int n=0){
		if(routine=="cyclic"){
			e = new vector(A.size1); // Vector to contain eigenvalues
			V = new matrix(A.size1,A.size1); V.set_identity(); // Matrix to contain eigenvectors
			int changed; int sweeps = 0; rotations = 0;
			do{changed = 0; sweeps += 1;
				for(int p=0;p<A.size1;p++){for(int q=p+1;q<A.size1;q++){
					rotations += 1;
					changed = rotation(p,q,A);
					}				
				}
			}while(changed != 0);
			for(int i=0;i<A.size1;i++){e[i] = A[i][i];}
		}
		if(routine=="value"){
			e = new vector(n); // Vector to contain eigenvalues
			int changed; rotations = 0;
			for(int p=0;p<n;p++){
				do{changed = 0; for(int q=p+1;q<A.size1;q++){
					rotations += 1;
					changed = single_row_rotation(p, q, A);
				}}while(changed != 0);}
			for(int i=0;i<n;i++){e[i] = A[i][i];}
		}		
	}
	public int rotation(int p, int q, matrix A){
		double App = A[p][p], Aqq = A[q][q], Apq = A[p][q];
		double phi = 0.5*Atan2(2*Apq,Aqq - App);
		double c = Cos(phi), s = Sin(phi);
		double app = c*c*App - 2*s*c*Apq + s*s*Aqq;
		double aqq = s*s*App + 2*s*c*Apq + c*c*Aqq;
		if(app!=App || aqq!=Aqq){int change=1;
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
			return change;
		}
		else{int change = 0; return change;}
	}
	public int single_row_rotation(int p, int q, matrix A){
		double App = A[p][p], Aqq = A[q][q], Apq = A[p][q];
		double phi = 0.5*Atan2(2*Apq,Aqq - App);
		double c = Cos(phi), s = Sin(phi);
		double app = c*c*App - 2*s*c*Apq + s*s*Aqq;
		double aqq = s*s*App + 2*s*c*Apq + c*c*Aqq;
		if(app!=App){int schanged=1;
			A[p][p] = app; A[q][q] = aqq; A[p][q] = 0.0;
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
		return schanged;
		}
		else{int schanged = 0; return schanged;}
	public vector get_eigenvalues(){
		return e;
	}
	public matrix get_eigenvectors(){
		return V;
	}
	public int get_rotations(){
		return rotations;
	}
}


