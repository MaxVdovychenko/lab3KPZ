using System;

namespace Task4
{
    public class SmartTextChecker : ISmartTextReader
    {
        private readonly ISmartTextReader reader;

        public SmartTextChecker(ISmartTextReader reader)
        {
            this.reader = reader;
        }

        public char[][] Read(string path)
        {
            Console.WriteLine("Opening file...");

            var data = reader.Read(path);

            Console.WriteLine("Reading file...");

            int lines = data.Length;
            int chars = 0;

            foreach (var line in data)
            {
                chars += line.Length;
            }

            Console.WriteLine($"Lines: {lines}");
            Console.WriteLine($"Chars: {chars}");
            Console.WriteLine("Closing file...");

            return data;
        }
    }
}