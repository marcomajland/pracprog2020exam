using static System.Console;
public static class plot{
	public static void Erf(double min, double max, double dx){
		for(double x=min+dx;x<=max;x+=dx){
			double y = x;
//			double y = functions.erf(x);
			WriteLine($"{x}, {y}");
		}		
	}	
	public static void Gamma(double min, double max, double dx){
		for(double x=min+dx;x<=max;x+=dx){
			double y = x;
//			double y = functions.gamma(x);
			WriteLine($"{x}, {y}");
		}		
	}
}
