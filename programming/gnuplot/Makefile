all : Erf.svg
Gamma.svg : gamma.gpi gamma.txt
	gnuplot $<
Erf.svg : erf.gpi erf.txt
	gnuplot $<
gamma.txt : main.exe
	mono main.exe gamma > $@
erf.txt : main.exe
	mono main.exe erf > $@
main.exe : main.cs functions.dll
	mcs $< -r:functions.dll
functions.dll : functions.cs
	mcs $< -t:library
gamma.gpi : Makefile gamma.txt
	echo 'set term svg' >> $@
	echo 'set out "Gamma.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot "gamma.txt" with lines' >> $@
erf.gpi: Makefile erf.txt
	echo 'set term svg' >> $@
	echo 'set out "Erf.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot "erf.txt" with lines' >> $@
clean :
	rm --force *.dll *.exe out* *.svg



