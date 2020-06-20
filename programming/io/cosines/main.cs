using System;
using static System.Console;
using static System.Math;
class main{
	public static int Main(string[] args){
		if(args.Length != 2){
			Error.Write("Error: Wrong input (format: input file and output file)\n");
		}
		else{
			//var input = new System.IO.StreamReader("input.txt");			

			string[] input = System.IO.File.ReadAllLines(args[0]);
			var output = new System.IO.StreamWriter("output.txt",append:true);			
			double[] numbers = new double[input.Length];
			for (int i=0;i<numbers.Length;i++){
				numbers[i] = double.Parse(input[i]);
				double cosine = Cos(numbers[i]);
				output.Write($"Number {i}: {numbers[i]}\n");
				output.Write($"cos({numbers[i]}) = {cosine}\n");
			}
		output.Close();
		}
		return 0;
	}
}
