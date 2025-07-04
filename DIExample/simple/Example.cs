/// <summary>
/// This is the most basic example of Dependency Injection in C#.
/// Doesn't use any DI framework or tools from .NET, but the concept is clear.
/// </summary>
public interface IExample
{
    void DoSomething();
}

public class ExampleA : IExample
{ 
    public void DoSomething()
    {
        Console.WriteLine("ExampleA is doing something. Read from file.");
    }
}

public class ExampleB : IExample
{
    public void DoSomething()
    {
        Console.WriteLine("ExampleB is doing something. Read from DB.");
    }
}

public class ExampleRunner
{
    private readonly IExample example;
    public ExampleRunner(IExample e)
    {
        example = e;
    }
    public void DoSomething()
    {
        example.DoSomething();
    }
}

public class SampleProgram()
{
    public void Run()
    {
        // dependency injection, ExampleA or ExampleB can be injected
        ExampleRunner theRunner = new ExampleRunner(new ExampleA());
        theRunner.DoSomething();
    }
}

