using COMMON.Utility;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TDChecker
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイント
        /// </summary>
        [STAThread]
        static void Main()
        {

            // .NetFrameworkバージョンを確認する
            if (!Get45or451FromRegistry())
            {
                // 動作対象Frameworkなし
                MessageBox.Show("動作対象の”.Netframework”がありません\n以下からダウンロードしてください。\nhttps://www.microsoft.com/ja-JP/download/details.aspx?id=42643", "TDCheker");
                return;
            }

            //アプリケーションの設定を読み込む
            Properties.Settings.Default.Reload();

            // タスクトレイに表示するアイコン
            NotifyIconWrapper notifyIcon = new NotifyIconWrapper(new MainForm());

            Application.ApplicationExit += (sender, e) =>
            {
                //タスクトレイから確実にアイコンを消す
                //他のメンバ変数も、必要なものは全部Disposeで後始末
                if (notifyIcon != null)
                {
                    notifyIcon.Dispose();
                } 
            };

            //メッセージループを開始する
            Application.Run();
            notifyIcon.Dispose();

            //アプリケーションの設定を保存する
            Properties.Settings.Default.Save();
        }


        static async Task RunAsync()
        {
            await Task.Delay(1000);
        }

        /// <summary>
        /// .NetFrameWorkをレジストリから読み込み
        /// </summary>
        /// <returns></returns>
        private static Boolean Get45or451FromRegistry()
        {
            using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
            {
                if (ndpKey != null && ndpKey.GetValue("Release") != null)
                {
                    Console.WriteLine("Version: " + CheckFor45DotVersion((int)ndpKey.GetValue("Release")));
                    return true;
                }
                else
                {
                    Console.WriteLine("Version 4.5 or later is not detected.");
                    return false;
                }
            }
        }

        /// <summary>
        /// .NetFrameWorkバージョン確認
        /// </summary>
        /// <param name="releaseKey"></param>
        /// <returns></returns>
        private static string CheckFor45DotVersion(int releaseKey)
        {
            if (releaseKey >= 393295)
            {
                return "4.6 or later";
            }
            if ((releaseKey >= 379893))
            {
                return "4.5.2 or later";
            }
            if ((releaseKey >= 378675))
            {
                return "4.5.1 or later";
            }
            if ((releaseKey >= 378389))
            {
                return "4.5 or later";
            }
            // This line should never execute. A non-null release key should mean
            // that 4.5 or later is installed.
            return "No 4.5 or later version detected";
        }
    }
}
