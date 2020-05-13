using System;
using static System.Math;
public class lsq_qr{
	public vector c;
	public matrix cov;
	public matrix A;
	public double[] dc;
	public lsq_qr(double[] x, double[] y, double[] dy, Func<double,double>[] F){
		A = new matrix(x.Length,F.Length);
		for(int i=0;i<F.Length;i++){
			for(int j=0;j<x.Length;j++){
				A[i][j] = F[i](x[j])/dy[j]; // Row/column convention interchanged
			}
		}
		vector b = new vector(x.Length);
		for(int i=0;i<x.Length;i++){
			b[i] = y[i]/dy[i];
		}
		qr AQR = new qr(A);
		c = AQR.solve(b);
	}
	public matrix cov_matrix(){
		qr ATA = new qr(A.transpose()*A);
		cov = ATA.inverse();
		return cov;
	}
	public double[] unc(){
		if(cov == null){
			cov = new matrix(c.size,c.size);
			cov = cov_matrix();
		}
		dc = new double[cov.size1];
		for(int i=0;i<dc.Length;i++){
			dc[i] = Sqrt(cov[i][i]);
		}
		return dc;
	}
	public vector get_c(){
		return c;
	}
	public matrix get_cov(){
		if(cov == null){
			cov = new matrix(c.size,c.size);
			cov = cov_matrix();
		}
		return cov;
	}
	public double[] get_dc(){
		if(dc == null){
			dc = unc();
		}
		return dc;
	}
}
