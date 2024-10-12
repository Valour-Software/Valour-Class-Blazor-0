using Microsoft.AspNetCore.SignalR;

namespace BlazorLearning.Server;

public class CoreHub : Hub
{

    private static List<string> ConnectionIds = new();
    
    // Called by client
    public void Connect()
    {
        if (!ConnectionIds.Contains(Context.ConnectionId))
            ConnectionIds.Add(Context.ConnectionId);
        
        // Sent out to all clients
        Clients.All.SendAsync("NotifyConnection", Context.ConnectionId);
        Clients.Client(Context.ConnectionId).SendAsync("Setup", Context.ConnectionId, ConnectionIds);
    }
}