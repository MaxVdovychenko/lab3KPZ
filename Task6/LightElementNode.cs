using System.Collections.Generic;
using System.Text;

namespace Task6
{
    public class LightElementNode : LightNode
    {
        private string tag;
        private List<LightNode> children = new List<LightNode>();

        public LightElementNode(string tag)
        {
            this.tag = tag;
        }

        public void AddChild(LightNode node)
        {
            children.Add(node);
        }

        public override string OuterHTML()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"<{tag}>");

            foreach (var child in children)
                sb.Append(child.OuterHTML());

            sb.Append($"</{tag}>");

            return sb.ToString();
        }
    }
}