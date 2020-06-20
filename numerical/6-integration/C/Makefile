MATLIB = ../../matlib
all : main_C.exe
	mono $<
main_C.exe : main_C.cs lib.dll
	mcs $< -r:lib.dll
lib.dll : $(MATLIB)/vector.cs $(MATLIB)/matrix.cs $(MATLIB)/quado8.cs $(MATLIB)/integrator.cs
	mcs $^ -t:library -out:$@
clean :
	rm --force *.exe *.dll *.svg *.txt
