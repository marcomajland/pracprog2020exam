all : lineq.exe
	mono $<
lineq.exe : lineq.cs matlib.dll
	mcs $< -r:matlib.dll
matlib.dll : matrix.cs vector.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll
