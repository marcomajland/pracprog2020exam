all : lineq.exe
	mono $<
lineq.exe : lineq.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : matrix.cs vector.cs qr.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll
