using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorLearning;

public class CoreHub
{
    const string HubUrl = "https://localhost:5001/hub";
    public HubConnection Hub { get; set; }
    
    public async Task InitAsync()
    {
        Hub = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .Build();

        Hub.On<string>("NotifyConnection", async (string msg) =>
        {
            Console.WriteLine(msg);
        });

        await Hub.StartAsync();

        while (Hub.State != HubConnectionState.Connected)
        {
            await Task.Delay(1000);
            Console.WriteLine("Connecting...");
        }
        
        Console.WriteLine("Connected!");

        await Hub.InvokeAsync("Connect");
    }
}