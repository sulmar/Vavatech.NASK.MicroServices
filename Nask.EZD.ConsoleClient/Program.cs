using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Bogus;

namespace Nask.EZD.ConsoleClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Task Demo!");

            IProgress<Document> progress = 
                new Progress<Document>(document => 
                    System.Console.WriteLine($"Processing {document.Number}"));

            DocumentFaker documentFaker = new DocumentFaker();

            var documents = documentFaker.GenerateLazy(100);

            CancellationTokenSource cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            Processor processor = new Processor();
            processor.ProcessAsync(documents, cts.Token, progress);

          //  cts.CancelAfter(TimeSpan.FromSeconds(10));

            System.Console.WriteLine("Press Enter key to cancel.");
            Console.ReadLine();
            cts.Cancel();


            System.Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
    }

     public class Document
    {
        public int Id { get; set; }
        public string Number { get; set; }     
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsChecked { get; set; }
        public DocumentType DocumentType { get; set; } 
    }

    public enum DocumentType
    {
        Draft,

        Note,

        Letter
    }


    public class DocumentFaker : Faker<Document>
    {
        public DocumentFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Name, f => f.Lorem.Word());
            RuleFor(p => p.Number, f => f.Phone.PhoneNumber());
            RuleFor(p => p.IsDeleted, f => f.Random.Bool(0.8f));
            RuleFor(p => p.DocumentType, f=>f.PickRandom<DocumentType>());
            Ignore(p => p.IsChecked);

        }
    }

    class Processor
    {
        public async Task ProcessAsync(IEnumerable<Document> documents, 
            CancellationToken cancellationToken = default(CancellationToken), 
            IProgress<Document> progress = null)
        {
            foreach(Document document in documents)
            {   
                cancellationToken.ThrowIfCancellationRequested();

                // if (cancellationToken.IsCancellationRequested)
                // {
                //     continue;
                // }

               await ProcessAsync(document, cancellationToken);

               progress?.Report(document);
            }
        }

        private async Task ProcessAsync(Document document, CancellationToken cancellationToken = default)
        {
           // System.Console.WriteLine($"Processing {document.Number}");
            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);
            //System.Console.WriteLine($"Done. {document.Number}");
        }

    } 
}
