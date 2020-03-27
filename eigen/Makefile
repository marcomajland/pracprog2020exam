MATLIB = ../matlib
all : eigen.exe plot.gpi plot_exact.gpi
	mono $<
	gnuplot plot.gpi
	gnuplot plot_exact.gpi
eigen.exe : eigen.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/matrix.cs $(MATLIB)/vector.cs jacobi.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg
