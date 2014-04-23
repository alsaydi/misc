//Euler 31
using System;
public class Program
{
    public static void Main(string [] args)
    {
        int n = 20;
        if (args.Length > 0)
        {
            n = int.Parse(args[0]);
        }
        var parts = CountPartitions(n, 1 );
        Console.WriteLine();
        Console.WriteLine(parts);
    }
    static void PrintList(List<int> list)
    {
        foreach (int i in list)
        {
            Console.Write("{0} ", i);
        }
        Console.WriteLine();
    }
    static int[] coins = { 1, 2, 5, 10, 20, 50, 100, 200 };
    static int CountRestrictedPartitions(List<int> list, int n)
    {
        if (n < 0)
        {
            return 0;
        }
        if (n == 0)
        {
            //PrintList(list);
            return 1;
        }
        int sum = 0;
        foreach (var c in coins)
        {
            var newList = new List<int>(list);
            newList.Add(c);
            if (newList.Count > 1 && newList[newList.Count - 2] < c) break;
            sum += CountRestrictedPartitions(newList, n - c);
        }
        return sum;
    }
    static ulong CountPartitions(int n, int largest)
    {
        if (n == 0)
        {
            return 1;
        }
        ulong sum = 0;
        for (int i = largest; i <= n;i++ )
        {
            if (n == largest )
            {
                sum+=1;
            }
            else if (n > largest)
            {
                sum += CountPartitions(n - i, i);
                
            }

        }
        return sum;
    }
}
