using System;
using static System.Math;
using static System.Console;
class eigen{
	public static int Main(){
		// A: Jacobi diagonalization with cyclic sweeps and quantum particle in a box
		matrix A = gen_matrix(10);
		matrix Ac = A.copy();
		var res = new jacobi_diagonalization(A);
		vector e = res.get_eigenvalues();
		matrix V = res.get_eigenvectors();
		matrix D = new matrix(A.size1,A.size1);
		for(int i=0;i<A.size1;i++){D[i][i] = e[i];}
		WriteLine("A: Jacobi diagonalization with cyclic sweeps and quantum particle in a box:");
		WriteLine("------------------------------------------------");
		WriteLine("Diagonalization of random real symmetric matrix:");
		WriteLine("------------------------------------------------");
		WriteLine("Random real symmetric matrix A:");
		Ac.print();
		WriteLine("Eigenvalues of A:");
		e.print();
		WriteLine("Eigenvectors of V:");
		V.print();
		WriteLine("V*D*V^T:");
		(V.transpose()*D*V).print();
		WriteLine("V^T*D*V:");
		(V*Ac*V.transpose()).print();
		WriteLine("---------------------------------------------");
		WriteLine("Diagonalization of quantum particle in a box:");
		WriteLine("---------------------------------------------");
		box();
		Write("\n");
		// B: Jacobi diagonalization eigenvalue-by-eigenvalue
		WriteLine("B: Jacobi diagonalization eigenvalue-by-eigenvalue:");
		WriteLine($"The amount of Jacobi rotations performed on random real symmetric matrix A in exercise A is {res.get_rotations()}.");
		WriteLine("------------------------------");
		WriteLine("Lowest eigenvalue computation:");
		WriteLine("------------------------------");
		int n = 5;
		vector lowest_eigenvalues = res.lowest_eigenvalues(A, n);
		WriteLine($"{n} lowest eigenvalues of A:");
		lowest_eigenvalues.print();
		WriteLine($"Eigenvalues of A for comparison:");
		e.print();
		vector lowest_eigenvalue = res.lowest_eigenvalues(A, 1);
		WriteLine($"Amount of Jacobi rotations to diagonalize A is {res.get_rotations()} while amount of Jacobi rotations to obtain lowest eigenvalue of A is {res.get_single_row_rotations()}.");	
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
	public static int box(){
		int n = 20;
		double s=1.0/(n+1);
		matrix H = new matrix(n,n);
		for(int i=0;i<n-1;i++){
			matrix.set(H,i,i,-2);
			matrix.set(H,i,i+1,1);
			matrix.set(H,i+1,i,1);
		}
		matrix.set(H,n-1,n-1,-2);
		H = -1/s/s*H;

		var res = new jacobi_diagonalization(H);
		vector e = res.get_eigenvalues();
		matrix V = res.get_eigenvectors();
		
		WriteLine("Eigenvalues of the Hamiltonian (calculated, exact):");
		for (int k=0; k < n/3; k++){
			double exact = PI*PI*(k+1)*(k+1);
			double calculated = e[k];
			WriteLine($"{k} {calculated} {exact}");
		}
		for(int k=0;k<3;k++){
			var eigenvectors = new System.IO.StreamWriter($"eigenvectors{k}.txt",append:false);
			eigenvectors.WriteLine($"{0} {0} {0}");
			for(int i=0;i<n;i++){
				eigenvectors.WriteLine($"{(i+1.0)/(n+1)} {V[k,i]} {Psi(k+1,(i+1.0)/(n+1))}");
			}
			eigenvectors.WriteLine($"{1} {0} {0}");
			eigenvectors.Close();
		}
		return 0;
	}
	public static double Psi(int n, double x){
		if(n % 2 == 0){
			return Sqrt(2)*Sin(n*PI*x+PI);
		}
		return Sqrt(2)*Cos(n*PI*x - PI/2);
	}		
}





