all : Var.svg
Var.svg : var.gpi var.txt
	gnuplot $<
var.txt : func.exe
	mono func.exe > $@
func.exe : func.cs quado8.dll
	mcs $< -r:quado8.dll
quado8.dll : quado8.cs
	mcs $< -t:library
var.gpi : Makefile var.txt
	echo 'set term svg' >> $@
	echo 'set out "Var.svg"' >> $@
	echo 'set xlabel "x"' >> $@
	echo 'set ylabel "y"' >> $@
	echo 'set xzeroaxis' >> $@
	echo 'set yzeroaxis' >> $@
	echo 'set tics out' >> $@
	echo 'set key bottom right' >> $@
	echo 'plot "var.txt" with lines' >> $@
clean :
	rm --force *.dll *.exe out* *.svg



