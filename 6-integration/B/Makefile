MATLIB = ../matlib
all : integration.exe accuracy.gpi counts.gpi
	mono $<
	gnuplot accuracy.gpi
	gnuplot counts.gpi
integration.exe : integration.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/quado8.cs integrator.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
