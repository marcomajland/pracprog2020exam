MATLIB = ../../matlib
all : main_C.exe plot.gpi
	mono $<
	gnuplot plot.gpi
main_C.exe : main_C.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/qr.cs $(MATLIB)/matrix.cs $(MATLIB)/vector.cs $(MATLIB)/lsq_qr.cs $(MATLIB)/misc.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
