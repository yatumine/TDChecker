namespace TDChecker
{
    partial class OptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.grpBellSound = new System.Windows.Forms.GroupBox();
            this.mTxtMusicFilePath = new System.Windows.Forms.MaskedTextBox();
            this.rdBell0 = new MetroFramework.Controls.MetroRadioButton();
            this.btnMusicReference = new MetroFramework.Controls.MetroButton();
            this.rdBell1 = new MetroFramework.Controls.MetroRadioButton();
            this.rdBell2 = new MetroFramework.Controls.MetroRadioButton();
            this.rdBell3 = new MetroFramework.Controls.MetroRadioButton();
            this.btnConfirm = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
            this.btnBack = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
            this.btnDefault = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
            this.lblReadListNum = new MetroFramework.Drawing.Html.HtmlLabel();
            this.metroTextButton1 = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
            this.metroTextButton2 = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
            this.metroTextButton3 = new MetroFramework.Controls.MetroTextBox.MetroTextButton();
            this.lblReadCycle = new MetroFramework.Drawing.Html.HtmlLabel();
            this.htmlLabel1 = new MetroFramework.Drawing.Html.HtmlLabel();
            this.lblNextRead = new MetroFramework.Drawing.Html.HtmlLabel();
            this.cmbCycle = new MetroFramework.Controls.MetroComboBox();
            this.optionReadCycleBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tDDataList = new TDChecker.TDDataList();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ntxtReadListNum = new TDChecker.Utility.NumericTextBox();
            this.htmlLabel2 = new MetroFramework.Drawing.Html.HtmlLabel();
            this.grpBellSound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionReadCycleBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDDataList)).BeginInit();
            this.SuspendLayout();
            // 
            // grpBellSound
            // 
            this.grpBellSound.Controls.Add(this.mTxtMusicFilePath);
            this.grpBellSound.Controls.Add(this.rdBell0);
            this.grpBellSound.Controls.Add(this.btnMusicReference);
            this.grpBellSound.Controls.Add(this.rdBell1);
            this.grpBellSound.Controls.Add(this.rdBell2);
            this.grpBellSound.Controls.Add(this.rdBell3);
            this.grpBellSound.Location = new System.Drawing.Point(17, 63);
            this.grpBellSound.Name = "grpBellSound";
            this.grpBellSound.Size = new System.Drawing.Size(374, 132);
            this.grpBellSound.TabIndex = 21;
            this.grpBellSound.TabStop = false;
            this.grpBellSound.Text = "通知音";
            // 
            // mTxtMusicFilePath
            // 
            this.mTxtMusicFilePath.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mTxtMusicFilePath.Location = new System.Drawing.Point(34, 104);
            this.mTxtMusicFilePath.Name = "mTxtMusicFilePath";
            this.mTxtMusicFilePath.Size = new System.Drawing.Size(257, 21);
            this.mTxtMusicFilePath.TabIndex = 20;
            // 
            // rdBell0
            // 
            this.rdBell0.AutoSize = true;
            this.rdBell0.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdBell0.Location = new System.Drawing.Point(13, 16);
            this.rdBell0.Name = "rdBell0";
            this.rdBell0.Size = new System.Drawing.Size(75, 15);
            this.rdBell0.TabIndex = 10;
            this.rdBell0.Text = "No sound";
            this.rdBell0.UseSelectable = true;
            this.rdBell0.CheckedChanged += new System.EventHandler(this.BellOptionCheckChanged);
            // 
            // btnMusicReference
            // 
            this.btnMusicReference.Location = new System.Drawing.Point(294, 103);
            this.btnMusicReference.Name = "btnMusicReference";
            this.btnMusicReference.Size = new System.Drawing.Size(63, 23);
            this.btnMusicReference.TabIndex = 14;
            this.btnMusicReference.Text = "参照";
            this.btnMusicReference.UseSelectable = true;
            this.btnMusicReference.Click += new System.EventHandler(this.btnMusicReference_Click);
            // 
            // rdBell1
            // 
            this.rdBell1.AutoSize = true;
            this.rdBell1.Location = new System.Drawing.Point(13, 37);
            this.rdBell1.Name = "rdBell1";
            this.rdBell1.Size = new System.Drawing.Size(58, 15);
            this.rdBell1.TabIndex = 11;
            this.rdBell1.Text = "Chime";
            this.rdBell1.UseSelectable = true;
            this.rdBell1.CheckedChanged += new System.EventHandler(this.BellOptionCheckChanged);
            // 
            // rdBell2
            // 
            this.rdBell2.AutoSize = true;
            this.rdBell2.Location = new System.Drawing.Point(13, 58);
            this.rdBell2.Name = "rdBell2";
            this.rdBell2.Size = new System.Drawing.Size(69, 15);
            this.rdBell2.TabIndex = 12;
            this.rdBell2.Text = "Bell Ring";
            this.rdBell2.UseSelectable = true;
            this.rdBell2.CheckedChanged += new System.EventHandler(this.BellOptionCheckChanged);
            // 
            // rdBell3
            // 
            this.rdBell3.AutoSize = true;
            this.rdBell3.Location = new System.Drawing.Point(13, 81);
            this.rdBell3.Name = "rdBell3";
            this.rdBell3.Size = new System.Drawing.Size(59, 15);
            this.rdBell3.TabIndex = 19;
            this.rdBell3.Text = "その他";
            this.rdBell3.UseSelectable = true;
            this.rdBell3.CheckedChanged += new System.EventHandler(this.BellOptionCheckChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = null;
            this.btnConfirm.Location = new System.Drawing.Point(312, 359);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(75, 23);
            this.btnConfirm.TabIndex = 24;
            this.btnConfirm.Text = "OK";
            this.btnConfirm.UseSelectable = true;
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnBack
            // 
            this.btnBack.Image = null;
            this.btnBack.Location = new System.Drawing.Point(201, 359);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 23;
            this.btnBack.Text = "Back";
            this.btnBack.UseSelectable = true;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Image = null;
            this.btnDefault.Location = new System.Drawing.Point(30, 359);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 22;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseSelectable = true;
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // lblReadListNum
            // 
            this.lblReadListNum.AutoScroll = true;
            this.lblReadListNum.AutoScrollMinSize = new System.Drawing.Size(107, 22);
            this.lblReadListNum.AutoSize = false;
            this.lblReadListNum.BackColor = System.Drawing.SystemColors.Window;
            this.lblReadListNum.Location = new System.Drawing.Point(27, 209);
            this.lblReadListNum.Name = "lblReadListNum";
            this.lblReadListNum.Size = new System.Drawing.Size(360, 23);
            this.lblReadListNum.TabIndex = 26;
            this.lblReadListNum.Text = "リスト読み込み件数";
            // 
            // metroTextButton1
            // 
            this.metroTextButton1.Image = null;
            this.metroTextButton1.Location = new System.Drawing.Point(30, 359);
            this.metroTextButton1.Name = "metroTextButton1";
            this.metroTextButton1.Size = new System.Drawing.Size(75, 23);
            this.metroTextButton1.TabIndex = 22;
            this.metroTextButton1.Text = "Default";
            this.metroTextButton1.UseSelectable = true;
            this.metroTextButton1.UseVisualStyleBackColor = true;
            this.metroTextButton1.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // metroTextButton2
            // 
            this.metroTextButton2.Image = null;
            this.metroTextButton2.Location = new System.Drawing.Point(201, 359);
            this.metroTextButton2.Name = "metroTextButton2";
            this.metroTextButton2.Size = new System.Drawing.Size(75, 23);
            this.metroTextButton2.TabIndex = 23;
            this.metroTextButton2.Text = "Back";
            this.metroTextButton2.UseSelectable = true;
            this.metroTextButton2.UseVisualStyleBackColor = true;
            this.metroTextButton2.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // metroTextButton3
            // 
            this.metroTextButton3.Image = null;
            this.metroTextButton3.Location = new System.Drawing.Point(312, 359);
            this.metroTextButton3.Name = "metroTextButton3";
            this.metroTextButton3.Size = new System.Drawing.Size(75, 23);
            this.metroTextButton3.TabIndex = 24;
            this.metroTextButton3.Text = "OK";
            this.metroTextButton3.UseSelectable = true;
            this.metroTextButton3.UseVisualStyleBackColor = true;
            this.metroTextButton3.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblReadCycle
            // 
            this.lblReadCycle.AutoScroll = true;
            this.lblReadCycle.AutoScrollMinSize = new System.Drawing.Size(81, 22);
            this.lblReadCycle.AutoSize = false;
            this.lblReadCycle.BackColor = System.Drawing.SystemColors.Window;
            this.lblReadCycle.Location = new System.Drawing.Point(27, 265);
            this.lblReadCycle.Name = "lblReadCycle";
            this.lblReadCycle.Size = new System.Drawing.Size(360, 23);
            this.lblReadCycle.TabIndex = 28;
            this.lblReadCycle.Text = "読み込み間隔";
            // 
            // htmlLabel1
            // 
            this.htmlLabel1.AutoScroll = true;
            this.htmlLabel1.AutoScrollMinSize = new System.Drawing.Size(87, 21);
            this.htmlLabel1.AutoSize = false;
            this.htmlLabel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlLabel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 8F);
            this.htmlLabel1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.htmlLabel1.Location = new System.Drawing.Point(113, 287);
            this.htmlLabel1.Name = "htmlLabel1";
            this.htmlLabel1.Size = new System.Drawing.Size(168, 23);
            this.htmlLabel1.TabIndex = 30;
            this.htmlLabel1.Text = "次回読み込み、";
            // 
            // lblNextRead
            // 
            this.lblNextRead.AutoScroll = true;
            this.lblNextRead.AutoScrollMinSize = new System.Drawing.Size(10, 0);
            this.lblNextRead.AutoSize = false;
            this.lblNextRead.BackColor = System.Drawing.SystemColors.Window;
            this.lblNextRead.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.lblNextRead.Location = new System.Drawing.Point(191, 286);
            this.lblNextRead.Name = "lblNextRead";
            this.lblNextRead.Size = new System.Drawing.Size(195, 23);
            this.lblNextRead.TabIndex = 31;
            // 
            // cmbCycle
            // 
            this.cmbCycle.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.optionReadCycleBindingSource, "DataColumnValue", true));
            this.cmbCycle.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cmbCycle.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.cmbCycle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCycle.FormattingEnabled = true;
            this.cmbCycle.ItemHeight = 19;
            this.cmbCycle.Location = new System.Drawing.Point(27, 286);
            this.cmbCycle.Name = "cmbCycle";
            this.cmbCycle.Size = new System.Drawing.Size(48, 25);
            this.cmbCycle.TabIndex = 32;
            this.cmbCycle.UseSelectable = true;
            this.cmbCycle.SelectedIndexChanged += new System.EventHandler(this.cmbCycle_SelectedIndexChanged);
            // 
            // optionReadCycleBindingSource
            // 
            this.optionReadCycleBindingSource.DataMember = "OptionReadCycle";
            this.optionReadCycleBindingSource.DataSource = this.tDDataList;
            // 
            // tDDataList
            // 
            this.tDDataList.DataSetName = "TDDataList";
            this.tDDataList.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "mp3, WAV, WMA|*.mp3;*.wav;*.wma";
            this.openFileDialog1.InitialDirectory = "C:\\";
            this.openFileDialog1.Title = "ファイルを開く";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // ntxtReadListNum
            // 
            this.ntxtReadListNum.AllowSpace = false;
            this.ntxtReadListNum.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntxtReadListNum.Location = new System.Drawing.Point(27, 231);
            this.ntxtReadListNum.MaxLength = 3;
            this.ntxtReadListNum.Name = "ntxtReadListNum";
            this.ntxtReadListNum.Size = new System.Drawing.Size(48, 19);
            this.ntxtReadListNum.TabIndex = 27;
            this.ntxtReadListNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntxtReadListNum.TextChanged += new System.EventHandler(this.ntxtReadListNum_TextChanged);
            // 
            // htmlLabel2
            // 
            this.htmlLabel2.AutoScroll = true;
            this.htmlLabel2.AutoScrollMinSize = new System.Drawing.Size(34, 22);
            this.htmlLabel2.AutoSize = false;
            this.htmlLabel2.BackColor = System.Drawing.SystemColors.Window;
            this.htmlLabel2.Location = new System.Drawing.Point(72, 287);
            this.htmlLabel2.Name = "htmlLabel2";
            this.htmlLabel2.Size = new System.Drawing.Size(35, 23);
            this.htmlLabel2.TabIndex = 33;
            this.htmlLabel2.Text = "分毎";
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 406);
            this.Controls.Add(this.lblNextRead);
            this.Controls.Add(this.htmlLabel1);
            this.Controls.Add(this.cmbCycle);
            this.Controls.Add(this.lblReadCycle);
            this.Controls.Add(this.ntxtReadListNum);
            this.Controls.Add(this.lblReadListNum);
            this.Controls.Add(this.metroTextButton3);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.metroTextButton2);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.metroTextButton1);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.grpBellSound);
            this.Controls.Add(this.htmlLabel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FilterForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Option";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OptionForm_Load);
            this.grpBellSound.ResumeLayout(false);
            this.grpBellSound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.optionReadCycleBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDDataList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBellSound;
        private System.Windows.Forms.MaskedTextBox mTxtMusicFilePath;
        private MetroFramework.Controls.MetroRadioButton rdBell0;
        private MetroFramework.Controls.MetroButton btnMusicReference;
        private MetroFramework.Controls.MetroRadioButton rdBell1;
        private MetroFramework.Controls.MetroRadioButton rdBell2;
        private MetroFramework.Controls.MetroRadioButton rdBell3;
        private MetroFramework.Controls.MetroTextBox.MetroTextButton btnConfirm;
        private MetroFramework.Controls.MetroTextBox.MetroTextButton btnBack;
        private MetroFramework.Controls.MetroTextBox.MetroTextButton btnDefault;
        private MetroFramework.Drawing.Html.HtmlLabel lblReadListNum;
        private Utility.NumericTextBox ntxtReadListNum;
        private MetroFramework.Controls.MetroTextBox.MetroTextButton metroTextButton1;
        private MetroFramework.Controls.MetroTextBox.MetroTextButton metroTextButton2;
        private MetroFramework.Controls.MetroTextBox.MetroTextButton metroTextButton3;
        private MetroFramework.Drawing.Html.HtmlLabel lblReadCycle;
        private MetroFramework.Drawing.Html.HtmlLabel htmlLabel1;
        private MetroFramework.Drawing.Html.HtmlLabel lblNextRead;
        private MetroFramework.Controls.MetroComboBox cmbCycle;
        private System.Windows.Forms.BindingSource optionReadCycleBindingSource;
        private TDDataList tDDataList;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private MetroFramework.Drawing.Html.HtmlLabel htmlLabel2;
    }
}