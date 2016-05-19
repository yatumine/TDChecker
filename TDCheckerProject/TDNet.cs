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
        /// <param name="pDate">yyyy/MM/dd</param>
        /// <returns>適時開示URL</returns>
        public static String GetTDUrl(String pDate,int pPageNo = 1)
        {
            String retUrl = null;
            DateTime dtDate;

            if ( DateTime.TryParse(pDate,out dtDate))
            {
                // 適時開示フォーマット
                //#if DEBUG
                //                retUrl = "https://www.release.tdnet.info/inbs/";
                //                retUrl += String.Format("I_list_{0}_{1}.html",
                //                    pPageNo.ToString("000"),
                //                    dtDate.ToString(Constants.YYYYMMDD)
                //                    );
                //#else
                //                retUrl = "http://www5210ui.sakura.ne.jp/I_list_001_20160213.html";
                //#endif
                retUrl = "https://www.release.tdnet.info/inbs/";
                retUrl += String.Format("I_list_{0}_{1}.html",
                    pPageNo.ToString("000"),
                    dtDate.ToString(Constants.YYYYMMDD)
                    );
            }
            Debug.WriteLine(retUrl, "接続URL");
            return retUrl;
        }

        public static List<TDData> ParseDoc(HtmlAgilityPack.HtmlDocument pHtmlDoc, int pGetListNumber, String pDate )
        {
            List<TDData> tdList = new List<TDData>();
            HtmlNode node;
            // 適時開示リスト検索
            node = pHtmlDoc.GetElementbyId("main-list-table");
            if (node == null)
            {
                return tdList; // 取得失敗
            }
            
            String strHtmlText = node.OuterHtml;
            tdList = GetTimelyDisclosureUrlList( strHtmlText, pGetListNumber, pDate);
            return tdList;
        }

        /// <summary>
        /// 取得したHTMLドキュメントから適時開示リストをとりだす
        /// </summary>
        /// <param name="pHtmlText"></param>
        /// <param name="pGetListNum"></param>
        /// <param name="pDate"></param>
        /// <returns></returns>
        public static List<TDData> GetTimelyDisclosureUrlList( String pHtmlText, int pGetListNum, String pDate )
        {
            List<TDData> TDContentsList = new List<TDData>();

            // 時間計測用のタイマー
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();
            Debug.WriteLine("HtmlDocument構築開始: {0:0.000}秒", timer.Elapsed.TotalSeconds);

            int UrlListCount = 0;
            if (pHtmlText != null)
            {
                // HtmlDocumentオブジェクトを構築する
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(pHtmlText);
                Debug.WriteLine("HtmlDocument構築完了: {0:0.000}秒", timer.Elapsed.TotalSeconds);

                // 目的の<a>要素を全て取り出して（XPath）、
                // そのhref属性とInnerTextを持つ匿名型オブジェクトのコレクションを作る（LINQ）
                // ※冒頭に「using System.Linq;」の追加が必要
                var UrlList = htmlDoc.DocumentNode.SelectNodes(@"//tbody/tr/td");
                // ライン分のデータ件数取得
                int iContentsCnt = 0;
                if (htmlDoc.DocumentNode.SelectNodes(@"//table/tr") != null)
                { 
                    iContentsCnt = htmlDoc.DocumentNode.SelectNodes(@"//table/tr")[0].SelectNodes(@" td").Count;
                }
                else
                {
                    iContentsCnt = htmlDoc.DocumentNode.SelectNodes(@"//table/tbody/tr")[0].SelectNodes(@" td").Count;
                }
                // 取得できていない場合、1つ上のノードで検索
                if (UrlList == null)
                {
                    UrlList= htmlDoc.DocumentNode.SelectNodes(@"//table/tr/td");
                    // ライン分のデータ件数取得
                    iContentsCnt = htmlDoc.DocumentNode.SelectNodes(@"//table/tr")[0].SelectNodes(@" td").Count;
                }

                // リスト取得
                TDData stTDdata;
                int tagCount = 0;
                for(tagCount = 0, UrlListCount = 0 ; tagCount < UrlList.Count; UrlListCount++)
                {
                    // 指定件数まで取得
                    if(pGetListNum == UrlListCount)
                    {
                        break;
                    }

                    // データ設定
                    stTDdata = new TDData();
                    stTDdata.Date = pDate;
                    stTDdata.Time = UrlList[tagCount].InnerText;
                    stTDdata.Code = UrlList[tagCount + 1].InnerText.Substring(0,4);
                    stTDdata.CompanyName = UrlList[tagCount + 2].InnerText;
                    stTDdata.Title = UrlList[tagCount + 3].InnerText;

                    // リスト追加
                    TDContentsList.Add(stTDdata);

                    // 1ラインの項目数分加算し、次データへ
                    tagCount += iContentsCnt;
                }

                Debug.WriteLine("タイトル取り出し完了: {0:0.000}秒", timer.Elapsed.TotalSeconds);
                Debug.WriteLine("取得タイトル先頭{0}件（全{1}件中）", pGetListNum, UrlList.Count()/7);
            }
            return TDContentsList;
        }

        public static Boolean AppVersionCheck()
        {
            Boolean bRet = false;

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
                System.Xml.XmlElement rootElement = xml.DocumentElement;

                //resタグのNodeListを取得する
                System.Xml.XmlNodeList nodelist = rootElement.GetElementsByTagName("res");
                //指定したタグが存在するか？
                if (nodelist.Count > 0)
                {
                    //指定したタグが存在したのでInnerTextプロパティで値を取得する
                    String strNewVersion = nodelist.Item(0).InnerText;
                    Debug.WriteLine("{0}{1}", "new version  : ", strNewVersion);

                    if(strNewVersion != null) { 

                        //自分自身のAssemblyを取得
                        System.Reflection.Assembly asm =
                            System.Reflection.Assembly.GetExecutingAssembly();
                        
                        //バージョンの取得
                        System.Version ver = asm.GetName().Version;
                        
                        //結果の表示
                        Debug.WriteLine("{0}{1}","this version : ", ver.ToString());

                        // バージョン判定
                        int iCompare = String.Compare(nodelist.Item(0).InnerText, ver.ToString());
                        if (iCompare > 0)
                        {
                            // 最新版あり
                            bRet = true;
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

            return bRet;
        }
    }
}
