
namespace XVisualizer.Strings.Jsons.XJson
{
    public class XJString : XJToken
    {
        public XJString(string str) : base(JsonType.String)
        {
            this.Value = str;
        }
        public override string ValueString()
        {
            return string.Format("\"{0}\"", Value ?? "null");
        }

        public override string ToString()
        {
            return string.Format("\"{0}\" : {1}", Name, ValueString());
        }
    }
}