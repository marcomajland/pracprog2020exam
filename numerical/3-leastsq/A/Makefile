MATLIB = ../../matlib
all : main_A.exe plot.gpi
	mono $<
	gnuplot plot.gpi
main_A.exe : main_A.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/qr.cs $(MATLIB)/matrix.cs $(MATLIB)/vector.cs $(MATLIB)/lsq_qr.cs $(MATLIB)/misc.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
