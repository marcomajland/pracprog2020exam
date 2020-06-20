MATLIB = ../../matlib
all : main_B.exe sirplot.gpi
	mono $<
	gnuplot sirplot.gpi
main_B.exe : main_B.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/ode_solver.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
