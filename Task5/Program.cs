using System;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            var editor = new HtmlEditor();

            var article = new LightElementNode("article", DisplayType.Block, ClosingType.Pair);
            var title = new LightElementNode("h1", DisplayType.Block, ClosingType.Pair);
            title.AddChild(new LightTextNode("Command Pattern Demo"));

            var paragraph = new LightElementNode("p", DisplayType.Block, ClosingType.Pair);
            paragraph.AddChild(new LightTextNode("This tree is edited through commands."));

            editor.Execute(new AddClassCommand(article, "post"));
            editor.Execute(new SetStylesCommand(article, "padding: 12px; border: 1px solid #ccc;"));
            editor.Execute(new AddChildCommand(article, title));
            editor.Execute(new AddChildCommand(article, paragraph));

            Console.WriteLine("After commands:");
            Console.WriteLine(article.OuterHTML());
            Console.WriteLine("History count: " + editor.HistoryCount);

            editor.UndoLast();
            Console.WriteLine();
            Console.WriteLine("After one undo:");
            Console.WriteLine(article.OuterHTML());
            Console.WriteLine("History count: " + editor.HistoryCount);

            editor.UndoLast();
            Console.WriteLine();
            Console.WriteLine("After second undo:");
            Console.WriteLine(article.OuterHTML());
            Console.WriteLine("History count: " + editor.HistoryCount);
        }
    }
}