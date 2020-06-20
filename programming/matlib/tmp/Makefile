CSFLAGS = -optimize+
cmath.dll: ../complex/complex.cs ../complex/cmath.cs
	mcs $(CFLAGS) -target:library -out:$@ $^

.PHONEY:clean
clean:
	$(RM) cmath.dll
