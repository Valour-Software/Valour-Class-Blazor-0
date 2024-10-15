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
        Clients.AllExcept(Context.ConnectionId).SendAsync("NotifyConnection", Context.ConnectionId);
        Clients.Client(Context.ConnectionId)
            .SendAsync("Setup", Context.ConnectionId, ConnectionIds);
    }

    public async Task SendBallPosition(float posX, float posY)
    {
        await Clients.AllExcept(Context.ConnectionId)
            .SendAsync("BallPosition", posX, posY, Context.ConnectionId);
    }
}