MATLIB = ../matlib
all : main.exe
	mono $<
#	gnuplot plot.gpi
#	inkview Plot.svg
main.exe : main.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs $(MATLIB)/ode_solver.cs $(MATLIB)/qnewton.cs $(MATLIB)/monte_carlo.cs $(MATLIB)/integrator.cs $(MATLIB)/ann.cs $(MATLIB)/misc.cs $(MATLIB)/simplex.cs power_method.cs $(MATLIB)/jacobi.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
