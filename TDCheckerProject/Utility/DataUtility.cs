using System.Collections.Generic;
using COMMON.Data;
using System;
using System.Linq;
using System.Data;

namespace COMMON.Utility
{
    class DataUtility
    {
        static public Boolean GetKeyWordRow(List<TDData> pList,String pKeyWord, out TDData[] pRet)
        {
            TDData[] dataBuff;
            List<TDData> lstBuff = new List<TDData>();
            Boolean bRet = false;
            String[] strKeyWord = pKeyWord.Split(',');
            int cnt;

            // 初期化
            pRet = null;

            // KeyWordチェック
            if (pKeyWord == null || pKeyWord == "")
            {
                return bRet;
            }

            for (cnt = 0; cnt < strKeyWord.Length; cnt++)
            {
                dataBuff = (
                    from row in pList
                    let columnID = row.Code
                    where columnID == strKeyWord[cnt]
                    orderby columnID
                    select row
                ).ToArray();

                lstBuff.AddRange(new List<TDData>(dataBuff));
            }

            pRet = lstBuff.ToArray();

            // 取得リスト数
            int iListCount = pRet.Count<TDData>();
            if (iListCount == 0)
            {
                bRet = false;
            }
            else
            {
                bRet = true;
            }

            return bRet;
        }

        static public DataRow[] GetKeyWordData(DataTable pDataTable,String pColumnID, String pKeyWord)
        {
            
            // KeyWordチェック
            if (pKeyWord == null || pKeyWord == "")
            {
                return null;
            }

            // KeyWordの抽出
            String[] strKeyWordList = pKeyWord.Split(',');
            List<DataRow> lstRet = new List<DataRow>();

            DataRow[] rows = null;
            for (int cnt = 0; cnt < strKeyWordList.Length; cnt++)
            {
                // KeyWordで検索
                rows = (
                    from row in pDataTable.AsEnumerable()
                    let columnID = row.Field<string>(pColumnID)
                    where columnID == strKeyWordList[cnt]
                    orderby columnID
                    select row
                ).ToArray();

                // 一時的にリストへ蓄積
                lstRet.AddRange(new List<DataRow>(rows));
            }

            // DataRowを返却
            return lstRet.ToArray();
        }

    }
}
