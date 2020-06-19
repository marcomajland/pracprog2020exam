using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
class eigen{
	public static int Main(){
		// A: Jacobi diagonalization with cyclic sweeps and quantum particle in a box
		matrix A = misc.gen_matrix(5); // Generate random symmetric matrix A
		matrix Ac = A.copy();
		var res = new jacobi_diagonalization(A); // Create an instance of jacobi diagonalization for matrix A
		vector e = res.get_eigenvalues(); // Retrieve eigenvalues
		matrix V = res.get_eigenvectors(); // Retrieves eigenvectors
		matrix D = new matrix(A.size1,A.size1); // Create D matrix
		for(int i=0;i<A.size1;i++){D[i][i] = e[i];}
		matrix VTAV = V*Ac*V.transpose();
		Tuple<vector, matrix> boxres = box(20);

		var outfile = new System.IO.StreamWriter("../out_A.txt",append:false);
		outfile.WriteLine($"-----------------------------------------");
		outfile.WriteLine($"Jacobi diagonalization with cyclic sweeps");
		outfile.WriteLine($"-----------------------------------------");
		outfile.WriteLine("Random real symmetric matrix A:");
		for(int ir=0;ir<Ac.size1;ir++){for(int ic=0;ic<Ac.size2;ic++){
			outfile.Write("{0,10:g3} ", Ac[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine("Eigenvalues of A:");
		for(int ir=0;ir<e.size;ir++){outfile.Write("{0,10:g3} ", e[ir]);}
			outfile.WriteLine("");
		outfile.WriteLine("");
		outfile.WriteLine("Eigenvectors of A (matrix V):");
		for(int ir=0;ir<V.size1;ir++){for(int ic=0;ic<V.size2;ic++){
			outfile.Write("{0,10:g3} ", V[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine("V*A*V^T:");
		for(int ir=0;ir<VTAV.size1;ir++){for(int ic=0;ic<VTAV.size2;ic++){
			outfile.Write("{0,10:g3} ", VTAV[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine("Matrix D:");
		for(int ir=0;ir<D.size1;ir++){for(int ic=0;ic<D.size2;ic++){
			outfile.Write("{0,10:g3} ", D[ir,ic]);}
			outfile.WriteLine("");}
		outfile.WriteLine("");
		outfile.WriteLine($"-----------------------------------------");
		outfile.WriteLine($"Quantum particle in a box");
		outfile.WriteLine($"-----------------------------------------");
		int n = 20;
		outfile.WriteLine("Eigenvalues of the Hamiltonian (k, calculated, exact, error):");
		for(int k=0;k<n/3;k++){
			double exact = PI*PI*(k+1)*(k+1);
			double calculated = boxres.Item1[k];
			outfile.WriteLine($"{k}      {calculated}      {exact}      {exact-calculated}");
		}
		for(int k=0;k<3;k++){
			var eigenvectors = new System.IO.StreamWriter($"./plot_files/eigenvectors{k}.txt",append:false);
			eigenvectors.WriteLine($"{0} {0} {0}");
			for(int i=0;i<n;i++){
				eigenvectors.WriteLine($"{(i+1.0)/(n+1)} {boxres.Item2[k,i]} {Psi(k+1,(i+1.0)/(n+1))}");
			}
			eigenvectors.WriteLine($"{1} {0} {0}");
			eigenvectors.Close();

		}
		outfile.Close();

		return 0;
	}
	public static Tuple<vector, matrix> box(int n){
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
	
		return new Tuple<vector, matrix>(e, V);
	}
	public static double Psi(int n, double x){
		if(n % 2 == 0){
			return Sqrt(2)*Sin(n*PI*x+PI);
		}
		return Sqrt(2)*Cos(n*PI*x - PI/2);
	}		
}





