using System.Collections.Generic;
using System.IO;

namespace BreadthFirstSearch {
    public static class Log {
        private static readonly List<string> _log = new List<string>();
        private static readonly string _filePath = @"C:\Users\Vytautas\Desktop\AI\BreadthFirstSearch\BreadthFirstSearch\result\result.txt";

        public static void AddToLog(string record) {
            _log.Add(record);
        }

        public static void WriteToFile() {
            File.WriteAllLines(_filePath, _log);
        }

    }
}