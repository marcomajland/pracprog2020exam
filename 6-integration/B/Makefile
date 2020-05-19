MATLIB = ../../matlib
all : main_B.exe 
	mono $<
#	gnuplot accuracy.gpi
#	gnuplot counts.gpi
main_B.exe : main_B.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/quado8.cs $(MATLIB)/integrator.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
