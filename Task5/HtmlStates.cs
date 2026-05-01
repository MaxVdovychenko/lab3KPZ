using System;

namespace Task5
{
    public interface IElementState
    {
        string Name { get; }

        void AddChild(LightElementNode element, LightNode child);
        void InsertChild(LightElementNode element, int index, LightNode child);
        bool RemoveChild(LightElementNode element, LightNode child);

        void AddClass(LightElementNode element, string className);
        bool RemoveClass(LightElementNode element, string className);

        void SetStyles(LightElementNode element, string styles);
    }

    public abstract class ElementStateBase : IElementState
    {
        public abstract string Name { get; }

        public virtual void AddChild(LightElementNode element, LightNode child)
        {
            throw Locked("add children", element);
        }

        public virtual void InsertChild(LightElementNode element, int index, LightNode child)
        {
            throw Locked("insert children", element);
        }

        public virtual bool RemoveChild(LightElementNode element, LightNode child)
        {
            throw Locked("remove children", element);
        }

        public virtual void AddClass(LightElementNode element, string className)
        {
            throw Locked("add classes", element);
        }

        public virtual bool RemoveClass(LightElementNode element, string className)
        {
            throw Locked("remove classes", element);
        }

        public virtual void SetStyles(LightElementNode element, string styles)
        {
            throw Locked("set styles", element);
        }

        protected InvalidOperationException Locked(string operation, LightElementNode element)
        {
            return new InvalidOperationException(
                $"{element.TagName} is in state '{Name}' and cannot {operation}.");
        }
    }

    public sealed class DraftState : ElementStateBase
    {
        public override string Name => "Draft";

        public override void AddChild(LightElementNode element, LightNode child)
        {
            element.AddChildCore(child);
        }

        public override void InsertChild(LightElementNode element, int index, LightNode child)
        {
            element.InsertChildCore(index, child);
        }

        public override bool RemoveChild(LightElementNode element, LightNode child)
        {
            return element.RemoveChildCore(child);
        }

        public override void AddClass(LightElementNode element, string className)
        {
            element.AddClassCore(className);
        }

        public override bool RemoveClass(LightElementNode element, string className)
        {
            return element.RemoveClassCore(className);
        }

        public override void SetStyles(LightElementNode element, string styles)
        {
            element.SetStylesCore(styles);
        }
    }

    public sealed class PublishedState : ElementStateBase
    {
        public override string Name => "Published";
    }
}