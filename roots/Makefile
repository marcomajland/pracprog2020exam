MATLIB = ../matlib
all : roots.exe plot.gpi
	mono $<
	gnuplot plot.gpi
roots.exe : roots.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/qr.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
