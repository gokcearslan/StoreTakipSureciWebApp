using Microsoft.AspNetCore.SignalR;

namespace twotableversion
{
    public class CrudHub : Hub
    {
        public async Task SendCrudOperation(string operation, string message)
        {
            await Clients.All.SendAsync("ReceiveCrudOperation", operation, message);
        }
    }
}


