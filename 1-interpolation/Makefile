MATLIB = ../matlib
LIBRARY = ./library
all : main.exe plot.gpi
	mono $<
	gnuplot plot.gpi
main.exe : main.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/quado8.cs $(MATLIB)/misc.cs $(LIBRARY)/qspline.cs $(LIBRARY)/lspline.cs $(LIBRARY)/cspline.cs $(MATLIB)/matrix.cs $(MATLIB)/vector.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.svg *.dll *.txt
	cd $(A); rm --force *.svg *.txt
	cd $(B); rm --force *.svg *.txt
	cd $(C); rm --force *.svg *.txt
