using static System.Console;
using static System.Math;

class mainA {
	static void Main() {
		// Test that the diagonalization works as intended: 
		Write("Problem A:\n");
		Write("First, testing the Jacobi sweeping on a random matrix A:");
		int n = 5;
		matrix A = matrixHelp.makeRandSymMatrix(n);
		matrix ACopy = A.copy();
		A.print("Random matrix A: ");
		matrix V = new matrix(n, n);
		vector e = new vector(n);
		int sweeps = jacobi.jacobi_cyclic(A, e, V);
		Write($"Sweeps = {sweeps}\n");
		A.print("Reduced matrix A: ");
		e.print("Eigenvalues: ");
		V.print("Eigenvectors V: ");
		
		// Test that this diagonalizes the matrix:
		(V.transpose()*ACopy*V).print("A in the eigenbasis: ");
		matrix D = new matrix(A.size1,A.size1);
		for(int i=0;i<A.size1;i++){D[i][i] = e[i];}
		(V*D*V.transpose()).print();
	}	
}
