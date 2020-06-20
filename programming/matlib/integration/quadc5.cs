// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
using System; using static System.Math; using static System.Double;
public static partial class quad{

public static double c5a
(Func<double,double> f,double a,double b,double acc,double eps,
double f1=NaN,double f3=NaN,double f5=NaN,int nrec=0,int limit=100)
{ /// five point closed adaptive integrator
	double h=b-a, f2=f(a+h/4), f4=f(a+3*h/4), sqr2=Sqrt(2);
	if(IsNaN(f1)){f1=f(a);f3=f(a+2*h/4);f5=f(b);nrec=0;}
	double integral=(7*f1+32*f2+12*f3+32*f4+7*f5)*h/90;
	double approx  =(  f1+ 8*f2+       8*f4+  f5)*h/18;
	double error=Abs(integral-approx)/2;
	double tolerance=acc+eps*Abs(integral);
	if(error<sqr2*tolerance) return integral;
	else if(++nrec>limit){
		Console.Error.WriteLine($"c5a: nrec>{limit}");
		return integral;
		}
	else return 	c5a(f,a,(a+b)/2,acc/sqr2,eps,f1,f2,f3,nrec,limit)+
			c5a(f,(a+b)/2,b,acc/sqr2,eps,f3,f4,f5,nrec,limit);
}//quadc5

}//math
