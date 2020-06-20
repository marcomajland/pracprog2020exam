Out.txt:main.exe
	mono $< > $@

main.exe: main.cs matlib.dll
	mcs $< -reference:$(lastword $^)
matlib.dll: ../matrix/matrix.cs ../matrix/vector.cs qr.cs
	mcs $^ -target:library -out:$@

clean:
	rm -rf Out* *.dll *.exe log*
