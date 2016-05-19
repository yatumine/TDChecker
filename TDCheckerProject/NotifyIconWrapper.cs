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
        private Form MyForm;               // タスクトレイ用フォーム
        private TDNetForm MyTDForm;        // データ表示用フォーム
        private WebTimer WebTimer;
        private List<TDData> TDDataList;   // 適時開示データ内部保持用
        private int BaloonCount = 0;
        private int Counter = 0;           // タイマー起動カウント

        /// <summary>
        /// NotifyIconWrapper クラス を生成、初期化
        /// </summary>
        public NotifyIconWrapper(Form myForm)
        {
            // コンポーネントの初期化
            this.InitializeComponent();

            // コンテキストメニューのイベントを設定
            this.toolStripMenuItem_Open.Click += this.toolStripMenuItem_Open_Click;
            this.toolStripMenuItem_Exit.Click += this.toolStripMenuItem_Exit_Click;
            this.toolStripMenuItem_Option.Click += this.toolStripMenuItem_Option_Click;

            // TD.net表示用フォーム初期化
            MyTDForm = null;

            // ----------------------
            // 初期化
            // ----------------------
            InitTray();                                  //トレイアイコンの初期化
            MyForm = myForm;
            ((MainForm)MyForm).InputCancelFlg = true;   // メインフォーム
            MyTDForm = null;                            // TD.net表示用フォーム初期化

            // タイマー起動（初回は起動後すぐに読み込み）
            TDDataList = new List<TDData>();
            WebTimer = new WebTimer(null, 1000, OnWebTimerElapsed);
            WebTimer.Start();

            if (MyTDForm == null || MyTDForm.IsDisposed == true)
            {
                MyTDForm = new TDNetForm();
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
            if (MyTDForm == null || MyTDForm.IsDisposed == true)
            {
                MyTDForm = new TDNetForm();
                // プロパティに値を設定して子フォームを開く
                MyTDForm.InputTDDataList = TDDataList;
            }

            // フォームが未生成なら
            if (MyTDForm.Created == false)
            {
                MyTDForm.Show();    // 新規生成
            }
            else
            {
                MyTDForm.Activate();// フォームをアクティブに
            }
        }

        /// <summary>
        /// コンテキストメニュー "オプション" を選択したとき呼ばれます。
        /// </summary>
        /// <param name="sender">呼び出し元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        private void toolStripMenuItem_Option_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm();
            optionForm.Show();

            // フォームをアクティブにする
            optionForm.Activate();

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
            ((MainForm)MyForm).InputCancelFlg = false;
            MyForm.Close();

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
            if(MyTDForm == null || MyTDForm.IsDisposed == true)
            {
                MyTDForm = new TDNetForm();
                // プロパティに値を設定して子フォームを開く
                MyTDForm.InputTDDataList = TDDataList;
            }

            if (MyTDForm.Created == false) { 
                MyTDForm.Show();
            }

            // フォームをアクティブにする
            MyTDForm.Activate();

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
            var stopWatchTimer = new System.Diagnostics.Stopwatch();
            stopWatchTimer.Start();

            Debug.WriteLine("■タイマー起動時刻 {0}",DateTime.Now);
            WebTimer.Stop();

            // ------------------------------
            // 次回のタイマー時刻設定
            // ------------------------------
            // 指定時刻から5秒間連続で毎秒取得する
            if (this.Counter < 1)
            {
                int iInterval = 1000;
                // 既に読み込み中なら、タイマーを10秒後に設定し追加読み込みを1回だけにする
                Debug.WriteLine("インターバル　{0}mSec  {1:0.000}秒", iInterval, stopWatchTimer.Elapsed.TotalSeconds); 
                this.WebTimer.SetInterval(iInterval);
                this.Counter++;
            }
            else
            {
                Debug.WriteLine("インターバル　オプション {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);
                double dInterval = ClockUtility.GetReadTime(Settings.Default.ReadCycle);
                this.WebTimer.SetInterval(dInterval);
                this.Counter = 0;
            }
            WebTimer.Start();

            // -----------------------------------
            // 適時開示リスト取得
            // -----------------------------------

            // Uriインスタンス作成
            Debug.WriteLine("Uriインスタンス作成 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);
            DateTime dt = DateTime.Now - new TimeSpan(0, 0, 00, 1);
            String strReleaseDate = dt.ToString(Constants.YYYYMMDD_SLASH);
            Uri uri = new Uri(TDNet.GetTDUrl( strReleaseDate ));
            Debug.WriteLine("Uriインスタンス作成完了 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

            // Web接続しDocumentを取得
            Debug.WriteLine("Web接続開始 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);
            HttpUtility htmlUtil = new HttpUtility();
            String webDocument = await htmlUtil.GetWebPageAsync(uri);

            Debug.WriteLine("Web接続終了 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

            List<TDData> listNewDate = null;
            List<TDData> listBuff;

            // Document取得確認
            if (webDocument == null )
            {
                return;
            }

            // 取得したDocumentをロード
            Debug.WriteLine("Documentロード開始 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(webDocument);

            Debug.WriteLine("Documentロード終了 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

            // 適時開示リストの取得処理
            Debug.WriteLine("適時開示リストの取得開始 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

            listBuff = TDNet.ParseDoc(doc, Settings.Default.ReadListNum, strReleaseDate );

            Debug.WriteLine("表示用リスト取得件数[{0}]件", listBuff.Count);

            // リストを最新状態に更新し、最新分のデータの見抜きだす
            TDDataList = GetNewTDData(listBuff, TDDataList, out listNewDate);

            Debug.WriteLine("適時開示リストの取得終了 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

            // フォームへデータを渡す
            if (MyTDForm != null && MyTDForm.IsDisposed != true)
            {
                if (TDDataList.Count == 0)
                {
                    MyTDForm.InputClearFlg = true;
                }

                if(listNewDate.Count > 0)
                {
                    // リスト内容に変化があれば、最新情報に更新
                    MyTDForm.InputTDDataList = TDDataList;
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
                        if (BaloonCount < 3)
                        {
                            Debug.WriteLine("バルーン表示開始");
                            if (MyTDForm == null || MyTDForm.IsDisposed == true)
                            {
                                MyTDForm = new TDNetForm();
                                // プロパティに値を設定して子フォームを開く
                                MyTDForm.InputTDDataList = TDDataList;
                            }

                            int endBaloon = BalloonForm.ShowNotifyBaloon(listNewDate, ++BaloonCount, MyTDForm);
                            Debug.WriteLine("バルーン表示終了");

                            BaloonCount = BaloonCount - endBaloon;
                        }
                    });
            };

            // 最新情報を通知
            if (listBuff.Count != 0)
            {
                func();
            }
        }

        /// <summary>
        /// 更新されたデータを取得
        /// </summary>
        /// <param name="newList"></param>
        /// <param name="pTDDataList"></param>
        /// <returns></returns>
        private List<TDData> GetNewTDData(List<TDData> newList, List<TDData> oldList, out List<TDData> outNewList )
        {
            // 時間計測用のタイマー
            var stopWatchTimer = new System.Diagnostics.Stopwatch();
            stopWatchTimer.Start();

            List<TDData> upDateList = new List<TDData>(oldList);

            outNewList = new List<TDData>();
            int newListIndex = 0;

            Debug.WriteLine("------ データ更新件数判定処理 ------");
            Debug.WriteLine("カウント NewList[{0}] 内部リスト[{1}]", newList.Count, upDateList.Count);
            Debug.WriteLine("データを検索開始 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

            try
            {
                // 最新のリスト内にある未登録データを検索
                if (newList == null && upDateList == null)
                {
                    return null;
                }

                // 取得リストがすべて最新の場合に備え、更新リスト数を最新リスト件数にしておく
                newListIndex = newList.Count;

                // 内部保持リストが0件の場合は、全件取得
                if (upDateList.Count > 0)
                {
                    // リスト内検索
                    for (int cnt = 0; cnt < newList.Count; cnt++)
                    {
                        Debug.WriteLine("Count[{0}]------------------------------------------------------", cnt);
                        Debug.WriteLine("Time 比較 NewList[{0}] 内部リスト[{1}]", newList[cnt].Time, upDateList[0].Time);
                        Debug.WriteLine("Code 比較 NewList[{0}] 内部リスト[{1}]", newList[cnt].Code, upDateList[0].Code);
                        Debug.WriteLine("Title比較 NewList[{0}] 内部リスト[{1}]", newList[cnt].Title, upDateList[0].Title);

                        // 現在のリストに登録されているIndexを検索
                        if (newList[cnt].Time.Equals(upDateList[0].Time) &&
                            newList[cnt].Code.Equals(upDateList[0].Code) &&
                            newList[cnt].Title.Equals(upDateList[0].Title)
                            )
                        {
                            Debug.WriteLine("同一内容発見　インデックス[{0}]", cnt);

                            // 同一の内容があるインデックス
                            newListIndex = cnt;
                            break;
                        }
                    }
                }

                Debug.WriteLine("データを検索終了 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

                Debug.WriteLine("データ更新件数[{0}]", newListIndex);
                Debug.WriteLine("データ更新開始 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

                // リストを更新
                int updateCnt = newListIndex - 1;
                int newCnt = 0;
                for (; updateCnt >= 0; updateCnt--, newCnt++)
                {
                    // 新着リストを設定
                    outNewList.Add(newList[newCnt]);
                    // 内部保持リストを更新
                    upDateList.Insert(0, newList[updateCnt]);
                    Debug.WriteLine("データ{0},{1},{2},{3},{4}", updateCnt, newList[updateCnt].Time, newList[updateCnt].Code, newList[updateCnt].CompanyName, newList[updateCnt].Title);
                }

                Debug.WriteLine("データ更新終了 {0:0.000}秒", stopWatchTimer.Elapsed.TotalSeconds);

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Debug.WriteLine("Error:{0}",ex.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error:{0}",ex.Message);
            }

            // TODO: ソート(必要なら）
            Debug.WriteLine("------ データ更新件数判定処理終了 ------");

            return upDateList;
        }

        /// <summary>
        /// 通知バルーン
        /// </summary>
        /// <param name="notifyIcon">ToolTipIconのEnum</param>
        /// <param name="title">表示タイトル</param>
        /// <param name="text">本文</param>
        protected void BallonTipInfo(ToolTipIcon notifyIcon, String title, String text)
        {
            // 情報通知バルーン起動
            this.notifyIcon1.BalloonTipIcon = notifyIcon;
            this.notifyIcon1.BalloonTipTitle = title;
            this.notifyIcon1.BalloonTipText = text;

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
            String url = "http://4doku.com/?page_id=109";
            Boolean conectFlg = false;

            try
            {
                conectFlg = HttpUtility.UrlConnectCheck(url);
                if ( conectFlg )
                {
                    Process.Start(url);
                }
                else
                {
                    url = "http://4doku.com/";
                    conectFlg = HttpUtility.UrlConnectCheck(url);
                    if( conectFlg ) Process.Start(url);
                }
            }
            catch(Exception)
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
            WebTimer.Stop();

            // ------------------------------
            // 次回のタイマー時刻設定
            // ------------------------------
            // 指定時刻から5秒間連続で毎秒取得する
            if (this.Counter < 1)
            {
                int iInterval = 1000;
                // 既に読み込み中なら、タイマーを10秒後に設定し追加読み込みを1回だけにする
                Debug.WriteLine("インターバル　{0}mSec  {1:0.000}秒", iInterval, swTimer.Elapsed.TotalSeconds);
                this.WebTimer.SetInterval(iInterval);
                this.Counter++;
            }
            else
            {
                Debug.WriteLine("インターバル　オプション {0:0.000}秒", swTimer.Elapsed.TotalSeconds);
                double dInterval = ClockUtility.GetReadTime(Settings.Default.ReadCycle);
                this.WebTimer.SetInterval(dInterval);
                this.Counter = 0;
            }
            WebTimer.Start();

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
                    TDDataList = GetNewTDData(listBuff, TDDataList, out listNewDate);

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
