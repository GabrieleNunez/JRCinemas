namespace JRCinemaControls
{
    partial class CinemaMovie
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.moviePictureBox = new System.Windows.Forms.PictureBox();
            this.standardQualityRadioButton = new System.Windows.Forms.RadioButton();
            this.highQualityRadioButton = new System.Windows.Forms.RadioButton();
            this.descriptionLabel = new System.Windows.Forms.Label();
            this.downloadButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moviePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.moviePictureBox);
            this.flowLayoutPanel1.Controls.Add(this.standardQualityRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.highQualityRadioButton);
            this.flowLayoutPanel1.Controls.Add(this.descriptionLabel);
            this.flowLayoutPanel1.Controls.Add(this.downloadButton);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(227, 436);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // moviePictureBox
            // 
            this.moviePictureBox.Location = new System.Drawing.Point(3, 3);
            this.moviePictureBox.Name = "moviePictureBox";
            this.moviePictureBox.Size = new System.Drawing.Size(224, 269);
            this.moviePictureBox.TabIndex = 0;
            this.moviePictureBox.TabStop = false;
            // 
            // standardQualityRadioButton
            // 
            this.standardQualityRadioButton.AutoSize = true;
            this.standardQualityRadioButton.Checked = true;
            this.standardQualityRadioButton.Location = new System.Drawing.Point(3, 278);
            this.standardQualityRadioButton.Name = "standardQualityRadioButton";
            this.standardQualityRadioButton.Size = new System.Drawing.Size(103, 17);
            this.standardQualityRadioButton.TabIndex = 1;
            this.standardQualityRadioButton.TabStop = true;
            this.standardQualityRadioButton.Text = "Standard Quality";
            this.standardQualityRadioButton.UseVisualStyleBackColor = true;
            // 
            // highQualityRadioButton
            // 
            this.highQualityRadioButton.AutoSize = true;
            this.highQualityRadioButton.Location = new System.Drawing.Point(112, 278);
            this.highQualityRadioButton.Name = "highQualityRadioButton";
            this.highQualityRadioButton.Size = new System.Drawing.Size(82, 17);
            this.highQualityRadioButton.TabIndex = 2;
            this.highQualityRadioButton.Text = "High Quality";
            this.highQualityRadioButton.UseVisualStyleBackColor = true;
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.AutoEllipsis = true;
            this.descriptionLabel.Font = new System.Drawing.Font("Verdana", 7F);
            this.descriptionLabel.Location = new System.Drawing.Point(3, 298);
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size(221, 106);
            this.descriptionLabel.TabIndex = 3;
            this.descriptionLabel.Text = "Short Movie Description";
            // 
            // downloadButton
            // 
            this.downloadButton.Location = new System.Drawing.Point(3, 407);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(75, 23);
            this.downloadButton.TabIndex = 4;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(84, 404);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Download Progress:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CinemaMovie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CinemaMovie";
            this.Size = new System.Drawing.Size(227, 436);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.moviePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox moviePictureBox;
        private System.Windows.Forms.RadioButton standardQualityRadioButton;
        private System.Windows.Forms.RadioButton highQualityRadioButton;
        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Label label2;

    }
}
