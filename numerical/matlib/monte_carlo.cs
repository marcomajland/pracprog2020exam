using System;
using static System.Console;
using static System.Math;
using static System.Double;
public partial class monte_carlo{
	public static void plain(Func<vector, double> f, vector a, vector b, double N, ref double result, ref double error){
		var rnd = new Random();
		vector x = new vector(a.size);
		double sum = 0; double ssum = 0; double fx = 0;	double V = 1;
		double ave = 0; double sigmas = 0;
		for(int i=0;i<a.size;i++){V = V*(b[i]-a[i]);}
		for(int i=0;i<N;i++){
			generate_xs(ref x, ref rnd,a,b);
			fx = f(x);
			sum+=fx;
			ssum+=fx*fx;
		}
		ave = sum/N;
		sigmas = ssum/N - ave*ave;
		result = ave*V;
		error = Sqrt(sigmas/N)*V;
	}
	public static void generate_xs(ref vector x, ref Random rnd, vector a, vector b){
		for(int i=0;i<a.size;i++){x[i] = a[i] + rnd.NextDouble()*(b[i]-a[i]);}
	}	
}
