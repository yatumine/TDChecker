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
        private Timer MonitorTimer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WebTimer()
        {
            
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="thisObj">呼び出し元オブジェクト</param>
        /// <param name="interval">インターバル</param>
        /// <param name="eventHdl">実行対象イベントハンドラ</param>
        public WebTimer(System.ComponentModel.ISynchronizeInvoke thisObj, double interval, ElapsedEventHandler eventHdl)
        {
            this.MonitorTimer = new Timer();
            // イベントをハンドル.
            this.MonitorTimer.Elapsed += eventHdl;
            // インターバル時間設定
            this.MonitorTimer.Interval = interval;
            // マーシャリング設定
            this.MonitorTimer.SynchronizingObject = thisObj;
        }

        public void Dispose()
        {
            ((IDisposable)MonitorTimer).Dispose();
        }

        /// <summary>
        /// インターバル設定
        /// </summary>
        /// <param name="interval"></param>
        /// <returns></returns>
        public bool SetInterval(double interval)
        {
            this.MonitorTimer.Interval = interval;
            return true;
        }

        /// <summary>
        /// Timer開始
        /// </summary>
        public void Start()
        {
            this.MonitorTimer.Start();
        }

        public bool Start(double pInterval)
        {
            this.MonitorTimer.Interval = pInterval;
            this.MonitorTimer.Start();

            return true;
        }

        /// <summary>
        /// Timer停止
        /// </summary>
        /// <returns></returns>
        public bool Stop()
        {
            this.MonitorTimer.Stop();
            return true;
        }
    }
}
