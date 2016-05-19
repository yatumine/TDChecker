using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using COMMON.Const;
using COMMON.Data;
using COMMON.Utility;
using TDChecker.Properties;

namespace TDChecker
{
    public partial class NotifyIconWrapper : Component
    {
        // --------------------------------
        // 外部DLL
        // --------------------------------
        /// <summary>アクティブウィンドウ制御要否判定に使用</summary>
        [DllImport("user32")]
        static extern IntPtr GetForegroundWindow();
        /// <summary>デスクトップをアクティブにする際に使用</summary>
        [System.Runtime.InteropServices.DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>デスクトップをアクティブにする際に使用</summary>
        [System.Runtime.InteropServices.DllImport("user32")]
        static extern IntPtr GetDesktopWindow();

        // --------------------------------
        // メンバー変数
        // --------------------------------
        private Form gMyForm;               // タスクトレイ用フォーム
        private TDNetForm gMyTDForm;        // データ表示用フォーム
        private WebTimer gWebTimer;
        private List<TDData> gTDDataList;   // 適時開示データ内部保持用
        private int gBaloonCount = 0;
        private int gCounter = 0;           // タイマー起動カウント

        /// <summary>
        /// NotifyIconWrapper クラス を生成、初期化
        /// </summary>
        public NotifyIconWrapper(Form pMyForm)
        {
            // コンポーネントの初期化
            this.InitializeComponent();

            // コンテキストメニューのイベントを設定
            this.toolStripMenuItem_Open.Click += this.toolStripMenuItem_Open_Click;
            this.toolStripMenuItem_Exit.Click += this.toolStripMenuItem_Exit_Click;
            this.toolStripMenuItem_Option.Click += this.toolStripMenuItem_Option_Click;

            // TD.net表示用フォーム初期化
            gMyTDForm = null;

            // ----------------------
            // 初期化
            // ----------------------
            InitTray();                                  //トレイアイコンの初期化
            gMyForm = pMyForm;
            ((MainForm)gMyForm).InputCancelFlg = true;   // メインフォーム
            gMyTDForm = null;                            // TD.net表示用フォーム初期化

            // タイマー起動（初回は起動後すぐに読み込み）
            gTDDataList = new List<TDData>();
            gWebTimer = new WebTimer(null, 1000, OnWebTimerElapsed);
            gWebTimer.Start();

            if (gMyTDForm == null || gMyTDForm.IsDisposed == true)
            {
                gMyTDForm = new TDNetForm();
            }

            // Appバージョンの更新通知
            if (TDNet.AppVersionCheck())
            {
                BallonTipInfo(ToolTipIcon.Info, "TDCheckerの最新版が公開されています", "ダウンロードして置き換えるだけでアップデートは完了です。クリックして取得してください。");
            }
        }

        /// <summary>
        //トレイアイコンの初期化
        /// </summary>
        void InitTray()
        {
            while (true) //無限ループ...
            {
                int tickCount = Environment.TickCount;
                this.notifyIcon1.Visible = true;
                tickCount = Environment.TickCount - tickCount;
                if (tickCount < 4000)
                {
                    // 4秒以内に登録できていれば成功
                    break;
                }
                //失敗した場合はVisibleをfalseにしてやりなおし
                this.notifyIcon1.Visible = false;
            }

            // Alt+F4対策#1：ContextMenuStrip表示中にAltキーが押されたら
            //               デスクトップをアクティブにする
            this.contextMenuStrip1.PreviewKeyDown += (sender, e) =>
            {
                if (e.Alt)
                {
                    SetForegroundWindow(GetDesktopWindow());
                }
            };

            // Alt+F4対策#2：メニューを閉じた段階でトレイの擬似ウィンドウがアクティブなら
            //               デスクトップをアクティブにする
            this.contextMenuStrip1.Closing += (sender, e) =>
            {
                //NotifyIconから、擬似ウィンドウ(NativeWindow)を取得
                FieldInfo fi = typeof(NotifyIcon).GetField("window", BindingFlags.NonPublic | BindingFlags.Instance);
                NativeWindow window = fi.GetValue(notifyIcon1) as NativeWindow;

                //アクティブなウィンドウのハンドル＝擬似ウィンドウのハンドルか判定
                IntPtr handle = GetForegroundWindow();
                if (handle == window.Handle)
                {
                    SetForegroundWindow(GetDesktopWindow());
                }
            };
        }

        /// <summary>
        /// コンテナ を指定して NotifyIconWrapper クラス を生成、初期化
        /// </summary>
        /// <param name="container">コンテナ</param>
        public NotifyIconWrapper(IContainer container)
        {
            container.Add(this);

            this.InitializeComponent();
        }

        /// <summary>
        /// コンテキストメニュー "表示" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Open_Click(object sender, EventArgs e)
        {
            // MainWindow表示
            //MyForm.Show();

            if (gMyTDForm == null || gMyTDForm.IsDisposed == true)
            {
                gMyTDForm = new TDNetForm();
                // プロパティに値を設定して子フォームを開く
                gMyTDForm.InputTDDataList = gTDDataList;
            }

            // フォームが未生成なら
            if (gMyTDForm.Created == false)
            {
                gMyTDForm.Show();    // 新規生成
            }
            else
            {
                gMyTDForm.Activate();// フォームをアクティブに
            }
        }

        /// <summary>
        /// コンテキストメニュー "オプション" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Option_Click(object sender, EventArgs e)
        {
            OptionForm OpForm = new OptionForm();
            OpForm.Show();

            // フォームをアクティブにする
            OpForm.Activate();

            // 設定を再読み込み
            Properties.Settings.Default.Reload();
        }

        /// <summary>
        /// コンテキストメニュー "終了" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            // メインフォームの終了
            ((MainForm)gMyForm).InputCancelFlg = false;
            gMyForm.Close();

            // 現在のアプリケーションを終了
            Application.Exit();
        }

        /// <summary>
        /// タスクトレイアイコンをダブルクリックしたとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(gMyTDForm == null || gMyTDForm.IsDisposed == true)
            {
                gMyTDForm = new TDNetForm();
                // プロパティに値を設定して子フォームを開く
                gMyTDForm.InputTDDataList = gTDDataList;
            }

            if (gMyTDForm.Created == false) { 
                gMyTDForm.Show();
            }

            // フォームをアクティブにする
            gMyTDForm.Activate();

            // 設定を再読み込み
            Properties.Settings.Default.Reload();
        }

        /// <summary>
        /// メインタイマー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnWebTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            // 時間計測用のタイマー
            var swTimer = new System.Diagnostics.Stopwatch();
            swTimer.Start();

            Debug.WriteLine("■タイマー起動時刻 {0}",DateTime.Now);
            gWebTimer.Stop();

            // ------------------------------
            // 次回のタイマー時刻設定
            // ------------------------------
            // 指定時刻から5秒間連続で毎秒取得する
            if (this.gCounter < 1)
            {
                int iInterval = 1000;
                // 既に読み込み中なら、タイマーを10秒後に設定し追加読み込みを1回だけにする
                Debug.WriteLine("インターバル　{0}mSec  {1:0.000}秒", iInterval, swTimer.Elapsed.TotalSeconds); 
                this.gWebTimer.SetInterval(iInterval);
                this.gCounter++;
            }
            else
            {
                Debug.WriteLine("インターバル　オプション {0:0.000}秒", swTimer.Elapsed.TotalSeconds);
                double dInterval = ClockUtility.GetReadTime(Settings.Default.ReadCycle);
                this.gWebTimer.SetInterval(dInterval);
                this.gCounter = 0;
            }
            gWebTimer.Start();

            // -----------------------------------
            // 適時開示リスト取得
            // -----------------------------------
            String strWebDocument = null;
            Uri uri;
            HttpUtility htmlUtil = new HttpUtility();
            List<TDData> listNewDate = null;
            List<TDData> listBuff;

            try
            {
                // -----------------------------------
                // Uriインスタンス作成
                // -----------------------------------
                Debug.WriteLine("Uriインスタンス作成 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);
                DateTime dt = DateTime.Now - new TimeSpan(0, 0, 00, 1);
                String strReleaseDate = dt.ToString(Constants.YYYYMMDD_SLASH);
                uri = new Uri(TDNet.GetTDUrl( strReleaseDate ));
                Debug.WriteLine("Uriインスタンス作成完了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                // -----------------------------------
                // Web接続しDocumentを取得
                // -----------------------------------
                Debug.WriteLine("Web接続開始 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                strWebDocument = await htmlUtil.GetWebPageAsync(uri);

                Debug.WriteLine("Web接続終了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                // Document取得確認
                if (strWebDocument == null )
                {
                    return;
                }
                else
                {
                    // -----------------------------------
                    // 取得したDocumentをロード
                    // -----------------------------------
                    Debug.WriteLine("Documentロード開始 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(strWebDocument);

                    Debug.WriteLine("Documentロード終了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                    // -----------------------------------
                    // 適時開示リストの取得処理
                    // -----------------------------------
                    Debug.WriteLine("適時開示リストの取得開始 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                    listBuff = TDNet.ParseDoc(doc, Settings.Default.ReadListNum, strReleaseDate );

                    Debug.WriteLine("表示用リスト取得件数[{0}]件", listBuff.Count);

                    // リストを最新状態に更新し、最新分のデータの見抜きだす
                    gTDDataList = GetNewTDData(listBuff, gTDDataList, out listNewDate);

                    Debug.WriteLine("適時開示リストの取得終了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                }

                // フォームへデータを渡す
                if (gMyTDForm != null && gMyTDForm.IsDisposed != true)
                {
                    if (gTDDataList.Count == 0)
                    {
                        gMyTDForm.InputClearFlg = true;
                    }

                    if(listNewDate.Count > 0)
                    {
                        // リスト内容に変化があれば、最新情報に更新
                        //gMyTDForm.InputTDDataList = listNewDate;
                        gMyTDForm.InputTDDataList = gTDDataList;
                        Debug.WriteLine("フォーム情報更新");
                    }
                }

                // ----------------------
                // バルーン通知用
                // ----------------------
                Action func = async () =>
                {
                    await Task.Run(() =>
                        {
                            if (gBaloonCount < 3)
                            {
                                Debug.WriteLine("バルーン表示開始");
                                if (gMyTDForm == null || gMyTDForm.IsDisposed == true)
                                {
                                    gMyTDForm = new TDNetForm();
                                    // プロパティに値を設定して子フォームを開く
                                    gMyTDForm.InputTDDataList = gTDDataList;
                                }

                                int endBaloon = BalloonForm.ShowNotifyBaloon(listNewDate, ++gBaloonCount, gMyTDForm);
                                Debug.WriteLine("バルーン表示終了");

                                gBaloonCount = gBaloonCount - endBaloon;
                            }
                        });
                };

                // 最新情報を通知
                if (listBuff.Count != 0)
                {
                    func();
                }
            }
            catch (Exception ex)
            {
                LogUtility.WriteTraceLog("【Main Timer】タイマー処理での例外発生",ex);
            }

        }

        /// <summary>
        /// 更新されたデータを取得
        /// </summary>
        /// <param name="pNewList"></param>
        /// <param name="pTDDataList"></param>
        /// <returns></returns>
        private List<TDData> GetNewTDData(List<TDData> pNewList, List<TDData> pOldList, out List<TDData> pRetNewList )
        {
            // 時間計測用のタイマー
            var swTimer = new System.Diagnostics.Stopwatch();
            swTimer.Start();

            List<TDData> updateList = new List<TDData>(pOldList);

            pRetNewList = new List<TDData>();
            int iNewListIndex = 0;

            Debug.WriteLine("------ データ更新件数判定処理 ------");
            Debug.WriteLine("カウント NewList[{0}] 内部リスト[{1}]", pNewList.Count, updateList.Count);
            Debug.WriteLine("データを検索開始 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

            try
            {
                // 最新のリスト内にある未登録データを検索
                if (pNewList == null && updateList == null)
                {
                    return null;
                }

                // 取得リストがすべて最新の場合に備え、更新リスト数を最新リスト件数にしておく
                iNewListIndex = pNewList.Count;

                // 内部保持リストが0件の場合は、全件取得
                if(updateList.Count > 0)
                { 
                    // リスト内検索
                    for (int cnt = 0; cnt < pNewList.Count; cnt++)
                    {
                        Debug.WriteLine("Count[{0}]------------------------------------------------------", cnt);
                        Debug.WriteLine("Time 比較 NewList[{0}] 内部リスト[{1}]", pNewList[cnt].Time, updateList[0].Time);
                        Debug.WriteLine("Code 比較 NewList[{0}] 内部リスト[{1}]", pNewList[cnt].Code, updateList[0].Code);
                        Debug.WriteLine("Title比較 NewList[{0}] 内部リスト[{1}]", pNewList[cnt].Title, updateList[0].Title);

                        // 現在のリストに登録されているIndexを検索
                        if (pNewList[cnt].Time.Equals( updateList[0].Time ) &&
                            pNewList[cnt].Code.Equals( updateList[0].Code ) &&
                            pNewList[cnt].Title.Equals( updateList[0].Title )
                            )
                        {
                            Debug.WriteLine("同一内容発見　インデックス[{0}]", cnt);

                            // 同一の内容があるインデックス
                            iNewListIndex = cnt;
                            break;
                        }
                    }
                }

                Debug.WriteLine("データを検索終了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                Debug.WriteLine("データ更新件数[{0}]", iNewListIndex);
                Debug.WriteLine("データ更新開始 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                // リストを更新
                int iUpdateCnt = iNewListIndex - 1;
                int iNewCnt = 0;
                for (; iUpdateCnt >= 0; iUpdateCnt--, iNewCnt++)
                {
                    // 新着リストを設定
                    pRetNewList.Add(pNewList[iNewCnt]);
                    // 内部保持リストを更新
                    updateList.Insert(0, pNewList[iUpdateCnt]);
                    Debug.WriteLine("データ{0},{1},{2},{3},{4}", iUpdateCnt, pNewList[iUpdateCnt].Time, pNewList[iUpdateCnt].Code, pNewList[iUpdateCnt].CompanyName, pNewList[iUpdateCnt].Title);
                }

                Debug.WriteLine("データ更新終了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

            }
            catch (Exception ex )
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine(ex.Source);
            }

            //// ソート(必要なら）
            Debug.WriteLine("------ データ更新件数判定処理終了 ------");

            return updateList;
        }

        /// <summary>
        /// 通知バルーン
        /// </summary>
        /// <param name="pNotifyIcon">ToolTipIconのEnum</param>
        /// <param name="pTitle">表示タイトル</param>
        /// <param name="pText">本文</param>
        protected void BallonTipInfo(ToolTipIcon pNotifyIcon, String pTitle, String pText)
        {
            // 情報通知バルーン起動
            this.notifyIcon1.BalloonTipIcon = pNotifyIcon;
            this.notifyIcon1.BalloonTipTitle = pTitle;
            this.notifyIcon1.BalloonTipText = pText;

            // 起動
            this.notifyIcon1.ShowBalloonTip(3000);
        }

        /// <summary>
        /// 通知バルーンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            // TDCheckerダウンロードページ
            String strUrl = "http://4doku.com/?page_id=109";
            Boolean bConect = false;

            try
            {
                bConect = HttpUtility.UrlConnectCheck(strUrl);
                if ( bConect )
                { 
                    System.Diagnostics.Process.Start(strUrl);
                }
                else
                {
                    strUrl = "http://4doku.com/";
                    bConect = HttpUtility.UrlConnectCheck(strUrl);
                    if( bConect ) System.Diagnostics.Process.Start(strUrl);
                }
            }
            catch
            {
                // URL解決ができない場合は、処理なし
                Debug.WriteLine("！！！ TDChekerダウンロードページURL解決失敗 ！！！");
                return;
            }
        }

        /// <summary>
        /// 適時開示データ全件取得
        /// </summary>
        private void GetAllTDList()
        {
            // TODO:全件取得処理を追加する。全ページ分読み込む処理を作成する。

            // 時間計測用のタイマー
            var swTimer = new System.Diagnostics.Stopwatch();
            swTimer.Start();

            Debug.WriteLine("■タイマー起動時刻 {0}", DateTime.Now);
            gWebTimer.Stop();

            // ------------------------------
            // 次回のタイマー時刻設定
            // ------------------------------
            // 指定時刻から5秒間連続で毎秒取得する
            if (this.gCounter < 1)
            {
                int iInterval = 1000;
                // 既に読み込み中なら、タイマーを10秒後に設定し追加読み込みを1回だけにする
                Debug.WriteLine("インターバル　{0}mSec  {1:0.000}秒", iInterval, swTimer.Elapsed.TotalSeconds);
                this.gWebTimer.SetInterval(iInterval);
                this.gCounter++;
            }
            else
            {
                Debug.WriteLine("インターバル　オプション {0:0.000}秒", swTimer.Elapsed.TotalSeconds);
                double dInterval = ClockUtility.GetReadTime(Settings.Default.ReadCycle);
                this.gWebTimer.SetInterval(dInterval);
                this.gCounter = 0;
            }
            gWebTimer.Start();

            // -----------------------------------
            // 適時開示リスト取得
            // -----------------------------------
            String strWebDocument = null;
            Uri uri;
            HttpUtility htmlUtil = new HttpUtility();
            List<TDData> listNewDate = null;
            List<TDData> listBuff;

            try
            {
                // -----------------------------------
                // Uriインスタンス作成
                // -----------------------------------
                Debug.WriteLine("Uriインスタンス作成 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);
                DateTime dt = DateTime.Now - new TimeSpan(0, 0, 00, 1);
                String strReleaseDate = dt.ToString(Constants.YYYYMMDD_SLASH);
                uri = new Uri(TDNet.GetTDUrl(strReleaseDate));
                Debug.WriteLine("Uriインスタンス作成完了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                // -----------------------------------
                // Web接続しDocumentを取得
                // -----------------------------------
                Debug.WriteLine("Web接続開始 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                //strWebDocument = await htmlUtil.GetWebPageAsync(uri);

                Debug.WriteLine("Web接続終了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                // Document取得確認
                if (strWebDocument == null)
                {
                    return;
                }
                else
                {
                    // -----------------------------------
                    // 取得したDocumentをロード
                    // -----------------------------------
                    Debug.WriteLine("Documentロード開始 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(strWebDocument);

                    Debug.WriteLine("Documentロード終了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                    // -----------------------------------
                    // 適時開示リストの取得処理
                    // -----------------------------------
                    Debug.WriteLine("適時開示リストの取得開始 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);

                    listBuff = TDNet.ParseDoc(doc, Settings.Default.ReadListNum, strReleaseDate);

                    Debug.WriteLine("表示用リスト取得件数[{0}]件", listBuff.Count);

                    // リストを最新状態に更新し、最新分のデータの見抜きだす
                    gTDDataList = GetNewTDData(listBuff, gTDDataList, out listNewDate);

                    Debug.WriteLine("適時開示リストの取得終了 {0:0.000}秒", swTimer.Elapsed.TotalSeconds);
                }
            }
            catch (UriFormatException)
            {
                Debug.WriteLine("URIの解析に失敗しました。");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("エラー:{0}", ex.Message);
            }
        }
    }
}
