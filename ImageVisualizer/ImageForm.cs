using System;
using System.Drawing;
using System.Windows.Forms;

namespace ImageVisualizer
{
    internal sealed class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();

            this.MinimumSize = new Size(256, 256);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.SizeChanged += ImageForm_SizeChanged;
        }

        private void ImageForm_SizeChanged(object sender, EventArgs e)
        {
            InitSize();
        }

        private int width, height;
        private Label lbl;
        private Panel panel1;
        private Label label1;

        internal void Set(Image image)
        {
            width = image.Width;
            height = image.Height;
            //var rf = image.RawFormat.ToString();
            var pf = image.PixelFormat.ToString();
            this.lbl.Text = string.Format("{0} * {1} {2}", width, height, pf);
            pictureBox = new PictureBox
            {
                Image = image,
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.AutoSize
            };
            this.pnl.Controls.Add(pictureBox);
            InitSize();
        }

        private void InitSize()
        {
            if (width > this.pnl.Width || height > this.pnl.Height)
            {
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                var rw = (double)(this.pnl.Width) / width;
                var rh = (double)(this.pnl.Height) / height;
                var rate = Math.Min(rw, rh);
                pictureBox.Width = (int)(width * rate);
                pictureBox.Height = (int)(height * rate);
            }
            var w = (this.pnl.Width - pictureBox.Width) / 2;
            var h = (this.pnl.Height - pictureBox.Height) / 2;
            pictureBox.Location = new Point(w, h);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnl = new System.Windows.Forms.Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnl
            // 
            this.pnl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl.Location = new System.Drawing.Point(0, 23);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(284, 239);
            this.pnl.TabIndex = 0;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(3, 5);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(41, 12);
            this.lbl.TabIndex = 1;
            this.lbl.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lbl);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 23);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(1, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(282, 2);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // ImageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.panel1);
            this.Name = "ImageForm";
            this.Text = "图像可视化工具";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnl;
        private PictureBox pictureBox;
    }
}
