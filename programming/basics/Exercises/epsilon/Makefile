all : out.txt
	cat out.txt
out.txt : main.exe
	mono main.exe > out.txt
main.exe : main.cs libs.dll
	mcs -reference:libs.dll main.cs
libs.dll : cmath.cs complex.cs machine_epsilon.cs harmonic_sum.cs
	mcs -target:library -out:$@ $^

