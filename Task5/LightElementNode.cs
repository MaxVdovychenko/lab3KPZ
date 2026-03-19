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
        private string tagName;
        private DisplayType displayType;
        private ClosingType closingType;
        private List<string> classes = new List<string>();
        private List<LightNode> children = new List<LightNode>();

        public LightElementNode(string tagName, DisplayType displayType, ClosingType closingType)
        {
            this.tagName = tagName;
            this.displayType = displayType;
            this.closingType = closingType;
        }

        public void AddClass(string className)
        {
            classes.Add(className);
        }

        public void AddChild(LightNode node)
        {
            children.Add(node);
        }

        public int ChildCount()
        {
            return children.Count;
        }

        public override string InnerHTML()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var child in children)
            {
                sb.Append(child.OuterHTML());
            }

            return sb.ToString();
        }

        public override string OuterHTML()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<" + tagName);

            if (classes.Count > 0)
            {
                sb.Append(" class=\"");
                sb.Append(string.Join(" ", classes));
                sb.Append("\"");
            }

            if (closingType == ClosingType.Single)
            {
                sb.Append("/>");
                return sb.ToString();
            }

            sb.Append(">");
            sb.Append(InnerHTML());
            sb.Append("</" + tagName + ">");

            return sb.ToString();
        }
    }
}