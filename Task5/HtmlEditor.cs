using System.Collections.Generic;

namespace Task5
{
    public sealed class HtmlEditor
    {
        private readonly Stack<IHtmlCommand> history = new Stack<IHtmlCommand>();

        public int HistoryCount => history.Count;

        public void Execute(IHtmlCommand command)
        {
            command.Execute();
            history.Push(command);
        }

        public void UndoLast()
        {
            if (history.Count == 0)
            {
                return;
            }

            history.Pop().Undo();
        }
    }
}