namespace IPForm
{
    partial class MainForm
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
            this.buttonTransfer = new System.Windows.Forms.Button();
            this.multiImageWorkspace1 = new IPForm.MultiImageWorkspace();
            this.imageWorkspace2 = new IPForm.ImageWorkspace();
            this.imageWorkspace1 = new IPForm.ImageWorkspace();
            this.SuspendLayout();
            // 
            // buttonTransfer
            // 
            this.buttonTransfer.Location = new System.Drawing.Point(871, 581);
            this.buttonTransfer.Name = "buttonTransfer";
            this.buttonTransfer.Size = new System.Drawing.Size(217, 38);
            this.buttonTransfer.TabIndex = 120;
            this.buttonTransfer.Text = "Resimleri Aktar";
            this.buttonTransfer.UseVisualStyleBackColor = true;
            this.buttonTransfer.Click += new System.EventHandler(this.buttonTransfer_Click);
            // 
            // multiImageWorkspace1
            // 
            this.multiImageWorkspace1.ImageA = null;
            this.multiImageWorkspace1.ImageB = null;
            this.multiImageWorkspace1.ImageDisplay = null;
            this.multiImageWorkspace1.Location = new System.Drawing.Point(870, 8);
            this.multiImageWorkspace1.Name = "multiImageWorkspace1";
            this.multiImageWorkspace1.Size = new System.Drawing.Size(218, 566);
            this.multiImageWorkspace1.TabIndex = 119;
            // 
            // imageWorkspace2
            // 
            this.imageWorkspace2.BackColor = System.Drawing.SystemColors.Info;
            this.imageWorkspace2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageWorkspace2.Image = null;
            this.imageWorkspace2.ImageName = null;
            this.imageWorkspace2.Location = new System.Drawing.Point(441, 8);
            this.imageWorkspace2.Name = "imageWorkspace2";
            this.imageWorkspace2.Size = new System.Drawing.Size(423, 803);
            this.imageWorkspace2.TabIndex = 118;
            // 
            // imageWorkspace1
            // 
            this.imageWorkspace1.BackColor = System.Drawing.SystemColors.Info;
            this.imageWorkspace1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.imageWorkspace1.Image = null;
            this.imageWorkspace1.ImageName = null;
            this.imageWorkspace1.Location = new System.Drawing.Point(12, 8);
            this.imageWorkspace1.Name = "imageWorkspace1";
            this.imageWorkspace1.Size = new System.Drawing.Size(423, 803);
            this.imageWorkspace1.TabIndex = 117;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1104, 861);
            this.Controls.Add(this.buttonTransfer);
            this.Controls.Add(this.multiImageWorkspace1);
            this.Controls.Add(this.imageWorkspace2);
            this.Controls.Add(this.imageWorkspace1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mercek";
            this.ResumeLayout(false);

        }

        #endregion
        private ImageWorkspace imageWorkspace1;
        private ImageWorkspace imageWorkspace2;
        private MultiImageWorkspace multiImageWorkspace1;
        private System.Windows.Forms.Button buttonTransfer;
    }
}

