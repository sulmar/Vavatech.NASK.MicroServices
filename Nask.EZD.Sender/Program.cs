using System;
using System.Reflection.Metadata;
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
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;

            // dotnet add package Microsoft.AspNetCore.SignalR.Client
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            DocumentFaker documentFaker = new DocumentFaker();

            while(true)
            {

                // Document document = new Document
                // {
                //     Id = 1,
                //     Number = "DOC 3232",
                //     Name = "My SignalR"
                // };

                Document document = documentFaker.Generate();

                Console.WriteLine($"Sending {document.Name}...");
                await connection.SendAsync("DocumentAdded", document);
                Console.WriteLine($"Sent {document.Name}.");

                await Task.Delay(TimeSpan.FromSeconds(1));

            }

            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();

            Console.ResetColor();
        

        }
    }
}
