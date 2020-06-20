MATLIB = ../../matlib
all : main_A.exe
	mono $<
main_A.exe : main_A.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/matrix.cs $(MATLIB)/vector.cs $(MATLIB)/qr.cs $(MATLIB)/misc.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.txt
