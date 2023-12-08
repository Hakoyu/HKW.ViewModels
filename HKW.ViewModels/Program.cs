#if DEBUG
#endif

namespace HKW.HKWViewModels;

internal class Program
{
    private static void Main(string[] args)
    {
#if DEBUG
        Console.WriteLine("Hello, World!");
#endif
    }
}
