using System;
using System.Collections.Generic;
using System.IO;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Vdovy\source\repos\lab3KPZ\Task6\book.txt");

            List<LightNode> tree = new List<LightNode>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                LightElementNode node;

                if (i == 0)
                    node = new LightElementNode("h1");
                else if (line.Length < 20)
                    node = new LightElementNode("h2");
                else if (line.StartsWith(" "))
                    node = new LightElementNode("blockquote");
                else
                    node = new LightElementNode("p");

                node.AddChild(new LightTextNode(line));
                tree.Add(node);
            }

            Console.WriteLine("=== HTML ===");
            foreach (var node in tree)
                Console.WriteLine(node.OuterHTML());

            Console.WriteLine("\nMemory before Flyweight:");
            Console.WriteLine(GC.GetTotalMemory(true));

            Console.WriteLine("\nMemory after Flyweight:");
            LightElementFactory factory = new LightElementFactory();
            List<LightNode> flyTree = new List<LightNode>();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string tag;

                if (i == 0)
                    tag = "h1";
                else if (line.Length < 20)
                    tag = "h2";
                else if (line.StartsWith(" "))
                    tag = "blockquote";
                else
                    tag = "p";

                var node = factory.GetElement(tag);
                node.AddChild(new LightTextNode(line));
                flyTree.Add(node);
            }

            Console.WriteLine(GC.GetTotalMemory(true));
        }
    }
}