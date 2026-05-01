using System;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            var ul = new LightElementNode("ul", DisplayType.Block, ClosingType.Pair);
            ul.SetStyles("list-style: none; padding: 0;");

            var li1 = new LightElementNode("li", DisplayType.Block, ClosingType.Pair);
            li1.AddClass("item");
            li1.AddChild(new LightTextNode("Item 1"));

            var li2 = new LightElementNode("li", DisplayType.Block, ClosingType.Pair);
            li2.AddChild(new LightTextNode("Item 2"));

            var li3 = new LightElementNode("li", DisplayType.Block, ClosingType.Pair);
            li3.AddChild(new LightTextNode("Item 3"));

            ul.AddChild(li1);
            ul.AddChild(li2);
            ul.AddChild(li3);

            Console.WriteLine(ul.OuterHTML());
            Console.WriteLine();
            Console.WriteLine("UL log: " + string.Join(" | ", ul.LifecycleLog));
            Console.WriteLine("LI1 log: " + string.Join(" | ", li1.LifecycleLog));
            Console.WriteLine("TEXT log: " + string.Join(" | ", ((LightTextNode)li1.Children[0]).LifecycleLog));
        }
    }
}