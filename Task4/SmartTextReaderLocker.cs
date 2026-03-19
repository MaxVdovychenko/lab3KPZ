using System;
using System.Text.RegularExpressions;

namespace Task4
{
    public class SmartTextReaderLocker : ISmartTextReader
    {
        private readonly ISmartTextReader reader;
        private readonly Regex regex;

        public SmartTextReaderLocker(ISmartTextReader reader, string pattern)
        {
            this.reader = reader;
            this.regex = new Regex(pattern);
        }

        public char[][] Read(string path)
        {
            if (regex.IsMatch(path))
            {
                Console.WriteLine("Access denied!");
                return null;
            }

            return reader.Read(path);
        }
    }
}