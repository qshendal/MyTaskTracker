using System;
using System.Diagnostics;

class  Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        long sum = 0;
        for (int i = 0; i < 1000000; i++)
        {
            sum += i;
        }
        stopwatch.Stop();

        Console.WriteLine(stopwatch.Elapsed.TotalSeconds);
    }
}