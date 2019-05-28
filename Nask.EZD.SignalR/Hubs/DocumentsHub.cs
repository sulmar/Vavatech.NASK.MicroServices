using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Nask.EZD.SignalR
{

    public interface IDocumentsClient
    {
        void DocumentAdded(Document document);
        
    }

    public class DocumentsHub : Hub
    {
        public async override Task OnConnectedAsync()
        {

            Debug.WriteLine($"ConnectionId {this.Context.ConnectionId}");

            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");

            await base.OnConnectedAsync();
        }

        public void DocumentAdded(Document document)
        {
           this.Clients.Others.SendAsync("Added", document);

           this.Clients.Group("SignalR Users").SendAsync("Added", document);

          
        }
    }
}