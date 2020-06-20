// (C) 2020 Dmitri Fedorov; License: GNU GPL v3+; no warranty.
#if HAVE_NAN
#undef NEED_NAN
#else
#define HAVE_NAN
#define NEED_NAN
#endif
using System;
using static System.Math;
public static partial class math{

#if NEED_NAN
public const double NaN=double.NaN;
#endif

public static bool approx(double x, double y, double acc=1e-6, double eps=1e-6){
	if(Abs(x-y)<acc)return true;
	if(Abs(x-y)<eps*(Abs(x)+Abs(y))/2)return true;
	return false;
}

public delegate double integrand (double x);

}//math
