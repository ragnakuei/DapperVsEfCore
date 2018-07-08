using System;
using System.Collections.Generic;
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

            #region 測試 Dapper 抓取關聯資料

            //var runDapper = new RunDapper();
            //var ordersInDapper = runDapper.GetOrder().ToList();
            //foreach (var order in ordersInDapper)
            //{
            //    Console.WriteLine($"OrderID:{order.OrderID}");
            //    Console.WriteLine($" -- CustoemrID:{order.Customer.CustomerID}");
            //    Console.WriteLine($" -- ShipperID:{order.ShippedBy.ShipperID}");
            //}

            #endregion

            // Console.WriteLine("----------------------------");

            #region 測試 EfCore 抓取關聯資料

            //var runEfCore = new RunEfCore(isTracking: true);
            //var ordersInEfCore = runEfCore.GetOrder().ToList();
            //foreach (var order in ordersInEfCore)
            //{
            //    Console.WriteLine($"OrderID:{order.OrderID}");
            //    Console.WriteLine($" -- CustoemrID:{order.Customer.CustomerID}");
            //    Console.WriteLine($" -- ShipperID:{order.ShippedBy.ShipperID}");
            //}

            #endregion


            #region 使用傳統計時法

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

            #endregion
        }
    }

    [CoreJob]
    [MinColumn, MaxColumn]
    [MemoryDiagnoser]
    public class TestRunner
    {
        private readonly TestClass _test = new TestClass();

        [Benchmark]
        public void Dapper_1M_Table() => _test.Dapper_1M_Table();

        [Benchmark]
        public void EfCore_1M_TableTracking() => _test.EfCore_1M_TableTracking();

        [Benchmark]
        public void EfCore_1M_TableNoTracking() => _test.EfCore_1M_TableNoTracking();


        [Benchmark]
        public void Dapper_1M2S_Table() => _test.Dapper_1M2S_Table();

        [Benchmark]
        public void EfCore_1M2S_TableTracking() => _test.EfCore_1M2S_TableTracking();

        [Benchmark]
        public void EfCore_1M2S_TableNoTracking() => _test.EfCore_1M2S_TableNoTracking();
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

        public IEnumerable<Customer> Dapper_1M_Table()
        {
            var result = _runDapper.GetCustomer().ToList();
            return result;
        }

        public IEnumerable<Customer> EfCore_1M_TableNoTracking()
        {
            var result = _runEfCore.GetCustomer().ToList();
            return result;
        }

        public IEnumerable<Customer> EfCore_1M_TableTracking()
        {
            var result = _runEfCoreTracking.GetCustomer().ToList();
            return result;
        }

        public IEnumerable<Order> Dapper_1M2S_Table()
        {
            var result = _runDapper.GetOrder().ToList();
            return result;
        }

        public IEnumerable<Order> EfCore_1M2S_TableTracking()
        {
            var result = _runEfCore.GetOrder().ToList();
            return result;
        }

        public IEnumerable<Order> EfCore_1M2S_TableNoTracking()
        {
            var result = _runEfCoreTracking.GetOrder().ToList();
            return result;
        }
    }

}
