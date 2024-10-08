namespace BlazorLearning.Utility;

public class AsyncVsSync
{
    public async Task MakeACakeAsync()
    {
        var tasks = new List<Task>()
        {
            MakeBatterAsync(),
            BakeCakeAsync(),
            FrostCakeAsync()
        };

        await Task.WhenAll(tasks);
    }
    
    public void MakeACake()
    {
        MakeBatter();
        BakeCake();
        FrostCake();
    }
    
    void MakeBatter()
    {
        Console.WriteLine("Making batter...");
        Thread.Sleep(2000);
        Console.WriteLine("Batter is ready!");
    }
    
    void BakeCake()
    {
        Console.WriteLine("Baking cake...");
        Thread.Sleep(5000);
        Console.WriteLine("Cake is ready!");
    }
    
    void FrostCake()
    {
        Console.WriteLine("Frosting cake...");
        Thread.Sleep(1000);
        Console.WriteLine("Cake is frosted!");
    }
    
    async Task MakeBatterAsync()
    {
        Console.WriteLine("Making batter...");
        await Task.Delay(2000);
        Console.WriteLine("Batter is ready!");
    }
    
    async Task BakeCakeAsync()
    {
        Console.WriteLine("Baking cake...");
        await Task.Delay(5000);
        Console.WriteLine("Cake is ready!");
    }
    
    async Task FrostCakeAsync()
    {
        Console.WriteLine("Frosting cake...");
        await Task.Delay(1000);
        Console.WriteLine("Cake is frosted!");
    }
}