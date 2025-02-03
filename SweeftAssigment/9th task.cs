using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    private static bool _isRunning = true;

    static async Task Main(string[] args)
    {
        await CountryDataGenerator.GenerateCountryDataFiles();  
        Console.WriteLine("Press any key to stop the application !!");

        var binaryTask = PrintBinaryNumbersAsync();
        var messageTask = ShowPeriodicMessageAsync();

        Console.ReadKey();
        _isRunning = false;

        await Task.WhenAll(binaryTask, messageTask);
    }

    static async Task PrintBinaryNumbersAsync()
    {
        while (_isRunning)
        {
            await _semaphore.WaitAsync();
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("1");
                Console.Write("0");
            }
            finally
            {
                _semaphore.Release();
            }
            await Task.Delay(100);
        }
    }

    static async Task ShowPeriodicMessageAsync()
    {
        while (_isRunning)
        {
            await Task.Delay(5000);

            await _semaphore.WaitAsync();
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nNeo, you are the chosen one");
                await Task.Delay(5000);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
