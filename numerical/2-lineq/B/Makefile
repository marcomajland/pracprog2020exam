MATLIB = ../../matlib
all : main_B.exe
	mono $<
main_B.exe : main_B.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/matrix.cs $(MATLIB)/vector.cs $(MATLIB)/qr.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.txt
