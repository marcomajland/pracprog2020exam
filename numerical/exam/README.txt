-------------------------------------------------------------
Practical Programming and Numerical Methods 2020 exam project
-------------------------------------------------------------

Name: Marco Majland
Student number: 201607680

Using the algorithm for project assignments, project number 14 is obtained.
A more in depth description of the project is found in the file Project.pdf.

Brief description of project:
I have implemented the inverse iteration method for random real symmetric matrices. The output file "test_out.txt" contains a demonstration of the implementation. The code which generates the output file is found in /exam/test. For comparison, I used the Jacobi diagonalization procedure which we implemented earlier in the course to diagonalize the matrix. The demonstration example of the algorithm chooses a random eigenvalue in the Jacobi spectrum and calculates a new deviated value. The deviated value is used as an initial eigenvalue for the algorithm. The initial eigenvector is chosen both as a random vector and a deviated Jacobi eigenvector. From the initial eigenvalue/eigenvector, the inverse iteration algorithm is used to approach the closest eigenvalue/eigenvector. To test for correct convergence, the resulting eigenvalue/eigenvector is compared to the Jacobi diagonalization.
Furthermore, I tested the convergence of the method for different deviations. To do this, I investigated two measures of error. First, the relative error which indicates convergence to whatever eigenvalue of the matrix. The other error measure quantizes whether the algorithm converges to the target Jacobi eigenvalue used as initial condition. Results of such calculations are shown in the figure files "Convergence_i.svg". Lastly, the convergence is also investigated using the Rayleigh updates to check for faster convergence. This is also demonstrated in the beforementioned figure files.

All auxiliary library files are found in the folder /root/numerical/matlib. Auxiliary methods used specifically for the inverse iteration are found in the file "power_method.cs".

Description of output files:
test_out.txt
This output file contains the testing of the algorithm for a random matrix. The sought eigenvalue is a deviated Jacobi eigenvalue and the results of the algorithm are compared to the Jacobi diagonalization results.

Convergence_i.pdf
This output file depicts the convergence of the algorithm as a function of iterations. This is done for different deviations from the sought eigenvalue. This is done both with and without the Rayleigh method, which demonstrates faster convergence using the Rayleigh method. Convergences are calculated for two measures of error; relative error and deviation from target eigenvalue.

Project.pdf
This output file contains a more in depth description of the project and the obtained results.
