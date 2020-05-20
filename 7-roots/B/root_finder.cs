using System;
public partial class root_finder{
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
}
