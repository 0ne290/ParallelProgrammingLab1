using System.Diagnostics;

namespace ParallelProgramming;

public class Machine
{
    private Machine(string name, int capacity)
    {
        Name = name;

        _capacity = capacity;
    }
    
    public async Task Mill(int milliseconds) => await Task.Run(() =>
    {
        var stopWatch = new Stopwatch(); 
        stopWatch.Start(); 
        while (stopWatch.ElapsedMilliseconds < milliseconds) { } 
    });

    public bool IsAvailable() => _flow < _capacity;

    public void Hold(string detailName)
    {
        _flow++;
        DetailNames.Add(detailName);
    }
    
    public void Release(string detailName)
    {
        _flow--;
        DetailNames.Remove(detailName);
    }
    
    public static Machine[] GetMachinesByName(string[] machineNames) => Machines.Where(m => machineNames.Contains(m.Name)).ToArray();
    
    public static void CreateMachine(string name, int capacity)
    {
        var machine = new Machine(name, capacity);
        ((List<Machine>)Machines).Add(machine);
    }
    
    public override string ToString() => $"Станок {Name}; пропускная способность = {_capacity};";
    
    public string Name { get; }

    public List<string> DetailNames { get; } = new();
    
    public static IEnumerable<Machine> Machines { get; } = new List<Machine>();

    private readonly int _capacity;
    
    private int _flow;
}