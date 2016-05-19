using System;
using System.Windows.Forms;

// 追加
using MetroFramework.Forms;
using COMMON.Const;
using TDChecker.Properties;
using COMMON.Data;
using System.Data;
using System.Collections.Generic;
using COMMON.Utility;
using System.Diagnostics;

namespace TDChecker
{
    public partial class OptionForm : MetroForm/* ←変更 */
    {
        // メンバ変数
        private static Boolean InitializeFlg = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OptionForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionForm_Load(object sender, EventArgs e)
        {
            // 初期化
            InitializeFlg = true;
            InitializeOption();
            InitializeFlg = false;
        }

        /// <summary>
        /// デフォルトボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefault_Click(object sender, EventArgs e)
        {
            // デフォルト値へ変更
            Settings.Default.Reset();
            Settings.Default.Save();
            InitializeOption();
        }
        /// <summary>
        /// 戻るボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            Settings.Default.Reload();
            Settings.Default.Save();
            // 設定変更せずに戻る
            Close();
        }
      /// <summary>
      /// 決定ボタン
      /// </summary>
      /// <param name="sender"></param>
      /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // 設定保存
            Settings.Default.Save();

            // オプションフォーム終了
            Close();
        }
        /// <summary>
        /// 参照ボタン（通知音設定）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMusicReference_Click(object sender, EventArgs e)
        {

            if(mTxtMusicFilePath.Text != null)
            {
                openFileDialog.InitialDirectory = mTxtMusicFilePath.Text;
            }

            // ダイアログを表示し、戻り値が [OK] の場合は、選択したファイルを表示する
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 選択されたファイル名を設定
                mTxtMusicFilePath.Text = openFileDialog.FileName;
            }

            // 不要になった時点で破棄する
            openFileDialog.Dispose();

            Settings.Default.BellSoundFile = mTxtMusicFilePath.Text;
            SoundUtility.PlayTimerAsync(Settings.Default.BellSoundFile, SoundUtility.PlaySec.e_10_Sec);

        }
        /// <summary>
        /// Option 初期設定
        /// </summary>
        private void InitializeOption()
        {
            // コンボボックス設定
            SetCycleCmb();

            // Bellラジオボタン選択
            switch (Settings.Default.BellType)
            {
                case Constants.BellNoSound:
                    mTxtMusicFilePath.Enabled = false;
                    btnMusicReference.Enabled = false;
                    break;
                case Constants.BellPlayChime:
                    Settings.Default.BellType = Constants.BellPlayChime;
                    mTxtMusicFilePath.Enabled = false;
                    btnMusicReference.Enabled = false;
                    break;
                case Constants.BellPlayBellRing:
                    Settings.Default.BellType = Constants.BellPlayBellRing;
                    mTxtMusicFilePath.Enabled = false;
                    btnMusicReference.Enabled = false;
                    break;
                case Constants.BellPlayMusic:
                    Settings.Default.BellType = Constants.BellPlayMusic;
                    mTxtMusicFilePath.Enabled = true;
                    btnMusicReference.Enabled = true;
                    break;
                default:
                    break;
            }
            // その他ラジオボタン初期化
            foreach (Control selectCtl in grpBellSound.Controls)
            {
                if (selectCtl is RadioButton)
                {
                    RadioButton radioBtnBell = (RadioButton)selectCtl;
                    short selectNo = short.Parse(radioBtnBell.Name.Substring((radioBtnBell.Name.Length - 1)));

                    if (selectNo == Settings.Default.BellType)
                    {
                        radioBtnBell.Checked = true;
                    }
                    else
                    {
                        radioBtnBell.Checked = false;
                    }
                }
            }

            // MusicFilePath
            mTxtMusicFilePath.Text = Settings.Default.BellSoundFile;

            // リスト読み込み件数
            ntxtReadListNum.Text = Settings.Default.ReadListNum.ToString();

            // 読み込み間隔
            cmbCycle.Text = Settings.Default.ReadCycle.TotalMinutes.ToString();
        }
        /// <summary>
        /// Bellラジオボタン変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BellOptionCheckChanged(object sender, EventArgs e)
        {
            if (InitializeFlg)
            {
                return;
            }
            // BellSoundOption（grpBellSound）の選択項目を取得
            foreach (Control selectCtl in grpBellSound.Controls)
            {
                short selectNo = 0;
                if (selectCtl is RadioButton && ((RadioButton)selectCtl).Checked)
                {
                    RadioButton radioBtnBell = (RadioButton)selectCtl;
                    selectNo = short.Parse(radioBtnBell.Name.Substring((radioBtnBell.Name.Length - 1)));
                    switch (selectNo)
                    {
                        case Constants.BellNoSound:
                            Settings.Default.BellType = Constants.BellNoSound;
                            mTxtMusicFilePath.Enabled = false;
                            btnMusicReference.Enabled = false;
                            break;
                        case Constants.BellPlayChime:
                            Settings.Default.BellType = Constants.BellPlayChime;
                            Microsoft.SmallBasic.Library.Sound.PlayChimes();
                            mTxtMusicFilePath.Enabled = false;
                            btnMusicReference.Enabled = false;
                            break;
                        case Constants.BellPlayBellRing:
                            Settings.Default.BellType = Constants.BellPlayBellRing;
                            Microsoft.SmallBasic.Library.Sound.PlayBellRing();
                            mTxtMusicFilePath.Enabled = false;
                            btnMusicReference.Enabled = false;
                            break;
                        case Constants.BellPlayMusic:
                            Settings.Default.BellType = Constants.BellPlayMusic;
                            SoundUtility.PlayTimerAsync(Settings.Default.BellSoundFile, SoundUtility.PlaySec.e_10_Sec);
                            mTxtMusicFilePath.Enabled = true;
                            btnMusicReference.Enabled = true;
                            break;
                        default:
                            break;
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// リスト読み込み件数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ntxtReadListNum_TextChanged(object sender, EventArgs e)
        {
            // パラメータチェック
            if (ntxtReadListNum.Text == null) return;

            // 数値変換チェック
            int result;
            if( int.TryParse(ntxtReadListNum.Text, out result ))
            {
                // 整数値のみ許可
                if (result > 0)
                { 
                    Settings.Default.ReadListNum = ntxtReadListNum.IntValue;
                }
                else
                {
                    Settings.Default.ReadListNum = Constants.DefaultReadingNum;
                }
            }
        }

        /// <summary>
        /// 読み込み間隔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            // パラメータチェック
            if (cmb.SelectedItem == null) return;

            // 数値変換チェック
            int result;
            if (int.TryParse(cmb.SelectedItem.ToString(), out result))
            {
                // 整数値のみ許可
                if (result > 0)
                {
                    // 時,分,秒
                    Settings.Default.ReadCycle = new TimeSpan(0, result, 0);
                }
                else
                {
                    // 時,分,秒
                    Settings.Default.ReadCycle = new TimeSpan(0, Constants.DefaultReadCycle, 0);
                }
            }
            Debug.WriteLine("次回読み込み：{0}", ClockUtility.GetReadTime(Settings.Default.ReadCycle));
            lblNextRead.Text = ClockUtility.IsNextTime;
        }

        /// <summary>
        /// 読み込み間隔コンボボックス設定
        /// </summary>
        private void SetCycleCmb()
        {
            // 読み込み間隔データ生成
            List<ReadCycle> readCycle = SetCycle();

            // -----------------------------------
            // 追加レコード作製
            // -----------------------------------
            for (int recordCnt = 0; recordCnt < readCycle.Count; recordCnt++)
            {
                // 値セット
                cmbCycle.Items.Add(readCycle[recordCnt]);
            }
        }

        /// <summary>
        /// 読込間隔（分）
        /// </summary>
        /// <returns></returns>
        private List<ReadCycle> SetCycle()
        {
            // 初期化
            List<ReadCycle> readCycle = new List<ReadCycle>();
            readCycle.Add(new ReadCycle("1", 1));
            readCycle.Add(new ReadCycle("5", 5));
            readCycle.Add(new ReadCycle("10", 10));
            readCycle.Add(new ReadCycle("30", 30));
            readCycle.Add(new ReadCycle("60", 60));

            return readCycle;
        }

        private void openFileDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
