using System;
using static System.Console;
public class main{
	public static int Main(){
	vector3d v = new vector3d(2,2,3);
	vector3d u = new vector3d(1,1,1);	
	vector3d w = vector3d.vp(v,u);
	w.print();
	double a = w.x; double b = w.y; double c = w.z;
	Write($"{a},{b},{c}\n");
	return 0;	
	}
}
