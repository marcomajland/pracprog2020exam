all : eigen.exe
	mono $<
#	gnuplot expplot.gpi
eigen.exe : eigen.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : matrix.cs vector.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg
