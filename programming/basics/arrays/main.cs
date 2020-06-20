using System;
using static System.Console;

class main{
	static int Main(){

	double[] v = new double[10];
	
	for(int i=0;i<10;i++){
	v[i] = i;
	Write($"{v[i]}\n");
	}
	return 0;
	}
}
