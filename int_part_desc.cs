using System;
class Program
{
    static void Main(string [] args)
    {
        int n = 6;
        if (args.Length > 0)
        {
            n = int.Parse(args[0]);
        }
        ulong solutions = part(n);
        Console.WriteLine(solutions);
    }
    static void printArray(int[] array, int index, int n)
    {
        int sum = 0;
        for (int i = 0; i <= index; i++)
        {
            Console.Write("{0} ", array[i]);
            sum += array[i];
        }
        if (sum != n)
        {
            throw new Exception(string.Format("Sum != n {0} != {1}", sum, n));
        }
        Console.WriteLine();

    }
    static ulong part(int n)
    {
        int[] array = new int[n];
        int sum = 0;
        int diff = 0;
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = 0;
        }
        array[0] = n;
        int partLen = 1;
        ulong counter = 1;
        while (partLen < n)
        {
            counter++;
            int index = partLen - 1;
            printArray(array, index,n);
            while (index>0 && array[index-1]>=array[index] && array[index]==1)
            {
                index--;
            }
            array[index]--;
            sum = 0;
            for (int i = 0; i <= index; i++)
            {
                sum += array[i];
            }
            int tempIdx = index + 1;
            partLen = index+1;
            while (sum < n)
            {
                diff = (sum + array[index]) <= n ? array[index] : n - sum;
                array[tempIdx] = diff;
                sum += diff;
                partLen++;
                tempIdx++;
                
            }
        }
        printArray(array, partLen-1,n);
        return counter;
    }
    // Define other methods and classes here
}
