namespace XVisualizer.Strings.Jsons.XJson
{
    public static class XJson
    {
        public static XJToken Parse(string json)
        {
            var reader = new XJReader(json);
            return reader.Read();
        }
    }
}
