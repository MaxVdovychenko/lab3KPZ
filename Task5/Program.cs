using System;
using System.Collections.Generic;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            var ul = new LightElementNode("ul", DisplayType.Block, ClosingType.Pair);

            var li1 = new LightElementNode("li", DisplayType.Block, ClosingType.Pair);
            li1.AddChild(new LightTextNode("Item 1"));

            var li2 = new LightElementNode("li", DisplayType.Block, ClosingType.Pair);
            li2.AddChild(new LightTextNode("Item 2"));

            var li3 = new LightElementNode("li", DisplayType.Block, ClosingType.Pair);
            li3.AddChild(new LightTextNode("Item 3"));

            ul.AddChild(li1);
            ul.AddChild(li2);
            ul.AddChild(li3);

            Console.WriteLine(ul.OuterHTML());
        }
    }
}