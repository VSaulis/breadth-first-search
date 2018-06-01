using System;

namespace BreadthFirstSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            int i, j;

            string filename;

            Console.WriteLine("Please enter filename:");
            string input = Console.ReadLine();
            filename = input;
            Console.WriteLine("Please enter starting position i:");
            input = Console.ReadLine();
            Int32.TryParse(input, out i);
            Console.WriteLine("Please enter starting position j:");
            input = Console.ReadLine();
            Int32.TryParse(input, out j);

            string path = @"C:\Users\Vytautas\Desktop\AI\BreadthFirstSearch\BreadthFirstSearch\tests\";
            BreadthFirstSearch breadthFirstSearch = new BreadthFirstSearch(path + filename);

            if (breadthFirstSearch.Start(i - 1, j - 1)) {
                Log.AddToLog("Path found.");
                Log.WriteToFile();
            }
            else {
                Log.AddToLog("Path not found.");
                Log.WriteToFile();
            }
        }
    }
}
