using Microsoft.Extensions.Options;

namespace Nask.EZD.Api
{

    public class MyOptions
    {
        public byte Count { get; set; }
        public string Content { get; set; }
    }

    public class FakeMessagesService : IMessagesService
    {
       // byte count = 10;

       private readonly MyOptions options;

        // public FakeMessagesService(IOptions<MyOptions> options)
        // {
        //     this.options = options.Value;
        // }

        public FakeMessagesService(MyOptions options)
        {
            this.options = options;
        }


        public void Send(string message)
        {
            for(int copy = 1; copy < options.Count; copy++)
            {
                System.Console.WriteLine($"Sending {message}");
            }
        }
    }
}