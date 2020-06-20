using System;
using static System.Console;
class main{
	public static int Main(string[] args){
		double min = 0;
		double max = 10;
		double dx = 0.05;
		
		if(args[0]=="erf"){
			for(double x=min+dx;x<=max;x+=dx){
				double y = functions.erf(x);
				WriteLine($"{x}, {y}");
			}
		}		
		if(args[0]=="gamma"){
			for(double x=min+dx;x<=max;x+=dx){
				double y = functions.gamma(x);
				WriteLine($"{x}, {y}");
			}
		}		
	return 0;
	}
}
