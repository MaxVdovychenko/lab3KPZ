using System.Collections.Generic;

namespace Task6
{
    public class LightElementFactory
    {
        private Dictionary<string, LightElementNode> cache = new Dictionary<string, LightElementNode>();

        public LightElementNode GetElement(string tag)
        {
            if (!cache.ContainsKey(tag))
            {
                cache[tag] = new LightElementNode(tag);
            }

            return cache[tag];
        }
    }
}