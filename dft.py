import cmath

#zs is a list of complex number
#n is the degree of the polynomial
#rev=1 is dft, -1 is inverse dft
def dft(zs,rev):
    n=len(zs)
    if n==1: return zs
    tmp=zs
    t1=[]
    t2=[]
    for i in range(0,n>>1):
        t1.append(zs[i<<1])
        t2.append(zs[(i<<1)|1])
    t1=dft(t1,rev)
    t2=dft(t2,rev)
    z1=1
    z2=cmath.exp(2*rev*cmath.pi*1j/n)
    for i in range(0,n>>1):
        tmp[i]=t1[i]+z1*t2[i]
        tmp[i+(n>>1)]=t1[i]-z1*t2[i]
        z1*=z2
    return tmp


print(dft([3,2,5,1],1))