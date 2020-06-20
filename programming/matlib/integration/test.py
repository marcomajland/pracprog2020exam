import scipy.integrate as integrate
import math
import numpy
global ncalls

def f(x): 
	global ncalls
	ncalls=ncalls+1
	return math.log(x)/math.sqrt(x)

ncalls=0
result = integrate.quad(f, 0, 1,epsabs=1e-5,epsrel=0)
print "result=",result,"ncalls=",ncalls

def g(x): 
	global ncalls
	ncalls=ncalls+1
	return math.exp(-x*x)*2/math.sqrt(math.pi)

ncalls=0
result = integrate.quad(g, 0, numpy.inf,epsabs=1e-5,epsrel=0)
print "result=",result,"ncalls=",ncalls
