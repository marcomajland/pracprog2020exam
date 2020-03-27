MATLIB = ../matlib
all : eigen.exe
	mono $<
eigen.exe : eigen.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/matrix.cs $(MATLIB)/vector.cs jacobi.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg
