all : lsq.exe
	mono $<
lsq.exe : lsq.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : qr.cs matrix.cs vector.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll
