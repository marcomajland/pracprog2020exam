MATLIB = ../../matlib
all : main_A.exe plot.gpi
	mono $<
	gnuplot plot.gpi
	inkview Plot.svg
main_A.exe : main_A.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs $(MATLIB)/ode_solver.cs $(MATLIB)/qnewton.cs $(MATLIB)/monte_carlo.cs $(MATLIB)/integrator.cs $(MATLIB)/ann.cs $(MATLIB)/misc.cs $(MATLIB)/simplex.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
	cd datafiles; rm --force *.exe *.dll *.svg *.txt
