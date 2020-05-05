using System;
using static System.Math;
using static System.Console;
using System.Collections;
using System.Collections.Generic;

public partial class ode{

public static vector rk23
(
	Func<double,vector,vector> F, /* equation */
	double a, vector ya, /* initial condition: {a,y(a)} */
	double b, 
	double acc=1e-3,
	double eps=1e-3,
	double h=0.1, 
	List<double> xlist=null,
	List<vector> ylist=null,
	int limit=999
){
return driver( F, a,ya,b,
	acc, eps, h,
	xlist, ylist,
	limit, rkstep23 );}

public static vector driver(
	Func<double,vector,vector> F, /* equation */
	double a, vector ya, double b, 
	double acc, double eps, double h, 
	List<double> xlist, List<vector> ylist,
	int limit,
	Func<
		Func<double,vector,vector>,
		double,vector,double,vector[]
	> stepper
	)

{// Generic ODE driver
int nsteps=0;
if(xlist!=null) {xlist.Clear(); xlist.Add(a);}
if(ylist!=null) {ylist.Clear(); ylist.Add(ya);}
do{
	if(a>=b) return ya;
	if(nsteps>limit) {
		Error.Write($"ode.driver: nsteps>{limit}\n");
		return ya;
		}
	if(a+h>b) h=b-a;
	vector[] trial=stepper(F,a,ya,h);
	vector   yh=trial[0];
	vector   er=trial[1];
	vector tol = new vector(er.size);
	for(int i=0; i<tol.size; i++){
		tol[i]=Max(acc,Abs(yh[i])*eps)*Sqrt(h/(b-a));
		if(er[i]==0)er[i]=tol[i]/4;
		}
	double factor=tol[0]/Abs(er[0]);
	for(int i=1; i<tol.size; i++)
		factor=Min(factor,Abs(tol[i]/er[i]));
	double hnew = h*Min( Pow(factor,0.25)*0.95, 2);
	int ok=1;
	for(int i=0;i<tol.size;i++)if(Abs(er[i])>tol[i])ok=0;
	if(ok==1){
		a+=h; ya=yh; h=hnew; nsteps++;
		if(xlist!=null) xlist.Add(a);
		if(ylist!=null) ylist.Add(ya);
		}
	else { h=hnew;
//	Error.WriteLine($"driver: bad step at {a}");
	}
	}while(true);
}// driver
public static vector[]
rkstep23(System.Func<double,vector,vector> F, double x, vector y, double h)
{// Embedded Runge-Kutta stepper of the order 2-3
	vector k0 = F(x,y);
	vector k1 = F(x+h/2  , y+(h/2  )*k0);
	vector k2 = F(x+3*h/4, y+(3*h/4)*k1);
	vector ka = (2*k0+3*k1+4*k2)/9;
	vector kb = k1;
	vector yh = y+ka*h;
	vector er = (ka-kb)*h;
	vector[] result={yh,er};
	return result;
}//rkstep23
}// class
