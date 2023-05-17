#if DEBUG
using System;

#endif

namespace HKW.ViewModels;

internal class Program
{
    private static void Main(string[] args)
    {
#if DEBUG
        Console.WriteLine("Hello, World!");
#endif
    }
}
