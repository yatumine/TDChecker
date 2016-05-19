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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
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
            this.grpBellSound.Location = new System.Drawing.Point(31, 110);
            this.grpBellSound.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.grpBellSound.Name = "grpBellSound";
            this.grpBellSound.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.grpBellSound.Size = new System.Drawing.Size(686, 231);
            this.grpBellSound.TabIndex = 21;
            this.grpBellSound.TabStop = false;
            this.grpBellSound.Text = "通知音";
            // 
            // mTxtMusicFilePath
            // 
            this.mTxtMusicFilePath.Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mTxtMusicFilePath.Location = new System.Drawing.Point(62, 182);
            this.mTxtMusicFilePath.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mTxtMusicFilePath.Name = "mTxtMusicFilePath";
            this.mTxtMusicFilePath.Size = new System.Drawing.Size(468, 31);
            this.mTxtMusicFilePath.TabIndex = 20;
            // 
            // rdBell0
            // 
            this.rdBell0.AutoSize = true;
            this.rdBell0.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdBell0.Location = new System.Drawing.Point(24, 28);
            this.rdBell0.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.rdBell0.Name = "rdBell0";
            this.rdBell0.Size = new System.Drawing.Size(82, 17);
            this.rdBell0.TabIndex = 10;
            this.rdBell0.Text = "No sound";
            this.rdBell0.UseSelectable = true;
            this.rdBell0.CheckedChanged += new System.EventHandler(this.BellOptionCheckChanged);
            // 
            // btnMusicReference
            // 
            this.btnMusicReference.Location = new System.Drawing.Point(539, 180);
            this.btnMusicReference.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnMusicReference.Name = "btnMusicReference";
            this.btnMusicReference.Size = new System.Drawing.Size(116, 40);
            this.btnMusicReference.TabIndex = 14;
            this.btnMusicReference.Text = "参照";
            this.btnMusicReference.UseSelectable = true;
            this.btnMusicReference.Click += new System.EventHandler(this.btnMusicReference_Click);
            // 
            // rdBell1
            // 
            this.rdBell1.AutoSize = true;
            this.rdBell1.Location = new System.Drawing.Point(24, 65);
            this.rdBell1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.rdBell1.Name = "rdBell1";
            this.rdBell1.Size = new System.Drawing.Size(60, 17);
            this.rdBell1.TabIndex = 11;
            this.rdBell1.Text = "Chime";
            this.rdBell1.UseSelectable = true;
            this.rdBell1.CheckedChanged += new System.EventHandler(this.BellOptionCheckChanged);
            // 
            // rdBell2
            // 
            this.rdBell2.AutoSize = true;
            this.rdBell2.Location = new System.Drawing.Point(24, 102);
            this.rdBell2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.rdBell2.Name = "rdBell2";
            this.rdBell2.Size = new System.Drawing.Size(74, 17);
            this.rdBell2.TabIndex = 12;
            this.rdBell2.Text = "Bell Ring";
            this.rdBell2.UseSelectable = true;
            this.rdBell2.CheckedChanged += new System.EventHandler(this.BellOptionCheckChanged);
            // 
            // rdBell3
            // 
            this.rdBell3.AutoSize = true;
            this.rdBell3.Location = new System.Drawing.Point(24, 142);
            this.rdBell3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.rdBell3.Name = "rdBell3";
            this.rdBell3.Size = new System.Drawing.Size(65, 17);
            this.rdBell3.TabIndex = 19;
            this.rdBell3.Text = "その他";
            this.rdBell3.UseSelectable = true;
            this.rdBell3.CheckedChanged += new System.EventHandler(this.BellOptionCheckChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Image = null;
            this.btnConfirm.Location = new System.Drawing.Point(572, 628);
            this.btnConfirm.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(138, 40);
            this.btnConfirm.TabIndex = 24;
            this.btnConfirm.Text = "OK";
            this.btnConfirm.UseSelectable = true;
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnBack
            // 
            this.btnBack.Image = null;
            this.btnBack.Location = new System.Drawing.Point(369, 628);
            this.btnBack.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(138, 40);
            this.btnBack.TabIndex = 23;
            this.btnBack.Text = "Back";
            this.btnBack.UseSelectable = true;
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnDefault
            // 
            this.btnDefault.Image = null;
            this.btnDefault.Location = new System.Drawing.Point(55, 628);
            this.btnDefault.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(138, 40);
            this.btnDefault.TabIndex = 22;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseSelectable = true;
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // lblReadListNum
            // 
            this.lblReadListNum.AutoScroll = true;
            this.lblReadListNum.AutoScrollMinSize = new System.Drawing.Size(179, 31);
            this.lblReadListNum.AutoSize = false;
            this.lblReadListNum.BackColor = System.Drawing.SystemColors.Window;
            this.lblReadListNum.Location = new System.Drawing.Point(50, 366);
            this.lblReadListNum.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.lblReadListNum.Name = "lblReadListNum";
            this.lblReadListNum.Size = new System.Drawing.Size(660, 40);
            this.lblReadListNum.TabIndex = 26;
            this.lblReadListNum.Text = "リスト読み込み件数";
            // 
            // metroTextButton1
            // 
            this.metroTextButton1.Image = null;
            this.metroTextButton1.Location = new System.Drawing.Point(55, 628);
            this.metroTextButton1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.metroTextButton1.Name = "metroTextButton1";
            this.metroTextButton1.Size = new System.Drawing.Size(138, 40);
            this.metroTextButton1.TabIndex = 22;
            this.metroTextButton1.Text = "Default";
            this.metroTextButton1.UseSelectable = true;
            this.metroTextButton1.UseVisualStyleBackColor = true;
            this.metroTextButton1.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // metroTextButton2
            // 
            this.metroTextButton2.Image = null;
            this.metroTextButton2.Location = new System.Drawing.Point(369, 628);
            this.metroTextButton2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.metroTextButton2.Name = "metroTextButton2";
            this.metroTextButton2.Size = new System.Drawing.Size(138, 40);
            this.metroTextButton2.TabIndex = 23;
            this.metroTextButton2.Text = "Back";
            this.metroTextButton2.UseSelectable = true;
            this.metroTextButton2.UseVisualStyleBackColor = true;
            this.metroTextButton2.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // metroTextButton3
            // 
            this.metroTextButton3.Image = null;
            this.metroTextButton3.Location = new System.Drawing.Point(572, 628);
            this.metroTextButton3.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.metroTextButton3.Name = "metroTextButton3";
            this.metroTextButton3.Size = new System.Drawing.Size(138, 40);
            this.metroTextButton3.TabIndex = 24;
            this.metroTextButton3.Text = "OK";
            this.metroTextButton3.UseSelectable = true;
            this.metroTextButton3.UseVisualStyleBackColor = true;
            this.metroTextButton3.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // lblReadCycle
            // 
            this.lblReadCycle.AutoScroll = true;
            this.lblReadCycle.AutoScrollMinSize = new System.Drawing.Size(135, 31);
            this.lblReadCycle.AutoSize = false;
            this.lblReadCycle.BackColor = System.Drawing.SystemColors.Window;
            this.lblReadCycle.Location = new System.Drawing.Point(50, 464);
            this.lblReadCycle.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.lblReadCycle.Name = "lblReadCycle";
            this.lblReadCycle.Size = new System.Drawing.Size(660, 40);
            this.lblReadCycle.TabIndex = 28;
            this.lblReadCycle.Text = "読み込み間隔";
            // 
            // htmlLabel1
            // 
            this.htmlLabel1.AutoScroll = true;
            this.htmlLabel1.AutoScrollMinSize = new System.Drawing.Size(145, 29);
            this.htmlLabel1.AutoSize = false;
            this.htmlLabel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlLabel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 8F);
            this.htmlLabel1.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.htmlLabel1.Location = new System.Drawing.Point(207, 502);
            this.htmlLabel1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.htmlLabel1.Name = "htmlLabel1";
            this.htmlLabel1.Size = new System.Drawing.Size(308, 40);
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
            this.lblNextRead.Location = new System.Drawing.Point(350, 500);
            this.lblNextRead.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.lblNextRead.Name = "lblNextRead";
            this.lblNextRead.Size = new System.Drawing.Size(358, 40);
            this.lblNextRead.TabIndex = 31;
            // 
            // cmbCycle
            // 
            this.cmbCycle.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.optionReadCycleBindingSource, "DataColumnValue", true));
            this.cmbCycle.FontSize = MetroFramework.MetroComboBoxSize.Small;
            this.cmbCycle.FontWeight = MetroFramework.MetroComboBoxWeight.Light;
            this.cmbCycle.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cmbCycle.FormattingEnabled = true;
            this.cmbCycle.ItemHeight = 21;
            this.cmbCycle.Location = new System.Drawing.Point(50, 500);
            this.cmbCycle.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.cmbCycle.Name = "cmbCycle";
            this.cmbCycle.Size = new System.Drawing.Size(85, 27);
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
            // openFileDialog
            // 
            this.openFileDialog.Filter = "mp3, WAV, WMA|*.mp3;*.wav;*.wma";
            this.openFileDialog.InitialDirectory = "C:\\";
            this.openFileDialog.Title = "ファイルを開く";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // ntxtReadListNum
            // 
            this.ntxtReadListNum.AllowSpace = false;
            this.ntxtReadListNum.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ntxtReadListNum.Location = new System.Drawing.Point(50, 404);
            this.ntxtReadListNum.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ntxtReadListNum.MaxLength = 3;
            this.ntxtReadListNum.Name = "ntxtReadListNum";
            this.ntxtReadListNum.Size = new System.Drawing.Size(85, 28);
            this.ntxtReadListNum.TabIndex = 27;
            this.ntxtReadListNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ntxtReadListNum.TextChanged += new System.EventHandler(this.ntxtReadListNum_TextChanged);
            // 
            // htmlLabel2
            // 
            this.htmlLabel2.AutoScroll = true;
            this.htmlLabel2.AutoScrollMinSize = new System.Drawing.Size(53, 31);
            this.htmlLabel2.AutoSize = false;
            this.htmlLabel2.BackColor = System.Drawing.SystemColors.Window;
            this.htmlLabel2.Location = new System.Drawing.Point(132, 502);
            this.htmlLabel2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.htmlLabel2.Name = "htmlLabel2";
            this.htmlLabel2.Size = new System.Drawing.Size(64, 40);
            this.htmlLabel2.TabIndex = 33;
            this.htmlLabel2.Text = "分毎";
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 710);
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
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionForm";
            this.Padding = new System.Windows.Forms.Padding(37, 105, 37, 35);
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
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private MetroFramework.Drawing.Html.HtmlLabel htmlLabel2;
    }
}