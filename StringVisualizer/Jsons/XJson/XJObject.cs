using System.Linq;

namespace StringVisualizer.Jsons.XJson
{
    public class XJObject : XJToken
    {
        public XJObject() : base(JsonType.Object)
        {

        }
        public override string ValueString()
        {
            if (this.Children == null)
                return "{}";
            var strs = this.Children.Select(x => x.ToString()).ToList();
            return "{" + string.Join(" , ", strs) + "}";
        }
    }
}