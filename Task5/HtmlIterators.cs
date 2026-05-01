using System;
using System.Collections.Generic;

namespace Task5
{
    public interface IHtmlTreeIterator
    {
        LightNode Current { get; }
        bool MoveNext();
        void Reset();
    }

    public abstract class HtmlTreeIteratorBase : IHtmlTreeIterator
    {
        protected readonly LightNode Root;

        public LightNode Current { get; protected set; }

        protected HtmlTreeIteratorBase(LightNode root)
        {
            Root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public abstract bool MoveNext();
        public abstract void Reset();

        protected static void PushChildrenReversed(Stack<LightNode> stack, LightElementNode element)
        {
            for (int i = element.Children.Count - 1; i >= 0; i--)
            {
                stack.Push(element.Children[i]);
            }
        }
    }

    public sealed class DepthFirstHtmlIterator : HtmlTreeIteratorBase
    {
        private readonly Stack<LightNode> stack = new Stack<LightNode>();

        public DepthFirstHtmlIterator(LightNode root) : base(root)
        {
            Reset();
        }

        public override void Reset()
        {
            stack.Clear();
            stack.Push(Root);
            Current = null;
        }

        public override bool MoveNext()
        {
            if (stack.Count == 0)
            {
                Current = null;
                return false;
            }

            Current = stack.Pop();

            if (Current is LightElementNode element)
            {
                PushChildrenReversed(stack, element);
            }

            return true;
        }
    }

    public sealed class BreadthFirstHtmlIterator : HtmlTreeIteratorBase
    {
        private readonly Queue<LightNode> queue = new Queue<LightNode>();

        public BreadthFirstHtmlIterator(LightNode root) : base(root)
        {
            Reset();
        }

        public override void Reset()
        {
            queue.Clear();
            queue.Enqueue(Root);
            Current = null;
        }

        public override bool MoveNext()
        {
            if (queue.Count == 0)
            {
                Current = null;
                return false;
            }

            Current = queue.Dequeue();

            if (Current is LightElementNode element)
            {
                foreach (LightNode child in element.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return true;
        }
    }
}