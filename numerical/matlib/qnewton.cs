using System;
using static System.Console;
using static System.Math;
public partial class qnewton{
	public static int minimize(Func<vector,double> f, ref vector x, double eps=1e-3, double alpha = 1e-4){
		double fx = f(x);
		vector grad = gradient(f,x);
		matrix B = new matrix(x.size,x.size); B.set_identity();
		vector s; double lambda; vector y; vector u; vector deltax;
		vector grads;
		int n=0;
		while(n<999 && grad.norm()>eps){
			n++;
			deltax = -B*grad; lambda=1;
			while(true){
				s = lambda*deltax;
				if(f(x+s) < fx + alpha*s.dot(grad)){
					break;
				}
				if(lambda<eps){
					B.set_identity(); break;
				}
				lambda *= 0.5;
			}
			grads = gradient(f,x+s);
			y = grads - grad;			
			u = s-B*y;
			double uTy = u%y;
			if(Abs(uTy)>1e-6){
				B.update(u,u,1/uTy); // SR1 update
			}
			x += s;
			grad = gradient(f,x);
			fx = f(x);
		}
		return n;
	}
	public static vector gradient(Func<vector,double> f, vector x, double eps=1e-8){
		double fx = f(x); double dx;
		vector grad = new vector(x.size);
		for(int i=0;i<grad.size;i++){
			dx = Abs(x[i])*eps;
			x[i] += dx;
			grad[i] = (f(x) - fx)/dx;
			x[i] -= dx;
		}
		return grad;
	}
}
