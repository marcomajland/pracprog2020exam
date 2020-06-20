// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System;
using static System.Math;
public static partial class math{

public static double adaptocc
(Func<double,double> f, double a, double b, double acc, double eps){
        Func<double,double> F = t => f((a+b)/2+(b-a)/2*Cos(t))*Sin(t)*(b-a)/2;
        return adapto(F,0,PI,acc,eps);
}//adapto4cc

public static double adapto
(Func<double,double> f,double a,double b,double acc,double eps,
double f2=double.NaN,double f3=double.NaN){
/// four point open adaptive integrator
	double h=b-a, sqr2=Sqrt(2), f1=f(a+h/6), f4=f(a+5*h/6);
	if(double.IsNaN(f2)){f2=f(a+2*h/6);f3=f(a+4*h/6);}
const double
w1=1.031812646037287,w2=-0.7737371672967065,w3=0,w4=0.7419245212594186,
u1=0.7419245212594186,u2=0,u3=-0.7737371672967065,u4=1.031812646037287;

	double approx1=(w1*f1+w2*f2+w3*f3+w4*f4)*h;
	double approx2=(u1*f1+u2*f2+u3*f3+u4*f4)*h;
	double integral=(approx1+approx2)/2;
	double error=Abs(approx1-approx2)/2;

/*
	double integral=(2*f1+f2+f3+2*f4)*h/6;
	double approx  =(f1  +f2+f3+  f4)*h/4;
	double error=Abs(integral-approx)/2;
*/

	double tolerance=acc+eps*Abs(integral);
	if(error<sqr2*tolerance)
		return integral;
	else
		return 	adapto(f,a,(a+b)/2,acc/sqr2,eps,f1,f2)+
			adapto(f,(a+b)/2,b,acc/sqr2,eps,f3,f4);
}//adapto4

}//math
