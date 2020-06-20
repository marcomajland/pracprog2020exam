all : output.gpi output2.gpi ms.exe ms.pdf
	mono ms.exe
	gnuplot output.gpi
	gnuplot output2.gpi
ms.pdf : ms.tex
	pdflatex $<
	pdflatex $<
ms.exe : ms.cs quado8.dll
	mcs $< -r:quado8.dll
quado8.dll : quado8.cs
	mcs $< -t:library
clean :
	rm --force *.dll *.exe *.log *.aux



