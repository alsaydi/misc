//euler 50
import sys
import math
def isPrime(n):
    if n <= 1:
        return False
    if n == 2:
        return True
    if n & 1 <= 0:
        return False
    root = int(math.sqrt(n))
    for i in range(3,root+1):
        if n % i == 0:
            return False

    return True

def sieve_of_Eratosthenes(n):
    primes = []
    numbers = []
    for i in range(1,n+2):
        numbers.append(0)
    p = 2
    mult = p
    while p <= n:
        if numbers[p] == 0:
            primes.append(p)
            while mult <= n:
                numbers[mult] = 1 #not prime
                mult += p
        p += 1
        mult = p*2
    #for i in range(2,n+1):
    #    if numbers[i] == 0:
    #        primes.append(i)
    return primes
    #1,2,3,4,5,6,7,8,9,11
    #0,10 -- mid = 5
def binSearch(value,list,start,end,offset):
    if start>=end:
        return start
    mid = start + int((end-start)/2) 
    if value+offset== list[mid] :
        return mid
    if value < list[mid]:
        return binSearch(value,list,start,mid-1,offset)
    return binSearch(value,list,mid+1,end,offset)

def primesSum(primes,n):
    #print(primes)
    print("solving ...")
    sums = [primes[0]]
    length = len(primes)
    index = 1
    while index < length:
        sums.append( primes[index]+sums[index-1])
        index+=1

    #print(sums)
    index = 0
    maxLength = 0
    currentLength = 0
    start = 0
    end   = 0
    maxSum = 0
    length = len(sums)
    primesLength = len(primes)
    print("sums len: " ,length)
    #return 0
    for i in range(0,length+1):
        #print(i)
        if i<length:
            tempIndex = binSearch(n,sums,i,length,sums[i])
            #print(tempIndex)
        for j in range(tempIndex,i+maxLength,-1):
            sum = sums[j]
            if i > 0:
                sum = sum - sums[i-1]
            if sum < n and sum in primes: #isPrime(sum) #primes[binSearch(sum,primes,0,primesLength,0)] == sum:
                currentLength = j - i + 1
                break
        if currentLength > maxLength:
            maxLength = currentLength
            maxSum = sum
            start = i
            end = j

    #print(primes[start:end+1])
    print("sum: " , maxSum)
    print("max length: " ,maxLength)
    print("start: " ,start)
    print("end: " ,end)
    return maxLength

def primesLessThan(n):
    list = [2]
    for i in range(1,n+1,2):
        if isPrime(i):
            list.append(i)

    return list

def main(args):
    if len(args) > 1 :
        print(args[1])
        primes = sieve_of_Eratosthenes(int(args[1]))
        #print(primes)
        #for n in primes:
        #    if not isPrime(n):
        #        print(n , " is not prime.")
        maxSum = primesSum(primes,int(args[1]))
        #print(maxSum)
        

if __name__ == '__main__':
    main(sys.argv)

