MATLIB = ../matlib
all : interp.exe lspline.gpi qspline.gpi cspline.gpi
	mono $<
	gnuplot lspline.gpi
	gnuplot qspline.gpi
	gnuplot cspline.gpi
interp.exe : interp.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/quado8.cs spline.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.svg *.dll
