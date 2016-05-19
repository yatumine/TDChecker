using System;

namespace COMMON.Utility
{
    public static class LogUtility
    {
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <remarks></remarks>
        public static void WriteTraceLog(String msg)
        {
            try
            {
                WriteTraceLog(msg, (Exception)null);
            }
            catch { }
        }

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="ex">Exception(無指定の場合はメッセージのみ出力)</param>
        /// <remarks></remarks>
        public static void WriteTraceLog(String msg, Exception ex)
        {
#if !DEBUG
            // TODO: Releaseで何か出力したい場合はここに追加
#else
            // Debugの場合
            try
            {
                // ログフォルダ名作成
                DateTime dt = DateTime.Now;
                String logFolder = System.AppDomain.CurrentDomain.BaseDirectory + "Log";

                // ログフォルダ名作成
                System.IO.Directory.CreateDirectory("./LOG");

                // ログファイル名作成
                String logFile = "./LOG\\log" + dt.ToString("dd") + ".log";

                // 翌日分のログファイル削除(１ヶ月分のログファイルしか保存しないようにするため)
                String logNext = "./LOG\\log"  + dt.AddDays(1).ToString("dd") + ".log";
                System.IO.File.Delete(logNext);

                // ログ出力文字列作成
                String logStr;
                logStr = dt.ToString("yyyy/MM/dd HH:mm:ss") + "\t" + msg;
                if (ex != null)
                {
                    logStr = logStr + "\n" + ex.ToString();
                }

                // Shift-JISでログ出力
                System.IO.StreamWriter sw = null;
                try
                {
                    sw = new System.IO.StreamWriter(logFile, true,
                        System.Text.Encoding.GetEncoding("Shift-JIS"));
                    sw.WriteLine(logStr);
                }
                catch { }
                finally
                {
                    if (sw != null) sw.Close();
                }
            }
            catch { }
#endif
        }

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="msg">メッセージ</param>
        /// <param name="ex">Exception(無指定の場合はメッセージのみ出力)</param>
        /// <remarks></remarks>
        public static void WriteTraceLog( String msgFormat, params object[] args)
        {
#if !DEBUG
            // TODO: Releaseで何か出力したい場合はここに追加
#else

            // Debugの場合
            try
            {
                if (args != null) {
                    int i = 0;
                    for (; i < args.Length; ++i)
                    {
                        try
                        {
                            if( args[i] != null)
                            {
                                msgFormat = msgFormat.Replace("{" + i.ToString() + "}", args[i].ToString());
                            }
                            else
                            {
                                msgFormat = msgFormat.Replace("{" + i.ToString() + "}", "null");
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                // ログフォルダ名作成
                DateTime dt = DateTime.Now;

                // ログフォルダ名作成
                System.IO.Directory.CreateDirectory("./LOG");

                // ログファイル名作成
                String logFile = "./LOG\\log"+ dt.ToString("dd") + ".log";

                // 翌日分のログファイル削除(１ヶ月分のログファイルしか保存しないようにするため)
                String logNext = "./LOG\\log" + dt.AddDays(1).ToString("dd") + ".log";
                System.IO.File.Delete(logNext);

                // ログ出力文字列作成
                String logStr;
                logStr = dt.ToString("yyyy/MM/dd HH:mm:ss") + "\t" + msgFormat;

                // Shift-JISでログ出力
                System.IO.StreamWriter sw = null;
                try
                {
                    sw = new System.IO.StreamWriter(logFile, true,
                        System.Text.Encoding.GetEncoding("Shift-JIS"));
                    sw.WriteLine(logStr);
                }
                catch { }
                finally
                {
                    if (sw != null) sw.Close();
                }
            }
            catch { }
#endif
        }

    }
}