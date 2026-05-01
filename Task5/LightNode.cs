using System;
using System.Collections.Generic;

namespace Task5
{
    public abstract class LightNode
    {
        private readonly List<string> lifecycleLog = new List<string>();

        public IReadOnlyList<string> LifecycleLog => lifecycleLog.AsReadOnly();

        protected void Trace(string message)
        {
            lifecycleLog.Add(message);
        }

        public string OuterHTML()
        {
            BeforeRender();
            string html = BuildOuterHTML();
            AfterRender(html);
            return html;
        }

        public virtual string InnerHTML()
        {
            return string.Empty;
        }

        protected abstract string BuildOuterHTML();

        protected virtual void BeforeRender()
        {
            Trace("BeforeRender");
        }

        protected virtual void AfterRender(string html)
        {
            Trace("AfterRender");
        }

        protected virtual void OnCreated()
        {
            Trace("OnCreated");
        }

        protected virtual void OnInserted(LightNode parent)
        {
            Trace($"OnInserted into {parent.GetType().Name}");
        }

        protected virtual void OnRemoved(LightNode parent)
        {
            Trace($"OnRemoved from {parent.GetType().Name}");
        }

        protected virtual void OnStylesApplied()
        {
            Trace("OnStylesApplied");
        }

        protected virtual void OnClassListApplied(string className)
        {
            Trace($"OnClassListApplied: {className}");
        }

        protected virtual void OnTextRendered()
        {
            Trace("OnTextRendered");
        }

        internal void RaiseInserted(LightNode parent)
        {
            OnInserted(parent);
        }

        internal void RaiseRemoved(LightNode parent)
        {
            OnRemoved(parent);
        }
    }
}