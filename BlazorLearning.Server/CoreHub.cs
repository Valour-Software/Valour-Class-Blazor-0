using Microsoft.AspNetCore.SignalR;

namespace BlazorLearning.Server;

public class CoreHub : Hub
{
    // Called by client
    public void Connect()
    {
        // Sent out to all clients
        Clients.All.SendAsync("NotifyConnection", $"{Context.ConnectionId} has connected!");
    }
}