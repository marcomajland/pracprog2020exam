using static System.Console;
class main{
static void Main(){
int n=3,m=3;
var A=new matrix(n,m);
var rnd=new System.Random();
for(int i=0;i<A.size1;i++)
	for(int j=0;j<A.size2;j++)
		A[i,j]=rnd.NextDouble();
A.print("random matrix A:");
var b=new vector(3);
for(int i=0;i<b.size;i++)
	b[i]=rnd.NextDouble();
b.print("random vector b:\n");
var qra=new qrdecomposition(A);
var x=qra.solve(b);
x.print("solution x to system A*x=b:\n");
var Ax=A*x;
Ax.print("check: A*x (should be equal b):\n");
if(vector.approx(Ax,b)) WriteLine("test passed");
else WriteLine("test failed");
}
}
