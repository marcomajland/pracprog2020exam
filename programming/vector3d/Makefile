CSC = mcs
all : out.txt
	cat out.txt
out.txt : main.exe
	mono $< > $@
main.exe : main.cs vector3d.dll
	mcs -reference:vector3d.dll $<
vector3d.dll : vector3d.cs
	mcs -target:library -out:$@ $<

