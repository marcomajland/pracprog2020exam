all : linint.exe output.gpi
	mono $<
	gnuplot output.gpi
quado8.dll : quado8.cs
	mcs $< -t:library
linint.exe : linint.cs quado8.dll
	mcs $< -r:quado8.dll
clean :
	rm --force *.exe *.svg *.dll
