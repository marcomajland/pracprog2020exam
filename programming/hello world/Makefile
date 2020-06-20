all : out.txt
	cat out.txt
out.txt : main.exe
	mono main.exe > out.txt
main.exe : main.cs
	mcs main.cs -out:main.exe
	cat out.txt
clean:
	rm --force out.txt hello.exe
