namespace IPForm
{
    partial class MultiImageWorkspace
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.groupBoxPicture = new System.Windows.Forms.GroupBox();
            this.groupBoxOps = new System.Windows.Forms.GroupBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDiv = new System.Windows.Forms.Button();
            this.buttonSub = new System.Windows.Forms.Button();
            this.buttonMult = new System.Windows.Forms.Button();
            this.buttonPicSave = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chartPic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBoxPicture.SuspendLayout();
            this.groupBoxOps.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPic)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxPicture
            // 
            this.groupBoxPicture.BackColor = System.Drawing.SystemColors.Info;
            this.groupBoxPicture.Controls.Add(this.groupBoxOps);
            this.groupBoxPicture.Controls.Add(this.buttonPicSave);
            this.groupBoxPicture.Controls.Add(this.panel2);
            this.groupBoxPicture.Controls.Add(this.panel1);
            this.groupBoxPicture.Location = new System.Drawing.Point(3, 3);
            this.groupBoxPicture.Name = "groupBoxPicture";
            this.groupBoxPicture.Size = new System.Drawing.Size(211, 560);
            this.groupBoxPicture.TabIndex = 118;
            this.groupBoxPicture.TabStop = false;
            this.groupBoxPicture.Text = "Resim";
            // 
            // groupBoxOps
            // 
            this.groupBoxOps.Controls.Add(this.buttonAdd);
            this.groupBoxOps.Controls.Add(this.buttonDiv);
            this.groupBoxOps.Controls.Add(this.buttonSub);
            this.groupBoxOps.Controls.Add(this.buttonMult);
            this.groupBoxOps.Location = new System.Drawing.Point(6, 456);
            this.groupBoxOps.Name = "groupBoxOps";
            this.groupBoxOps.Size = new System.Drawing.Size(200, 97);
            this.groupBoxOps.TabIndex = 97;
            this.groupBoxOps.TabStop = false;
            this.groupBoxOps.Text = "Soldaki Resmi Sağdaki Resim İle";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAdd.Location = new System.Drawing.Point(3, 16);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(0);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(90, 30);
            this.buttonAdd.TabIndex = 93;
            this.buttonAdd.Text = "Topla";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDiv
            // 
            this.buttonDiv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDiv.Location = new System.Drawing.Point(106, 55);
            this.buttonDiv.Margin = new System.Windows.Forms.Padding(0);
            this.buttonDiv.Name = "buttonDiv";
            this.buttonDiv.Size = new System.Drawing.Size(91, 30);
            this.buttonDiv.TabIndex = 96;
            this.buttonDiv.Text = "Böl";
            this.buttonDiv.UseVisualStyleBackColor = true;
            this.buttonDiv.Click += new System.EventHandler(this.buttonDiv_Click);
            // 
            // buttonSub
            // 
            this.buttonSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSub.Location = new System.Drawing.Point(106, 16);
            this.buttonSub.Margin = new System.Windows.Forms.Padding(0);
            this.buttonSub.Name = "buttonSub";
            this.buttonSub.Size = new System.Drawing.Size(90, 30);
            this.buttonSub.TabIndex = 94;
            this.buttonSub.Text = "Çıkar";
            this.buttonSub.UseVisualStyleBackColor = true;
            this.buttonSub.Click += new System.EventHandler(this.buttonSub_Click);
            // 
            // buttonMult
            // 
            this.buttonMult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMult.Location = new System.Drawing.Point(3, 55);
            this.buttonMult.Margin = new System.Windows.Forms.Padding(0);
            this.buttonMult.Name = "buttonMult";
            this.buttonMult.Size = new System.Drawing.Size(90, 30);
            this.buttonMult.TabIndex = 95;
            this.buttonMult.Text = "Çarp";
            this.buttonMult.UseVisualStyleBackColor = true;
            this.buttonMult.Click += new System.EventHandler(this.buttonMult_Click);
            // 
            // buttonPicSave
            // 
            this.buttonPicSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPicSave.Location = new System.Drawing.Point(83, 16);
            this.buttonPicSave.Margin = new System.Windows.Forms.Padding(0);
            this.buttonPicSave.Name = "buttonPicSave";
            this.buttonPicSave.Size = new System.Drawing.Size(55, 30);
            this.buttonPicSave.TabIndex = 92;
            this.buttonPicSave.Text = "Kaydet";
            this.buttonPicSave.UseVisualStyleBackColor = true;
            this.buttonPicSave.Click += new System.EventHandler(this.buttonPicSave_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox);
            this.panel2.Location = new System.Drawing.Point(5, 49);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 200);
            this.panel2.TabIndex = 91;
            // 
            // pictureBox
            // 
            this.pictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(200, 200);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chartPic);
            this.panel1.Location = new System.Drawing.Point(6, 249);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 200);
            this.panel1.TabIndex = 90;
            // 
            // chartPic
            // 
            chartArea8.Name = "ChartArea1";
            this.chartPic.ChartAreas.Add(chartArea8);
            this.chartPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPic.Location = new System.Drawing.Point(0, 0);
            this.chartPic.Margin = new System.Windows.Forms.Padding(0);
            this.chartPic.Name = "chartPic";
            this.chartPic.Size = new System.Drawing.Size(200, 200);
            this.chartPic.TabIndex = 89;
            // 
            // MultiImageWorkspace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxPicture);
            this.Name = "MultiImageWorkspace";
            this.Size = new System.Drawing.Size(218, 566);
            this.groupBoxPicture.ResumeLayout(false);
            this.groupBoxOps.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxPicture;
        private System.Windows.Forms.Button buttonPicSave;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPic;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonSub;
        private System.Windows.Forms.Button buttonDiv;
        private System.Windows.Forms.Button buttonMult;
        private System.Windows.Forms.GroupBox groupBoxOps;
    }
}
