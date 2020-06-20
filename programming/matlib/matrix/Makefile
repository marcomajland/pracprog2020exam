CSC = mcs
all: out.txt; cat $<
out.txt: main.exe
	mono $< > $@
main.exe: main.cs matrix.dll
	$(CSC) -reference:$(word 2,$^) $<
matrix.dll : vector.cs matrix.cs
	$(CSC) -target:library -out:$@ $^
