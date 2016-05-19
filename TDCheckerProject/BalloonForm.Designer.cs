namespace TDChecker
{
    partial class BalloonForm
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
            this.components = new System.ComponentModel.Container();
            this.apperTimer = new System.Windows.Forms.Timer(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.lblCompany = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.lblListMax = new System.Windows.Forms.Label();
            this.lblListMin = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lowerTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Location = new System.Drawing.Point(46, 89);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(422, 88);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "title";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Location = new System.Drawing.Point(24, 37);
            this.lblCode.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(53, 21);
            this.lblCode.TabIndex = 1;
            this.lblCode.Text = "code";
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Location = new System.Drawing.Point(106, 37);
            this.lblCompany.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.Size = new System.Drawing.Size(130, 21);
            this.lblCompany.TabIndex = 2;
            this.lblCompany.Text = "compay name";
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = global::TDChecker.Properties.Resources.RightButton32;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnNext.FlatAppearance.BorderSize = 0;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Location = new System.Drawing.Point(436, 182);
            this.btnNext.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(31, 35);
            this.btnNext.TabIndex = 4;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.BackgroundImage = global::TDChecker.Properties.Resources.LeftButton32;
            this.btnPrev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnPrev.FlatAppearance.BorderSize = 0;
            this.btnPrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrev.Location = new System.Drawing.Point(46, 182);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(31, 35);
            this.btnPrev.TabIndex = 3;
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // lblListMax
            // 
            this.lblListMax.AutoSize = true;
            this.lblListMax.Location = new System.Drawing.Point(259, 189);
            this.lblListMax.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblListMax.Name = "lblListMax";
            this.lblListMax.Size = new System.Drawing.Size(46, 21);
            this.lblListMax.TabIndex = 5;
            this.lblListMax.Text = "Max";
            // 
            // lblListMin
            // 
            this.lblListMin.AutoSize = true;
            this.lblListMin.Location = new System.Drawing.Point(172, 189);
            this.lblListMin.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblListMin.Name = "lblListMin";
            this.lblListMin.Size = new System.Drawing.Size(41, 21);
            this.lblListMin.TabIndex = 6;
            this.lblListMin.Text = "Min";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 189);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 21);
            this.label1.TabIndex = 7;
            this.label1.Text = "/";
            // 
            // BalloonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 243);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblListMin);
            this.Controls.Add(this.lblListMax);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.lblCompany);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BalloonForm";
            this.Padding = new System.Windows.Forms.Padding(37, 105, 37, 35);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.BalloonForm_Load);
            this.MouseEnter += new System.EventHandler(this.BalloonForm_MouseEnter);
            this.MouseHover += new System.EventHandler(this.BalloonForm_MouseHover);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer apperTimer;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblCompany;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblListMax;
        private System.Windows.Forms.Label lblListMin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer lowerTimer;
    }
}