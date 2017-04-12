using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace XVisualizer.Strings.Xmls
{
    public partial class XmlViewer : BaseViewer
    {
        public XmlViewer()
        {
            InitializeComponent();

            this.tvXml.DrawMode = TreeViewDrawMode.OwnerDrawText;
            this.tvXml.DrawNode += TvXml_DrawNode;
        }

        public override void SetContent(string value)
        {
            try
            {
                tvXml.BeginUpdate();
                XmlDocument XMLdoc = new XmlDocument();
                XMLdoc.LoadXml(value);
                var root = XMLdoc.FirstChild;
                if (root.NodeType == XmlNodeType.XmlDeclaration)
                {
                    this.AddNode(this.tvXml.Nodes, root);
                }
                root = root.NextSibling;
                if (root != null)
                {
                    this.AddNode(this.tvXml.Nodes, root);
                }
                foreach (TreeNode node in this.tvXml.Nodes)
                {
                    node.Expand();
                }
            }
            finally
            {
                tvXml.EndUpdate();
            }
        }

        private void AddNode(TreeNodeCollection nodes, XmlNode ele)
        {
            var node = new XmlTreeNode();
            var obj = new XmlObject(ele);
            node.SetXml(obj);
            nodes.Add(node);

            if (!obj.IsText && !obj.IsDeclaration)
            {
                foreach (XmlNode childNode in ele.ChildNodes)
                {
                    AddNode(node.Nodes, childNode);
                }
            }
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            XmlTreeNode node = GetSelectedTreeNode();
            if (node.Xml != null)
            {
                if (node.IsClose)
                {
                    node = node.OpenNode;
                }
                var fullText = GetFormatXml(node);
                SetClipboard(fullText);
            }
            else
            {
                SetClipboard("");
            }
        }

        private string GetFormatXml(XmlTreeNode node, int level = 0)
        {
            var sn = level * 2;
            string str = new string(' ', sn) + node.Text + Environment.NewLine;
            foreach (XmlTreeNode n in node.Nodes)
            {
                if (n.IsClose)
                {
                    continue;
                }
                str += GetFormatXml(n, level + 1);
            }
            if (node.CloseNode != null)
            {
                str += new string(' ', sn) + node.CloseNode.Text + Environment.NewLine;
            }
            return str;
        }

        private void mnuTree_Opening(object sender, CancelEventArgs e)
        {
            var node = GetSelectedTreeNode();
            mnuCopy.Enabled = node != null;
        }

        private void tvXml_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = tvXml.GetNodeAt(e.Location);
                if (node != null)
                {
                    tvXml.SelectedNode = node;
                }
            }
        }
        private XmlTreeNode GetSelectedTreeNode()
        {
            return (XmlTreeNode)tvXml.SelectedNode;
        }

        private void SetClipboard(string text)
        {
            try
            {
                Clipboard.SetText(text ?? "");
            }
            catch (Exception ex)
            {
                ShowInfo(ex.Message, true);
            }
        }

        private void tvXml_AfterExpandOrCollapse(object sender, TreeViewEventArgs e)
        {
            XmlTreeNode obj = e.Node as XmlTreeNode;
            if (obj?.CloseNode != null)
            {
                var nodes = obj.Parent?.Nodes ?? this.tvXml.Nodes;
                if (e.Action == TreeViewAction.Expand)
                    nodes.Insert(obj.Index + 1, obj.CloseNode);
                else if (e.Action == TreeViewAction.Collapse)
                    nodes.Remove(obj.CloseNode);
            }
        }

        private readonly Brush tagColor = Brushes.DarkMagenta,
            txtColor = Brushes.Black,
            atrColor = Brushes.Red,
            valColor = Brushes.Green,
            makColor = Brushes.Blue;
        private void TvXml_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            int top = e.Bounds.Top + 2;
            int left = e.Bounds.Left;
            var font = this.tvXml.Font;

            var node = e.Node as XmlTreeNode;
            var xml = node.Xml;

            int wLetter = (int)(e.Graphics.MeasureString("-", font).Width - e.Graphics.MeasureString(" ", font).Width);

            if (xml.IsDeclaration)
            {
                e.Graphics.DrawString("<?xml", font, makColor, left, top);
                left += wLetter * "<?xml".Length;

                var kvs = xml.InnerText.Split(' ');
                foreach (var item in kvs)
                {
                    var kv = item.Split('=');
                    left += wLetter;

                    e.Graphics.DrawString(kv[0], font, atrColor, left, top);
                    left += wLetter * kv[0].Length;

                    e.Graphics.DrawString("=", font, makColor, left, top);
                    left += wLetter;

                    e.Graphics.DrawString(kv[1], font, valColor, left, top);
                    left += wLetter * kv[1].Length;
                }

                e.Graphics.DrawString("?>", font, makColor, left, top);
            }
            else if (xml.IsText)
            {
                e.Graphics.DrawString(xml.InnerText, font, txtColor, left, top);
            }
            else if (node.IsClose)
            {
                e.Graphics.DrawString("</", font, makColor, left, top);
                left += wLetter * "</".Length;

                e.Graphics.DrawString(xml.Name, font, tagColor, left, top);
                left += wLetter * xml.Name.Length;

                e.Graphics.DrawString(">", font, makColor, left, top);
            }
            else
            {
                e.Graphics.DrawString("<", font, makColor, left, top);
                left += wLetter;

                e.Graphics.DrawString(xml.Name, font, tagColor, left, top);
                left += wLetter * xml.Name.Length;

                if (xml.Attrs.Any())
                {
                    foreach (var attr in xml.Attrs)
                    {
                        left += wLetter;

                        e.Graphics.DrawString(attr.Key, font, atrColor, left, top);
                        left += wLetter * attr.Key.Length;

                        e.Graphics.DrawString("=", font, makColor, left, top);
                        left += wLetter;

                        e.Graphics.DrawString("\"" + attr.Value + "\"", font, valColor, left, top);
                        left += wLetter * (attr.Value.Length + 2);
                    }
                }

                e.Graphics.DrawString(xml.AutoClose ? "/>" : ">", font, makColor, left, top);
            }
        }
    }
}
