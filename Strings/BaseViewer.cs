using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XVisualizer.Strings
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
