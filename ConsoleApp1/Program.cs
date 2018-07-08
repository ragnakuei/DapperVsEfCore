using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Columns;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Running;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<TestRunner>();

        //    var times = Enumerable.Range(0, 100);
        //    var watch = new System.Diagnostics.Stopwatch();
        //    //以上可事先宣告

        //    var _runDapper = new RunDapper();
        //    var _runEfCore = new RunEfCore();

        //    do
        //    {
        //        watch.Restart();
        //        //進行測試
        //        foreach (var item in times)
        //        {
        //            //此處放測試的部份
        //            _runEfCore.GetCustomer().ToList();
        //        }
        //        //測試結束
        //        watch.Stop();
        //        var elapsedMs = watch.ElapsedMilliseconds;
        //        Console.WriteLine("Time Cost:{0}", elapsedMs);
        //    } while (Console.ReadKey().Key != ConsoleKey.Escape);
        //    Console.ReadLine();

        //    do
        //    {
        //        watch.Restart();
        //        //進行測試
        //        foreach (var item in times)
        //        {
        //            //此處放測試的部份
        //            _runDapper.GetCustomer().ToList();
        //        }
        //        //測試結束
        //        watch.Stop();
        //        var elapsedMs = watch.ElapsedMilliseconds;
        //        Console.WriteLine("Time Cost:{0}", elapsedMs);
        //    } while (Console.ReadKey().Key != ConsoleKey.Escape);
        }
    }

    [CoreJob]
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class TestRunner
    {
        private readonly TestClass _test = new TestClass();
        public TestRunner()
        {
        }

        [Benchmark]
        public void Dapper() => _test.Dapper();

        [Benchmark]
        public void EfCoreTracking() => _test.EfCoreTracking();

        [Benchmark]
        public void EfCoreNoTracking() => _test.EfCoreNoTracking();

    }

    public class TestClass
    {
        private readonly RunDapper _runDapper;
        private readonly RunEfCore _runEfCore;
        private readonly RunEfCore _runEfCoreTracking;

        public TestClass()
        {
            _runDapper = new RunDapper();
            _runEfCore = new RunEfCore(false);
            _runEfCoreTracking = new RunEfCore(true);
        }

        public IEnumerable<Customer> Dapper()
        {
            var result = _runDapper.GetCustomer().ToList();
            return result;
        }

        public IEnumerable<Customer> EfCoreNoTracking()
        {
            var result = _runEfCore.GetCustomer().ToList();
            return result;
        }

        public IEnumerable<Customer> EfCoreTracking()
        {
            var result = _runEfCoreTracking.GetCustomer().ToList();
            return result;
        }
    }

}
