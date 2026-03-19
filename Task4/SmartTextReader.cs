using System.IO;

namespace Task4
{
    public class SmartTextReader : ISmartTextReader
    {
        public char[][] Read(string path)
        {
            string[] lines = File.ReadAllLines(path);
            char[][] result = new char[lines.Length][];

            for (int i = 0; i < lines.Length; i++)
            {
                result[i] = lines[i].ToCharArray();
            }

            return result;
        }
    }
}