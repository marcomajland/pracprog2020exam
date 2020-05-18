using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.Diagnostics;
class eigen{
	public static int Main(){
		// 1) Amount of operations scaling
		matrix A;
		int nmax = 60;
		Stopwatch time = new Stopwatch();
		var diag_scale = new System.IO.StreamWriter("./plot_files/diag_scale.txt",append:false);
		for(int n=2;n<nmax;n+=1){
			A = misc.gen_matrix(n);
			time.Reset();
			time.Start();
			var res = new jacobi_diagonalization(A);
			time.Stop();
			diag_scale.WriteLine($"{n} {time.ElapsedMilliseconds}");
		}
		diag_scale.Close();

		// 2) Demonstration of value-by-value method
		int dim = 10; int eigenvalues = 5;
		A = misc.gen_matrix(dim);
		matrix Ac = A.copy();
		var A_res = new jacobi_diagonalization(A);
		A.print();
		var Ac_res = new jacobi_diagonalization(Ac);
		var outfile = new System.IO.StreamWriter("./outfile.txt",append:false);
		outfile.WriteLine($"-----------------------------------------------");
		outfile.WriteLine($"Jacobi diagonalization eigenvalue-by-eigenvalue");
		outfile.WriteLine($"-----------------------------------------------");
		outfile.WriteLine($"A random symmetric matrix A of dimension {dim}x{dim} is generated.");
		outfile.WriteLine($"{eigenvalues} lowest eigenvalues of A:");
		vector lowest_eigenvalues = A_res.lowest_eigenvalues(A, 1);
		for(int ir=0;ir<lowest_eigenvalues.size;ir++){outfile.Write("{0,10:g3} ", lowest_eigenvalues[ir]);}
			outfile.WriteLine("");
		outfile.WriteLine($"Eigenvalues of A for comparison:");
		vector all_eigenvalues = A_res.get_eigenvalues();
		for(int ir=0;ir<all_eigenvalues.size;ir++){outfile.Write("{0,10:g3} ", all_eigenvalues[ir]);}
			outfile.WriteLine("");

		outfile.Close();
		// 3) Comparison of cyclic sweep method and value-by-value method


//		WriteLine($"Amount of Jacobi rotations to diagonalize A is {res.get_rotations()} while amount of Jacobi rotations to obtain lowest eigenvalue of A is {res.get_single_row_rotations()}.");	

	return 0;
	}
}





