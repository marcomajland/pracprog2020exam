using System;
using static System.Console;

public class vector{
	public double[] data;
	public double this[int i]{
		get{return data[i];}
		set{data[i] = value;}
	}
	public int size{
	get{return data.Length;}
	}
	public vector(int n){data = new double[n];}
	public void print(string s = ""){
		Write(s);
		for(int i=0;i<size;i++)
			System.Console.Write("{0:f3} ",this[i]);
		Write("\n");
	}
	public static vector operator+(vector u, vector v){
		vector r = new vector(u.size);
		for(int i=0;i<u.size;i++){
		r[i] = u[i] + v[i];
		}
		return r;
	}
	public static vector operator*(double a, vector u){
		vector r = new vector(u.size);
		for(int i=0;i<u.size;i++){
		r[i] = a*u[i];
		}
		return r;
	}
	public static vector operator*(vector u, double a){
		return a*u;
	}
}

