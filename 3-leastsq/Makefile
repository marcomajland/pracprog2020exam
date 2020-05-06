MATLIB = ../matlib
all : lsq.exe expplot.gpi logplot.gpi
	mono $<
	gnuplot expplot.gpi
	gnuplot logplot.gpi
lsq.exe : lsq.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/qr.cs $(MATLIB)/matrix.cs $(MATLIB)/vector.cs lsq_qr.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg
