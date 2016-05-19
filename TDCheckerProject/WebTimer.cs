using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
namespace TDChecker
{
    /// <summary>
    /// Webサイト監視をタイマーで行う
    /// </summary>
    class WebTimer:IDisposable
    {
        // --------------------
        // メンバ変数
        // --------------------
        private Timer timer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WebTimer()
        {
            
        }

        public WebTimer(System.ComponentModel.ISynchronizeInvoke pThis, double pInterval, ElapsedEventHandler pEvent)
        {
            this.timer = new Timer();
            // イベントをハンドル.
            this.timer.Elapsed += pEvent;
            // インターバル時間設定
            this.timer.Interval = pInterval;
            // マーシャリング設定
            this.timer.SynchronizingObject = pThis;
        }

        public void Dispose()
        {
            ((IDisposable)timer).Dispose();
        }

        /// <summary>
        /// インターバル設定
        /// </summary>
        /// <param name="pInterval"></param>
        /// <returns></returns>
        public bool SetInterval(double pInterval)
        {
            this.timer.Interval = pInterval;
            return true;
        }

        /// <summary>
        /// Timer開始
        /// </summary>
        public void Start()
        {
            this.timer.Start();
        }

        public bool Start(double pInterval)
        {
            this.timer.Interval = pInterval;
            this.timer.Start();

            return true;
        }

        /// <summary>
        /// Timer停止
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            this.timer.Stop();
            return true;
        }
    }
}
