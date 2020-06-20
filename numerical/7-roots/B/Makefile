MATLIB = ../../matlib
all : main_B.exe hydrogen.gpi
	mono $<
	gnuplot hydrogen.gpi
main_B.exe : main_B.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs $(MATLIB)/root_finder.cs $(MATLIB)/ode_solver.cs $(MATLIB)/ode_solver_Fedorov.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
