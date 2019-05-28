using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace Nask.EZD.Receiver
{
    class Program
    {
         static async Task Main(string[] args)
        {
            const string url = "http://localhost:5000/hubs/documents";

            Console.WriteLine("Signal-R Received!");
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;

            // dotnet add package Microsoft.AspNetCore.SignalR.Client
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            Console.WriteLine("Connecting...");

            await connection.StartAsync();

            Console.WriteLine("Connected.");

            // connection.Closed += 

            connection.On<Document>("Added", 
                        document => System.Console.WriteLine($"Received document {document.Name}"));
           
            Console.WriteLine("Press any key to exit.");

            Console.ReadKey();

            Console.ResetColor();
        

        }
    }
    public class Document
    {
        public int Id { get; set; }
        public string Number { get; set; }     
        public string Name { get; set; }
    }
}
