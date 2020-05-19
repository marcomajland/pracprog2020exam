MATLIB = ../../matlib
all : ode.exe cosplot.gpi sirplot.gpi
	mono $<
	gnuplot cosplot.gpi
	gnuplot sirplot.gpi
ode.exe : ode.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs ode_solver.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
