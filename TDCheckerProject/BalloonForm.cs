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
    public partial class BalloonForm : MetroForm // 変更
    {
        // プロパティの設定
        private String InputTitle { get; set; }
        private String InputCode { get; set; }
        private String InputCompany { get; set; }
        private List<TDData> InputList { get; set; }
        private int InputDispCount { get; set; }

        // ---------------------
        // メンバー変数
        // ---------------------
        private int Index = 0;
        private Timer FormActiveTimer;
        private Boolean TimerFlg = false;
        private TDData[] TDDataList;
        private TDNetForm TDNetForm;

        // ---------------------
        // 定数
        // ---------------------
        private readonly int IndexEnd = -1;

        /// <summary>
        /// フォーム透明度
        /// </summary>
        public enum FormOpacity : int
        {
            e_Opacity_0 = 0,
            e_Opacity_1 = 1,
            e_Opacity_5 = 5,
        };

        /// <summary>
        /// 遅延時間(ミリ秒)
        /// </summary>
        public enum Delay : int
        {
            e_MilliSec_1500 = 1500,
            e_MilliSec_5000 = 5000,

        };

        public BalloonForm(TDNetForm form = null)
        {
            // 起動用フォーム内部保持
            TDNetForm = form;
            InitializeComponent();
            this.ShowIcon = false;
        }

        // ---------------------
        // メイン処理
        // ---------------------

        /// <summary>
        /// バルーン呼び出し処理：単体
        /// </summary>
        /// <param name="code">銘柄コード</param>
        /// <param name="company">会社名</param>
        /// <param name="title">タイトル</param>
        /// <returns>バルーン表示数</returns>
        static public int ShowNotifyBaloon(String code, String company, String title)
        {
            BalloonForm f = new BalloonForm();
            f.InputCode = code;
            f.InputCompany = company;
            f.InputTitle = title;
            f.ShowDialog();
            f.Dispose();

            return 1;
        }
        /// <summary>
        /// バルーン呼び出し処理：リスト
        /// </summary>
        /// <param name="tdDataList"></param>
        /// <returns>バルーン表示数</returns>
        static public int ShowNotifyBaloon(List<TDData> tdDataList, int baloonCount, TDNetForm tdNetForm = null)
        {
            if (tdDataList.Count != 0)
            { 
                BalloonForm f = new BalloonForm( tdNetForm );
                f.InputDispCount = baloonCount;
                f.InputList = tdDataList;
                SoundUtility.CallSound( ( SoundUtility.SoundType )Properties.Settings.Default.BellType );
                f.ShowDialog();
                f.Dispose();
            }
            return 1;
        }

        /// <summary>
        /// 初期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BalloonForm_Load(object sender, EventArgs e)
        {
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
                TDData[] tdData;
                Boolean keyWordFlg;
                keyWordFlg = DataUtility.GetKeyWordRow(InputList, Settings.Default.KeyCode, out tdData);

                // メンバー変数で保持
                TDDataList = tdData;

                // キーワードがない場合全件検索
                if (keyWordFlg == false)
                {
                    this.Close();
                    return;
                }
            }

            // リスト表示
            DisplayList(Index);

            // ウィンドウを画面右下に表示させる
            int left = Screen.PrimaryScreen.WorkingArea.Width - this.Width;
            int top = Screen.PrimaryScreen.WorkingArea.Height - this.Height * InputDispCount;
            this.DesktopBounds = new Rectangle(left, top, this.Width, this.Height);

            this.Opacity = (int)FormOpacity.e_Opacity_0;
            TimerFlg = true;
            apperTimer.Enabled = true;
            apperTimer.Interval = 50;
            apperTimer.Start();
            apperTimer.Tick += new EventHandler(appear_Tick);

            // ボタン状態
            if (Index == 0)
            {
                btnPrev.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
            }

            // ボタン状態
            if (Index >= InputList.Count)
            {
                btnNext.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
            }

            // タイマーの設定
            FormActiveTimer = new Timer();
            FormActiveTimer.Enabled = true;
            FormActiveTimer.Interval = 50;
            FormActiveTimer.Start();
            FormActiveTimer.Tick += new EventHandler(active_Tick);
        }

        /// <summary>
        /// タイマー処理：非アクティブ最前面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void active_Tick(object sender, EventArgs e)
        {
            SetNotActiveWindow(this.Handle);
        }

        /// <summary>
        /// タイマー処理：フェードイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void appear_Tick(object sender, EventArgs e)
        {
            apperTimer.Stop();

            if (this.Opacity < (int)FormOpacity.e_Opacity_1)
            { 
                this.Opacity += (int)FormOpacity.e_Opacity_1;
                apperTimer.Start();
            }
            else
            {
                Debug.WriteLine("フェードイン終了");
                await Task.Delay((int)Delay.e_MilliSec_5000);
                if(TimerFlg == true)
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
            if (this.Opacity > (int)FormOpacity.e_Opacity_0)
            {
                this.Opacity -= (int)FormOpacity.e_Opacity_1;
            }
            else
            {
                Debug.WriteLine("フェードアウト終了");

                FormActiveTimer.Stop();
                lowerTimer.Stop();
                lowerTimer.Dispose();
                this.Close();
            }
        }

        /// <summary>
        /// 表示リスト変更
        /// </summary>
        /// <param name="displayIndex">表示インデックス</param>
        private int DisplayList( int displayIndex )
        {
            int index = 0;

            // キーワード検索使用/不使用
            if (Settings.Default.KeyFlag == false)
            { 
                //全件表示
                Debug.WriteLine("pIndex[{0}/{1}]", displayIndex, InputList.Count);
                if( displayIndex < 0 || displayIndex >= InputList.Count)
                {
                    return IndexEnd;
                }

                lblCode.Text = InputList[ displayIndex ].Code;
                lblCompany.Text = InputList[displayIndex].CompanyName;
                lblTitle.Text = InputList[displayIndex].Title;
                lblListMin.Text = (displayIndex + 1).ToString();
                lblListMax.Text = InputList.Count.ToString();
            }
            else
            {
                // キーワード検索 
                int listCount = TDDataList.Count<TDData>();
                Debug.WriteLine("pIndex[{0}/{1}]", displayIndex, listCount);
                if (displayIndex < 0 || displayIndex >= listCount)
                {
                    index = IndexEnd;
                    return index;
                }

                lblCode.Text = TDDataList[displayIndex].Code;
                lblCompany.Text = TDDataList[displayIndex].CompanyName;
                lblTitle.Text = TDDataList[displayIndex].Title;
                lblListMin.Text = (displayIndex + 1).ToString();
                lblListMax.Text = listCount.ToString();
            }
            return index;

        }

        /// <summary>
        /// 前リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            // インデックスを前に戻す
            if (DisplayList(Index - 1) != IndexEnd)
            {
                Index--;
            }

            // ボタン状態
            if (Index == 0)
            {
                btnPrev.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
            }
            if (Index >= InputList.Count)
            {
                btnNext.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
            }

            this.Opacity = (int)FormOpacity.e_Opacity_5;

        }

        /// <summary>
        /// 次リスト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            // インデックスを次に進める
            if(DisplayList(Index + 1) != IndexEnd)
            {
                Index++ ;
            }

            // ボタン状態
            if (Index == 0)
            {
                btnPrev.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
            }
            if (Index >= InputList.Count)
            {
                btnNext.Enabled = false;
            }
            else
            {
                btnNext.Enabled = true;
            }

            this.Opacity = (int)FormOpacity.e_Opacity_5;

        }

        /// <summary>
        /// フェードイン/フェードアウトのタイマーの停止
        /// </summary>
        private void AllTimerStop()
        {
            FormActiveTimer.Stop();
            apperTimer.Stop();
            lowerTimer.Stop();

            FormActiveTimer.Dispose();
            apperTimer.Dispose();
            lowerTimer.Dispose();

            lowerTimer.Tick -= new EventHandler(fade_Tick);
            apperTimer.Tick -= new EventHandler(appear_Tick);
            FormActiveTimer.Tick -= new EventHandler(active_Tick);

            apperTimer.Enabled = false;
            lowerTimer.Enabled = false;
            FormActiveTimer.Enabled = false;
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
            this.Opacity = (int)FormOpacity.e_Opacity_1;
            AllTimerStop();
            this.TopMost = true;
            TimerFlg = false;
            await Task.Delay((int)Delay.e_MilliSec_1500);
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

        /// <summary>
        /// アクティブ化されないスタイル設定
        /// </summary>
        /// <param name="hWnd"></param>
        private void SetNotActiveWindow(IntPtr hWnd)
        {
            // 現在のスタイルを取得
            UInt32 style = GetWindowLong(hWnd, GWL.EXSTYLE);

            // キャプションのスタイルを削除
            style = (style | WS_EX_NOACTIVATE);

            // スタイルを反映
            UInt32 unret = SetWindowLong(hWnd, GWL.EXSTYLE, style);

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
            // TODO:ブラウザ起動（PDF画面）処理追加予定
        }

        /// <summary>
        /// ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_DoubleClick(object sender, EventArgs e)
        {
            if (TDNetForm == null || TDNetForm.IsDisposed == true)
            {
                return;
            }

            if (TDNetForm.Created == false)
            {
                if (TDNetForm.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate
                    {
                        TDNetForm.Show();
                        // フォームアクティブ
                        TDNetForm.Activate();
                    }));
                }
                else
                {
                    TDNetForm.Show();
                    // フォームアクティブ
                    TDNetForm.Activate();
                }
            }

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
    }
}
