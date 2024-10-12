using BlazorLearning.Components;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorLearning;

public class CoreHub
{
    const string HubUrl = "https://localhost:5001/hub";
    public HubConnection Hub { get; set; }
    public event Action SetupCompleted;
    public event Action ConnectionAdded;
    
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
            
            if (ConnectionAdded is not null)
                ConnectionAdded.Invoke();
        });

        Hub.On<string, List<string>>("Setup", (string id, List<string> connections) =>
        {
            Id = id;
            ConnectedClients.AddRange(connections.Select(x => new ClientData(){ Id = x }));
            
            if (SetupCompleted is not null)
                SetupCompleted.Invoke();
        });

        Hub.On<int, int, string>("BallPosition", (int posX, int posY, string id) =>
        {
            var client = ConnectedClients.FirstOrDefault(x => x.Id == id);
            if (client is not null)
                client.Square.SetPosition(posX, posY);
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