MATLIB = ../../matlib
all : main_B.exe errors_plot.gpi
	mono $<
	gnuplot errors_plot.gpi
	inkview Errors.svg
main_B.exe : main_B.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs $(MATLIB)/ode_solver.cs $(MATLIB)/qnewton.cs $(MATLIB)/monte_carlo.cs $(MATLIB)/integrator.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
	cd plot_files; rm --force *.exe *.dll *.svg *.txt
