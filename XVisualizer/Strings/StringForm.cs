using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using XVisualizer.Strings.Htmls;
using XVisualizer.Strings.Jsons;
using XVisualizer.Strings.Xmls;

namespace XVisualizer.Strings
{
    public partial class StringForm : Form
    {
        public string Value { get; set; }
        private List<ControlInfo> list = new List<ControlInfo>();

        public StringForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(750, 500);
            this.MinimumSize = new Size(520, 360);
            this.textBox1.ScrollBars = ScrollBars.Vertical;
        }

        public void SetString(string str)
        {
            str = str.Replace("\r", Environment.NewLine);
            str = str.Replace("\r\n\n", Environment.NewLine);
            str = str.Replace("\n", Environment.NewLine);
            str = str.Replace("\r\r\n", Environment.NewLine);
            this.Value = str;
            this.textBox1.Text = str;
            ShowInfo("TextViewer 准备就绪。");
        }

        private void chkWrap_CheckedChanged(object sender, System.EventArgs e)
        {
            this.textBox1.WordWrap = this.chkWrap.Checked;
            this.textBox1.ScrollBars = this.chkWrap.Checked ? ScrollBars.Vertical : ScrollBars.Both;
        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnJson_Click(object sender, System.EventArgs e)
        {
            CreateOrChange<JsonViewer>("json");
        }

        private void btnXml_Click(object sender, System.EventArgs e)
        {
            CreateOrChange<XmlViewer>("xml");
        }

        private void btnHtml_Click(object sender, EventArgs e)
        {
            CreateOrChange<HtmlViewer>("html");
        }

        private void CreateOrChange<T>(string key) where T : BaseViewer, new()
        {
            foreach (var it in list.Where(x => x.IsVisible && x.Key != key))
            {
                ChangeControl(it);
            }
            var ctl = list.FirstOrDefault(x => x.Key == key);
            if (ctl == null)
            {
                try
                {
                    BaseViewer viewer = new T();
                    viewer.ShowInfo = this.ShowInfo;
                    viewer.SetContent(this.Value);
                    ctl = new ControlInfo
                    {
                        Key = key,
                        Control = viewer,
                    };
                }
                catch (Exception ex)
                {
                    ShowInfo(ex.Message, true);
                }
            }
            if (ctl != null)
            {
                ChangeControl(ctl);
            }
        }

        private void ChangeControl(ControlInfo control)
        {
            control.IsVisible = !control.IsVisible;
            if (control.IsVisible)
            {
                ShowControl(control);
            }
            else
            {
                HideControl(control);
            }
        }

        private void ShowControl(ControlInfo control)
        {
            this.textBox1.Visible = false;
            this.chkWrap.Enabled = false;
            var ctl = control.Control;
            this.panel1.Controls.Add(ctl);
            list.Add(control);
            ShowInfo(ctl.GetType().Name + " 准备就绪。");
        }

        private void HideControl(ControlInfo control)
        {
            var ctl = control.Control;
            this.panel1.Controls.Remove(ctl);
            this.textBox1.Visible = true;
            this.chkWrap.Enabled = true;
            ShowInfo("TextViewer 准备就绪。");
        }

        public void ShowInfo(string info, bool isError = false)
        {
            label1.ForeColor = isError ? Color.Red : Color.Black;
            label1.Text = info;
        }
    }

    class ControlInfo
    {
        public BaseViewer Control { get; set; }
        public string Key { get; set; }
        public bool IsVisible { get; set; }
    }
}
