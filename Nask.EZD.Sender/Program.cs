using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Nask.EZD.Sender
{
    class Program
    {

        // C# 7.0
       // static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/hubs/documents";

            Console.WriteLine("Signal-R Sender!");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.White;

            // dotnet add package Microsoft.AspNetCore.SignalR.Client
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();

            Console.ResetColor();
        

        }
    }
}
