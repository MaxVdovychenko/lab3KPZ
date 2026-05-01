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

        public string TagName => tagName;
        public DisplayType DisplayType => displayType;
        public ClosingType ClosingType => closingType;
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

        public void AddClass(string className)
        {
            classes.Add(className);
            OnClassListApplied(className);
        }

        public bool RemoveClass(string className)
        {
            return classes.Remove(className);
        }

        public void SetStyles(string styles)
        {
            inlineStyles = styles;
            OnStylesApplied();
        }

        public void AddChild(LightNode node)
        {
            children.Add(node);
            node.RaiseInserted(this);
        }

        public void InsertChild(int index, LightNode node)
        {
            children.Insert(index, node);
            node.RaiseInserted(this);
        }

        public bool RemoveChild(LightNode node)
        {
            bool removed = children.Remove(node);
            if (removed)
            {
                node.RaiseRemoved(this);
            }

            return removed;
        }

        public int IndexOfChild(LightNode node)
        {
            return children.IndexOf(node);
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