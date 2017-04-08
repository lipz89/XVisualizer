namespace XVisualizer.Strings.Xmls
{
    partial class XmlViewer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mnuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tvXml = new System.Windows.Forms.TreeView();
            this.mnuTree.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuTree
            // 
            this.mnuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCopy});
            this.mnuTree.Name = "mnuTree";
            this.mnuTree.Size = new System.Drawing.Size(117, 26);
            this.mnuTree.Opening += new System.ComponentModel.CancelEventHandler(this.mnuTree_Opening);
            // 
            // mnuCopy
            // 
            this.mnuCopy.Name = "mnuCopy";
            this.mnuCopy.Size = new System.Drawing.Size(116, 22);
            this.mnuCopy.Text = "复制(&C)";
            this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
            // 
            // tvXml
            // 
            this.tvXml.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tvXml.ContextMenuStrip = this.mnuTree;
            this.tvXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvXml.HideSelection = false;
            this.tvXml.ItemHeight = 16;
            this.tvXml.Location = new System.Drawing.Point(0, 0);
            this.tvXml.Name = "tvXml";
            this.tvXml.Size = new System.Drawing.Size(979, 476);
            this.tvXml.TabIndex = 0;
            this.tvXml.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tvXml_AfterExpandOrCollapse);
            this.tvXml.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tvXml_AfterExpandOrCollapse);
            this.tvXml.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvXml_MouseDown);
            // 
            // XmlViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tvXml);
            this.Name = "XmlViewer";
            this.Size = new System.Drawing.Size(979, 476);
            this.mnuTree.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvXml;
        private System.Windows.Forms.ContextMenuStrip mnuTree;
        private System.Windows.Forms.ToolStripMenuItem mnuCopy;
    }
}
