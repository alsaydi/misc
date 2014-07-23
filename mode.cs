using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsTest
{
    class Mode
    {
        public static void Test()
        {
            var list = new List<int[]>();
            list.Add(new int[] { 1, 2, 3, 10, 10, 10, 10, 10, 10, 10, 20, 20, 20, 20, 20, 20, 20, 20, 20, 20, 30, 30, 30, 30, 30, 30, 30, 30, 30, 30, 40, 40, 40, 40, 40, 40, 40, 40, 40, 40, 50, 50, 50, 50, 50, 50, 50, 50, 50, 50, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 });
            list.Add(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 });
            list.Add(new int[] { 2, 2, 1, 3, 1, 3, 1, 3, 1, 3, 3, 1, 1, 3, 3 });
            list.Add(new int[] { 2, 2, 3, 3, 2, 3, 2, 3, 1, 2 });
            list.Add(new int[] { 2, 2, 1, 3, 1, 3, 1, 3, 1, 3, 3, 3 });
            list.Add(new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 });
            list.Add(new int[] { 2, 2, 3, 1, 3, 3 });
            list.Add(new int[] { 1, 2, 1, 2, 2, 1, 3, 3, 2, 2, 2 });
            list.Add(new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 3, 3, 3, 2, 2, 2 });
            list.Add(new int[] { 3, 3, 3, 1, 3, 1, 3, 1, 3, 1, 3, 3, 1, 3, 3, 4, 4, 4, 4 });
            list.Add(new int[] { 3, 3, 1, 3, 3, 1, 3, 3, 1, 3, 3, 1, 4, 4, 4, 4 });
            list.Add(new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 1, 2, 2, 1, 2, 2, 1, 2, 2, 1, 2, 2, 1, 2, 2 });
            list.Add(new int[] { 1, 2, 1, 2, 1, 2, 3, 3, 1, 1 });
            list.Add(new int[] { 1, 3, 1, 1, 2, 2 });
            list.Add(new int[] { 1, 3, 2, 2, 1, 1 });
            list.Add(new int[] { 11, 10, 9, 8, 7, 6, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1 });
            list.Add(new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 3, 3, 3, 3, 3, 1, 1, 1, 2, 2, 1, 1, 2, 1, 1, 1, 2, 1, 1, 2, 1, 1 });
            list.Add(new int[] { 4,5,1, 1,3, 1, 1, 2, 2,1 });
            list.Add(new int[] { 1, 1, 1, 2, 1, 1, 3, 3, 3, 3, 3, 3, 1, 1, 1 });
            list.Add(new int[] { 2, 3, 2, 1, 3, 3 });
            list.Add(new int[] { 2, 3, 2, 3, 3, 3 });
            list.Add(new int[] { 2, 2, 2, 3, 3, 3 });
            list.Add(new int[] { 1, 1, 2, 1, 1, 2, 1, 1, 2, 3, 3, 3, 3, 3, 3, 1 });
            foreach (var arr in list)
            {
                int foundMode = FindMode(arr, 0, arr.Length - 1).Mode;
                int qMode = FindModeQuadratic(arr);
                int sMode = SortAndFindMode(arr);
                int lMode = FindModeLinear(arr,false);
                if (false)
                {
                    Console.WriteLine("Mode in Quadratic time: {0}", qMode);
                    Console.WriteLine("Mode using hashtable: {0}", foundMode);
                    Console.WriteLine("Mode in sorted array: {0}", sMode);
                    Console.WriteLine("Mode lineary method: {0}", lMode);
                }
                if (foundMode != lMode)
                {
                    if (!(arr.Distinct().Count() == 2))
                    {
                        PrintArray(arr);
                        Console.WriteLine("Different expected: {0} actual: {1}", foundMode, lMode);
                        lMode = FindModeLinear(arr,true);
                        Console.WriteLine("*".PadRight(30, '*'));
                        Console.WriteLine(arr.Length);
                       // break;
                    }
                }
            }            
        }
        static int[] BuildArray(int N)
        {
            var timeSeed = (int)DateTime.Now.Ticks & 0x0000FFFF + (int)Environment.WorkingSet;
            int max = N * 2;
            var arr = new int[N];
            var mode = (new Random(timeSeed)).Next(1, max);
            //Console.WriteLine(mode);
            int index = (new Random(timeSeed)).Next(0, N - 1);
            int counter = 0;
            int seed = 3;
            while (counter < N / 2)
            {
                if (arr[index] == 0)
                {
                    arr[index] = mode;
                    counter++;
                }
                index = (new Random(timeSeed++ * seed++)).Next(0, N - 1);
            }
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 0)
                {
                    arr[i] = (new Random(i + 1 + timeSeed)).Next(1, max);
                }
            }
            return arr;
        }
        static double StdDev(int[] arr)
        {
            var avg = arr.Average();
            var list = from num in arr select (num - avg) * (num - avg);
            return Math.Sqrt(list.Average());
        }
        static ModeClass FindMode(int[] arr, int start, int end)
        {
            //Console.WriteLine("{0} {1}",start,end);
            //var ht = hashTable;//new Dictionary<int,int>();
            var ht = new Dictionary<int, int>();
            int max = 1;
            int mode = arr[start];
            for (int i = start; i <= end; i++)
            {
                if (ht.ContainsKey(arr[i]))
                {
                    ht[arr[i]]++;
                    if (ht[arr[i]] > max)
                    {
                        max = ht[arr[i]];
                        mode = arr[i];
                    }
                }
                else
                {
                    ht[arr[i]] = 1;
                }
            }
            return new ModeClass(mode, max);
        }
        static void PrintArray(int[] arr)
        {
            Console.WriteLine();
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write("{0} ", arr[i]);                
            }
            Console.WriteLine();
        }
        class ModeClass
        {
            public ModeClass(int mode, int reps)
            {
                this.Mode = mode;
                this.Reps = reps;
            }
            public int Mode { get; set; }
            public int Reps { get; set; }
        }

        // Define other methods and classes here
        static int FindMode(int[] array)
        {
            int start = 0;
            int midLen = array.Length / 2;
            int mid = array.Length / 2;
            int end = array.Length;
            while (start < midLen && mid < end)
            {
                while (array[start] == array[mid])
                {
                    start++;
                    mid++;
                }
                if (array[start] > array[mid])
                {
                    int temp = array[mid];
                    array[mid] = array[start];
                    array[start] = temp;
                    start++;
                }
                else
                {
                    mid++;
                }
            }
            throw new NotImplementedException("Incomplete");
        }
        //buggy
        static int FindModeQuadratic(int[] arr)
        {
            //Console.WriteLine(arr.Length);
            int reps = 0;
            int mode = arr[0];
            int maxReps = 0;
            int ctr = 0;
            for (int i = 0; i < arr.Length / 2; i++)
            {
                reps = 1;
                for (int j = i + 1; j < arr.Length; j++)
                {
                    ctr++;
                    if (arr[j] == arr[i])
                    {
                        reps++;
                    }
                    if (reps >= arr.Length / 2)
                    {
                        break;
                    }
                }
                if (reps > maxReps)
                {
                    maxReps = reps;
                    mode = arr[i];
                }
                if (reps >= arr.Length / 2)
                {
                    mode = arr[i];
                    break;
                }
            }
            //Console.WriteLine("CTR: {0}", ctr);
            return mode;
        }
        static int SortAndFindMode(int[] array)
        {
            var sorted = array.OrderBy((a) => a).ToArray();
            int mode = sorted[0];
            int maxOccurrences = 1;
            int occurrences = 1;
            int finalMode = mode;
            for (int i = 1; i < sorted.Length; i++)
            {
                if (sorted[i] == mode)
                {
                    occurrences++;
                }
                else
                {
                    if (occurrences > maxOccurrences)
                    {
                        finalMode = mode;
                        maxOccurrences = occurrences;
                    }
                    occurrences = 1;
                    mode = sorted[i];
                }
            }
            if (occurrences > maxOccurrences)
            {
                finalMode = sorted[sorted.Length - 1];
            }
            return finalMode;
        }
        static int FindModeLinear(int[] arr,bool debug)
        {
            int mostRepeated = 0;
            int mostRepeatedReps = 0;
            int runnerUp = 0;
            int runnerUpReps = 0;
            mostRepeated = arr[0];
            mostRepeatedReps = 1;
            int prevRunnerUp = 0;
            int prevRunnerUpReps = 0;
            int flag = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == mostRepeated)
                {
                    mostRepeatedReps++;
                }
                else if (runnerUpReps == 0)
                {
                    runnerUp = arr[i];
                    runnerUpReps++;
                }
                else if (arr[i] == runnerUp)
                {
                    runnerUpReps++;
                }
                else
                {
                    if (runnerUpReps > mostRepeatedReps)
                    {
                        int temp = runnerUp;
                        runnerUp = mostRepeated;
                        mostRepeated = temp;

                        temp = runnerUpReps;
                        runnerUpReps = mostRepeatedReps;
                        mostRepeatedReps = temp;
                    }
                    int occurrences = 1;
                    while (i < arr.Length - 1 && arr[i] == arr[i + 1])
                    {
                        occurrences++;
                        i++;
                    }
                    if (flag == 1)
                    {
                        if (arr[i] == prevRunnerUp)
                        {
                            occurrences += prevRunnerUpReps;
                        }   
                    }                 
                    if (occurrences >= mostRepeatedReps)
                    {
                        runnerUp = mostRepeated;
                        runnerUpReps = mostRepeatedReps; //if we set mostRepeatedReps to zero then let's look for a new runner up
                        mostRepeated = arr[i];
                        mostRepeatedReps = occurrences;
                    }
                    else if (occurrences >= runnerUpReps)
                    {
                        runnerUp = arr[i];
                        runnerUpReps = occurrences;
                    }
                }
            }
            if (runnerUpReps == mostRepeatedReps)
            {
                int index = 0;
                while (index < arr.Length - 1 && arr[index] == arr[index + 1])
                {
                    index++;
                }
                if (index > 0)
                {
                    index++;
                }
                if (index < arr.Length - 2 && (runnerUp == arr[index] || runnerUp == arr[index + 1])
                    && (mostRepeated != arr[index] && mostRepeated != arr[index+1])) /* the mode must occurr in the first two elements otherwise  a repetition must have happend later in 
                                                               * the array and we would not be in this situation; i think.*/
                {
                    mostRepeated = runnerUp;
                }
            }
            if (runnerUpReps > mostRepeatedReps)
            {
                mostRepeated = runnerUp;
            }
            //Console.WriteLine(mostRepeatedReps);
            return mostRepeated;
        }        
    }
}
