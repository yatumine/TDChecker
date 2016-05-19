using System;
using System.Globalization;
using System.Windows.Forms;

namespace TDChecker.Utility
{
    /// <summary>
    /// 数値テキストボックス
    /// </summary>
    public partial class NumericTextBox : TextBox
    {
        private TextBox textBox1;
        bool allowSpace = false;

        /// <summary>
        /// キー入力判定
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
             keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if (this.allowSpace && e.KeyChar == ' ')
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        public int IntValue
        {
            get
            {
                return Int32.Parse(this.Text);
            }
        }

        public decimal DecimalValue
        {
            get
            {
                return Decimal.Parse(this.Text);
            }
        }

        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }


        public NumericTextBox()
        {
            InitializeComponent();
        }


        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 19);
            this.textBox1.TabIndex = 0;
            this.ResumeLayout(false);

        }
    }
}
