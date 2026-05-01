using System;
using System.Linq;
namespace Task5
{
    public interface IHtmlCommand
    {
        void Execute();
        void Undo();
    }

    public sealed class AddClassCommand : IHtmlCommand
    {
        private readonly LightElementNode element;
        private readonly string className;
        private bool executed;
        private bool actuallyAdded;

        public AddClassCommand(LightElementNode element, string className)
        {
            this.element = element ?? throw new ArgumentNullException(nameof(element));
            this.className = className ?? throw new ArgumentNullException(nameof(className));
        }

        public void Execute()
        {
            actuallyAdded = !element.Classes.Contains(className);
            element.AddClass(className);
            executed = true;
        }

        public void Undo()
        {
            if (executed && actuallyAdded)
            {
                element.RemoveClass(className);
            }
        }
    }

    public sealed class RemoveClassCommand : IHtmlCommand
    {
        private readonly LightElementNode element;
        private readonly string className;
        private bool executed;
        private bool actuallyRemoved;

        public RemoveClassCommand(LightElementNode element, string className)
        {
            this.element = element ?? throw new ArgumentNullException(nameof(element));
            this.className = className ?? throw new ArgumentNullException(nameof(className));
        }

        public void Execute()
        {
            actuallyRemoved = element.Classes.Contains(className);
            element.RemoveClass(className);
            executed = true;
        }

        public void Undo()
        {
            if (executed && actuallyRemoved)
            {
                element.AddClass(className);
            }
        }
    }

    public sealed class SetStylesCommand : IHtmlCommand
    {
        private readonly LightElementNode element;
        private readonly string newStyles;
        private string previousStyles;
        private bool executed;

        public SetStylesCommand(LightElementNode element, string newStyles)
        {
            this.element = element ?? throw new ArgumentNullException(nameof(element));
            this.newStyles = newStyles ?? string.Empty;
        }

        public void Execute()
        {
            previousStyles = element.Styles;
            element.SetStyles(newStyles);
            executed = true;
        }

        public void Undo()
        {
            if (executed)
            {
                element.SetStyles(previousStyles);
            }
        }
    }

    public sealed class AddChildCommand : IHtmlCommand
    {
        private readonly LightElementNode parent;
        private readonly LightNode child;
        private bool executed;

        public AddChildCommand(LightElementNode parent, LightNode child)
        {
            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
            this.child = child ?? throw new ArgumentNullException(nameof(child));
        }

        public void Execute()
        {
            parent.AddChild(child);
            executed = true;
        }

        public void Undo()
        {
            if (executed)
            {
                parent.RemoveChild(child);
            }
        }
    }

    public sealed class RemoveChildCommand : IHtmlCommand
    {
        private readonly LightElementNode parent;
        private readonly LightNode child;
        private int previousIndex = -1;
        private bool executed;

        public RemoveChildCommand(LightElementNode parent, LightNode child)
        {
            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
            this.child = child ?? throw new ArgumentNullException(nameof(child));
        }

        public void Execute()
        {
            previousIndex = parent.IndexOfChild(child);
            if (previousIndex < 0)
            {
                throw new InvalidOperationException("Child is not attached to the parent.");
            }

            executed = parent.RemoveChild(child);
        }

        public void Undo()
        {
            if (executed && previousIndex >= 0)
            {
                parent.InsertChild(previousIndex, child);
            }
        }
    }
}