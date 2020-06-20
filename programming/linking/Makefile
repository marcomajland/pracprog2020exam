all:
	cat out.txt
out.txt : main.exe
	mono main.exe > out.txt
	cat out.txt
main.exe : main.cs lib.dll
	mcs -reference:lib.dll main.cs
lib.dll : lib.cs
	mcs -target:library lib.cs
clean:
	cat out.txt
