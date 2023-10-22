using System;

namespace WebScraper1
{
  internal class Program
  {
    static void Main(string[] args)
    {
      while (true)
      {
        Console.Write("Enter chosen number: ");

        string choice = Console.ReadLine();

        Console.Clear();

        Console.Write($"Enter chosen number: {choice}");

        Console.WriteLine("\nLoading...");

        Problem problem = new Problem(Convert.ToInt32(choice));

        Console.Clear();

        Console.WriteLine(problem);

        Console.WriteLine();
      }

      // Console.ReadKey();
    }
  }
}
