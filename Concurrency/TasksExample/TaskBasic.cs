namespace CSharpAppPlayground.Concurrency.TasksExample
{
    public class TaskBasic
    {
        public async Task<string> ShowAsync()
        {

            // if await is not used, the method will return immediately
            await Task.Delay(1000); // Simulate work

            return $"task done on thread {Environment.CurrentManagedThreadId}";
        }

        public Task SimulateFaultedTask()
        {
            return Task.FromException(new Exception("Simulated fault"));
        }
    }
}
