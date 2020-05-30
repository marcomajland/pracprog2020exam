using System;
using static System.Console;
using static System.Math;
public partial class simplex{
	public static int downhill(Func<vector,double> f, ref vector x, double step=1.0/4, double tol=1e-3,int n_max=999){
		vector[] p = new vector[x.size+1];
		vector fs = new vector(x.size+1);
		p[x.size] = x.copy(); fs[x.size] = f(p[x.size]); // move
		for(int i=0;i<x.size;i++){
			x[i] = x[i] + step;
			p[i] = x.copy();
			fs[i] = f(p[i]);
			x[i] = x[i] - step;
		}
		int low = 0; int high = 0; double f_low = 0; double f_high = 0; int n = 0;
		while(size(p) > tol && n < n_max){
			n++;
			get_low_high(fs, ref low, ref high, ref f_low, ref f_high);

			vector centroid = new vector(p[0].size);
			for(int i=0;i<p.Length;i++){if(i != high){centroid += p[i];}}
			centroid /= centroid.size;

			vector reflection = 2*centroid - p[high];

			double f_ref = f(reflection);
			if(f_ref < fs[low]){
				vector expansion = 3*centroid - 2*p[high];
				double f_exp = f(expansion);
				if(f_exp < f_ref){
					p[high] = expansion;
					fs[high] = f_exp;
					continue;
					}
				else{p[high] = reflection; fs[high]= f_ref; continue;}
			}
			else if(f_ref < fs[high]){p[high] = reflection; fs[high] = f_ref; continue;}
			else{
				vector contraction = (centroid + p[high])/2;
				double f_con = f(contraction);
				if(f_con < fs[high]){p[high] = contraction; fs[high] = f_con; continue;}
				for(int i=0;i<p.Length;i++){
					if(i != low){
						p[i] = (p[i] + p[low])/2;
						fs[i] = f(p[i]);
					}
				}
			}
		}			
		x = p[low];
		return n;		
	}
	public static void get_low_high(vector fs, ref int low, ref int high, ref double f_low, ref double f_high){
		low = 0; high = 0; double fi = 0;
		f_low = fs[low]; f_high = fs[high];
		for(int i=1;i<fs.size;i++){
			fi = fs[i];
			if(fi > f_high){f_high = fi; high = i;}
			if(fi < f_low){f_low = fi; low = i;}
		}
	}
	public static double size(vector[] p){
		double s = 0;
		for(int i=1;i<p.Length;i++){s = Max(s, (p[0] - p[i]).norm());}
		return s;
	}
}

