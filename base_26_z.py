import sys
letters = "0ABCDEFGHIJKLMNOPQRSTUVWXYZ"
#http://stackoverflow.com/questions/181596/how-to-convert-a-column-number-eg-127-into-an-excel-column-eg-aa
def excel_id_simpler(n):
    r = 0
    s = ''
    while n>0:
        r = (n-1)%26
        s = chr(r+65) + s
        n = (n - r) // 26
        
    return s

def excel_id(n):
    r = 0
    q = 0
    mults = []
    while n>0:
        q = n//26
        r = n%26
        mults.append(r)
        n = q
        
    mults.reverse()
    mlen = len(mults)
    while 0 in mults and mults.index(0)>0:
        for i in range(1,mlen):
            if i >= mlen:
                break
            if mults[i] == 0:
                mults[i] = 26
                mults[i-1] -= 1
    s = ""
    for i in mults:
        if i == 0:
            continue
        s += letters[i]
    return s

def main():
    n = 26
    if len(sys.argv)>1:
        n = int(sys.argv[1])

    s = excel_id(n)
    print s
    s = excel_id_simpler(n)
    print s


if __name__ == '__main__':
    main()
