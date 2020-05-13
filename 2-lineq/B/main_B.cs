using System;
using static System.Console;
class main_B{
	public static int Main(){
		Random rnd = new Random();
		int minint = 0; int maxint = 20; // Range of random integer entries in the tall matrix
		int n = 5; int m = 5; // Row and column dimensions

		matrix A = new matrix(n,m);
		for(int i=0;i<m;i++){for(int j=0;j<n;j++){A[i][j] = rnd.Next(minint,maxint);}} // Insert random matrix entries
		var data = new qr(A); // Instance of qr decomposition class of matrix A

		matrix Q = data.Q;
		matrix R = data.R;
		matrix B = data.inverse();
		matrix AB = A*B;
		
		// Output
		var outfile = new System.IO.StreamWriter("./out_B.txt",append:false);
		outfile.WriteLine($"--------------------------------");
		outfile.WriteLine($"Matrix inverse");
		outfile.WriteLine($"--------------------------------");
		outfile.WriteLine($"Random square matrix A:");
		for(int ir=0;ir<A.size1;ir++){for(int ic=0;ic<A.size2;ic++){
			outfile.Write("{0,10:g3} ", A[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine($"Matrix Q:");
		for(int ir=0;ir<Q.size1;ir++){for(int ic=0;ic<Q.size2;ic++){
			outfile.Write("{0,10:g3} ", Q[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine($"Matrix R:");
		for(int ir=0;ir<R.size1;ir++){for(int ic=0;ic<R.size2;ic++){
			outfile.Write("{0,10:g3} ", R[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine($"Inverse matrix B:");
		for(int ir=0;ir<B.size1;ir++){for(int ic=0;ic<B.size2;ic++){
			outfile.Write("{0,10:g3} ", B[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine($"A*B:");
		for(int ir=0;ir<AB.size1;ir++){for(int ic=0;ic<AB.size2;ic++){
			outfile.Write("{0,10:g3} ", AB[ir,ic]);}
			outfile.WriteLine("");}
		outfile.Close();
		return 0;
	}
}
