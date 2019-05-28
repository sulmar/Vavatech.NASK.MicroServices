using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Nask.EZD.SignalR
{

    public class DocumentsHub : Hub
    {
        public async override Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public void DocumentAdded(Document document)
        {
           this.Clients.Others.SendAsync("Added", document);

          
        }
    }
}