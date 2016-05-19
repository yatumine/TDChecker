using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using COMMON.Data;
using COMMON.Utility;

// 追加
using MetroFramework.Forms;
using System.Diagnostics;
using System.Data;
using System.Linq;
using TDChecker.Properties;

namespace TDChecker
{
    public partial class BalloonForm : MetroForm/* ←変更 */
    {
        /* プロパティの設定 */
        private String InputTitle { get; set; }
        private String InputCode { get; set; }
        private String InputCompany { get; set; }
        private List<TDData> InputList { get; set; }
        private int InputDispCount { get; set; }

        // ---------------------
        // メンバー変数
        // ---------------------
        private int gIndex = 0;
        private Timer gFormActivetimer;
        private Boolean gTimerFlg = false;
        private TDData[] gTDDataList;
        private TDNetForm gForm;

        public BalloonForm(TDNetForm pForm = null)
        {
            // 起動用フォーム内部保持
            gForm = pForm;
            InitializeComponent();
            this.ShowIcon = false;
        }

        // ---------------------
        // メイン処理
        // ---------------------

        /// <summary>
        /// バルーン呼び出し処理：単体
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        static public int ShowNotifyBaloon(String pCode, String pCompany, String pTitle)
        {
            int ret = 1;
            BalloonForm f = new BalloonForm();
            f.InputCode = pCode;
            f.InputCompany = pCompany;
            f.InputTitle = pTitle;
            f.ShowDialog();
            f.Dispose();

            return ret;
        }
        /// <summary>
        /// バルーン呼び出し処理：リスト
        /// </summary>
        /// <param name="pList"></param>
        /// <returns></returns>
        static public int ShowNotifyBaloon(List<TDData> pList, int pBaloonCount, TDNetForm pForm = null)
        {
            int ret = 1;
            if (pList.Count != 0)
            { 
                BalloonForm f = new BalloonForm( pForm );
                f.InputDispCount = pBaloonCount;
                f.InputList = pList;
                SoundUtility.CallSound( ( SoundUtility.SOUND_TYPE )Properties.Settings.Default.BellType );
                f.ShowDialog();
                f.Dispose();
            }
            return ret;
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BalloonForm_Load(object sender, EventArgs e)
        {
            try {

                // 初期化
                lblCode.Text = "";
                lblTitle.Text = "";
                lblCompany.Text = "";
                lblListMin.Text = "";
                lblListMax.Text = "";

                // リスト存在
                if (InputList == null)
                {
                    this.Close();
                    return;
                }

                // リスト件数
                if ( InputList.Count == 0)
                {
                    this.Close();
                    return;
                }

                // キーワード使用/不使用
                if (Settings.Default.KeyFlag == true)
                {
                    // キーワード検索
                    TDData[] tddata;
                    Boolean bKeyWordFlg;
                    bKeyWordFlg = DataUtility.GetKeyWordRow(InputList, Settings.Default.KeyCode, out tddata);

                    // メンバー変数で保持
                    gTDDataList = tddata;

                    LogPut();
                    // キーワードがない場合全件検索
                    if (bKeyWordFlg == false)
                    {
                        this.Close();
                        return;
                    }
                }

                if (Settings.Default.KeyFlag == false)
                {
                    LogPut2();
                }

                // リスト表示
                DisplayList( gIndex);

                // ウィンドウを画面右下に表示させる
                int left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
                int top = Screen.PrimaryScreen.WorkingArea.Height - this.Height * InputDispCount;
                this.DesktopBounds = new Rectangle(left, top, this.Width, this.Height);

                //this.DesktopLocation = new Point(700, 700);
                this.Opacity = 0;
                gTimerFlg = true;
                apperTimer.Enabled = true;
                apperTimer.Interval = 50;
                apperTimer.Start();
                apperTimer.Tick += new EventHandler(appear_Tick);

                // タスクバー非表示
                //this.ShowInTaskbar = false;

                // ボタン状態
                if (gIndex == 0)
                {
                    btnPrev.Enabled = false;
                }
                else
                {
                    btnPrev.Enabled = true;
                }

                // ボタン状態
                if (gIndex >= InputList.Count)
                {
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                }
                gFormActivetimer = new Timer();
                gFormActivetimer.Enabled = true;
                gFormActivetimer.Interval = 50;
                gFormActivetimer.Start();
                gFormActivetimer.Tick += new EventHandler(active_Tick);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.Source);
                Debug.WriteLine(ex.TargetSite);
            }
        }

        /// <summary>
        /// タイマー処理：非アクティブ最前面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void active_Tick(object sender, EventArgs e)
        {
            setNotActiveWindow(this.Handle);
        }

        /// <summary>
        /// タイマー処理：フェードイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void appear_Tick(object sender, EventArgs e)
        {
            apperTimer.Stop();

            if (this.Opacity < 1)
            { 
                this.Opacity += .1;
                apperTimer.Start();
            }
            else
            {
                Debug.WriteLine("フェードイン終了");
                await Task.Delay(5000);
                if(gTimerFlg == true)
                { 
                    // タイマー実行中ならフェードアウト処理開始
                    disolve();
                }
            }
        }

        /// <summary>
        /// 非表示処理
        /// </summary>
        private void disolve()
        {
            apperTimer.Stop();
            apperTimer.Dispose();
            lowerTimer.Enabled = true;
            lowerTimer.Interval = 50;
            lowerTimer.Start();
            lowerTimer.Tick += new EventHandler(fade_Tick);
        }

        /// <summary>
        /// タイマー処理：フェードアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fade_Tick(object sender, EventArgs e)
        {
            if (this.Opacity > 0)
            {
                this.Opacity -= .1;
            }
            else
            {
                Debug.WriteLine("フェードアウト終了");

                gFormActivetimer.Stop();
                lowerTimer.Stop();
                lowerTimer.Dispose();
                this.Close();
            }
        }

        /// <summary>
        /// 表示リスト変更
        /// </summary>
        /// <param name="pIndex"></param>
        private int DisplayList( int pIndex )
        {
            int retIndex = 0;

            // キーワード検索使用/不使用
            if (Settings.Default.KeyFlag == false)
            { 
                //全件表示
                Debug.WriteLine("pIndex[{0}/{1}]", pIndex, InputList.Count);
                if( pIndex < 0 || pIndex >= InputList.Count)
                {
                    return -1;
                }

                lblCode.Text = InputList[ pIndex ].Code;
                lblCompany.Text = InputList[pIndex].CompanyName;
                lblTitle.Text = InputList[pIndex].Title;
                lblListMin.Text = (pIndex + 1).ToString();
                lblListMax.Text = InputList.Count.ToString();
            }
            else
            {
                // キーワード検索 
                int iListCount = gTDDataList.Count<TDData>();
                Debug.WriteLine("pIndex[{0}/{1}]", pIndex, iListCount);
                if (pIndex < 0 || pIndex >= iListCount)
                {
                    retIndex = -1;
                    return retIndex;
                }

                lblCode.Text = gTDDataList[pIndex].Code;
                lblCompany.Text = gTDDataList[pIndex].CompanyName;
                lblTitle.Text = gTDDataList[pIndex].Title;
                lblListMin.Text = (pIndex + 1).ToString();
                lblListMax.Text = iListCount.ToString();
            }
            return retIndex;

        }

        /// <summary>
        /// 前リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            int retIndex = DisplayList( gIndex - 1);
            if (retIndex != -1)
            {
                gIndex--;
            }

            // ボタン状態
            if (gIndex == 0)
            {
                btnPrev.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
            }
            if (gIndex >= InputList.Count)
            {
                btnNext.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
            }

            this.Opacity = 5;

        }

        /// <summary>
        /// 次リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            int retIndex = DisplayList( gIndex + 1);
            if( retIndex != -1)
            {
                gIndex++ ;
            }

            // ボタン状態
            if (gIndex == 0)
            {
                btnPrev.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
            }
            if (gIndex >= InputList.Count)
            {
                btnNext.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
            }

            this.Opacity = 5;

        }

        private void allTimerStop()
        {
            gFormActivetimer.Stop();
            apperTimer.Stop();
            lowerTimer.Stop();

            gFormActivetimer.Dispose();
            apperTimer.Dispose();
            lowerTimer.Dispose();

            lowerTimer.Tick -= new EventHandler(fade_Tick);
            apperTimer.Tick -= new EventHandler(appear_Tick);
            gFormActivetimer.Tick -= new EventHandler(active_Tick);

            apperTimer.Enabled = false;
            lowerTimer.Enabled = false;
            gFormActivetimer.Enabled = false;
        }

        /// <summary>
        /// マウスがコンポーネントに表示領域に入った
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BalloonForm_MouseEnter(object sender, EventArgs e)
        {
            this.MouseLeave -= BalloonForm_MouseLeave;
            Debug.WriteLine("フォーカスIN");
            this.Opacity = 1;
            allTimerStop();
            this.TopMost = true;
            gTimerFlg = false;
            await Task.Delay(1500);
            this.MouseLeave += BalloonForm_MouseLeave;

        }
        /// <summary>
        /// マウスがフォーカスした
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BalloonForm_MouseHover(object sender, EventArgs e)
        {
            Debug.WriteLine("フォーカスHover");
            this.MouseLeave += BalloonForm_MouseLeave;
        }

        /// <summary>
        /// マウスのフォーカスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BalloonForm_MouseLeave(object sender, EventArgs e)
        {
            Debug.WriteLine("フォーカスEND");
            disolve();
        }

        // アクティブ化されないスタイル設定
        private void setNotActiveWindow(IntPtr hWnd)
        {
            // 現在のスタイルを取得
            UInt32 unSyle = GetWindowLong(hWnd, GWL.EXSTYLE);

            // キャプションのスタイルを削除
            unSyle = (unSyle | WS_EX_NOACTIVATE);

            // スタイルを反映
            UInt32 unret = SetWindowLong(hWnd, GWL.EXSTYLE, unSyle);

            // ウィンドウを再描画
            SetWindowPos(hWnd, new IntPtr(-1),
            0, 0, 0, 0,
            SWP.NOSIZE | SWP.NOMOVE | SWP.NOACTIVATE |
            SWP.SHOWWINDOW | SWP.NOSENDCHANGING);

        }

        /// <summary>
        /// タイトルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTitle_Click(object sender, EventArgs e)
        {
            // ブラウザ起動（PDF画面）
        }

        #region Win32API
        const UInt32 WS_EX_NOACTIVATE = 0x8000000;  // アクティブ化されないスタイル
        private enum GWL : int
        {
            WINDPROC = -4,
            HINSTANCE = -6,
            HWNDPARENT = -8,
            STYLE = -16,
            EXSTYLE = -20,
            USERDATA = -21,
            ID = -12
        }
        private enum SWP : int
        {
            NOSIZE = 0x0001,            //ウィンドウサイズを変えないで移動のみ(cxとcyの設定を無視する)
            NOMOVE = 0x0002,            //サイズだけ変える(xとyを無視)
            NOZORDER = 0x0004,          //現在のZオーダーを維持(hWndInsertAfterを無視)
            NOREDRAW = 0x0008,          //自動的に再描画しない
            NOACTIVATE = 0x0010,        //ウィンドウをアクティブにしない(このフラッグを指定しないとき、自動的にアクティブになる)
            FRAMECHANGED = 0x0020,      //ウィンドウのサイズ変更中でなくてもWM_NCCALCSIZEを送る
            SHOWWINDOW = 0x0040,        //再描画のとき、ウィンドウを表示
            HIDEWINDOW = 0x0080,        //再描画のとき、ウィンドウを非表示にする
            NOCOPYBITS = 0x0100,        //クライアント領域の内容をクリアする(このフラッグを指定しないとき、
                                        //元のクライアント領域を保存し再描画する)
            NOOWNERZORDER = 0x0200,     //オーダーウィンドウのZオーダーは変更しない
            NOSENDCHANGING = 0x400      //WM_WINDOWPOSCHANGINGを送らない
        }
        // ---------------------
        // 外部関数
        // ---------------------
        [DllImport("user32.dll")]
        private static extern UInt32 GetWindowLong(IntPtr hWnd,
        GWL index);
        [DllImport("user32.dll")]
        private static extern UInt32 SetWindowLong(IntPtr hWnd,
        GWL index, UInt32 unValue);
        [DllImport("user32.dll")]
        private static extern UInt32 SetWindowPos(IntPtr hWnd,
        IntPtr hWndInsertAfter,
        int x, int y, int width, int height, SWP flags);



        #endregion
        /// <summary>
        /// ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_DoubleClick(object sender, EventArgs e)
        {
            if (gForm == null || gForm.IsDisposed == true)
            {
                return;
            }

            if (gForm.Created == false)
            {
                if (gForm.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        gForm.Show();
                        // フォームアクティブ
                        gForm.Activate();
                    }));
                }
                else
                {
                    gForm.Show();
                    // フォームアクティブ
                    gForm.Activate();
                }
            }

        }

        /// <summary>
        /// ログファイル出力
        /// </summary>
        private void LogPut()
        {
            try {

                if (gTDDataList == null)
                {
                    return;
                }

                LogUtility.WriteTraceLog(" ======== リスト通知 ======== ");
                int iListCount = gTDDataList.Count<TDData>();
                for (int cnt = 0; cnt < iListCount; cnt++)
                {
                    LogUtility.WriteTraceLog("{0},{1},{2},{3}",
                        gTDDataList[cnt].Time,
                        gTDDataList[cnt].Code,
                        gTDDataList[cnt].CompanyName,
                        gTDDataList[cnt].Title
                        );
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        /// <summary>
        /// ログファイル出力
        /// </summary>
        private void LogPut2()
        {
            try
            {
                if (gTDDataList == null)
                {
                    return;
                }

                LogUtility.WriteTraceLog(" ======== リスト通知 ======== ");
                int iListCount = InputList.Count;
                for (int cnt = 0; cnt < iListCount; cnt++)
                {
                    LogUtility.WriteTraceLog("{0},{1},{2},{3}",
                        InputList[cnt].Time,
                        InputList[cnt].Code,
                        InputList[cnt].CompanyName,
                        InputList[cnt].Title
                        );
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
