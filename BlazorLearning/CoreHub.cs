using System.Text.Json;
using BlazorLearning.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorLearning;

public class CoreHub
{
    const string HubUrl = "http://192.168.0.226:5000/hub";
    public HubConnection Hub { get; set; }
    public event Action SetupCompleted;
    public event Action ConnectionChange;
    
    public string Id { get; set; }

    public List<ClientData> ConnectedClients = new();
    
    public async Task InitAsync()
    {
        Hub = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .Build();

        Hub.On<string>("NotifyConnection", async (string id) =>
        {
            Console.WriteLine($"{id} has connected!");
            if (!ConnectedClients.Any(x => x.Id == id))
                ConnectedClients.Add(new ClientData(){ Id = id });
            
            if (ConnectionChange is not null)
                ConnectionChange.Invoke();
        });

        Hub.On<string, List<string>>("Setup", (string id, List<string> connections) =>
        {
            Id = id;
            ConnectedClients.AddRange(connections.Select(x => new ClientData(){ Id = x }));
            
            Console.WriteLine("Connections: " + JsonSerializer.Serialize(connections));
            
            if (SetupCompleted is not null)
                SetupCompleted.Invoke();
        });

        Hub.On("ReceiveBallData", (float posX, float posY, float velX, float velY, float hueDegrees, DateTime time, string id) =>
        {
            var client = ConnectedClients.FirstOrDefault(x => x.Id == id);
            if (client is not null)
                client.Square.SetData(posX, posY, velX, velY, hueDegrees, time);
            
            Console.WriteLine($"Position updated for {id} to {posX}, {posY}");
        });
        
        Hub.On("NotifyDisconnection", (string id) =>
        {
            Console.WriteLine($"{id} has disconnected!");
            ConnectedClients.RemoveAll(x => x.Id == id);
            
            if (ConnectionChange is not null)
                ConnectionChange.Invoke();
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

public class ClientData
{
    public string Id { get; set; }
    public ColorSquare Square { get; set; }
}