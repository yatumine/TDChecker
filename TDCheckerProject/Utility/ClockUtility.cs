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
        // プロパティ
        // --------------------
        private static String gNextTime;

        /// 次回更新時刻
        /// </summary>
        public static String IsNextTime { get { return gNextTime; } }


        /// <summary>
        /// 次回タイマー起動時刻取得処理
        /// </summary>
        /// <param name="pReadCycle"></param>
        /// <returns></returns>
        public static double GetReadTime(TimeSpan pReadCycle)
        {
            DateTime dtNext = DateTime.Now;
            TimeSpan ts;
            int iMinute = dtNext.Minute;
            int iOneMin = (iMinute % 10);

            //-----------------------------------
            // 目標時刻までのTimeSpanを取得
            //-----------------------------------
            // 秒を正秒へ
            dtNext = dtNext.AddSeconds(-dtNext.Second);

            // 指定タイミングの時刻設定
            switch (pReadCycle.Minutes)
            {
                case 1:
                    // そのまま1分後
                    dtNext = dtNext.AddMinutes(1);
                    break;

                case 5:
                    // 一桁目取得

                    // 55分以上だった場合、時を繰り上げ
                    if (dtNext.Minute >= 55)
                    {
                        dtNext = dtNext.AddHours(1);
                        dtNext = dtNext.AddMinutes(-iMinute);
                    }
                    else if (iOneMin >= 5) // 5分以上かどうか
                    {
                        // 桁繰上げ
                        dtNext = dtNext.AddMinutes(-iOneMin);   // 1の位を0へ
                        dtNext = dtNext.AddMinutes(10);   // 10の位をあげる
                    }
                    else
                    {
                        // 5分に設定
                        dtNext = dtNext.AddMinutes(-iOneMin);
                        dtNext = dtNext.AddMinutes(5);
                    }
                    break;

                case 10:
                    // 10分後設定
                    if (iMinute >= 50)
                    {
                        // 時を繰り上げ
                        dtNext = dtNext.AddHours(1);
                        dtNext = dtNext.AddMinutes(-iMinute);
                    }
                    else
                    {
                        // 10分後に設定
                        dtNext = dtNext.AddMinutes(10);   // 10の位をあげる
                        // 1の位は切り捨て
                        dtNext = dtNext.AddMinutes(-iOneMin);   // 1の位を0へ

                    }
                    break;

                case 30:
                    // 30分以上かどうか
                    if (iMinute >= 30)
                    {
                        // 30分以上なら時を繰り上げ
                        dtNext = dtNext.AddMinutes(-iOneMin);   // 1の位を0へ
                        // 1時間繰上げ
                        dtNext = dtNext.AddHours(1);

                    }
                    else
                    {
                        // 30分に設定
                        dtNext = dtNext.AddMinutes(-iMinute);
                        dtNext = dtNext.AddMinutes(30);
                    }
                    break;

                default:
                    // 1時間後
                    if (pReadCycle.Hours == 1)
                    {
                        // 1時間後に設定
                        dtNext = dtNext.AddHours(1);
                        dtNext = dtNext.AddMinutes(-iMinute);
                    }
                    break;

            }

            // 目標時刻までのTimeSpanを取得
            ts = dtNext - DateTime.Now;
            gNextTime = dtNext.ToString();

            Debug.WriteLine("★次回タイマー起動時刻 {0}", dtNext);

            return ts.TotalMilliseconds;
        }
    }
}
