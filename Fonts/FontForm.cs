using System.Drawing;
using System.Windows.Forms;

namespace XVisualizer
{
    public partial class FontForm : Form
    {
        public FontForm()
        {
            InitializeComponent();
            this.MinimumSize = new Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        internal void Set(Font font)
        {
            if (font != null)
            {
                textBox1.Text = font.Name;
                textBox2.Text = font.FontFamily.Name;
                textBox3.Text = "" + font.Size + font.Unit + " ; " + font.SizeInPoints + "Points";
                textBox4.Text = font.Style.ToString();

                label1.Visible = label2.Visible = label3.Visible = label4.Visible = label5.Visible = true;
                label1.Font = label2.Font = label3.Font = label4.Font = label5.Font = font;

                label2.Top = label1.Top + label1.Height + 5;
                label3.Top = label2.Top + label2.Height + 5;
                label4.Top = label3.Top + label3.Height + 5;
                label5.Top = label4.Top + label4.Height + 5;
            }
            else
            {
                textBox1.Text = "目标字体未初始化。";
                textBox2.Text = "--";
                textBox3.Text = "--";
                textBox4.Text = "--";
                label1.Visible = label2.Visible = label3.Visible = label4.Visible = label5.Visible = false;
            }
        }
    }
}
