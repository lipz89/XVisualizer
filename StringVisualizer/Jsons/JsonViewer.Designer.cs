namespace StringVisualizer.Jsons
{
    partial class JsonViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JsonViewer));
            this.mnuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopyName = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopyValue = new System.Windows.Forms.ToolStripMenuItem();
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.tvJson = new System.Windows.Forms.TreeView();
            this.pnlFind = new System.Windows.Forms.Panel();
            this.txtFind = new System.Windows.Forms.TextBox();
            this.lblFind = new System.Windows.Forms.Label();
            this.mnuTree.SuspendLayout();
            this.pnlFind.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuTree
            // 
            this.mnuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy,
            this.mnuCopyName,
            this.mnuCopyValue});
            this.mnuTree.Name = "mnuTree";
            this.mnuTree.Size = new System.Drawing.Size(143, 70);
            this.mnuTree.Opening += new System.ComponentModel.CancelEventHandler(this.mnuTree_Opening);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(142, 22);
            this.mnuCopy.Text = "复制(&C)";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // mnuCopyName
            // 
            this.mnuCopyName.Name = "mnuCopyName";
            this.mnuCopyName.Size = new System.Drawing.Size(142, 22);
            this.mnuCopyName.Text = "复制名称(&N)";
            this.mnuCopyName.Click += new System.EventHandler(this.mnuCopyName_Click);
            // 
            // mnuCopyValue
            // 
            this.mnuCopyValue.Name = "mnuCopyValue";
            this.mnuCopyValue.Size = new System.Drawing.Size(142, 22);
            this.mnuCopyValue.Text = "复制值(&V)";
            this.mnuCopyValue.Click += new System.EventHandler(this.mnuCopyValue_Click);
            // 
            // imgList
            // 
            this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
            this.imgList.TransparentColor = System.Drawing.Color.White;
            this.imgList.Images.SetKeyName(0, "object.png");
            this.imgList.Images.SetKeyName(1, "array.png");
            this.imgList.Images.SetKeyName(2, "value.bmp");
            this.imgList.Images.SetKeyName(3, "string.png");
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(516, 2);
            this.label1.TabIndex = 7;
            this.label1.Text = "label1";
            // 
            // tvJson
            // 
            this.tvJson.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvJson.ContextMenuStrip = this.mnuTree;
            this.tvJson.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvJson.FullRowSelect = true;
            this.tvJson.HideSelection = false;
            this.tvJson.ImageIndex = 0;
            this.tvJson.ImageList = this.imgList;
            this.tvJson.Location = new System.Drawing.Point(0, 30);
            this.tvJson.Name = "tvJson";
            this.tvJson.SelectedImageIndex = 0;
            this.tvJson.Size = new System.Drawing.Size(516, 378);
            this.tvJson.TabIndex = 3;
            this.tvJson.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvJson_MouseDown);
            // 
            // pnlFind
            // 
            this.pnlFind.Controls.Add(this.txtFind);
            this.pnlFind.Controls.Add(this.lblFind);
            this.pnlFind.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFind.Location = new System.Drawing.Point(0, 0);
            this.pnlFind.Name = "pnlFind";
            this.pnlFind.Size = new System.Drawing.Size(516, 30);
            this.pnlFind.TabIndex = 6;
            // 
            // txtFind
            // 
            this.txtFind.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFind.Location = new System.Drawing.Point(53, 5);
            this.txtFind.Name = "txtFind";
            this.txtFind.Size = new System.Drawing.Size(460, 21);
            this.txtFind.TabIndex = 1;
            this.txtFind.TextChanged += new System.EventHandler(this.txtFind_TextChanged);
            this.txtFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFind_KeyDown);
            // 
            // lblFind
            // 
            this.lblFind.AutoSize = true;
            this.lblFind.Location = new System.Drawing.Point(3, 9);
            this.lblFind.Name = "lblFind";
            this.lblFind.Size = new System.Drawing.Size(47, 12);
            this.lblFind.TabIndex = 0;
            this.lblFind.Text = "查找(&F)";
            // 
            // JsonViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tvJson);
            this.Controls.Add(this.pnlFind);
            this.Name = "JsonViewer";
            this.Size = new System.Drawing.Size(516, 408);
            this.mnuTree.ResumeLayout(false);
            this.pnlFind.ResumeLayout(false);
            this.pnlFind.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imgList;
        private System.Windows.Forms.TreeView tvJson;
        private System.Windows.Forms.Panel pnlFind;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.ContextMenuStrip mnuTree;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyValue;
        private System.Windows.Forms.ToolStripMenuItem mnuCopyName;
        private System.Windows.Forms.Label label1;
    }
}
