MATLIB = ../../matlib
all : main_A.exe
	\time -f "%U" -ao time.txt mono $<
#	gnuplot plot.gpi
#	gnuplot plot_exact.gpi
main_A.exe : main_A.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/matrix.cs $(MATLIB)/vector.cs $(MATLIB)/jacobi.cs $(MATLIB)/misc.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
