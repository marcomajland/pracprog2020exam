public static class matrixHelp {
	public static matrix makeRandSymMatrix(int n) {
		var rand = new System.Random();
		matrix A = new matrix(n, n);
		
		for(int i = 0; i < n; i++) {
			A[i, i] = rand.NextDouble();
		}
		for(int i = 0; i < n; i++) {
			for(int j = i+1; j < n; j++) {
				double Aij = rand.NextDouble();
				A[i,j] = Aij;
				A[j,i] = Aij;
			}
		}
		return A;
	} 
}
