using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.Diagnostics;
class eigen{
	public static int Main(){
		// 1) Amount of operations scaling
		matrix A;
		int nmax = 100;
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
		var A_res = new jacobi_diagonalization(A,"cyclic");
		var Ac_res = new jacobi_diagonalization(Ac,"value",eigenvalues);
		var outfile = new System.IO.StreamWriter("../out_B.txt",append:false);
		outfile.WriteLine($"-----------------------------------------------");
		outfile.WriteLine($"Jacobi diagonalization eigenvalue-by-eigenvalue");
		outfile.WriteLine($"-----------------------------------------------");
		outfile.WriteLine($"Test of eigenvalue-by-eigenvalue routine:");
		outfile.WriteLine($"A random symmetric matrix A of dimension {dim}x{dim} is generated.\n");
		outfile.WriteLine($"{eigenvalues} lowest eigenvalues of A:");
		vector lowest_eigenvalues = Ac_res.get_eigenvalues();
		for(int ir=0;ir<lowest_eigenvalues.size;ir++){outfile.Write("{0,10:g3} ", lowest_eigenvalues[ir]);}
		outfile.WriteLine("\n");
		outfile.WriteLine($"Eigenvalues of A for comparison:");
		vector all_eigenvalues = A_res.get_eigenvalues();
		for(int ir=0;ir<all_eigenvalues.size;ir++){outfile.Write("{0,10:g3} ", all_eigenvalues[ir]);}
		outfile.WriteLine("\n");

		outfile.WriteLine($"Comparison of the two diagonalization routines in terms of number of rotations:");
		dim = 50;
		A = misc.gen_matrix(dim);
		Ac = A.copy();
		matrix Acc = A.copy();
		A_res = new jacobi_diagonalization(A,"cyclic");
		Ac_res = new jacobi_diagonalization(Ac,"value",1);
		var Acc_res = new jacobi_diagonalization(Acc,"value",dim);
		outfile.WriteLine($"Another random symmetric matrix A of dimension {dim}x{dim} is generated.\n");
		outfile.WriteLine($"Cyclic method:");
		outfile.WriteLine($"Lowest eigenvalue:                            {A_res.get_eigenvalues()[0]}");
		outfile.WriteLine($"Amount of rotations (full diagonalization):   {A_res.get_rotations()}\n");
		outfile.WriteLine($"Eigenvalue-by-eigenvalue method:");
		outfile.WriteLine($"Lowest eigenvalue:                            {Ac_res.get_eigenvalues()[0]}");
		outfile.WriteLine($"Amount of rotations (lowest eigenvalue):      {Ac_res.get_rotations()}");
		outfile.WriteLine($"Amount of rotations (full diagonalization):   {Acc_res.get_rotations()}\n");
		outfile.WriteLine($"Thus, the eigenvalue-by-eigenvalue method is suitable for calculuating only the lowest eigenvalues of a matrix whereas the cyclic sweep method is faster for full diagonalization.");
		outfile.WriteLine($"Rotation angle should be changed into 0.5*arctan2(-Apq, App-Aqq) to achieve largest eigenvalue.");
		outfile.Close();
	return 0;
	}
}





