using System.Drawing;
using System.Windows.Forms;

namespace XVisualizer
{
    public partial class ColorForm : Form
    {
        public ColorForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        internal void Set(Color color)
        {
            if (!color.IsEmpty && color != Color.Transparent)
            {
                this.pnlColor.BackColor = color;
            }
            else
            {
                this.pnlColor.BackColor = this.BackColor;
            }

            if (color.IsEmpty)
            {
                textBox1.Text = "目标颜色未初始化。";
                textBox2.Text = "Empty";
                textBox3.Text = "--";
                textBox4.Text = "--";
            }
            else
            {
                textBox1.Text = color.GetName();
                textBox2.Text = color.ToHex();
                textBox3.Text = color.ToARGB();
                textBox4.Text = color.ToCMYK();
            }
        }
    }
}
