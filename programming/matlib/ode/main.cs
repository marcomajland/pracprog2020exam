using System;
using static System.Console;
using static System.Math;
using System.Collections;
using System.Collections.Generic;

class main{
static bool approx(double a,double b, double acc=1e-6, double eps=1e-6){
	if( Abs(a-b)<acc )return true;
	if( Abs(a-b)<eps*Max(Abs(a),Abs(b)) ) return true;
	return false;
}

static Func<double,vector,vector>
F=delegate(double x, vector y){
	return new vector(y[1],-y[0]);
		};

static void Main(){
	double a=0;
	vector ya=new vector(0,1);
	double b=10;
	double h=0.1,acc=1e-3,eps=1e-3;
	var xs=new List<double>();
	var ys=new List<vector>();

vector y=ode.rk23(F,a,ya,b,acc,eps,h,xlist:xs,ylist:ys);

	Error.WriteLine($"y0    ={y[0]}    y1\t={y[1]}");
	Error.WriteLine($"sin(b)={Sin(b)} cos(b)\t={Cos(b)}");
if(approx(y[0],Sin(b),acc,eps) && approx(y[1],Cos(b),acc,eps))
	Error.WriteLine("test passed");
else
	Error.WriteLine("test failed");
Error.WriteLine($"npoints={xs.Count}");

	for(int i=0;i<xs.Count;i++)
		WriteLine($"{xs[i]} {ys[i][0]} {ys[i][1]}");
}
}
