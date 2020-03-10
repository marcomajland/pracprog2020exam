all : interp.exe lspline.gpi qspline.gpi cspline.gpi
	mono $<
	gnuplot lspline.gpi
	gnuplot qspline.gpi
	gnuplot cspline.gpi
interp.exe : interp.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : lspline.cs quado8.cs qspline.cs cspline.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.svg *.dll *.txt
