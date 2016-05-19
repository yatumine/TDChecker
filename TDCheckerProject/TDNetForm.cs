using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMMON.Data;

// 追加
using MetroFramework.Forms;
using COMMON.Const;
using COMMON.Utility;
using System.Diagnostics;
using TDChecker.Properties;

namespace TDChecker
{
    public partial class TDNetForm : MetroForm // 変更
    {
        /* プロパティの設定 */
        public List<TDData> InputTDDataList { get; set; }
        public bool InputClearFlg { get; set; }

        // --------------------
        // メンバー変数
        // --------------------
        private TDData gOldData;
        private Boolean gInitFlg = true;    // 画面起動フラグ
        private Boolean gInitReadFlg = true;// 画面起動時初回読み込みフラグ
        private Boolean gKeyWordFlg = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="argumentValues"></param>
        public TDNetForm() 
        {
            // 初期化フラグOn
            gInitFlg = true;

            InitializeComponent();

            // 初期化
            gOldData = new TDData();
            lblClock.Text = DateTime.Now.ToString(Constants.YYYYMMDDHHMM_SLASH);
            timer1.Enabled = false;

            // -------------------
            // リスト設定
            // -------------------
            // カラム名変更
            mGridMain.Columns[0].HeaderCell.Value = "日";
            mGridMain.Columns[1].HeaderCell.Value = "時刻";
            mGridMain.Columns[2].HeaderCell.Value = "銘柄";
            mGridMain.Columns[3].HeaderCell.Value = "会社";
            mGridMain.Columns[4].HeaderCell.Value = "タイトル";
            mGridMain.Columns[5].HeaderCell.Value = "URL";

            mGridKeyWord.Columns[0].HeaderCell.Value = "日";
            mGridKeyWord.Columns[1].HeaderCell.Value = "時刻";
            mGridKeyWord.Columns[2].HeaderCell.Value = "銘柄";
            mGridKeyWord.Columns[3].HeaderCell.Value = "会社";
            mGridKeyWord.Columns[4].HeaderCell.Value = "タイトル";
            mGridKeyWord.Columns[5].HeaderCell.Value = "URL";

            // 初期化フラグOff
            gInitFlg = false;
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TDNetForm_Load(object sender, EventArgs e)
        {
            // 検索設定の取得
            GetKeyWord();

            // タイマー起動
            timer1.Enabled = true;

            // ツールチップ設定
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(this.mtxtKeyWord, "複数銘柄を登録する場合には、カンマ「,」で区切ってください");

        }
        /// <summary>
        /// 閉じるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// メインタイマ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            try { 
                // 時計更新
                lblClock.Text = DateTime.Now.ToString(Constants.YYYYMMDDHHMM_SLASH);


                if (gInitFlg)
                {
                    return;
                }

                timer1.Enabled = false;

                // ボタン設定
                SetFilterStatus(gKeyWordFlg);

                // 検索キーワード存在
                if (mtxtKeyWord.Text != "" && gKeyWordFlg == true)
                {
                    // ------------------------------
                    // 画面の再描画 検索
                    // ------------------------------
                    KeyWordMode(Constants.GRID_MODE_KEYWORD);
                    SetDisplayGrid(Constants.GRID_MODE_KEYWORD);

                }
                else
                {
                    // ------------------------------
                    // 画面の再描画 全件
                    // ------------------------------
                    KeyWordMode(Constants.GRID_MODE_NOMAL);
                    SetDisplayGrid(Constants.GRID_MODE_NOMAL);

                } 

                // アクティブグリッド表示件数
                GetListCount();

                gInitReadFlg = false;
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                LogUtility.WriteTraceLog("【SubForm】 Timerでの例外発生", ex);
                this.Close();
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// オプションボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //フォーカスを外す
            this.ActiveControl = null;
            OptionForm OpForm = new OptionForm();
            OpForm.Show();

            // 設定を再読み込み
            Properties.Settings.Default.Reload();
        }

        /// <summary>
        /// フィルタ検索ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void metroButton1_Click(object sender, EventArgs e)
        {

            if (gKeyWordFlg == false)
            {
                // キーワード検索モード
                KeyWordMode(Constants.GRID_MODE_KEYWORD, true);
            }
            else
            {
                // 通常モード
                KeyWordMode(Constants.GRID_MODE_NOMAL);

            }

            // ボタン設定
            SetFilterStatus(gKeyWordFlg);

            // 検索設定の保存
            SetKeyWord();
        }

        /// <summary>
        /// フィルタボタンの状態設定
        /// </summary>
        private void SetFilterStatus( Boolean pKeyWordFilter )
        {
            // キーワード検索判定
            if (pKeyWordFilter == true)
            {
                // キーワード検索ON
                mBtnFilter.BackColor = Color.Gray;
                mBtnFilter.ForeColor = Color.White;
                mBtnFilter.Text = Constants.BTN_FILTER_ON;

            }
            else
            {
                // キーワード検索OFF
                mBtnFilter.BackColor = Control.DefaultBackColor;
                mBtnFilter.ForeColor = Color.Black;
                mBtnFilter.Text = Constants.BTN_FILTER_DEFAULT;

            }
        }

        /// <summary>
        /// 再描画
        /// </summary>
        /// <param name="pRow"></param>
        private void ListRepaint(DataRow[] pRow = null)
        {
            // 検索設定の保存
            SetKeyWord();

            // 適時開示リストデータチェック
            if (InputTDDataList == null)
            {
                return;
            }

            // バッファにコピー
            List<TDData> listBuff = new List<TDData>(InputTDDataList);
            
            // ---------------------------------------------
            // 再描写
            // ---------------------------------------------
            if (listBuff.Count != 0 &&
                (!listBuff[0].Time.Equals(gOldData.Time) ||
                 !listBuff[0].Code.Equals(gOldData.Code) ||
                 !listBuff[0].Title.Equals(gOldData.Title))
                )
            {

                // --------------------
                // 検索データ表示
                // --------------------
                // 検索データがある または 検索モードの場合、検索データを使用
                if (pRow != null && gKeyWordFlg == true)
                {
                    // 検索データをセット
                    foreach (DataRow dRow in pRow)
                    {
                        // Key検索用グリッドの表示
                        tdDataListKey.TDNet.ImportRow(dRow);
                    }

                    //tdDataListKey.TDNet.Select("", "DataColumnTime DESC");

                    // -----------------------------------
                    // oldデータへ時刻が最新なものを保持
                    // ※リストにデータ追加後にoldデータを取得すること
                    // -----------------------------------
                    SetOldList(listBuff[0]);

                    return;
                }
                else if ( gKeyWordFlg == false )
                {
                    // --------------------
                    // 通常表示
                    // --------------------
                    Debug.WriteLine("◆再描写　list count[{0}] old[{1}]", listBuff.Count, gOldData.Code);
                    for (int cnt = 0; cnt < listBuff.Count; cnt++)
                    {
                        Debug.WriteLine("データ{0},{1},{2},{3},{4}", cnt, listBuff[cnt].Time, listBuff[cnt].Code, listBuff[cnt].CompanyName, listBuff[cnt].Title);

                        // -----------------------------------
                        // 追加レコード作製
                        // -----------------------------------
                        DataRow dRow;
                        dRow = tDDataList.TDNet.NewRow();
                        // パラメータ設定
                        dRow["DataColumnDate"] = listBuff[cnt].Date;
                        dRow["DataColumnTime"] = listBuff[cnt].Time;
                        dRow["DataColumnCode"] = listBuff[cnt].Code;
                        dRow["DataColumnCompanyName"] = listBuff[cnt].CompanyName;
                        dRow["DataColumnTitle"] = listBuff[cnt].Title;
                        dRow["DataColumnURL"] = listBuff[cnt].URL;

                        // -----------------------------------
                        // 行追加
                        // -----------------------------------
                        if (gInitReadFlg == true)
                        {
                            // oldデータがnullなら末尾から追加
                            tDDataList.TDNet.Rows.Add(dRow);
                        }
                        else
                        {
                            // oldデータが存在するなら先頭から追加
                            tDDataList.TDNet.Rows.InsertAt(dRow, 0);
                        }

                    }
                }

                // -----------------------------------
                // oldデータへ時刻が最新なものを保持
                // ※リストにデータ追加後にoldデータを取得すること
                // -----------------------------------
                SetOldList(listBuff[0]);

            }
            else if (InputClearFlg == true)
            {
                gOldData = new TDData();
                // Key検索用の表クリア
                tdDataListKey.Clear();
                // 全件表示用の表クリア
                tDDataList.Clear();
                InputClearFlg = false;
            }
        }

        /// <summary>
        /// oldデータへ時刻が最新な適時開示データを保存
        /// </summary>
        /// <param name="pNewData"></param>
        /// <returns></returns>
        private Boolean SetOldList(TDData pNewData)
        {
            gOldData.Date = pNewData.Date;
            gOldData.Time = pNewData.Time;
            gOldData.Code = pNewData.Code;
            gOldData.CompanyName = pNewData.CompanyName;
            gOldData.Title = pNewData.Title;
            gOldData.URL = pNewData.URL;

            return true;
        }

        /// <summary>
        /// アクティブリスト件数表示
        /// </summary>
        private void GetListCount()
        {
            // 現在アクティブなリストを判定
            if (mGridMain.Visible == true)
            {
                // 取得リスト件数
                lblListNum.Text = mGridMain.RowCount + "件";
                mGridMain.Refresh();

            }
            else
            {
                // 取得リスト件数
                lblListNum.Text = mGridKeyWord.RowCount + "件";
                mGridMain.Refresh();

            }

        }

        /// <summary>
        /// 検索オプションの設定
        /// </summary>
        private void SetKeyWord()
        {
            // KeyWord 使用フラグ
            Settings.Default.KeyFlag = gKeyWordFlg;
            // KeyWord　Code設定
            Settings.Default.KeyCode = mtxtKeyWord.Text;

            // 設定の保存
            Settings.Default.Save();
        }

        /// <summary>
        /// 検索オプションの取得
        /// </summary>
        private void GetKeyWord()
        {
            // 設定の読み込み
            Settings.Default.Reload();

            // KeyWord 使用フラグ
            gKeyWordFlg = Settings.Default.KeyFlag;
            // KeyWord　Code設定
            mtxtKeyWord.Text = Settings.Default.KeyCode;

        }

        /// <summary>
        /// キーワード入力後エンタ―
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mtxtKeyWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Enter)
            {
                return;
            }

            if (gKeyWordFlg == false)
            {

                // 再表示用に初期化
                gOldData = new TDData();

                // Key検索用の表クリア
                tdDataListKey.Clear();

                // キーワード検索モード
                KeyWordMode(Constants.GRID_MODE_KEYWORD,true);

            }
            else
            {
                // 通常モード
                KeyWordMode(Constants.GRID_MODE_NOMAL);

            }

            // 検索設定の保存
            SetKeyWord();

        }


        /// <summary>
        /// キーワード検索モードON処理
        /// </summary>
        /// <param name="pKeyWordFlg"></param>
        /// <returns></returns>
        private Boolean KeyWordMode(int pKeyWordMode,Boolean pForceReset = false)
        {

            switch (pKeyWordMode)
            {
                case Constants.GRID_MODE_NOMAL:
                    // ----------------------------------
                    // 通常モード
                    // ----------------------------------

                    // -------------------------------
                    // 再表示が必要か判断
                    // -------------------------------
                    if (tDDataList.TDNet.Count == 0 || pForceReset == true)
                    {
                        // 再表示用に初期化
                        gOldData = new TDData();

                        // 全表示用の表クリア
                        tDDataList.Clear();
                    }
                    gKeyWordFlg = false;
                    ListRepaint();
                    break;
                case Constants.GRID_MODE_KEYWORD:
                    // -----------------------
                    // キーワード検索モード
                    // -----------------------

                    gKeyWordFlg = true;
                    // 全件表示用にデータが存在するか
                    if (tDDataList.TDNet.Count == 0)
                    {
                        // 再表示用に初期化
                        gOldData = new TDData();

                        // マスターデータの読み込み処理
                        ListRepaint();
                    }

                    // -------------------------------
                    // 再表示が必要か判断
                    // -------------------------------
                    if (tdDataListKey.TDNet.Count == 0 || pForceReset == true)
                    {
                        // 再表示用に初期化
                        gOldData = new TDData();

                        // Key検索用の表クリア
                        tdDataListKey.Clear();
                    }

                    // マスターデータから検索
                    DataRow[] rows = DataUtility.GetKeyWordData(tDDataList.TDNet, "DataColumnCode", mtxtKeyWord.Text);
                    ListRepaint(rows);
                    break;
                case Constants.GRID_MODE_AUTOSELECT:
                    if (mGridMain.Visible == true)
                    {
                        // 通常モード表示グリッド切り替え
                        mGridKeyWord.Visible = false;
                        mGridMain.Visible = true;
                    }
                    else
                    {
                        // 検索モード表示グリッド切り替え
                        mGridKeyWord.Visible = true;
                        mGridMain.Visible = false;
                    }
                    break;
            }

            return true;
        }

        /// <summary>
        /// 表示グリッド切り替え
        /// </summary>
        /// <param name="pKeyWordMode"></param>
        private void SetDisplayGrid(int pMode)
        {

            switch (pMode)
            {
                case Constants.GRID_MODE_NOMAL:
                    // 通常モード表示グリッド切り替え
                    mGridKeyWord.Visible = false;
                    mGridMain.Visible = true;
                    break;
                case Constants.GRID_MODE_KEYWORD:
                    // 検索モード表示グリッド切り替え
                    mGridKeyWord.Visible = true;
                    mGridMain.Visible = false;
                    break;
                case Constants.GRID_MODE_AUTOSELECT:
                    if (mGridMain.Visible == true)
                    {
                        // 通常モード表示グリッド切り替え
                        mGridKeyWord.Visible = false;
                        mGridMain.Visible = true;
                    }
                    else
                    {
                        // 検索モード表示グリッド切り替え
                        mGridKeyWord.Visible = true;
                        mGridMain.Visible = false;
                    }
                    break;
            }
        }

        /// <summary>
        /// データグリッドのセルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mGrid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try { 
                DataGridView dgv = (DataGridView)sender;
                DataGridViewSelectedRowCollection dgvCol = dgv.SelectedRows;

                // 銘柄コードカラムの列番号取得
                // DataGridViewの1行目のDataGridViewRow.DataBoundItemをDataRowViewとして取得
                DataRowView drv = dgvCol[0].DataBoundItem as DataRowView;
                // DataRowView.RowがDataRow型
                DataRow dr = drv.Row;

                String strCode = dr["DataColumnCode"].ToString();

                // クリップボードに銘柄コード設定
                Clipboard.SetText(strCode);

                lblStatus.Text = Constants.MSG_CODE_COPY + "[" + strCode + "]";
            }
            catch(Exception)
            {
                return;
            }
        }
    }
}
