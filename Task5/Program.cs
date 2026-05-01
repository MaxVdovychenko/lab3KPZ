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
            var strong = new LightElementNode("strong", DisplayType.Inline, ClosingType.Pair);
            strong.AddChild(new LightTextNode("Item 2"));
            li2.AddChild(strong);

            var li3 = new LightElementNode("li", DisplayType.Block, ClosingType.Pair);
            li3.AddChild(new LightTextNode("Item 3"));

            ul.AddChild(li1);
            ul.AddChild(li2);
            ul.AddChild(li3);

            Console.WriteLine(ul.OuterHTML());
            Console.WriteLine();

            Console.WriteLine("Depth-first traversal:");
            IHtmlTreeIterator dfs = ul.CreateDepthFirstIterator();
            while (dfs.MoveNext())
            {
                Console.WriteLine(DescribeNode(dfs.Current));
            }

            Console.WriteLine();
            Console.WriteLine("Breadth-first traversal:");
            IHtmlTreeIterator bfs = ul.CreateBreadthFirstIterator();
            while (bfs.MoveNext())
            {
                Console.WriteLine(DescribeNode(bfs.Current));
            }

            Console.WriteLine();
            Console.WriteLine("UL log: " + string.Join(" | ", ul.LifecycleLog));
            Console.WriteLine("LI1 log: " + string.Join(" | ", li1.LifecycleLog));
            Console.WriteLine("TEXT log: " + string.Join(" | ", ((LightTextNode)li1.Children[0]).LifecycleLog));
        }

        private static string DescribeNode(LightNode node)
        {
            if (node is LightElementNode element)
            {
                return $"<{element.TagName}>";
            }

            if (node is LightTextNode textNode)
            {
                return $"\"{textNode.Text}\"";
            }

            return node.GetType().Name;
        }
    }
}