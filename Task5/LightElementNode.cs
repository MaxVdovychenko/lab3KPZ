using System.Collections.Generic;
using System.Text;

namespace Task5
{
    public enum DisplayType
    {
        Block,
        Inline
    }

    public enum ClosingType
    {
        Single,
        Pair
    }

    public class LightElementNode : LightNode
    {
        private readonly string tagName;
        private readonly DisplayType displayType;
        private readonly ClosingType closingType;
        private readonly List<string> classes = new List<string>();
        private readonly List<LightNode> children = new List<LightNode>();
        private string inlineStyles = string.Empty;
        private IElementState state = new DraftState();

        public string TagName => tagName;
        public DisplayType DisplayType => displayType;
        public ClosingType ClosingType => closingType;
        public string Styles => inlineStyles;
        public string CurrentStateName => state.Name;
        public IReadOnlyList<string> Classes => classes.AsReadOnly();
        public IReadOnlyList<LightNode> Children => children.AsReadOnly();
        public int ChildCount => children.Count;

        public LightElementNode(string tagName, DisplayType displayType, ClosingType closingType)
        {
            this.tagName = tagName;
            this.displayType = displayType;
            this.closingType = closingType;
            OnCreated();
        }

        public override void Accept(ILightNodeVisitor visitor)
        {
            visitor.VisitElement(this);
        }

        public void Publish()
        {
            state = new PublishedState();
            Trace("State changed to Published");
        }

        public void Reopen()
        {
            state = new DraftState();
            Trace("State changed to Draft");
        }

        public void AddClass(string className)
        {
            state.AddClass(this, className);
        }

        public bool RemoveClass(string className)
        {
            return state.RemoveClass(this, className);
        }

        public bool HasClass(string className)
        {
            return classes.Contains(className);
        }

        public void SetStyles(string styles)
        {
            state.SetStyles(this, styles);
        }

        public void AddChild(LightNode node)
        {
            state.AddChild(this, node);
        }

        public void InsertChild(int index, LightNode node)
        {
            state.InsertChild(this, index, node);
        }

        public bool RemoveChild(LightNode node)
        {
            return state.RemoveChild(this, node);
        }

        public int IndexOfChild(LightNode node)
        {
            return children.IndexOf(node);
        }

        internal void AddClassCore(string className)
        {
            if (!classes.Contains(className))
            {
                classes.Add(className);
                OnClassListApplied(className);
            }
        }

        internal bool RemoveClassCore(string className)
        {
            return classes.Remove(className);
        }

        internal void SetStylesCore(string styles)
        {
            inlineStyles = styles;
            OnStylesApplied();
        }

        internal void AddChildCore(LightNode node)
        {
            children.Add(node);
            node.RaiseInserted(this);
        }

        internal void InsertChildCore(int index, LightNode node)
        {
            children.Insert(index, node);
            node.RaiseInserted(this);
        }

        internal bool RemoveChildCore(LightNode node)
        {
            bool removed = children.Remove(node);
            if (removed)
            {
                node.RaiseRemoved(this);
            }

            return removed;
        }

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();

            foreach (LightNode child in children)
            {
                sb.Append(child.OuterHTML());
            }

            return sb.ToString();
        }

        protected override string BuildOuterHTML()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('<').Append(tagName);

            if (classes.Count > 0)
            {
                sb.Append(" class=\"");
                sb.Append(string.Join(" ", classes));
                sb.Append('"');
            }

            if (!string.IsNullOrWhiteSpace(inlineStyles))
            {
                sb.Append(" style=\"");
                sb.Append(inlineStyles);
                sb.Append('"');
            }

            if (closingType == ClosingType.Single)
            {
                sb.Append("/>");
                return sb.ToString();
            }

            sb.Append('>');
            sb.Append(InnerHTML());
            sb.Append("</").Append(tagName).Append('>');
            return sb.ToString();
        }

        public IHtmlTreeIterator CreateDepthFirstIterator()
        {
            return new DepthFirstHtmlIterator(this);
        }

        public IHtmlTreeIterator CreateBreadthFirstIterator()
        {
            return new BreadthFirstHtmlIterator(this);
        }
    }
}