using System;
using static System.Console;
using static System.Math;
using System.Collections.Generic;
public partial class misc{
	public static List<double[]> load_data(string filename){
		string[] lines = System.IO.File.ReadAllLines(filename);
		int n = (lines[0].Split(' ')).Length;
		List<double[]> data = new List<double[]>(n);
		string[] subline;
		for(int i=0;i<n;i++){data.Add(new double[lines.Length]);}
	        for(int i=0;i<lines.Length;i++){
			subline = lines[i].Split(' ');
			for(int j=0;j<n;j++){data[j][i] = double.Parse(subline[j]);}
		}
		return data;
	}
	public static int binary_search(double[] x, double z){
		int i = 0;
		int len = x.Length-1;
		while(i<len-1){
			int mid = (i+len)/2;
			if(x[mid] < z){
				i = mid;
			}
			else{
				len = mid;
			}
		}
		return i;	
	}
	public static void generate_data(Func<double,double> f, double xmin, double xmax, double dx, string destination){
		var data = new System.IO.StreamWriter(destination,append:false);
		for(double _x=xmin;_x<=xmax;_x+=dx){
			data.WriteLine($"{_x} {f(_x)}");
		}
		data.Close();
	}

// Following makes random matrix with double entries
	public static matrix gen_matrix(int n, double max = 10){
		Random rnd = new Random();
		matrix A = new matrix(n,n);
		for(int i=0;i<n;i++){for(int j=0;j<n;j++){A[i][j] = rnd.NextDouble()*max; A[j][i]=A[i][j];}}
		return A;
	}
	public static matrix random_matrix(int n, int m, double max = 10){
		Random rnd = new Random();
		matrix A = new matrix(n,m);
		for(int i=0;i<m;i++){for(int j=0;j<n;j++){A[i][j] = rnd.NextDouble()*max;}}
		return A;
	}
	public static vector gen_vector(int n, double max = 10){
		Random rnd = new Random();
		vector v = new vector(n);
		for(int i=0;i<n;i++){v[i] = rnd.NextDouble()*max;}
		return v;
	}
// Following makes random matrix with integer entries
/*	public static matrix gen_matrix(int n){
		Random rnd = new Random();
		int minint = 0;
		int maxint = 10;
		matrix A = new matrix(n,n);
		for(int i=0;i<n;i++){for(int j=0;j<n;j++){A[i][j] = rnd.Next(minint,maxint);A[j][i]=A[i][j];}}
		return A;
	}
*/

}


