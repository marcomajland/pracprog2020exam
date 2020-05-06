using System;
using static System.Console;
using static System.Math;
public partial class qnewton{
	public static vector minimize(Func<vector,double> f, vector x_initial, double eps=1e-3, double alpha = 1e-4){
		vector deltax; double lambda;
		vector s; vector x;
		matrix B = new matrix(x_initial.size,x_initial.size); B.set_identity();
		x = x_initial;
		while(gradient(f,x).norm() > eps){
			deltax = -B*gradient(f,x);
			lambda = 1; s = lambda*deltax;
			while(f(x + s) < f(x) + alpha*s.dot(gradient(f,x))){
				if(lambda<0.1){s = lambda*deltax;}
				lambda *= 0.5; s = lambda*deltax;
			}
			x += s;
		}
		return null;
	}
	public static vector gradient(Func<vector,double> f, vector x, double eps=1e-3){
		double fx = f(x); double dx;
		vector grad = new vector(x.size);
		for(int i=0;i<grad.size;i++){
			dx = Abs(x[i])*eps;
			if(Abs(x[i]) < Sqrt(eps)){dx=eps;}
			x[i] += dx;
			grad[i] = (f(x) - fx)/dx;
			x[i] -= dx;
		}
		return grad;
	}
}
