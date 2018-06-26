using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace StringVisualizer.Xmls
{
    class XmlObject
    {
        public string OpenText
        {
            get
            {
                var txt = "<" + Name;
                var ats = string.Join(" ", this.Attrs.Select(x => x.Key + "=\"" + x.Value + "\""));
                if (!string.IsNullOrWhiteSpace(ats))
                {
                    txt += " " + ats;
                }
                if (AutoClose)
                {
                    txt += "/";
                }
                txt += ">";
                return txt;
            }
        }

        public string InnerText { get; private set; }

        public string CloseText
        {
            get { return AutoClose ? "" : string.Format("</{0}>", Name); }
        }

        public bool AutoClose { get; private set; }
        public bool IsText { get; private set; }
        public bool IsDeclaration { get; private set; }
        public string FullText { get; private set; }

        public string Name { get; private set; }
        public Dictionary<string, string> Attrs { get; set; }

        public XmlObject(XmlNode element)
        {
            this.FullText = element.OuterXml;
            if (element.NodeType == XmlNodeType.XmlDeclaration)
            {
                this.IsDeclaration = true;
                this.InnerText = element.InnerText;
            }
            else if (element is XmlText)
            {
                this.IsText = true;
                this.InnerText = element.InnerText;
            }
            else
            {
                this.Name = element.Name;
                this.Attrs = element.Attributes.OfType<XmlAttribute>().ToDictionary(x => x.Name, x => x.Value);
                this.AutoClose = !element.HasChildNodes && this.FullText.EndsWith("/>");
            }
        }
    }
}