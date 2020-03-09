all : linint.exe output.gpi
	mono $<
	gnuplot output.gpi
linint.exe : linint.cs
	mcs $<
clean :
	rm --force *.exe *.svg
