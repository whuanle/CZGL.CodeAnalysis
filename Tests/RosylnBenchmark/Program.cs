using BenchmarkDotNet.Running;
using System;

namespace RosylnBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
            Console.ReadKey();
        }
    }
}
