all: mainA.exe
	mono $< 1> $@
mainA.exe: mainA.cs math.dll
	mcs -reference:math.dll -out:$@ $<
SRC := ../eigen/matrix.cs
SRC += ../eigen/vector.cs
SRC += jacobi.cs
SRC += matrixHelp.cs
math.dll: $(SRC)
	mcs -target:library -out:$@ $^
