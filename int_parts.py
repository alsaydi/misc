import sys

def part(list,n,counter):
    if n == 0: #sum(list) == orig:
        print(list)
        counter[0]+=1
    for i in range(1,n+1):
        newList = list.copy()
        newList.append(i)
        if len(newList)>1 and i<newList[-2]: #if a number we've seen before, ignore subsequent lists
            continue
        part(newList,n-i,counter)
        
        

#taken from http://code.activestate.com/recipes/218332-generator-for-integer-partitions/
def partitions(n):
    # base case of recursion: zero is the sum of the empty list
    if n == 0:
        yield []
        return

# modify partitions of n-1 to form partitions of n
    for p in partitions(n-1):
        yield [1] + p
        if p and (len(p) < 2 or p[1] > p[0]):
            yield [p[0] + 1] + p[1:]

def print_partitions(n):
    lists = partitions(n)
    counter = 0 #len(lists)
    for l in lists:
        print(l)
        counter+=1
    print ("# solutions: " ,counter)

def main(args):
    n = 6
    if len(args)>1:
        n = int(args[1])
#    print(n)
    counter = [0]
    part([],n,counter)
    print ("my solutions: ", counter[0])
    print("-"*8)
    print_partitions(n)

if __name__ == '__main__':
    main(sys.argv)
