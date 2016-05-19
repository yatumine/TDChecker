using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COMMON.Utility
{
    /// <summary>
    /// Httpに関する処理クラス
    /// </summary>
    class HttpUtility
    {
        /// <summary>
        /// タイムアウト（秒）
        /// </summary>
        private readonly double TimeoutSec = 10.0;

        /// <summary>
        /// Urlへ接続してWeb取得
        /// </summary>
        /// <param name="uri">Url</param>
        /// <returns>String</returns>
        public async Task<String> GetWebPageAsync(Uri uri)
        {
            using (HttpClient client = new HttpClient())
            {
                // ユーザーエージェント文字列をセット（IEとしてデータを取得）
                client.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "Mozilla/5.0 (Windows NT 6.3; Trident/7.0; rv:11.0) like Gecko");

                // 受け入れ言語をセット
                client.DefaultRequestHeaders.Add("Accept-Language", "ja-JP");

                // タイムアウトをセット
                client.Timeout = TimeSpan.FromSeconds(TimeoutSec);

                try
                {
                    // Webページを取得
                    return await client.GetStringAsync(uri);
                }
                catch (HttpRequestException e)
                {
                    // 404エラーや、名前解決失敗など
                    Debug.WriteLine("\n例外発生!");
                    // InnerExceptionも含めて、再帰的に例外メッセージを表示する
                    Exception ex = e;
                    while (ex != null)
                    {
                        Debug.WriteLine("例外メッセージ: {0} ", ex.Message);
                        ex = ex.InnerException;
                    }
                }
                catch (TaskCanceledException e)
                {
                    // タスクがキャンセルされたとき（一般的にタイムアウト）
                    Debug.WriteLine("\nタイムアウト!");
                    Debug.WriteLine("例外メッセージ: {0} ", e.Message);
                }
                return null;
            }
        }

        /// <summary>
        /// 接続先URLチェック
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Boolean UrlConnectCheck(String url)
        {
            //WebRequestの作成
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse webres = null;
            try
            {
                //サーバーからの応答を受信するためのWebResponseを取得
                webres = (HttpWebResponse)webreq.GetResponse();

                //応答したURIを表示する
                Debug.WriteLine(webres.ResponseUri);
                //応答ステータスコードを表示する
                Debug.WriteLine("{0}:{1}",
                    webres.StatusCode, webres.StatusDescription);
            }
            catch (WebException ex)
            {
                //HTTPプロトコルエラーかどうか調べる
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    //HttpWebResponseを取得
                    HttpWebResponse errres =
                        (HttpWebResponse)ex.Response;
                    //応答したURIを表示する
                    Debug.WriteLine(errres.ResponseUri);
                    //応答ステータスコードを表示する
                    Debug.WriteLine("{0}:{1}",
                        errres.StatusCode, errres.StatusDescription);
                }
                else
                { 
                    Debug.WriteLine(ex.Message);
                }
                return false;
            }
            finally
            {
                //閉じる
                if (webres != null)
                {
                    webres.Close();
                }
            }
            return true;
        }
    }
}
