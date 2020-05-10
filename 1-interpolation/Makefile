MATLIB = ../matlib
all : interp.exe lspline.gpi qspline.gpi cspline.gpi lspline_int.gpi
	mono $<
	gnuplot lspline.gpi
	gnuplot qspline.gpi
	gnuplot cspline.gpi
	gnuplot lspline_int.gpi
interp.exe : interp.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/quado8.cs $(MATLIB)/misc.cs qspline.cs linterp.cs cspline.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.svg *.dll
