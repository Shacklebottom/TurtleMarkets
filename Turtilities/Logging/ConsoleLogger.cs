namespace BusinessLogic.Logging
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"{DateTime.Now:M/d/y h:mm:ss tt}-=> {message}");
        }
    }
}
