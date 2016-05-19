using System;
using System.Collections.Generic;
using COMMON.Const;
using COMMON.Data;
using HtmlAgilityPack;
using System.Linq;
using COMMON.Utility;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace TDChecker
{
    /// <summary>
    /// TD.netコンテンツ取得クラス
    /// </summary>
    class TDNet
    {
        /// <summary>
        /// 適時開示リストのURLを生成
        /// </summary>
        /// <param name="targetDate">取得日付（yyyy/mm/dd）</param>
        /// <param name="pageNo">ページ番号（デフォルト1ページ目）</param>
        /// <returns></returns>
        public static String GetTDUrl(String targetDate,int pageNo = 1)
        {
            String url = null;
            DateTime date;

            if ( DateTime.TryParse(targetDate,out date))
            {
                url = "https://www.release.tdnet.info/inbs/";
                url += String.Format("I_list_{0}_{1}.html",
                    pageNo.ToString("000"),
                    date.ToString(Constants.YYYYMMDD)
                    );
            }
            Debug.WriteLine(url, "接続URL");
            return url;
        }

        /// <summary>
        /// 取得ドキュメントのパース処理
        /// </summary>
        /// <param name="htmlDoc">Htmlドキュメント</param>
        /// <param name="getListNumber">取得件数</param>
        /// <param name="releaseDate">適時開示日</param>
        /// <returns></returns>
        public static List<TDData> ParseDoc(HtmlAgilityPack.HtmlDocument htmlDoc, int getListNumber, String releaseDate )
        {
            List<TDData> tdList = new List<TDData>();
            HtmlNode node;
            // 適時開示リスト検索
            node = htmlDoc.GetElementbyId("main-list-table");
            if (node == null)
            {
                return tdList; // 取得失敗
            }
            
            String htmlText = node.OuterHtml;
            tdList = GetTimelyDisclosureUrlList( htmlText, getListNumber, releaseDate);
            return tdList;
        }

        /// <summary>
        /// 取得したHTMLドキュメントから適時開示リストをとりだす
        /// </summary>
        /// <param name="htmlText">Htmlドキュメント</param>
        /// <param name="getListNum">取得件数</param>
        /// <param name="releaseDate">適時開示日</param>
        /// <returns></returns>
        public static List<TDData> GetTimelyDisclosureUrlList( String htmlText, int getListNum, String releaseDate )
        {
            List<TDData> tDContentsList = new List<TDData>();

            // 時間計測用のタイマー
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Debug.WriteLine("HtmlDocument構築開始: {0:0.000}秒", stopwatch.Elapsed.TotalSeconds);

            int urlListCount = 0;
            if (htmlText != null)
            {
                // HtmlDocumentオブジェクトを構築する
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(htmlText);
                Debug.WriteLine("HtmlDocument構築完了: {0:0.000}秒", stopwatch.Elapsed.TotalSeconds);

                // 目的の<a>要素を全て取り出して（XPath）、
                // そのhref属性とInnerTextを持つ匿名型オブジェクトのコレクションを作る（LINQ）
                // ※冒頭に「using System.Linq;」の追加が必要
                var urlList = htmlDoc.DocumentNode.SelectNodes(@"//tbody/tr/td");
                // ライン分のデータ件数取得
                int contentsCnt = 0;
                if (htmlDoc.DocumentNode.SelectNodes(@"//table/tr") != null)
                { 
                    contentsCnt = htmlDoc.DocumentNode.SelectNodes(@"//table/tr")[0].SelectNodes(@" td").Count;
                }
                else
                {
                    contentsCnt = htmlDoc.DocumentNode.SelectNodes(@"//table/tbody/tr")[0].SelectNodes(@" td").Count;
                }
                // 取得できていない場合、1つ上のノードで検索
                if (urlList == null)
                {
                    urlList= htmlDoc.DocumentNode.SelectNodes(@"//table/tr/td");
                    // ライン分のデータ件数取得
                    contentsCnt = htmlDoc.DocumentNode.SelectNodes(@"//table/tr")[0].SelectNodes(@" td").Count;
                }

                // リスト取得
                TDData tDdata;
                int tagCount = 0;
                for(tagCount = 0, urlListCount = 0 ; tagCount < urlList.Count; urlListCount++)
                {
                    // 指定件数まで取得
                    if(getListNum == urlListCount)
                    {
                        break;
                    }

                    // データ設定
                    tDdata = new TDData();
                    tDdata.Date = releaseDate;
                    tDdata.Time = urlList[tagCount].InnerText;
                    tDdata.Code = urlList[tagCount + 1].InnerText.Substring(0,4);
                    tDdata.CompanyName = urlList[tagCount + 2].InnerText;
                    tDdata.Title = urlList[tagCount + 3].InnerText;

                    // リスト追加
                    tDContentsList.Add(tDdata);

                    // 1ラインの項目数分加算し、次データへ
                    tagCount += contentsCnt;
                }

                Debug.WriteLine("タイトル取り出し完了: {0:0.000}秒", stopwatch.Elapsed.TotalSeconds);
                Debug.WriteLine("取得タイトル先頭{0}件（全{1}件中）", getListNum, urlList.Count()/7);
            }
            return tDContentsList;
        }

        /// <summary>
        /// アプリケーションバージョン確認
        /// </summary>
        /// <returns></returns>
        public static Boolean AppVersionCheck()
        {
            //要求するURL
            string url = "http://4doku.com/tdchk/update.php";

            //WebRequestの作成
            System.Net.HttpWebRequest webreq =
                (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);

            System.Net.HttpWebResponse webres = null;

            try
            {
                //サーバーからの応答を受信するためのWebResponseを取得
                webres = (System.Net.HttpWebResponse)webreq.GetResponse();

                //応答したURIを表示する
                Console.WriteLine(webres.ResponseUri);
                //応答ステータスコードを表示する
                Debug.WriteLine("{0}:{1}",
                    webres.StatusCode, webres.StatusDescription);

                // OKの場合、レスポンスボディを取得する
                Encoding enc = Encoding.GetEncoding("UTF-8");
                Stream st = webres.GetResponseStream();
                StreamReader sr = new StreamReader(st, enc);

                string html = sr.ReadToEnd();
                sr.Close();
                st.Close();

                Debug.WriteLine(html);

                // 取得したレスポンスボディをXMLで読み込む
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(html);

                //ルート要素を取得する
                XmlElement rootElement = xml.DocumentElement;

                //resタグのNodeListを取得する
                XmlNodeList nodelist = rootElement.GetElementsByTagName("res");
                //指定したタグが存在するか？
                if (nodelist.Count > 0)
                {
                    //指定したタグが存在したのでInnerTextプロパティで値を取得する
                    String newVersion = nodelist.Item(0).InnerText;
                    Debug.WriteLine("{0}{1}", "new version  : ", newVersion);

                    if(newVersion != null) { 

                        //自分自身のAssemblyを取得
                        System.Reflection.Assembly asm =
                            System.Reflection.Assembly.GetExecutingAssembly();
                        
                        //バージョンの取得
                        Version ver = asm.GetName().Version;
                        
                        //結果の表示
                        Debug.WriteLine("{0}{1}","this version : ", ver.ToString());

                        // バージョン判定
                        int compare = String.Compare(nodelist.Item(0).InnerText, ver.ToString());
                        if (compare > 0)
                        {
                            // 最新版あり
                            return true;
                        }
                    }
                }
            }
            catch (System.Net.WebException ex)
            {
                //HTTPプロトコルエラーかどうか調べる
                if (ex.Status == System.Net.WebExceptionStatus.ProtocolError)
                {
                    //HttpWebResponseを取得
                    System.Net.HttpWebResponse errres =
                        (System.Net.HttpWebResponse)ex.Response;
                    //応答したURIを表示する
                    Debug.WriteLine(errres.ResponseUri);
                    //応答ステータスコードを表示する
                    Debug.WriteLine("{0}:{1}",
                        errres.StatusCode, errres.StatusDescription);
                }
                else
                    Debug.WriteLine(ex.Message);
            }
            finally
            {
                //閉じる
                if (webres != null)
                    webres.Close();
            }

            return false;
        }
    }
}
