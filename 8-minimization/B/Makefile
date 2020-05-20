MATLIB = ../../matlib
all : main_B.exe higgs_plot.gpi
	mono $<
	gnuplot higgs_plot.gpi
main_B.exe : main_B.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs $(MATLIB)/ode_solver.cs $(MATLIB)/qnewton.cs $(MATLIB)/misc.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
