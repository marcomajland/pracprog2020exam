using System;
using static System.Math;
public partial class ode_solver{
	public static matrix rk12(Func<double,vector,vector> f, vector ya, double a, double b, 
		double acc=1e-3, double eps=1e-3, double h=0.1, int n=999){
		return driver(f, ya, a, b, h, acc, eps, n);
	}

	public static void rkstep12(
		Func<double,vector,vector> f, double t, vector yt,
		double h, vector yh, vector dy){
			vector k_0 = new vector(yt.size);
			vector k_12 = new vector(yt.size);
			vector k_s = new vector(yt.size);

			k_0 = f(t,yt);
			k_12 = f(t + 1/2*h, yt + 1/2*h*k_0);
			yh = yt + h*k_12;
			k_s = f(t, yt);
			dy = h*(k_12 - k_s);
	}

	public static matrix driver(Func<double, vector, vector> f, vector ya, double a, double b,
		double h, double acc, double eps, int n){	
			vector xs = new vector(n);
			matrix ys = new matrix(ya.size,n);
			vector yh = new vector(n);
			vector dy = new vector(n);
			double s; double normy=0; double tol; double x; vector y; double err=0;
			xs[0] = a;
			int i=0;
			while(xs[i]<b){
				x = xs[i]; y = ys[i];
				if(x + h > b){h = b-x;}
				rkstep12(f, x, y, h, yh, dy);
				s=0;
				for(int j=0;j<n;j++){s+=dy[j]*dy[j]; err = Sqrt(s);}
				s=0;
				for(int j=0;j<n;j++){s+=yh[j]*yh[j]; normy = Sqrt(s);}
				tol = (normy*eps+acc)*Sqrt(h/(b-a));
				if(err<tol){
					i++;
					if(i>n-1){i=-i;}
					xs[i] = x+h; for(int j=0;j<n;j++){ys[i][j] = yh[j];}
				}
				if(err>0){h*=Pow(tol/err,0.25)*0.95;}else{h*=2;}
			}	
			return ys;
	}
}




