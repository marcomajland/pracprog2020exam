using static System.Math;
public class qrdecomposition{

public matrix data;

public qrdecomposition(matrix M){
matrix A = M.copy();
for(int q=0;q<A.size2;q++){
	for(int p=q+1;p<A.size1;p++){
      		double theta=Atan2(A[p,q],A[q,q]);
		for(int k=q;k<A.size2;k++){
			double xq=A[q,k], xp=A[p,k];
			A[q,k]=+xq*Cos(theta)+xp*Sin(theta);
			A[p,k]=-xq*Sin(theta)+xp*Cos(theta);
			}
		A[p,q]=theta;
		}
	}
data = A;
}

public vector solve(vector rhs){
	vector b=rhs.copy();
	for(int q=0;q<data.size2;q++){
		for(int p=q+1;p<data.size1;p++){
			double theta = data[p,q];
			double xq=b[q], xp=b[p];
			b[q]=+xq*Cos(theta)+xp*Sin(theta);
			b[p]=-xq*Sin(theta)+xp*Cos(theta);
			}
		}
	vector x = new vector(data.size2);
	for(int i=data.size2-1;i>=0;i--){
		double s=0; for(int k=i+1;k<data.size2;k++) s+=data[i,k]*x[k];
		x[i]=(b[i]-s)/data[i,i];
		}
	return x;
}//solve

}
