using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main_B{
	public static int Main(){
		Func<vector, double> f = delegate(vector x){return 1/Sqrt(x[0]);};		
		double result=0; double error=0;
		vector a = new vector(0.0); vector b = new vector(1.0); double analytical = 2.0;

		int N_min = 100; int N_max = 40000; int dN = 10;

		monte_carlo.plain(f,a,b,N_max,ref result,ref error);
		double factor = error*Sqrt(N_max);

		var errors = new System.IO.StreamWriter($"./plot_files/errors.txt",append:false);
		for(int N=N_min;N<=N_max;N+=dN){
			monte_carlo.plain(f,a,b,N,ref result,ref error);
			errors.WriteLine($"{N} {error} {factor/Sqrt(N)} {analytical-result}");
		}
		errors.Close();
		return 0;
	}
}

