using System;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading;

namespace Nask.EZD.ReactiveClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Reactive!");

            // dotnet add package System.Reactive

            // dotnet add package System.Diagnostics.PerformanceCounter --version 4.5.0
          //  var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            // cpuCounter.NextValue

           Random random = new Random();

           // var cpu = random.NextDouble() * 100;

            var source = Observable.Interval(TimeSpan.FromSeconds(1))
                .Select(cpu => (float) random.NextDouble() * 100)
                .Publish();

            source.Subscribe(new ConsoleObserver("Marcin"));

            var filteredSource = source
                .Where(cpu => cpu > 50);

            filteredSource
                .Subscribe(new ConsoleObserver("ALERT"));

            source.Take(3).Subscribe(cpu => System.Console.WriteLine($"Take {cpu}"));

             source.TakeLast(3).Subscribe(cpu => System.Console.WriteLine($"Take Last {cpu}"));

            source.Connect();

           // ColdSourceTest();

            System.Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }

        private static void ColdSourceTest()
        {
            var source = new EKGObservable();

            var observer = new ConsoleObserver("Marcin");

            using (var subscription = source.Subscribe(observer))
            {

            }
        }
    }

    public class ConsoleObserver : IObserver<float>
    {
        public ConsoleObserver(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }

        public void OnCompleted()
        {
            System.Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} [{Name}] OnCompleted!");
        }

        public void OnError(Exception error)
        {
            System.Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} [{Name}] {error.Message}!");
        }

        public void OnNext(float value)
        {
            System.Console.WriteLine($"#{Thread.CurrentThread.ManagedThreadId} [{Name}] {value}");
        }
    }

    public class EKGObservable : IObservable<float>
    {
        public IDisposable Subscribe(IObserver<float> observer)
        {
            Random random = new Random();

            for(int i = 0; i <100; i++)
            {
                observer.OnNext((float) random.NextDouble() * 100);

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            observer.OnCompleted();

            return new EmptyDisposable();
        }

        private class EmptyDisposable : IDisposable
        {
            public void Dispose()
            {
                System.Console.WriteLine("Disposed!");
            }
        }
    }
}
