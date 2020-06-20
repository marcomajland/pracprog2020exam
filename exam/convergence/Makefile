MATLIB = ../../matlib
all : main.exe plot.gpi
	mono $<
	gnuplot plot.gpi
main.exe : main.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs $(MATLIB)/misc.cs $(MATLIB)/jacobi.cs ../power_method.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
