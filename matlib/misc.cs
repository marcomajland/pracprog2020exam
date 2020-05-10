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
}


