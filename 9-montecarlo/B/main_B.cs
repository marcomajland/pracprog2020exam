using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main_A{
	public static int Main(){
//		Func<vector, double> f = delegate(vector x){return x[0]*x[0];};		
		Func<vector, double> f = delegate(vector x){return 1/Sqrt(x[0]);};		
//		Func<vector, double> f = delegate(vector x){return Sqrt(x[0]);};		
//		Func<vector, double> f = delegate(vector x){return 1/(PI*PI*PI)*1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]));};
		double result=0; double error=0;
//		vector a = new vector(0.0); vector b = new vector(1.0);
//		vector a = new vector(0.0,0.0,0.0); vector b = new vector(PI,PI,PI);
		vector a = new vector(0.0); vector b = new vector(1.0);

		int N_min = 100;
		int N_max = 40000;
		int dN = 10;

		int[] Ns = new int[N_max/dN];
		double[] errors = new double[N_max/dN];

		int i=0;
		for(int N=N_min;N<N_max;N+=dN){
			Ns[i] = N;
			monte_carlo.plain(f,a,b,Ns[i],ref result,ref error);
			errors[i] = error;	
			i++;		
		}
		Func<double, double, double, double> expected_error = delegate(double me, double mN, double x){
			return me*Sqrt(mN)/Sqrt(x);};		
		double errors_max = errors[errors.Length-1];
		N_max = Ns[Ns.Length-1];
		var plot_errors = new System.IO.StreamWriter($"./plot_files/errors.txt",append:false);
		for(i=0;i<Ns.Length;i++){
			plot_errors.WriteLine($"{Ns[i]} {errors[i]} {expected_error(errors_max,N_max,Ns[i])}");
		}
		plot_errors.Close();


		return 0;
	}


}


//		double[] Ns = new double[] {100, 200, 300, 400, 500, 1000, 1500, 2000, 2500, 3000, 3500, 4000, 5000, 7500, 10000, 12500, 15000, 17500, 20000, 22500, 25000, 27500, 30000, 35000, 40000};
//		double[] errors = new double[Ns.Length];

/*
		for(int i=0;i<Ns.Length;i++){
			monte_carlo.plain(f1,a,b,Ns[i],ref result,ref error);
			errors[i] = error;
		}
		Func<double, double, double, double> expected_error = delegate(double me, double mN, double x){
			return me*Sqrt(mN)/Sqrt(x);};		
		double max_error = errors[errors.Length-1];
		double max_N = Ns[Ns.Length-1];
		var plot_errors = new System.IO.StreamWriter($"./plot_files/errors.txt",append:false);
		for(int i=0;i<Ns.Length;i++){
			plot_errors.WriteLine($"{Ns[i]} {errors[i]} {expected_error(max_error,max_N,Ns[i])} {errors[i]-expected_error(max_error,max_N,Ns[i])}");
		}
		plot_errors.Close();
		var outfile = new System.IO.StreamWriter($"Outfile.txt",append:false);
		outfile.Close();
*/


