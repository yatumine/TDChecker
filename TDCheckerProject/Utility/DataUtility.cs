using System.Collections.Generic;
using COMMON.Data;
using System;
using System.Linq;
using System.Data;

namespace COMMON.Utility
{
    class DataUtility
    {
        static public Boolean GetKeyWordRow(List<TDData> tdDataList,String keyWord, out TDData[] outTDData)
        {
            TDData[] tdDataBuff;
            List<TDData> tdDatelistBuff = new List<TDData>();
            String[] keyWordList = keyWord.Split(',');
            int keyWordCount;

            // 初期化
            outTDData = null;

            // KeyWordチェック
            if (keyWord == null || keyWord == "")
            {
                return false;
            }

            for (keyWordCount = 0; keyWordCount < keyWordList.Length; keyWordCount++)
            {
                tdDataBuff = (
                    from row in tdDataList
                    let columnID = row.Code
                    where columnID == keyWordList[keyWordCount]
                    orderby columnID
                    select row
                ).ToArray();

                tdDatelistBuff.AddRange(new List<TDData>(tdDataBuff));
            }

            outTDData = tdDatelistBuff.ToArray();

            // 取得リスト数
            if (outTDData.Count<TDData>() == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// データテーブルから、キーワードに一致したデータを取得する
        /// </summary>
        /// <param name="dataTable">データテーブル</param>
        /// <param name="columnIDName">カラムID名</param>
        /// <param name="keyWord">キーワード</param>
        /// <returns></returns>
        static public DataRow[] GetKeyWordData(DataTable dataTable,String columnIDName, String keyWord)
        {
            
            // KeyWordチェック
            if (keyWord == null || keyWord == "")
            {
                return null;
            }

            // KeyWordの抽出
            String[] keyWordList = keyWord.Split(',');
            List<DataRow> resultList = new List<DataRow>();

            DataRow[] rows = null;
            for (int keyWordListCnt = 0; keyWordListCnt < keyWordList.Length; keyWordListCnt++)
            {
                // KeyWordで検索
                rows = (
                    from row in dataTable.AsEnumerable()
                    let columnID = row.Field<string>(columnIDName)
                    where columnID == keyWordList[keyWordListCnt]
                    orderby columnID
                    select row
                ).ToArray();

                // 一時的にリストへ蓄積
                resultList.AddRange(new List<DataRow>(rows));
            }

            // DataRowを返却
            return resultList.ToArray();
        }

    }
}
