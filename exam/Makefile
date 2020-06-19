MATLIB = ../matlib
all : main.exe plot.gpi
	mono $<
	gnuplot plot.gpi
	inkview Deviations.svg
main.exe : main.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs $(MATLIB)/misc.cs power_method.cs $(MATLIB)/jacobi.cs
	mcs $^ -t:library -out:$@
Deviations.svg : plot.gpi
	gnuplot plot.gpi
	inkview Deviations.svg
clean :
	rm --force *.exe *.dll *.svg *.txt
