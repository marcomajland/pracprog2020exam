all : interp.exe lspline.gpi qspline.gpi
	mono $<
	gnuplot lspline.gpi
	gnuplot qspline.gpi
interp.exe : interp.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : lspline.cs quado8.cs qspline.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.svg *.dll *.txt
