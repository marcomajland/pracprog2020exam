MATLIB = ../matlib
all : main.exe
	mono $<
#	gnuplot plot.gpi
#	inkview Plot.svg
main.exe : main.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs $(MATLIB)/misc.cs power_method.cs $(MATLIB)/jacobi.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
