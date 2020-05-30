MATLIB = ../../matlib
all : main_A.exe cosplot.gpi
	mono $<
	gnuplot cosplot.gpi
main_A.exe : main_A.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/ode_solver.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
