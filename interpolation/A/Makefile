all : linint.exe output.gpi
	mono $<
	gnuplot output.gpi
linint.exe : linint.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : lspline.cs quado8.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.svg *.dll
