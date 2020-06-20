MATLIB = ../../matlib
all : main_B.exe plot.gpi
	mono $<
	gnuplot plot.gpi
main_B.exe : main_B.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/matrix.cs $(MATLIB)/vector.cs $(MATLIB)/jacobi.cs $(MATLIB)/misc.cs $(MATLIB)/lsq_qr.cs $(MATLIB)/qr.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
	cd plot_files; rm --force *.svg *.txt
