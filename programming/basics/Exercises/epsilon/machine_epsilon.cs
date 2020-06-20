public static class MachineEpsilon{
	public static int Max(){
		int i = 0;
		while (i+1>i){
		i++;
		}
		return i;
	}
	public static int Min(){
		int i = 0;
		while(i-1<i){
		i--;
		}
		return i;
	}
	public static float MachineEpsilonFloat(){
		float eps = 1;
		while(1-eps!=1){
		eps /= 2;
		}
		return eps;
	}
	public static double MachineEpsilonDouble(){
		double eps = 1;
		while(1-eps!=1){
		eps /= 2;
		}
		return eps;
	}
}
