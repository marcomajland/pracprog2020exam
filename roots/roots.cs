using System;
using static System.Console;
class roots{
	public static int Main(){
		double eps = 1e-3;
		double dx = 1e-7;
//		vector x1 = new vector(1.0,-1.0);
		vector x2 = new vector(-0.5,3.0);
//		vector res1 = newton(f1,x1,eps,dx);
		vector res2 = newton(f2,x2,eps,dx);
		WriteLine($"{res2[0]}");
		WriteLine($"{res2[1]}");
		return 0;
	}
	public static vector newton(Func<vector,vector> f, vector x, double eps=1e-3, double dx=1e-7){
		while(f(x).norm() > eps){
			matrix J = jacobian(f,x,dx);
			qr Jqr = new qr(J);
			vector deltax = Jqr.solve(-1*f(x));
			double a = 1;
			while((f(x) + a*deltax).norm() < (1-a/2)*f(x).norm() && a>1/64){a = a/2;}
			x += a*deltax;
		}
		return x;
	}
	public static matrix jacobian(Func<vector,vector> f, vector x, double dx=1e-10){
		vector fs = f(x);
		vector xj = new vector(x.size); for(int i=0;i<x.size;i++){xj[i] = x[i];}
		matrix J = new matrix(fs.size,x.size);
		for(int i=0;i<x.size;i++){
			for(int j=0;j<x.size;j++){
				xj[j] = xj[j] + dx;
				J[j][i] = (f(xj)[i] - fs[i])/dx;
				xj = x.copy();
			}
		}
		return J;
	}
	public static Func<vector,vector> f1 = delegate(vector x){
		return new vector(x[0]*x[0] + 4*x[1]*x[1] - 9, 18*x[1] - 14*x[0]*x[0] + 45);};	
	public static Func<vector,vector> f2 = delegate(vector x){
		return new vector(2*(x[0]-1) + 400*x[0]*(x[0]*x[0] - x[1]), 200*(x[1]-x[0]*x[0]));};	


}
