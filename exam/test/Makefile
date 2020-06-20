MATLIB = ../../matlib
all : main.exe
	mono $<
main.exe : main.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs $(MATLIB)/misc.cs ../power_method.cs $(MATLIB)/jacobi.cs
	mcs $^ -t:library -out:$@
plot : plot.gpi
	gnuplot plot.gpi
	cd outfiles; inkview Deviations.svg
clean :
	rm --force *.exe *.dll *.svg *.txt
