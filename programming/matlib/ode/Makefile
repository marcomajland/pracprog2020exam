DATA = out.txt
PLOT = plot.svg
$(PLOT): plot.gpi $(DATA)
	gnuplot $<
$(DATA): main.exe
	mono $< 1> $@ 2> log
	
main.exe: main.cs matlib.dll
	mcs $< -reference:matlib.dll

matlib.dll: ode.cs ../matrix/vector.cs
	mcs -target:library -out:$@ $^

clean:
	rm -f *.dll *.exe out* *.svg

plot.gpi: Makefile
	cat /dev/null > $@
	echo 'set term svg fsize 18 background rgb "white"' >> $@
	echo 'set out "$(PLOT)"' >> $@
	echo 'set key out' >>$@
	echo 'set tics out' >>$@
	echo 'set xlabel "x"' >>$@
	echo 'plot \' >>$@
	echo ' "$(DATA)" using 1:2 with points pt 7 ps 0.5 title "sin" \' >>$@
	echo ',"$(DATA)" using 1:3 with lines title "cos" \' >>$@
	echo ',"$(DATA)" using ($$1):(0) w p pt 7 ps 0.1 not \' >>$@
	
