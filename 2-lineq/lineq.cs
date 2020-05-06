using System;
using static System.Console;
class lineq{
	public static int Main(){
		Random rnd = new Random();
		int minint = 0;
		int maxint = 20;
		int n = 5;
		int m = 5;
		matrix A = new matrix(n,m); // Row/column convention is interchanged
		vector b = new vector(n);
		for(int i=0;i<m;i++){for(int j=0;j<n;j++){A[i][j] = rnd.Next(minint,maxint);}}
		for(int i=0;i<n;i++){b[i] = rnd.Next(minint,maxint);}
		var data = new qr(A);
		vector x = new vector(n);
		matrix Q = data.Q;
		matrix R = data.R;
		matrix A_i = data.inverse();
		x = data.solve(b);
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
		WriteLine($"b:");
		b.print();
		WriteLine($"Ax:");
		(A*x).print();
		WriteLine($"A.inverse:");
		A_i.print();
		WriteLine($"A.inverse*A:");
		(A*A_i).print();
		return 0;
	}
}
