using System.Windows.Forms;

namespace XVisualizer.Strings.Xmls
{
    class XmlTreeNode : TreeNode
    {
        public XmlTreeNode CloseNode { get; private set; }
        public XmlTreeNode OpenNode { get; private set; }
        public XmlObject Xml { get; private set; }
        public bool IsClose { get; private set; }

        public void SetXml(XmlObject xml)
        {
            this.Xml = xml;
            if (xml.IsDeclaration)
            {
                this.Text = xml.FullText;
            }
            else if (xml.IsText)
            {
                this.Text = xml.InnerText;
            }
            else
            {
                this.Text = xml.OpenText;
                if (!xml.AutoClose)
                {
                    CloseNode = new XmlTreeNode { Xml = xml, Text = xml.CloseText, IsClose = true, OpenNode = this };
                }
            }
        }
    }
}