namespace WinValidateCodeRecognition
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTemplatePath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBarRedress = new System.Windows.Forms.TrackBar();
            this.picBoxPrev = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.picBoxTemplate = new System.Windows.Forms.PictureBox();
            this.txtRedress = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRedress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "图像二值化";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(105, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "图像主体识别";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(279, 39);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(47, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "浏览";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // txtTargetPath
            // 
            this.txtTargetPath.Location = new System.Drawing.Point(105, 12);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.Size = new System.Drawing.Size(168, 21);
            this.txtTargetPath.TabIndex = 3;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(279, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(47, 23);
            this.button4.TabIndex = 4;
            this.button4.Text = "浏览";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "目标图片路径：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "模板图片路径：";
            // 
            // txtTemplatePath
            // 
            this.txtTemplatePath.Location = new System.Drawing.Point(105, 39);
            this.txtTemplatePath.Name = "txtTemplatePath";
            this.txtTemplatePath.Size = new System.Drawing.Size(168, 21);
            this.txtTemplatePath.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "识别容错率：";
            // 
            // trackBarRedress
            // 
            this.trackBarRedress.Location = new System.Drawing.Point(12, 99);
            this.trackBarRedress.Maximum = 100;
            this.trackBarRedress.Name = "trackBarRedress";
            this.trackBarRedress.Size = new System.Drawing.Size(314, 45);
            this.trackBarRedress.TabIndex = 10;
            this.trackBarRedress.ValueChanged += new System.EventHandler(this.trackBarRedress_ValueChanged);
            // 
            // picBoxPrev
            // 
            this.picBoxPrev.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxPrev.Location = new System.Drawing.Point(12, 275);
            this.picBoxPrev.Name = "picBoxPrev";
            this.picBoxPrev.Size = new System.Drawing.Size(245, 70);
            this.picBoxPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxPrev.TabIndex = 12;
            this.picBoxPrev.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 251);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 13;
            this.label4.Text = "预览：";
            // 
            // picBoxTemplate
            // 
            this.picBoxTemplate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBoxTemplate.Location = new System.Drawing.Point(276, 275);
            this.picBoxTemplate.Name = "picBoxTemplate";
            this.picBoxTemplate.Size = new System.Drawing.Size(50, 50);
            this.picBoxTemplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picBoxTemplate.TabIndex = 14;
            this.picBoxTemplate.TabStop = false;
            // 
            // txtRedress
            // 
            this.txtRedress.Location = new System.Drawing.Point(95, 72);
            this.txtRedress.Name = "txtRedress";
            this.txtRedress.Size = new System.Drawing.Size(46, 21);
            this.txtRedress.TabIndex = 15;
            this.txtRedress.Text = "10";
            this.txtRedress.TextChanged += new System.EventHandler(this.txtRedress_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 363);
            this.Controls.Add(this.txtRedress);
            this.Controls.Add(this.picBoxTemplate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picBoxPrev);
            this.Controls.Add(this.trackBarRedress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTemplatePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.txtTargetPath);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.Text = "MainForm";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarRedress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxPrev)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTemplate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTemplatePath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBarRedress;
        private System.Windows.Forms.PictureBox picBoxPrev;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picBoxTemplate;
        private System.Windows.Forms.TextBox txtRedress;
    }
}