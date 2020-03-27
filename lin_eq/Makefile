MATLIB = ../matlib
all : lineq.exe
	mono $<
lineq.exe : lineq.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/matrix.cs $(MATLIB)/vector.cs $(MATLIB)/qr.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll
