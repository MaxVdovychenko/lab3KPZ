namespace Task5
{
    public class LightTextNode : LightNode
    {
        private readonly string text;

        public string Text => text;

        public LightTextNode(string text)
        {
            this.text = text;
            OnCreated();
        }

        public override string InnerHTML()
        {
            return text;
        }

        protected override string BuildOuterHTML()
        {
            OnTextRendered();
            return text;
        }
    }
}