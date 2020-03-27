using System;
public class qr{
	public matrix Q;
	public matrix R;
	public int n;
	public int m;
	public qr(matrix A){
		n = A.size1;
		m = A.size2;
		Q = new matrix(n,m);
		R = new matrix(m,m);		
		matrix B = A.copy();
		for(int i=0;i<m;i++){
		// In the matrix library, row/column convention is interchanged 
		// such that A[i][j] = A[j][i]
		R[i][i] = B[i].norm();
		Q[i] = B[i]/R[i][i];
		for(int j=i+1;j<m;j++){
			R[j][i] = Q[i].dot(B[j]);
			B[j] = B[j] - Q[i]*R[j][i];
			}
		}
	}
	public vector solve(vector b){
		vector x = new vector(n);
		vector y = Q.transpose()*b;
		for(int i=n-1;i>=0;i--){
			x[i] = y[i]/R[i][i];
			for(int j=i+1;j<n;j++){
				x[i] -= (R[j][i]*x[j])/R[i][i];
			}
		}
		return x;
	}
	public matrix inverse(){
		matrix A_i = new matrix(n,n);
		vector b = new vector(n);
		for(int i=0;i<n;i++){
			b[i] = 1;
			A_i[i] = solve(b);
			b[i] = 0;
		}
		return A_i;
	}
}





