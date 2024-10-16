using Microsoft.AspNetCore.SignalR;

namespace BlazorLearning.Server;

public class CoreHub : Hub
{

    private static List<string> ConnectionIds = new();

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        ConnectionIds.Remove(Context.ConnectionId);
        await Clients.AllExcept(Context.ConnectionId).SendAsync("NotifyDisconnection", Context.ConnectionId);
    }

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

    public async Task SendBallData(
        float posX, 
        float posY,
        float velX,
        float velY,
        float hueDegrees,
        DateTime time
    )
    {
        await Clients.AllExcept(Context.ConnectionId)
            .SendAsync("ReceiveBallData", posX, posY, velX, velY, hueDegrees, time, Context.ConnectionId);
    }
}