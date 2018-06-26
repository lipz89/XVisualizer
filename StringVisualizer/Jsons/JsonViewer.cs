using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using StringVisualizer.Jsons.XJson;

namespace StringVisualizer.Jsons
{
    public partial class JsonViewer : BaseViewer
    {
        public override void SetContent(string value)
        {
            try
            {
                tvJson.BeginUpdate();
                var jsonTree = XJson.XJson.Parse(value);
                VisualizeJsonTree(jsonTree);
            }
            finally
            {
                tvJson.EndUpdate();
            }
        }

        public JsonViewer()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void VisualizeJsonTree(XJToken tree)
        {
            AddNode(tvJson.Nodes, tree);
            TreeNode node = GetRootNode();
            node.Expand();
            tvJson.SelectedNode = node;
        }

        private void AddNode(TreeNodeCollection nodes, XJToken jsonObject)
        {
            TreeNode newNode = new TreeNode();
            nodes.Add(newNode);
            newNode.Text = GetShowText(jsonObject);
            newNode.Tag = jsonObject;
            newNode.ImageIndex = (int)jsonObject.JsonType;
            newNode.SelectedImageIndex = newNode.ImageIndex;

            if (jsonObject.Children != null)
            {
                foreach (XJToken field in jsonObject.Children)
                {
                    AddNode(newNode.Nodes, field);
                }
            }
        }

        private string GetShowText(XJToken jsonObject)
        {
            string txt = jsonObject.Name;
            if (jsonObject.IsRoot)
            {
                txt = "JSON";
            }
            switch (jsonObject.JsonType)
            {
                case JsonType.Array:
                    txt += " : [" + jsonObject.Children.Count + "]";
                    break;
                case JsonType.Object:
                    txt += " : {" + jsonObject.Children.Count + "}";
                    break;
                default:
                    txt += " : " + jsonObject.ValueString();
                    break;
            }
            return txt;
        }

        //public void ShowInfo(string info, bool isError = false)
        //{
        //    lblError.ForeColor = isError ? Color.Red : Color.Black;
        //    lblError.Text = info;
        //}

        private void txtFind_TextChanged(object sender, EventArgs e)
        {
            txtFind.BackColor = SystemColors.Window;
            FindNext(true, true);
        }

        public bool FindNext(bool includeSelected)
        {
            return FindNext(txtFind.Text, includeSelected);
        }

        public void FindNext(bool includeSelected, bool fromUI)
        {
            if (!FindNext(includeSelected) && fromUI)
                txtFind.BackColor = Color.LightCoral;
        }

        public bool FindNext(string text, bool includeSelected)
        {
            TreeNode startNode = tvJson.SelectedNode;
            if (startNode == null && HasNodes())
                startNode = GetRootNode();
            if (startNode != null)
            {
                startNode = FindNext(startNode, text, includeSelected);
                if (startNode != null)
                {
                    tvJson.SelectedNode = startNode;
                    return true;
                }
            }
            return false;
        }

        public TreeNode FindNext(TreeNode startNode, string text, bool includeSelected)
        {
            if (text == string.Empty)
                return startNode;

            if (includeSelected && IsMatchingNode(startNode, text))
                return startNode;

            TreeNode originalStartNode = startNode;
            startNode = GetNextNode(startNode);
            text = text.ToLower();
            while (startNode != originalStartNode)
            {
                if (IsMatchingNode(startNode, text))
                    return startNode;
                startNode = GetNextNode(startNode);
            }

            return null;
        }

        private TreeNode GetNextNode(TreeNode startNode)
        {
            TreeNode next = startNode.FirstNode ?? startNode.NextNode;
            if (next == null)
            {
                while (startNode != null && next == null)
                {
                    startNode = startNode.Parent;
                    if (startNode != null)
                        next = startNode.NextNode;
                }
                if (next == null)
                {
                    next = GetRootNode();
                    FlashControl(txtFind, Color.Cyan);
                }
            }
            return next;
        }

        private bool IsMatchingNode(TreeNode startNode, string text)
        {
            return (startNode.Text.ToLower().Contains(text));
        }

        private TreeNode GetRootNode()
        {
            if (tvJson.Nodes.Count > 0)
                return tvJson.Nodes[0];
            return null;
        }

        private bool HasNodes()
        {
            return (tvJson.Nodes.Count > 0);
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindNext(false, true);
            }
        }

        private void FlashControl(Control control, Color color)
        {
            Color prevColor = control.BackColor;
            control.BackColor = color;
            control.Refresh();
            Task.Run(() =>
            {
                Thread.Sleep(100);

                control.BackColor = prevColor;
                control.Refresh();
            });
        }

        //private TreeNode GetSelectedTreeNode()
        //{
        //    return tvJson.SelectedNode;
        //}

        private void tvJson_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeNode node = tvJson.GetNodeAt(e.Location);
                if (node != null)
                {
                    tvJson.SelectedNode = node;
                }
            }
        }

        private void mnuTree_Opening(object sender, CancelEventArgs e)
        {
            var node = tvJson.SelectedNode;
            mnuCopy.Enabled = node != null;
            mnuCopyName.Enabled = node != null;
            mnuCopyValue.Enabled = node != null;
        }

        private void mnuCopy_Click(object sender, EventArgs e)
        {
            var node = tvJson.SelectedNode;
            if (node?.Tag != null)
            {
                XJToken obj = node.Tag as XJToken;
                SetClipboard(obj.ToString());
            }
            else
            {
                SetClipboard("");
            }
        }

        private void mnuCopyName_Click(object sender, EventArgs e)
        {
            var node = tvJson.SelectedNode;
            if (node?.Tag != null)
            {
                XJToken obj = node.Tag as XJToken;
                SetClipboard(obj.Name);
            }
            else
            {
                SetClipboard("");
            }
        }

        private void mnuCopyValue_Click(object sender, EventArgs e)
        {
            var node = tvJson.SelectedNode;
            if (node?.Tag != null)
            {
                XJToken obj = node.Tag as XJToken;
                SetClipboard(obj.ValueString());
            }
            else
            {
                SetClipboard("");
            }
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
    }
}