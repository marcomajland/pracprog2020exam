public struct complex{
	private double re,im;
	double Re{get{return re;}set{re=value;}}
	double Im{get{return im;}set{im=value;}}
	public complex(double x, double y){re = x;im = y;}
	public static complex operator+(complex a, complex b){
		double x = a.Re + b.Re;
		double y = a.Im + b.Im;
		return new complex(x,y);
	}
	public static complex operator-(complex a, complex b){
		double x = a.Re - b.Re;
		double y = a.Im - b.Im;
		return new complex(x,y);
	}
}
