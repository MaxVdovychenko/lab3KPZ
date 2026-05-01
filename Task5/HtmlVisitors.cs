using System;
using System.Collections.Generic;

namespace Task5
{
    public interface ILightNodeVisitor
    {
        void VisitElement(LightElementNode element);
        void VisitText(LightTextNode textNode);
    }

    public sealed class HtmlStatisticsVisitor : ILightNodeVisitor
    {
        public int ElementCount { get; private set; }
        public int TextNodeCount { get; private set; }
        public int TotalTextLength { get; private set; }
        public int TotalClassCount { get; private set; }

        public void VisitElement(LightElementNode element)
        {
            ElementCount++;
            TotalClassCount += element.Classes.Count;

            foreach (LightNode child in element.Children)
            {
                child.Accept(this);
            }
        }

        public void VisitText(LightTextNode textNode)
        {
            TextNodeCount++;
            TotalTextLength += textNode.Text.Length;
        }

        public override string ToString()
        {
            return $"Elements: {ElementCount}, Text nodes: {TextNodeCount}, Classes: {TotalClassCount}, Text length: {TotalTextLength}";
        }
    }

    public sealed class TagCollectorVisitor : ILightNodeVisitor
    {
        private readonly HashSet<string> tags = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public IReadOnlyCollection<string> Tags => tags;

        public void VisitElement(LightElementNode element)
        {
            tags.Add(element.TagName);

            foreach (LightNode child in element.Children)
            {
                child.Accept(this);
            }
        }

        public void VisitText(LightTextNode textNode)
        {
        }
    }
}