# 26
import sys

#my inefficient div function. using it 
#instead of the remainder, allows for print the periods themselves
#
def div(n,d):
    temp = n
    q = 0
    r = 0
    while n>=d:
        q+=1
        n-=d
    
    r = n
#    print temp,d,q,r
    return [r,q]

def get_fraction_period(d):
    n = 10
    p = 0
    
    while n < d:
        n*=10
        
    remainders = []
    result = div(n,d)
    r = result[0]
    q = result[1]
    #print q,
    while r>0:
        if remainders.count(r)>0:
            break
        p+=1
        remainders.append(r)
        n = r
        if n < d:
            adjust = 1
        while n<d:
            p += 1
            n *= 10

        if adjust == 1:
            p -= 1
            adjust = 0

        result = div(n,d)
        r = result[0]
        q = result[1]
        #print q,

   # print
   # print p

    return p

def main():
    #d = 7 #d is the denominator; the numerator is understood to be 1
    #if len(sys.argv)>1:
    #    d = int(sys.argv[1])
        
    #period_length = get_fraction_period(d)
    #print period_length
    max_period_length = 0
    current_period_length = 0
    d_with_longest_period = 0
    for i in range(2,1001):
        current_period_length = get_fraction_period(i)
        if current_period_length > max_period_length:
            d_with_longest_period = i
            max_period_length = current_period_length

    print "number with longest period is: " , d_with_longest_period
    print "longest period is: " , max_period_length
        
    
if __name__ == '__main__':
    main()
