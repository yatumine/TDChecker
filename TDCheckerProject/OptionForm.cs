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

namespace TDChecker
{
    public partial class OptionForm : MetroForm/* ←変更 */
    {
        // クラス内関数
        private static Boolean bInitializeFlg = false;

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
            bInitializeFlg = true;
            InitializeOption();
            bInitializeFlg = false;
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
                openFileDialog1.InitialDirectory = mTxtMusicFilePath.Text;
            }

            // ダイアログを表示し、戻り値が [OK] の場合は、選択したファイルを表示する
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // 選択されたファイル名を設定
                mTxtMusicFilePath.Text = openFileDialog1.FileName;
            }

            // 不要になった時点で破棄する (正しくは オブジェクトの破棄を保証する を参照)
            openFileDialog1.Dispose();

            Settings.Default.BellSoundFile = mTxtMusicFilePath.Text;
            SoundUtility.PlayTimerAsync(Settings.Default.BellSoundFile, 10);

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
                case Constants.BELL_NO_SOUND:
                    mTxtMusicFilePath.Enabled = false;
                    btnMusicReference.Enabled = false;
                    break;
                case Constants.BELL_PLAY_CHIME:
                    Settings.Default.BellType = Constants.BELL_PLAY_CHIME;
                    mTxtMusicFilePath.Enabled = false;
                    btnMusicReference.Enabled = false;
                    break;
                case Constants.BELL_PLAY_BELL_RING:
                    Settings.Default.BellType = Constants.BELL_PLAY_BELL_RING;
                    mTxtMusicFilePath.Enabled = false;
                    btnMusicReference.Enabled = false;
                    break;
                case Constants.BELL_PLAY_MUSIC:
                    Settings.Default.BellType = Constants.BELL_PLAY_MUSIC;
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
                    RadioButton rdBell = (RadioButton)selectCtl;
                    short srtSelectNo = short.Parse(rdBell.Name.Substring((rdBell.Name.Length - 1)));

                    if (srtSelectNo == Settings.Default.BellType)
                    {
                        rdBell.Checked = true;
                    }
                    else
                    {
                        rdBell.Checked = false;
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
            if (bInitializeFlg)
            {
                return;
            }
            // BellSoundOption（grpBellSound）の選択項目を取得
            foreach (Control selectCtl in grpBellSound.Controls)
            {
                short srtSelectNo = 0;
                if (selectCtl is RadioButton && ((RadioButton)selectCtl).Checked)
                {
                    RadioButton rdBell = (RadioButton)selectCtl;
                    srtSelectNo = short.Parse(rdBell.Name.Substring((rdBell.Name.Length - 1)));
                    switch (srtSelectNo)
                    {
                        case Constants.BELL_NO_SOUND:
                            Settings.Default.BellType = Constants.BELL_NO_SOUND;
                            mTxtMusicFilePath.Enabled = false;
                            btnMusicReference.Enabled = false;
                            break;
                        case Constants.BELL_PLAY_CHIME:
                            Settings.Default.BellType = Constants.BELL_PLAY_CHIME;
                            Microsoft.SmallBasic.Library.Sound.PlayChimes();
                            mTxtMusicFilePath.Enabled = false;
                            btnMusicReference.Enabled = false;
                            break;
                        case Constants.BELL_PLAY_BELL_RING:
                            Settings.Default.BellType = Constants.BELL_PLAY_BELL_RING;
                            Microsoft.SmallBasic.Library.Sound.PlayBellRing();
                            mTxtMusicFilePath.Enabled = false;
                            btnMusicReference.Enabled = false;
                            break;
                        case Constants.BELL_PLAY_MUSIC:
                            Settings.Default.BellType = Constants.BELL_PLAY_MUSIC;
                            SoundUtility.PlayTimerAsync(Settings.Default.BellSoundFile, 10);
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
                    Settings.Default.ReadListNum = Constants.DEFAULT_READLISTNUM;
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

            //// 数値変換チェック
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
                    Settings.Default.ReadCycle = new TimeSpan(0, Constants.DEFAULT_READCYCLE, 0);
                }
            }
            Console.WriteLine("次回読み込み：{0}", ClockUtility.GetReadTime(Settings.Default.ReadCycle));
            lblNextRead.Text = ClockUtility.IsNextTime;
        }

        /// <summary>
        /// 読み込み間隔コンボボックス設定
        /// </summary>
        private void SetCycleCmb()
        {
            // 読み込み間隔データ生成
            List<ReadCycle> rc = SetCycle();

            // -----------------------------------
            // 追加レコード作製
            // -----------------------------------
            for (int cnt = 0; cnt < rc.Count; cnt++)
            {
                // 値セット
                cmbCycle.Items.Add(rc[cnt]);
            }
        }

        private List<ReadCycle> SetCycle()
        {
            // 初期化
            List<ReadCycle> rc = new List<ReadCycle>();
            rc.Add(new ReadCycle("1", 1));
            rc.Add(new ReadCycle("5", 5));
            rc.Add(new ReadCycle("10", 10));
            rc.Add(new ReadCycle("30", 30));
            rc.Add(new ReadCycle("60", 60));

            return rc;
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
