public struct vector3d{
	// Struct constructor
	private double[] data;
	public double this[int i]{
		get{return data[i];}
		set{data[i]=value;}
	}	
	public double x{
		get{return data[0];}
		set{data[0] = value;}
	}
	public double y{
		get{return data[1];}
		set{data[1] = value;}
	}
	public double z{
		get{return data[2];}
		set{data[2] = value;}
	}
	public vector3d(double x, double y, double z){
		data = new double[3];
		data[0] = x; data[1] = y; data[2] = z;	
	}
	// Print method
	public void print(string s=""){
		System.Console.Write(s);
		for(int i=0;i<3;i++)
			System.Console.Write("{0:f3} ",this[i]);
			System.Console.Write("\n");
	}
	// Addition
	public static vector3d operator+(vector3d u, vector3d v){
		vector3d r = new vector3d(u[0] + v[0],
					  u[1] + v[1],
					  u[2] + v[2]);
		return r;
	}
	// Subtraction
	public static vector3d operator-(vector3d u, vector3d v){
		vector3d r = new vector3d(u[0] - v[0],
					  u[1] - v[1],
					  u[2] - v[2]);
		return r;	
	}
	// Scalar multiplication
	public static vector3d operator*(double a, vector3d v){
		vector3d r = new vector3d(a*v[0], a*v[1], a*v[2]);
		return r;
	}
	// Scalar multiplication
	public static vector3d operator*(vector3d v, double a){
		return a*v;
	}
	// Scalar product
	public static double sp(vector3d u, vector3d v){
		double a = u[0]*v[0] + u[1]*v[1] + u[2]*v[2];
		return a;
	}
	// Vector product
	public static vector3d vp(vector3d u, vector3d v){
		vector3d r = new vector3d(u[1]*v[2] - u[2]*v[1],
					  u[2]*v[0] - u[0]*v[2],
					  u[0]*v[1] - u[1]*v[0]);
		return r;
	}
}
