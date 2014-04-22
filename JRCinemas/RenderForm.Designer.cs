namespace JRCinemas
{
    partial class RenderForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cinemaBackground1 = new JRCinemaControls.CinemaBackground();
            this.SuspendLayout();
            // 
            // cinemaBackground1
            // 
            this.cinemaBackground1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cinemaBackground1.Location = new System.Drawing.Point(0, 0);
            this.cinemaBackground1.Name = "cinemaBackground1";
            this.cinemaBackground1.PrimaryColor = System.Drawing.Color.Gainsboro;
            this.cinemaBackground1.SecondaryColor = System.Drawing.Color.Gray;
            this.cinemaBackground1.Size = new System.Drawing.Size(958, 460);
            this.cinemaBackground1.TabIndex = 0;
            // 
            // RenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(958, 460);
            this.Controls.Add(this.cinemaBackground1);
            this.Name = "RenderForm";
            this.Text = "RenderForm";
            this.ResumeLayout(false);

        }

        #endregion

        private JRCinemaControls.CinemaBackground cinemaBackground1;
    }
}