using System.Linq;

namespace XVisualizer.Strings.Jsons.XJson
{
    public class XJArray : XJToken
    {
        public XJArray() : base(JsonType.Array)
        {

        }
        public override string ValueString()
        {
            if (this.Children == null)
                return "[]";
            var strs = this.Children.Select(x => x.ValueString()).ToList();
            return "[" + string.Join(" , ", strs) + "]";
        }
    }
}