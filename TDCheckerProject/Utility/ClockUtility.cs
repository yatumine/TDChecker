using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

namespace COMMON.Utility
{
    public static class ClockUtility
    {
        // --------------------
        // メンバ変数
        // --------------------
        private static String NextTime;

        // --------------------
        // プロパティ
        // --------------------

        /// 次回更新時刻
        /// </summary>
        public static String IsNextTime { get { return NextTime; } }


        /// <summary>
        /// 読込間隔（分）
        /// </summary>
        public enum ReadCycle : int
        { 
            e_1_Minutes = 1,
            e_5_Minutes = 5,
            e_10_Minutes = 10,
            e_30_Minutes = 30,
            e_50_Minutes = 50,
            e_55_Minutes = 55,
            e_60_Minutes = 60,
            e_1_Hour = 1,

        };

        /// <summary>
        /// 次回タイマー起動時刻取得処理
        /// </summary>
        /// <param name="readCycle"></param>
        /// <returns></returns>
        public static double GetReadTime(TimeSpan readCycle)
        {
            DateTime nextTimer = DateTime.Now;
            int minute = nextTimer.Minute;
            int remainingMin = (minute % (int)ReadCycle.e_10_Minutes);

            //-----------------------------------
            // 目標時刻までのTimeSpanを取得
            //-----------------------------------
            // 秒を正秒へ
            nextTimer = nextTimer.AddSeconds(-nextTimer.Second);

            // 指定タイミングの時刻設定
            switch (readCycle.Minutes)
            {
                case (int)ReadCycle.e_1_Minutes:
                    // そのまま1分後
                    nextTimer = nextTimer.AddMinutes((int)ReadCycle.e_1_Minutes);
                    break;

                case (int)ReadCycle.e_5_Minutes:
                    // 5分毎の時間へ変換
                    // 55分以上だった場合、時を繰り上げ
                    if (nextTimer.Minute >= (int)ReadCycle.e_55_Minutes)
                    {
                        nextTimer = nextTimer.AddHours((int)ReadCycle.e_1_Hour);
                        nextTimer = nextTimer.AddMinutes(-minute);
                    }
                    else if (remainingMin >= (int)ReadCycle.e_5_Minutes) // 5分以上かどうか
                    {
                        // 桁繰上げ
                        nextTimer = nextTimer.AddMinutes(-remainingMin);   // 1の位を0へ
                        nextTimer = nextTimer.AddMinutes((int)ReadCycle.e_10_Minutes);   // 10の位をあげる
                    }
                    else
                    {
                        // 5分に設定
                        nextTimer = nextTimer.AddMinutes(-remainingMin);
                        nextTimer = nextTimer.AddMinutes((int)ReadCycle.e_5_Minutes);
                    }
                    break;

                case (int)ReadCycle.e_10_Minutes:
                    // 10分後設定
                    if (minute >= (int)ReadCycle.e_50_Minutes)
                    {
                        // 時を繰り上げ
                        nextTimer = nextTimer.AddHours((int)ReadCycle.e_1_Hour);
                        nextTimer = nextTimer.AddMinutes(-minute);
                    }
                    else
                    {
                        // 10分後に設定
                        nextTimer = nextTimer.AddMinutes((int)ReadCycle.e_10_Minutes);   // 10の位をあげる
                        // 1の位は切り捨て
                        nextTimer = nextTimer.AddMinutes(-remainingMin);   // 1の位を0へ

                    }
                    break;

                case (int)ReadCycle.e_30_Minutes:
                    // 30分以上かどうか
                    if (minute >= 30)
                    {
                        // 30分以上なら時を繰り上げ
                        nextTimer = nextTimer.AddMinutes(-remainingMin);   // 1の位を0へ
                        // 1時間繰上げ
                        nextTimer = nextTimer.AddHours((int)ReadCycle.e_1_Hour);

                    }
                    else
                    {
                        // 30分に設定
                        nextTimer = nextTimer.AddMinutes(-minute);
                        nextTimer = nextTimer.AddMinutes((int)ReadCycle.e_30_Minutes);
                    }
                    break;

                default:
                    // 1時間後
                    if (readCycle.Hours == (int)ReadCycle.e_1_Hour)
                    {
                        // 1時間後に設定
                        nextTimer = nextTimer.AddHours((int)ReadCycle.e_1_Hour);
                        nextTimer = nextTimer.AddMinutes(-minute);
                    }
                    break;

            }

            // 目標時刻までのTimeSpanを取得
            TimeSpan nextTimeInterval = nextTimer - DateTime.Now;
            NextTime = nextTimer.ToString();

            Debug.WriteLine("★次回タイマー起動時刻 {0}", nextTimer);

            return nextTimeInterval.TotalMilliseconds;
        }
    }
}
