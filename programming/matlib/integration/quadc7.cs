// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System; using static System.Double; using static System.Math;
public static partial class quad{

public static double c7a
(Func<double,double> f,double a,double b,double acc,double eps,
double f1=NaN,double f3=NaN,double f5=NaN,double f7=NaN,int nrec=0,int limit=100)
{ /// seven point closed adaptive integrator
double h=b-a,f2=f(a+h/6),f4=f(a+3*h/6),f6=f(a+5*h/6),sqr2=Sqrt(2);
if(IsNaN(f1)) {f1=f(a);f3=f(a+2*h/6);f5=f(a+4*h/6);f7=f(b);nrec=0;}
double integral=
(41*f1/840+9*f2/35+9*f3/280+34*f4/105+9*f5/280+9*f6/35+41*f7/840)*h;
double approx  =
(13*f1/200+4*f2/25+11*f3/40+          11*f5/40+4*f6/25+13*f7/200)*h;
double error=Abs(integral-approx)/2;
double tolerance=acc+eps*Abs(integral);
if(error<sqr2*tolerance) return integral;
else if(++nrec>limit){
	Console.Error.WriteLine($"c7a: nrec>{limit}, a={a} b={b}");
	return integral;
	}
else return 	c7a(f,a,(a+b)/2,acc/sqr2,eps,f1,f2,f3,f4,nrec,limit)+
		c7a(f,(a+b)/2,b,acc/sqr2,eps,f4,f5,f6,f7,nrec,limit);
}//quadc7

}//quad
