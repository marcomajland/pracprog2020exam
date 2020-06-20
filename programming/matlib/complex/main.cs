using System;
using static System.Math;
using static System.Console;
using static cmath;
class main{
static int Main(){
	int return_code=0;
	bool test;
	var rnd=new Random();
	int n=9;
	complex[] zs = new complex[n];
	for(int i=0;i<n;i++)
		zs[i]=new complex(2*rnd.NextDouble()-1,2*rnd.NextDouble()-1);

	Write("testing exp(log(z))=z ...");
	test=true;
	for(int i=0;i<n;i++){
		complex z=zs[i];
		test=test && exp(log(z)).approx(z);
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }

	Write("testing log(exp(z))=z ...");
	test=true;
	for(int i=0;i<n;i++){
		complex z=zs[i];
		test=test && log(exp(z)).approx(z);
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }

	Write("testing abs(z)^2=z*conj(z) ...");
	test=true;
	for(int i=0;i<n;i++){
		complex z=zs[i];
		test=test && abs(z).pow(2).approx(z*~z);
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }

	Write("testing sqrt(a)*sqrt(a)=a ...");
	test=true;
	for(int i=0;i<n;i++){
		complex z=zs[i];
		test=test && z.approx(sqrt(z)*sqrt(z));
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }

	Write("testing sin(a)^2+cos(a)^2=1 ...");
	test=true;
	for(int i=0;i<n;i++){
		complex z=zs[i];
		test=test &&
			(sin(z).pow(2)+cos(z).pow(2)).approx(1);
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }

	Write("testing sin(a+b)=sin(a)*cos(b)+cos(a)*sin(b) ...");
	test=true;
	for(int i=0;i<n-1;i++){
		complex a=zs[i],b=zs[i+1];
		test=test &&
			sin(a+b).approx(sin(a)*cos(b)+cos(a)*sin(b));
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }

	Write("testing (a/b)*b=a ...");
	test=true;
	for(int i=0;i<n-1;i++){
		complex a=zs[i],b=zs[i+1];
		test=test &&
			((a/b)*b).approx(a);
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }

	Write("testing exp(a+b)=exp(a)*exp(b) ...");
	test=true;
	for(int i=0;i<n-1;i++){
		complex a=zs[i],b=zs[i+1];
		test=test &&
			(exp(a)*exp(b)).approx(exp(a+b));
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }

	Write("testing cos(a+b)=cos(a)*cos(b)-sin(a)*sin(b) ...");
	test=true;
	for(int i=0;i<n-1;i++){
		complex a=zs[i],b=zs[i+1];
		test=test &&
			cos(a+b).approx(cos(a)*cos(b)-sin(a)*sin(b));
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }


	Write("testing abs(z)*exp(I*arg(z))=z ...");
	test=true; complex I=complex.I;
	for(int i=0;i<n;i++){
		complex z=zs[i];
		test=test &&
			( abs(z)*exp(I*arg(z)) ).approx(z);
	}
	if(test) Write(" ...passed\n");
	else { Write(" ...FAILED\n"); return_code += 1; }

if(return_code==0)
	Write("all tests passed :)\n");
else 
	Write("{0} tests FAILED :(\n",return_code);
return return_code;

}//Main
}//main
