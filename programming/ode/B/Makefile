DATA = data.txt
PLOT_DATA = Data.svg
all : $(PLOT_DATA)
$(PLOT_DATA) : data.gpi $(DATA)
	gnuplot $<
$(DATA) : main.exe
	mono $< > $(DATA)
matlib.dll : ode.cs vector.cs quado8.cs
	mcs $^ -t:library -out:$@
main.exe : main.cs matlib.dll
	mcs $< -r:matlib.dll
#data.gpi : Makefile $(DATA)
#	echo 'set term svg' >> $@
#	echo 'set out "Data.svg"' >> $@
#	echo 'set xlabel "x"' >> $@
#	echo 'set ylabel "y"' >> $@
#	echo 'set xzeroaxis' >> $@
#	echo 'set yzeroaxis' >> $@
#	echo 'set tics out' >> $@
#	echo 'set key bottom right' >> $@
#	echo 'plot "data.txt" using (1/$2)*sin($1):(1/$2)*cos($1) with lines notitle' >> $@
clean :
	rm --force *.dll *.exe out* *.svg
