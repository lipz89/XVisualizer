using System.Drawing;
using System.Windows.Forms;

namespace XVisualizer.Expressions
{
    internal class TreeWindow : Form
    {
        // Fields
        private Panel pnl;
        private ImageList imageList1;
        private System.ComponentModel.IContainer components;
        private TextBox txtName;

        public TreeWindow()
        {
            this.InitializeComponent();
            this.MinimumSize = new Size(300, 256);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        internal void Set(TreeView browser, string expression)
        {
            browser.BorderStyle = BorderStyle.None;
            browser.Dock = DockStyle.Fill;
            browser.ExpandAll();
            browser.ImageList = this.imageList1;
            this.pnl.Controls.Add(browser);
            this.txtName.Text = expression;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TreeWindow));
            this.txtName = new System.Windows.Forms.TextBox();
            this.pnl = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(12, 12);
            this.txtName.Multiline = true;
            this.txtName.Name = "txtName";
            this.txtName.ReadOnly = true;
            this.txtName.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtName.Size = new System.Drawing.Size(660, 57);
            this.txtName.TabIndex = 1;
            this.txtName.TabStop = false;
            // 
            // pnl
            // 
            this.pnl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnl.Location = new System.Drawing.Point(12, 75);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(660, 375);
            this.pnl.TabIndex = 2;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "0.png");
            this.imageList1.Images.SetKeyName(1, "1.png");
            this.imageList1.Images.SetKeyName(2, "2.png");
            this.imageList1.Images.SetKeyName(3, "3.png");
            this.imageList1.Images.SetKeyName(4, "4.png");
            this.imageList1.Images.SetKeyName(5, "5.png");
            this.imageList1.Images.SetKeyName(6, "6.png");
            this.imageList1.Images.SetKeyName(7, "7.png");
            this.imageList1.Images.SetKeyName(8, "8.png");
            this.imageList1.Images.SetKeyName(9, "9.png");
            this.imageList1.Images.SetKeyName(10, "10.png");
            this.imageList1.Images.SetKeyName(11, "11.png");
            // 
            // TreeWindow
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(684, 462);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.txtName);
            this.Name = "TreeWindow";
            this.Text = "表达式树可视化工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}