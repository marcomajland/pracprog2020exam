------------------------------------------------------------
Practial Programming and Numerical Methods 2020 exam project
------------------------------------------------------------

Name: Marco Majland
Student number: 201607680

Using the algorithm for project assignments, project number 14 is obtained.

Brief description of project:
I have implemented the inverse iteration method for random real symmetric matrices. To test the algorithm, I have made a program (/exam/test/) which calculates closest eigenvalues (eigenvectors) for a random real symmetric matrix. To perform benchmarking, I used the Jacobi diagonalization procedure which we implemented earlier in the course to diagonalize the matrix. The program then chooses a random eigenvalue in the Jacobi spectrum and calculates a new deviated value. The deviated value is used as an initial eigenvalue for the algorithm. The initial eigenvector is chosen both as a random vector and a deviated Jacobi eigenvector. To test for correct convergence, the resulting eigenvalue/eigenvector is compared to the Jacobi diagonalization.
Furtermore, I tested the convergence of the method. In the program (/exam/convergence/), the relative error is monitored as function of number of iterations for different initial eigenvalues. Here, the initial eigenvalues are further and further deviated from the sought eigenvalue to demonstrate slower convergence. Here, I also implemented the Rayleigh update to demonstrate faster convergence.

All library files are found in the folder matlib in the root of the repository.

Description of output files:

test_out.txt
This output file contains the testing of the algorithm for a random matrix. The sought eigenvalue is a deviated Jacobi eigenvalue and the results of the algorithm are compared to the Jacobi diagonalization results.

Convergence.pdf
This output file depicts the convergence of the algorithm as a function of iterations. This is done for different deviations from the sought eigenvalue. This is done both with and without the Rayleigh method, which demonstrates faster convergence using the Rayleigh method.

project.pdf
This output file contains a more in depth description of the project.
