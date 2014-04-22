namespace JRCinemaControls
{
    partial class TorrentDownloadControl
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
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.downloadItemLabel = new System.Windows.Forms.Label();
            this.downloadSpeedLabel = new System.Windows.Forms.Label();
            this.uploadSpeedLabel = new System.Windows.Forms.Label();
            this.peersLabel = new System.Windows.Forms.Label();
            this.seedersLabel = new System.Windows.Forms.Label();
            this.statelabel = new System.Windows.Forms.Label();
            this.etaLabel = new System.Windows.Forms.Label();
            this.playButton = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadProgressBar.Location = new System.Drawing.Point(618, 11);
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(184, 23);
            this.downloadProgressBar.Step = 1;
            this.downloadProgressBar.TabIndex = 0;
            // 
            // downloadItemLabel
            // 
            this.downloadItemLabel.AutoEllipsis = true;
            this.downloadItemLabel.BackColor = System.Drawing.Color.Transparent;
            this.downloadItemLabel.Font = new System.Drawing.Font("Calibri", 10F);
            this.downloadItemLabel.Location = new System.Drawing.Point(3, 11);
            this.downloadItemLabel.Name = "downloadItemLabel";
            this.downloadItemLabel.Size = new System.Drawing.Size(135, 19);
            this.downloadItemLabel.TabIndex = 1;
            this.downloadItemLabel.Text = "Dowload Item Label";
            // 
            // downloadSpeedLabel
            // 
            this.downloadSpeedLabel.AutoSize = true;
            this.downloadSpeedLabel.BackColor = System.Drawing.Color.Transparent;
            this.downloadSpeedLabel.Font = new System.Drawing.Font("Calibri", 8F);
            this.downloadSpeedLabel.Location = new System.Drawing.Point(351, 13);
            this.downloadSpeedLabel.Name = "downloadSpeedLabel";
            this.downloadSpeedLabel.Size = new System.Drawing.Size(89, 13);
            this.downloadSpeedLabel.TabIndex = 2;
            this.downloadSpeedLabel.Text = "Download Speed:";
            // 
            // uploadSpeedLabel
            // 
            this.uploadSpeedLabel.AutoSize = true;
            this.uploadSpeedLabel.BackColor = System.Drawing.Color.Transparent;
            this.uploadSpeedLabel.Font = new System.Drawing.Font("Calibri", 8F);
            this.uploadSpeedLabel.Location = new System.Drawing.Point(351, 26);
            this.uploadSpeedLabel.Name = "uploadSpeedLabel";
            this.uploadSpeedLabel.Size = new System.Drawing.Size(75, 13);
            this.uploadSpeedLabel.TabIndex = 3;
            this.uploadSpeedLabel.Text = "Upload Speed:";
            // 
            // peersLabel
            // 
            this.peersLabel.AutoSize = true;
            this.peersLabel.BackColor = System.Drawing.Color.Transparent;
            this.peersLabel.Font = new System.Drawing.Font("Calibri", 8F);
            this.peersLabel.Location = new System.Drawing.Point(270, 11);
            this.peersLabel.Name = "peersLabel";
            this.peersLabel.Size = new System.Drawing.Size(49, 13);
            this.peersLabel.TabIndex = 4;
            this.peersLabel.Text = "Leeches:";
            // 
            // seedersLabel
            // 
            this.seedersLabel.AutoSize = true;
            this.seedersLabel.BackColor = System.Drawing.Color.Transparent;
            this.seedersLabel.Font = new System.Drawing.Font("Calibri", 8F);
            this.seedersLabel.Location = new System.Drawing.Point(271, 26);
            this.seedersLabel.Name = "seedersLabel";
            this.seedersLabel.Size = new System.Drawing.Size(48, 13);
            this.seedersLabel.TabIndex = 5;
            this.seedersLabel.Tag = "";
            this.seedersLabel.Text = "Seeders:";
            // 
            // statelabel
            // 
            this.statelabel.AutoSize = true;
            this.statelabel.BackColor = System.Drawing.Color.Transparent;
            this.statelabel.Font = new System.Drawing.Font("Calibri", 8F);
            this.statelabel.Location = new System.Drawing.Point(144, 26);
            this.statelabel.Name = "statelabel";
            this.statelabel.Size = new System.Drawing.Size(35, 13);
            this.statelabel.TabIndex = 6;
            this.statelabel.Text = "State:";
            // 
            // etaLabel
            // 
            this.etaLabel.AutoEllipsis = true;
            this.etaLabel.BackColor = System.Drawing.Color.Transparent;
            this.etaLabel.Font = new System.Drawing.Font("Calibri", 8F);
            this.etaLabel.Location = new System.Drawing.Point(144, 11);
            this.etaLabel.Name = "etaLabel";
            this.etaLabel.Size = new System.Drawing.Size(120, 13);
            this.etaLabel.TabIndex = 7;
            this.etaLabel.Text = "ETA:";
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.Location = new System.Drawing.Point(537, 11);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 8;
            this.playButton.Text = "Open";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.stateToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 70);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // stateToolStripMenuItem
            // 
            this.stateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.stateToolStripMenuItem.Name = "stateToolStripMenuItem";
            this.stateToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stateToolStripMenuItem.Text = "State";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.ToolTipText = "Starts the download, if done will seed";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // TorrentDownloadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.etaLabel);
            this.Controls.Add(this.statelabel);
            this.Controls.Add(this.seedersLabel);
            this.Controls.Add(this.peersLabel);
            this.Controls.Add(this.uploadSpeedLabel);
            this.Controls.Add(this.downloadSpeedLabel);
            this.Controls.Add(this.downloadItemLabel);
            this.Controls.Add(this.downloadProgressBar);
            this.DoubleBuffered = true;
            this.Name = "TorrentDownloadControl";
            this.Size = new System.Drawing.Size(815, 42);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.Label downloadItemLabel;
        private System.Windows.Forms.Label downloadSpeedLabel;
        private System.Windows.Forms.Label uploadSpeedLabel;
        private System.Windows.Forms.Label peersLabel;
        private System.Windows.Forms.Label seedersLabel;
        private System.Windows.Forms.Label statelabel;
        private System.Windows.Forms.Label etaLabel;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
    }
}
