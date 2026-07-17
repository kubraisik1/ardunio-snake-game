namespace yılan
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblBaslangic = new Label();
            lblEnIyiSkor = new Label();
            chkDuvarOlsun = new CheckBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // lblBaslangic
            // 
            lblBaslangic.AutoSize = true;
            lblBaslangic.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblBaslangic.Location = new Point(188, 153);
            lblBaslangic.Name = "lblBaslangic";
            lblBaslangic.Size = new Size(230, 34);
            lblBaslangic.TabIndex = 0;
            lblBaslangic.Text = " YILAN OYUNU";
            lblBaslangic.TextAlign = ContentAlignment.TopCenter;
            // 
            // lblEnIyiSkor
            // 
            lblEnIyiSkor.AutoSize = true;
            lblEnIyiSkor.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 162);
            lblEnIyiSkor.Location = new Point(433, 23);
            lblEnIyiSkor.Name = "lblEnIyiSkor";
            lblEnIyiSkor.Size = new Size(141, 31);
            lblEnIyiSkor.TabIndex = 2;
            lblEnIyiSkor.Text = "En İyi Skor: 0";
            // 
            // chkDuvarOlsun
            // 
            chkDuvarOlsun.AutoSize = true;
            chkDuvarOlsun.FlatStyle = FlatStyle.Flat;
            chkDuvarOlsun.Font = new Font("Times New Roman", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            chkDuvarOlsun.ForeColor = Color.Green;
            chkDuvarOlsun.Location = new Point(208, 349);
            chkDuvarOlsun.Name = "chkDuvarOlsun";
            chkDuvarOlsun.Size = new Size(181, 21);
            chkDuvarOlsun.TabIndex = 3;
            chkDuvarOlsun.Text = "Duvar Ölümü: KAPALI";
            chkDuvarOlsun.UseVisualStyleBackColor = true;
            chkDuvarOlsun.CheckedChanged += chkDuvarOlsun_CheckedChanged;
            // 
            // button1
            // 
            button1.Font = new Font("Times New Roman", 9F, FontStyle.Bold, GraphicsUnit.Point, 162);
            button1.Location = new Point(208, 252);
            button1.Name = "button1";
            button1.Size = new Size(168, 53);
            button1.TabIndex = 1;
            button1.Text = "BAŞLA";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(602, 593);
            Controls.Add(chkDuvarOlsun);
            Controls.Add(lblEnIyiSkor);
            Controls.Add(button1);
            Controls.Add(lblBaslangic);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBaslangic;
        private Label lblEnIyiSkor;
        private CheckBox chkDuvarOlsun;
        private Button button1;
    }
}