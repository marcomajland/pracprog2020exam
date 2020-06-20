MATLIB = ../../matlib
PLOTFILES = ./plot_files/
all : main_B.exe plot.gpi
	mono $<
	gnuplot plot.gpi
main_B.exe : main_B.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/quado8.cs $(MATLIB)/integrator.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
	cd $(PLOTFILES); rm --force *.svg *.txt
