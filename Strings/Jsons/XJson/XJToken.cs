using System.Collections.Generic;

namespace XVisualizer.Strings.Jsons.XJson
{
    public abstract class XJToken
    {
        protected XJToken(JsonType type)
        {
            this.JsonType = type;
        }
        public JsonType JsonType { get; private set; }
        public bool IsRoot { get; protected internal set; }
        public string Name { get; protected internal set; }
        public object Value { get; protected internal set; }
        public XJToken Parent { get; protected internal set; }
        public List<XJToken> Children { get; protected internal set; }
        public abstract string ValueString();
        public override string ToString()
        {
            return IsRoot
                ? ValueString()
                : string.Format("\"{0}\" : {1}", Name, ValueString());
        }
    }
}