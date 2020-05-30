using System;
using static System.Console;
using static System.Math;
using static System.Double;
public class ann{
	public int n;
	public Func<double,double> f;
	public vector pars;
	public ann(Func<double,double> g, int m){
		n = m; f = g; pars = new vector(3*n);
	}
	public double feedforward(double x){
		double output = 0; double a = 0;
		double b = 0; double w = 0;
		for(int i=0;i<n;i++){
			a = pars[3*i+0];
			b = pars[3*i+1];
			w = pars[3*i+2];
			output += w*f((x-a)/b);
		}
		return output;
	}
	public void train(vector x, vector y){
		int calls = 0;
		Func<vector,double> delta = (ps) =>{
			calls++;
			double feed=0;
			double sum=0;
			pars = ps;
			for(int i=0;i<x.size;i++){
				feed = feedforward(x[i]);
				sum += (feed - y[i])*(feed - y[i]);
			}
			return sum/x.size;
		};
		vector pars_initial = new vector(pars.size);
		double x_min = x[0];
		double x_max = x[x.size-1];
		for(int i=0;i<n;i++){
			pars_initial[3*i+0] = x_min + (x_max - x_min)*i/(n - 1);
			pars_initial[3*i+1] = (x_max - x_min)/(n - 1);
			pars_initial[3*i+2] = 1;
		}
//		int min_steps = qnewton.minimize(delta, ref pars_initial, 1e-2);
		int min_steps = simplex.downhill(delta, ref pars_initial, 0.2, 1e-2, 3000);
	}
	public double derivative(double x){ // WORKS ONLY FOR GAUSSIAN WAVELETS
		double output = 0; double a = 0;
		double b = 0; double w = 0;
		for(int i=0;i<n;i++){
			a = pars[3*i+0];
			b = pars[3*i+1];
			w = pars[3*i+2];
			output += w*gaussian_derivative((x-a)/b)/b;
		}
		return output;
	}
	public Func<double,double> gaussian_derivative = delegate(double x){
		return -2*x*Exp(-x*x);
	};
}



