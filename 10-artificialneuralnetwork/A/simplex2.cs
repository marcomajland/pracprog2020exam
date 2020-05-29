using System;
using static System.Console;
public partial class simplex{

public static double size(vector[] p){
	double s=0;
	for(int i=1;i<p.Length;i++){ s=Math.Max(s,(p[0]-p[i]).norm()); }
	return s;
	}

public static int downhill
(System.Func<vector,double>F,ref vector x,
double step=1.0/4,double dx=1e-3,int maxsteps=999){

vector[] p=new vector[x.size+1];
double[] f=new double[x.size+1];
p[x.size]=x.copy();
f[x.size]=F(p[x.size]);
for(int i=0;i<x.size;i++){
	x[i]+=step;
	p[i]=x.copy();
	f[i]=F(p[i]);
	x[i]-=step;
	}
int hi=0,lo=0,nsteps=0;

while(size(p)>dx && ++nsteps<maxsteps){
	hi=0;lo=0;
	double fhi=f[hi],flo=f[lo];
	for(int k=1;k<p.Length;k++){
		double fnext=f[k];
		if(fnext>fhi){fhi=fnext;hi=k;}
		if(fnext<flo){flo=fnext;lo=k;}
		}
	vector pce=new vector(p[0].size);
	for(int i=0;i<p.Length;i++) if(i!=hi)pce+=p[i];
	pce/=pce.size;

	vector pre=2*pce-p[hi];
	double fre=F(pre);
	if(fre<flo){ /* good */
		vector pex=3*pce-2*p[hi];
		double fex=F(pex);
		if(fex<fre){ /* very good */
			Error.Write("expansion\n");
			p[hi]=pex;
			f[hi]=fex;
			continue;
		}
		else{ /* just goog */
			Error.Write("reflection\n");
			p[hi]=pre;
			f[hi]=fre;
			continue;
		}
	}
	else if(fre<fhi){
		Error.Write("reflection\n");
		p[hi]=pre;
		f[hi]=fre;
		continue;
	}
	else{
		vector pco=(pce+p[hi])/2;
		double fco=F(pco);
		if(fco<fhi){
			Error.Write("contraction\n");
			p[hi]=pco;
			f[hi]=fco;
			continue;
		}
		Error.Write("reduction\n");
		for(int i=0;i<p.Length;i++)
		if(i!=lo){
			p[i]=(p[i]+p[lo])/2;
			f[i]=F(p[i]);
		}
	}
}//while
Error.WriteLine($"nsteps={nsteps}");
x=p[lo];
return nsteps;
}//simplex
}//class amoeba
