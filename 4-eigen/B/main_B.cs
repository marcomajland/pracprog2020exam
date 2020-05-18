using System;
using static System.Math;
using static System.Console;
using System.Collections.Generic;
using System.Diagnostics;
class eigen{
	public static int Main(){
		matrix A;
		int nmax = 60;
		Stopwatch time = new Stopwatch();
		double[] times = new double[nmax-2];
		for(int n=2;n<nmax;n++){
			A = misc.gen_matrix(n);
			time.Reset();
			time.Start();
			var res = new jacobi_diagonalization(A);
			time.Stop();
			times[n-2] = time.ElapsedMilliseconds;
		}
		var diag_scale = new System.IO.StreamWriter("./plot_files/diag_scale.txt",append:false);
		for(int i=0;i<times.Length;i++){
			diag_scale.WriteLine($"{i} {times[i]/times[times.Length-1]}");
		}
		diag_scale.Close();
/*
		var outfile = new System.IO.StreamWriter("./out_B.txt",append:false);
		outfile.WriteLine($"-----------------------------------------------");
		outfile.WriteLine($"Jacobi diagonalization eigenvalue-by-eigenvalue");
		outfile.WriteLine($"-----------------------------------------------");
		outfile.Close();
*/

/*
		for(int ir=0;ir<VTAV.size1;ir++){for(int ic=0;ic<VTAV.size2;ic++){
			outfile.Write("{0,10:g3} ", VTAV[ir,ic]);}
			outfile.WriteLine("");}
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
*/
	return 0;
	}
}





