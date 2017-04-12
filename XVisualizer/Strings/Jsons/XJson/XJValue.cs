
namespace XVisualizer.Strings.Jsons.XJson
{
    public class XJValue : XJToken
    {
        public XJValue(object obj) : base(JsonType.Value)
        {
            this.Value = obj;
        }
        public override string ValueString()
        {
            return string.Format("{0}", Value ?? "null").ToLower();
        }
        public override string ToString()
        {
            return string.Format("\"{0}\" : {1}", Name, ValueString());
        }
    }
}