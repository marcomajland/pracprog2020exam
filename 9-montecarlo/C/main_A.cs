using System;
using static System.Console;
using System.Collections.Generic;
using static System.Math;
class main_A{
	public static int Main(){
		Func<vector, double> f1 = delegate(vector x){return Sqrt(x[0]);};		
		Func<vector, double> f2 = delegate(vector x){return 4.0*Sqrt(1-x[0]*x[0]);};
		Func<vector, double> f3 = delegate(vector x){return 1/(PI*PI*PI)*1/(1-Cos(x[0])*Cos(x[1])*Cos(x[2]));};
		vector a; vector b; double N; double analytical;

		var outfile = new System.IO.StreamWriter($"Outfile.txt",append:false);
		outfile.WriteLine($"-----------------------------");
		outfile.WriteLine($"Plain Monte Carlo integration");
		outfile.WriteLine($"-----------------------------");
		outfile.WriteLine($"The plain Monte Carlo integration routine is tested below using the same functions as in the adaptive integration problems.\n");
		a = new vector(0.0); b = new vector(1.0); N = 1e+5; analytical = 2.0/3;
		outfile.WriteLine($"Definite integral of sqrt(x) from {a[0]} to {b[0]}:");
		calculate_integrals(f1,a,b,N,analytical,ref outfile);
		a = new vector(0.0); b = new vector(1.0); N = 1e+5; analytical = PI;
		outfile.WriteLine($"Definite integral of 4*sqrt(1-x*x) from {a[0]} to {b[0]}:");
		calculate_integrals(f2,a,b,N,analytical,ref outfile);
		a = new vector(0.0,0.0,0.0); b = new vector(PI,PI,PI); N = 1e+5; analytical = 1.3932039296856768591842462603255;
		outfile.WriteLine($"Definite integral of 1/(pi^3)*(1-cos(x)*cos(y)*cos(z))^(-1) from {a[0]} to {b[0]} (for x, y and z):");
		calculate_integrals(f3,a,b,N,analytical,ref outfile);



		outfile.Close();
		return 0;
	}

	public static void calculate_integrals(Func<vector,double> f, vector a, vector b, double N, double analytical, ref System.IO.StreamWriter outfile){
		double result=0; double error=0;
		monte_carlo.plain(f,a,b,N,ref result,ref error);

		outfile.WriteLine($"Plain Monte Carlo:               {result}");
		outfile.WriteLine($"Monte Carlo error estimate:      {error}");
		outfile.WriteLine($"Analytical result:               {analytical}");
		outfile.WriteLine($"Error (analytical-numerical):    {analytical-result}");
		outfile.WriteLine($"Amount of samples:               {N}\n");
	}



}
