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
		for(int n=20;n<nmax;n+=1){
			A = misc.gen_matrix(n);
			time.Reset();
			time.Start();
			var res = new jacobi_diagonalization(A);
			time.Stop();
			diag_scale.WriteLine($"{Log(n)} {Log(time.ElapsedMilliseconds)}");
		}
		diag_scale.Close();

		// Linear fit to logarithmic data to verify n^3 dependence
		List<double[]> data = misc.load_data("./plot_files/diag_scale.txt");
		double[] x = data[0]; double[] y = data[1];
		double[] dy = new double[y.Length]; // Allocate array for y errors
		for(int i=0;i<dy.Length;i++){dy[i]= 1e-3;} // Logarithmic y errors
		Func<double,double>[] F = new Func<double,double>[] {s => 1, s => s}; // Array of fitting functions
		var fit = new lsq_qr(x,y,dy,F); // Variable storing instance of least square class
		vector c = fit.get_c(); // Retrieve expansion coefficients of fitting functions
		double a = c[0];
		double b = c[1];
		var fit_data = new System.IO.StreamWriter("./plot_files/fit_data.txt",append:false);
		for(double n=x[0];n<x[x.Length-1];n+=0.1){
			fit_data.WriteLine($"{n} {a + b*n}");
		}
		fit_data.Close();

		// 2) Demonstration of value-by-value method
		int dim = 10; int eigenvalues = 5;
		A = misc.gen_matrix(dim);
		matrix Ac = A.copy();
		matrix Acc = A.copy();
		var A_res = new jacobi_diagonalization(A,"cyclic");
		var Ac_res = new jacobi_diagonalization(Ac,"value","min",eigenvalues);
		var Acc_res = new jacobi_diagonalization(Acc,"value","max",eigenvalues);
		var outfile = new System.IO.StreamWriter("../out_B.txt",append:false);
		outfile.WriteLine($"---------------------------------");
		outfile.WriteLine($"Scaling of matrix diagonalization");
		outfile.WriteLine($"---------------------------------");
		outfile.WriteLine($"To demonstrate the O(n^3) scaling of the matrix diagonalization procedure, a linear fit is applied to the logaritmic (n,t) data.");
		outfile.WriteLine($"Fit result:");
		outfile.WriteLine($"log(t) = {a} + {b}*log(n)");
		outfile.WriteLine($"Thus, the scaling must be O(n^3) since the slope is approximately equal to 3.\n");
		outfile.WriteLine($"-----------------------------------------------");
		outfile.WriteLine($"Jacobi diagonalization eigenvalue-by-eigenvalue");
		outfile.WriteLine($"-----------------------------------------------");
		outfile.WriteLine($"Test of eigenvalue-by-eigenvalue routine:");
		outfile.WriteLine($"A random symmetric matrix A of dimension {dim}x{dim} is generated. Rotation angle should be changed into 0.5*arctan2(-2*Apq, App-Aqq) to achieve largest eigenvalue.\n");
		outfile.WriteLine($"{eigenvalues} lowest eigenvalues of A:");
		vector lowest_eigenvalues = Ac_res.get_eigenvalues();
		for(int ir=0;ir<lowest_eigenvalues.size;ir++){outfile.Write("{0,10:g3} ", lowest_eigenvalues[ir]);}
		outfile.WriteLine("\n");
		outfile.WriteLine($"{eigenvalues} highest eigenvalues of A:");
		vector highest_eigenvalues = Acc_res.get_eigenvalues();
		for(int ir=0;ir<highest_eigenvalues.size;ir++){outfile.Write("{0,10:g3} ", highest_eigenvalues[ir]);}
		outfile.WriteLine("\n");
		outfile.WriteLine($"Eigenvalues of A for comparison:");
		vector all_eigenvalues = A_res.get_eigenvalues();
		for(int ir=0;ir<all_eigenvalues.size;ir++){outfile.Write("{0,10:g3} ", all_eigenvalues[ir]);}
		outfile.WriteLine("\n");

		outfile.WriteLine($"Comparison of the two diagonalization routines in terms of number of rotations:");
		dim = 50;
		A = misc.gen_matrix(dim);
		Ac = A.copy();
		Acc = A.copy();
		A_res = new jacobi_diagonalization(A,"cyclic");
		Ac_res = new jacobi_diagonalization(Ac,"value","min",1);
		Acc_res = new jacobi_diagonalization(Acc,"value","min",dim);
		outfile.WriteLine($"Another random symmetric matrix A of dimension {dim}x{dim} is generated.\n");
		outfile.WriteLine($"Cyclic method:");
		outfile.WriteLine($"Lowest eigenvalue:                            {A_res.get_eigenvalues()[0]}");
		outfile.WriteLine($"Amount of rotations (full diagonalization):   {A_res.get_rotations()}\n");
		outfile.WriteLine($"Eigenvalue-by-eigenvalue method:");
		outfile.WriteLine($"Lowest eigenvalue:                            {Ac_res.get_eigenvalues()[0]}");
		outfile.WriteLine($"Amount of rotations (lowest eigenvalue):      {Ac_res.get_rotations()}");
		outfile.WriteLine($"Amount of rotations (full diagonalization):   {Acc_res.get_rotations()}\n");
		outfile.WriteLine($"Thus, the eigenvalue-by-eigenvalue method is suitable for calculuating only the lowest eigenvalues of a matrix whereas the cyclic sweep method is faster for full diagonalization.\n");
		outfile.Close();
	return 0;
	}

}





