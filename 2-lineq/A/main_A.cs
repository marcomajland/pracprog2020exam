using System;
using static System.Console;
class main_A{
	public static int Main(){
		Random rnd = new Random();
		int minint = 0; int maxint = 20; // Range of random integer entries in the tall matrix
		int n = 5; int m = 3; // Row and column dimensions

		matrix A = new matrix(n,m);
		for(int i=0;i<m;i++){for(int j=0;j<n;j++){A[i][j] = rnd.Next(minint,maxint);}} // Insert random matrix entries
		var data = new qr(A); // Instance of qr decomposition class of matrix A
		matrix Q = data.Q;
		matrix R = data.R;		
		matrix QTQ = Q.transpose()*Q;
		matrix QR = Q*R;

		// Output
		var outfile = new System.IO.StreamWriter("./out_A.txt",append:false);
		outfile.WriteLine($"--------------------------------");
		outfile.WriteLine($"1: QR decomposition");
		outfile.WriteLine($"--------------------------------");
		outfile.WriteLine($"Random tall matrix A:");
		for(int ir=0;ir<A.size1;ir++){for(int ic=0;ic<A.size2;ic++){
			outfile.Write("{0,10:g3} ", A[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine($"Upper triangular matrix R:");
		for(int ir=0;ir<R.size1;ir++){for(int ic=0;ic<R.size2;ic++){
			outfile.Write("{0,10:g3} ", R[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine($"Q.transpose()*Q:");
		for(int ir=0;ir<QTQ.size1;ir++){for(int ic=0;ic<QTQ.size2;ic++){
			outfile.Write("{0,10:g3} ", QTQ[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine($"Q*R:");
		for(int ir=0;ir<QR.size1;ir++){for(int ic=0;ic<QR.size2;ic++){
			outfile.Write("{0,10:g3} ", QR[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");

		n = 5; m = 5;
		A = new matrix(n,m);
		vector b = new vector(n);
		for(int i=0;i<m;i++){for(int j=0;j<n;j++){A[i][j] = rnd.Next(minint,maxint);}}
		for(int i=0;i<n;i++){b[i] = rnd.Next(minint,maxint);} // b vector for linear equation
		data = new qr(A);
		vector x = new vector(n);
		x = data.solve(b);
		vector B = A*x;

		outfile.WriteLine($"--------------------------------");
		outfile.WriteLine($"2: Linear equations");
		outfile.WriteLine($"--------------------------------");
		outfile.WriteLine($"Random square matrix A:");
		for(int ir=0;ir<QR.size1;ir++){for(int ic=0;ic<QR.size2;ic++){
			outfile.Write("{0,10:g3} ", QR[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine($"Random vector b:");
		for(int ir=0;ir<b.size;ir++){outfile.Write("{0,10:g3} ", b[ir]);}
			outfile.WriteLine("");
		outfile.WriteLine($"A*x:");
		for(int ir=0;ir<B.size;ir++){outfile.Write("{0,10:g3} ", B[ir]);}
			outfile.WriteLine("");
		outfile.Close();

















		return 0;
	}
}
