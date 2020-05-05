MATLIB = ../matlib
all : roots.exe plot.gpi hydrogen.gpi
	mono $<
	gnuplot plot.gpi
	gnuplot hydrogen.gpi
roots.exe : roots.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs root_finder.cs $(MATLIB)/ode_solver.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
