using System;
using System.Windows.Forms;

namespace StringVisualizer
{
    public class BaseViewer : UserControl
    {
        public BaseViewer()
        {
            this.Dock = DockStyle.Fill;
        }
        public Action<string, bool> ShowInfo { get; set; }

        public virtual void SetContent(string value)
        {
        }
    }
}
