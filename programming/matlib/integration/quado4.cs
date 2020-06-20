// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System; using static System.Math; using static System.Double;
public static partial class quad{

public static double o4av
(Func<double,double> f, double a, double b, double acc=1e-3, double eps=1e-3){
if(IsNegativeInfinity(a)) return o4av(t=>f(a-(1-t)/t)/t/t,0,1,acc,eps);
if(IsPositiveInfinity(b)) return o4av(t=>f(a+(1-t)/t)/t/t,0,1,acc,eps);
return o4a(t=>f(a+(b-a)*(3*t*t-2*t*t*t))*(b-a)*6*(t-t*t),0,1,acc,eps);
}//o4av

public static double o4a
(Func<double,double> f,double a,double b,double acc=1e-3,double eps=1e-3,
double f2=NaN,double f3=NaN,int nrec=0,int limit=99){
/// four point open adaptive integrator
double h=b-a, sqr2=Sqrt(2), f1=f(a+h/6), f4=f(a+5*h/6);
if(IsNaN(f2)){ f2=f(a+2*h/6); f3=f(a+4*h/6); nrec=0; }
double approx1=(3*f1+4*f2      +5*f4)*h/12;
double approx2=(5*f1      +4*f3+3*f4)*h/12;
double integral=(approx1+approx2)/2;
double error=Abs(approx1-approx2)/2;
double tolerance=acc+eps*Abs(integral);
if(error<sqr2*tolerance) return integral;
else if(++nrec>limit){
	Console.Error.Write("o4a: nrec>limit\n");
	return integral;
	}
else return 	o4a(f,a,(a+b)/2,acc/sqr2,eps,f1,f2,nrec,limit)+
		o4a(f,(a+b)/2,b,acc/sqr2,eps,f3,f4,nrec,limit);
}//o4a

public static double o4acc
(Func<double,double> f, double a, double b, double acc=1e-3, double eps=1e-3){
if(IsNegativeInfinity(a)) return o4acc(t=>f(a-(1-t)/t)/t/t,0,1,acc,eps);
if(IsPositiveInfinity(b)) return o4acc(t=>f(a+(1-t)/t)/t/t,0,1,acc,eps);
return o4a(t=>f((a+b)/2+(b-a)/2*Cos(t))*Sin(t)*(b-a)/2,0,PI,acc,eps);
}//o4acc

}//quad
