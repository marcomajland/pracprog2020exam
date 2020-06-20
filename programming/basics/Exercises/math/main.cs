using System;
using static System.Math;
using static System.Console;

class math{
	static void Main(){

	var I = new complex(0,1);

	double sqrt2Math = Sqrt(2);
	double sqrt2cmath = cmath.sqrt(2);
	var expicmath = cmath.exp(I);
	var expipicmath = cmath.exp(I*PI);
	var iicmath = cmath.pow(I,I);
	var sinipi = cmath.sin(I*PI);

	Write($"sqrt(2) = {sqrt2Math}\n");
	Write($"sqrt(2) = {sqrt2cmath}\n");

	Write($"exp(i) = {expicmath}\n");
	Write($"exp(ipi) = {expipicmath}\n");
	Write($"i^i = {iicmath}\n");
	Write($"sin(ipi) = {sinipi}\n");

	Write("Checking if two numbers are equal:\n");
	check(sqrt2Math,sqrt2cmath);
	}

	public static void check(double x, double y){
	if (x == y){
		Write("The two numbers are equal\n");
	}
	else{
		Write("The two numbers are not equal\n");
	}
	}
}
