namespace TDChecker
{
    partial class TDNetForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TDNetForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.lblClock = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.mGridMain = new MetroFramework.Controls.MetroGrid();
            this.dataColumnDateDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataColumnTimeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataColumnCodeDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataColumnCompanyNameDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataColumnTitleDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataColumnURLDataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tDNetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tDDataList = new TDChecker.TDDataList();
            this.mBtnFilter = new MetroFramework.Controls.MetroButton();
            this.mtxtKeyWord = new MetroFramework.Controls.MetroTextBox();
            this.mGridKeyWord = new MetroFramework.Controls.MetroGrid();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tDNetBindingSourceKey = new System.Windows.Forms.BindingSource(this.components);
            this.tdDataListKey = new TDChecker.TDDataList();
            this.lblListNum = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mGridMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDNetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDDataList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mGridKeyWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDNetBindingSourceKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdDataListKey)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTimer
            // 
            this.mainTimer.Interval = 1;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // lblClock
            // 
            this.lblClock.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClock.AutoSize = true;
            this.lblClock.Location = new System.Drawing.Point(61, 105);
            this.lblClock.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(208, 21);
            this.lblClock.TabIndex = 1;
            this.lblClock.Text = "yyyy/mm/dd hh:mm:ss";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(1036, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(37, 38);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mGridMain
            // 
            this.mGridMain.AllowUserToAddRows = false;
            this.mGridMain.AllowUserToDeleteRows = false;
            this.mGridMain.AllowUserToResizeRows = false;
            this.mGridMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mGridMain.AutoGenerateColumns = false;
            this.mGridMain.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mGridMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mGridMain.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.mGridMain.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mGridMain.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.mGridMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mGridMain.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataColumnDateDataGridViewTextBoxColumn1,
            this.dataColumnTimeDataGridViewTextBoxColumn1,
            this.dataColumnCodeDataGridViewTextBoxColumn1,
            this.dataColumnCompanyNameDataGridViewTextBoxColumn1,
            this.dataColumnTitleDataGridViewTextBoxColumn1,
            this.dataColumnURLDataGridViewTextBoxColumn1});
            this.mGridMain.DataSource = this.tDNetBindingSource;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mGridMain.DefaultCellStyle = dataGridViewCellStyle2;
            this.mGridMain.EnableHeadersVisualStyles = false;
            this.mGridMain.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mGridMain.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mGridMain.Location = new System.Drawing.Point(64, 161);
            this.mGridMain.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mGridMain.Name = "mGridMain";
            this.mGridMain.ReadOnly = true;
            this.mGridMain.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mGridMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.mGridMain.RowHeadersWidth = 25;
            this.mGridMain.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.mGridMain.RowTemplate.Height = 21;
            this.mGridMain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mGridMain.Size = new System.Drawing.Size(1095, 607);
            this.mGridMain.TabIndex = 4;
            this.mGridMain.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.mGrid_CellMouseClick);
            // 
            // dataColumnDateDataGridViewTextBoxColumn1
            // 
            this.dataColumnDateDataGridViewTextBoxColumn1.DataPropertyName = "DataColumnDate";
            this.dataColumnDateDataGridViewTextBoxColumn1.HeaderText = "DataColumnDate";
            this.dataColumnDateDataGridViewTextBoxColumn1.Name = "dataColumnDateDataGridViewTextBoxColumn1";
            this.dataColumnDateDataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataColumnDateDataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataColumnTimeDataGridViewTextBoxColumn1
            // 
            this.dataColumnTimeDataGridViewTextBoxColumn1.DataPropertyName = "DataColumnTime";
            this.dataColumnTimeDataGridViewTextBoxColumn1.HeaderText = "DataColumnTime";
            this.dataColumnTimeDataGridViewTextBoxColumn1.Name = "dataColumnTimeDataGridViewTextBoxColumn1";
            this.dataColumnTimeDataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataColumnTimeDataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataColumnCodeDataGridViewTextBoxColumn1
            // 
            this.dataColumnCodeDataGridViewTextBoxColumn1.DataPropertyName = "DataColumnCode";
            this.dataColumnCodeDataGridViewTextBoxColumn1.HeaderText = "DataColumnCode";
            this.dataColumnCodeDataGridViewTextBoxColumn1.Name = "dataColumnCodeDataGridViewTextBoxColumn1";
            this.dataColumnCodeDataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataColumnCodeDataGridViewTextBoxColumn1.Width = 60;
            // 
            // dataColumnCompanyNameDataGridViewTextBoxColumn1
            // 
            this.dataColumnCompanyNameDataGridViewTextBoxColumn1.DataPropertyName = "DataColumnCompanyName";
            this.dataColumnCompanyNameDataGridViewTextBoxColumn1.HeaderText = "DataColumnCompanyName";
            this.dataColumnCompanyNameDataGridViewTextBoxColumn1.Name = "dataColumnCompanyNameDataGridViewTextBoxColumn1";
            this.dataColumnCompanyNameDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataColumnTitleDataGridViewTextBoxColumn1
            // 
            this.dataColumnTitleDataGridViewTextBoxColumn1.DataPropertyName = "DataColumnTitle";
            this.dataColumnTitleDataGridViewTextBoxColumn1.HeaderText = "DataColumnTitle";
            this.dataColumnTitleDataGridViewTextBoxColumn1.Name = "dataColumnTitleDataGridViewTextBoxColumn1";
            this.dataColumnTitleDataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataColumnTitleDataGridViewTextBoxColumn1.Width = 345;
            // 
            // dataColumnURLDataGridViewTextBoxColumn1
            // 
            this.dataColumnURLDataGridViewTextBoxColumn1.DataPropertyName = "DataColumnURL";
            this.dataColumnURLDataGridViewTextBoxColumn1.HeaderText = "DataColumnURL";
            this.dataColumnURLDataGridViewTextBoxColumn1.Name = "dataColumnURLDataGridViewTextBoxColumn1";
            this.dataColumnURLDataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataColumnURLDataGridViewTextBoxColumn1.Visible = false;
            // 
            // tDNetBindingSource
            // 
            this.tDNetBindingSource.DataMember = "TDNet";
            this.tDNetBindingSource.DataSource = this.tDDataList;
            // 
            // tDDataList
            // 
            this.tDDataList.DataSetName = "TDDataList";
            this.tDDataList.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mBtnFilter
            // 
            this.mBtnFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mBtnFilter.Location = new System.Drawing.Point(1021, 86);
            this.mBtnFilter.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mBtnFilter.Name = "mBtnFilter";
            this.mBtnFilter.Size = new System.Drawing.Size(138, 40);
            this.mBtnFilter.TabIndex = 5;
            this.mBtnFilter.Text = "フィルター";
            this.mBtnFilter.UseCustomBackColor = true;
            this.mBtnFilter.UseCustomForeColor = true;
            this.mBtnFilter.UseSelectable = true;
            this.mBtnFilter.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // mtxtKeyWord
            // 
            this.mtxtKeyWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.mtxtKeyWord.CustomButton.Image = null;
            this.mtxtKeyWord.CustomButton.Location = new System.Drawing.Point(325, 4);
            this.mtxtKeyWord.CustomButton.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mtxtKeyWord.CustomButton.Name = "";
            this.mtxtKeyWord.CustomButton.Size = new System.Drawing.Size(64, 61);
            this.mtxtKeyWord.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.mtxtKeyWord.CustomButton.TabIndex = 1;
            this.mtxtKeyWord.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.mtxtKeyWord.CustomButton.UseSelectable = true;
            this.mtxtKeyWord.CustomButton.Visible = false;
            this.mtxtKeyWord.Lines = new string[0];
            this.mtxtKeyWord.Location = new System.Drawing.Point(796, 86);
            this.mtxtKeyWord.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mtxtKeyWord.MaxLength = 32767;
            this.mtxtKeyWord.Name = "mtxtKeyWord";
            this.mtxtKeyWord.PasswordChar = '\0';
            this.mtxtKeyWord.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtKeyWord.SelectedText = "";
            this.mtxtKeyWord.SelectionLength = 0;
            this.mtxtKeyWord.SelectionStart = 0;
            this.mtxtKeyWord.Size = new System.Drawing.Size(215, 40);
            this.mtxtKeyWord.TabIndex = 6;
            this.mtxtKeyWord.UseSelectable = true;
            this.mtxtKeyWord.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.mtxtKeyWord.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.mtxtKeyWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtxtKeyWord_KeyDown);
            // 
            // mGridKeyWord
            // 
            this.mGridKeyWord.AllowUserToAddRows = false;
            this.mGridKeyWord.AllowUserToDeleteRows = false;
            this.mGridKeyWord.AllowUserToResizeRows = false;
            this.mGridKeyWord.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mGridKeyWord.AutoGenerateColumns = false;
            this.mGridKeyWord.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.mGridKeyWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mGridKeyWord.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.mGridKeyWord.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mGridKeyWord.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.mGridKeyWord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mGridKeyWord.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6});
            this.mGridKeyWord.DataSource = this.tDNetBindingSourceKey;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(136)))), ((int)(((byte)(136)))), ((int)(((byte)(136)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mGridKeyWord.DefaultCellStyle = dataGridViewCellStyle5;
            this.mGridKeyWord.EnableHeadersVisualStyles = false;
            this.mGridKeyWord.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.mGridKeyWord.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.mGridKeyWord.Location = new System.Drawing.Point(64, 161);
            this.mGridKeyWord.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.mGridKeyWord.Name = "mGridKeyWord";
            this.mGridKeyWord.ReadOnly = true;
            this.mGridKeyWord.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(174)))), ((int)(((byte)(219)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(198)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mGridKeyWord.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.mGridKeyWord.RowHeadersWidth = 25;
            this.mGridKeyWord.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.mGridKeyWord.RowTemplate.Height = 21;
            this.mGridKeyWord.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mGridKeyWord.Size = new System.Drawing.Size(1095, 607);
            this.mGridKeyWord.TabIndex = 7;
            this.mGridKeyWord.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.mGrid_CellMouseClick);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "DataColumnDate";
            this.dataGridViewTextBoxColumn1.HeaderText = "DataColumnDate";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DataColumnTime";
            this.dataGridViewTextBoxColumn2.HeaderText = "DataColumnTime";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 60;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "DataColumnCode";
            this.dataGridViewTextBoxColumn3.HeaderText = "DataColumnCode";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 60;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "DataColumnCompanyName";
            this.dataGridViewTextBoxColumn4.HeaderText = "DataColumnCompanyName";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "DataColumnTitle";
            this.dataGridViewTextBoxColumn5.HeaderText = "DataColumnTitle";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 345;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "DataColumnURL";
            this.dataGridViewTextBoxColumn6.HeaderText = "DataColumnURL";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Visible = false;
            // 
            // tDNetBindingSourceKey
            // 
            this.tDNetBindingSourceKey.DataMember = "TDNet";
            this.tDNetBindingSourceKey.DataSource = this.tdDataListKey;
            // 
            // tdDataListKey
            // 
            this.tdDataListKey.DataSetName = "TDDataList";
            this.tdDataListKey.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblListNum
            // 
            this.lblListNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblListNum.AutoSize = true;
            this.lblListNum.Location = new System.Drawing.Point(61, 786);
            this.lblListNum.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblListNum.Name = "lblListNum";
            this.lblListNum.Size = new System.Drawing.Size(95, 21);
            this.lblListNum.TabIndex = 8;
            this.lblListNum.Text = "リスト件数";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblStatus.Location = new System.Drawing.Point(304, 774);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(854, 37);
            this.lblStatus.TabIndex = 9;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TDNetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 822);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblListNum);
            this.Controls.Add(this.mGridKeyWord);
            this.Controls.Add(this.mtxtKeyWord);
            this.Controls.Add(this.mBtnFilter);
            this.Controls.Add(this.mGridMain);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblClock);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MinimumSize = new System.Drawing.Size(1089, 760);
            this.Name = "TDNetForm";
            this.Padding = new System.Windows.Forms.Padding(37, 105, 37, 35);
            this.Text = "TDChecker";
            this.Load += new System.EventHandler(this.TDNetForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mGridMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDNetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDDataList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mGridKeyWord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tDNetBindingSourceKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tdDataListKey)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TDDataList tDDataList;
        private System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.BindingSource tDNetBindingSource;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroGrid mGridMain;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataColumnDateDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataColumnTimeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataColumnCodeDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataColumnCompanyNameDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataColumnTitleDataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataColumnURLDataGridViewTextBoxColumn1;
        private MetroFramework.Controls.MetroButton mBtnFilter;
        private MetroFramework.Controls.MetroTextBox mtxtKeyWord;
        private MetroFramework.Controls.MetroGrid mGridKeyWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.BindingSource tDNetBindingSourceKey;
        private TDDataList tdDataListKey;
        private System.Windows.Forms.Label lblListNum;
        private System.Windows.Forms.Label lblStatus;
    }
}