using System;
using static System.Console;
using System.Collections.Generic;
class minimization{
	public static int Main(){
		List<double[]> data = misc.load_data("./plot_files/data.txt");
		double[] E = data[0];
		double[] sigma = data[1];
		double[] error = data[2];

		Func<vector,double> D = delegate(vector x){
			double val = 0;
			for(int i=0;i<E.Length;i++){
				val += (F(E[i],x[0],x[1],x[2]) - sigma[i])*(F(E[i],x[0],x[1],x[2]) - sigma[i]);
			}
			return val;
		};
		vector xi_higgs = new vector(125.0,4.0,15.0);	
		int steps_higgs = qnewton.minimize(D, ref xi_higgs, 1e-6);
		WriteLine($"Higgs (steps: {steps_higgs}):");
		WriteLine($"m: {xi_higgs[0]}");
		WriteLine($"gamma: {xi_higgs[1]}");
		WriteLine($"A: {xi_higgs[2]}");
	
		var higgs_plot = new System.IO.StreamWriter($"higgs_plot.txt",append:false);
		double E_min = E[0];
		double E_max = E[E.Length-1];
		double dE = 0.01;
		for(double Es=E_min;Es<E_max;Es+=dE){
			higgs_plot.WriteLine($"{Es} {F(Es,xi_higgs[0],xi_higgs[1],xi_higgs[2])}");
		}
		higgs_plot.Close();
		return 0;
	}
	public static double F(double E, double m, double gamma, double A){
		return A/((E-m)*(E-m) + gamma*gamma/4);
	}


}
