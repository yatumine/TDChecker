using System;

namespace COMMON.Data
{
    /// <summary>
    /// 適時開示データ
    /// </summary>
    public class TDData
    {
        public String Date { get; set; }            // 公開年月日
        public String Time { get; set; }            // 公開時間
        public String Code { get; set; }            // 銘柄コード
        public String CompanyName { get; set; }     // 会社名
        public String Title { get; set; }           // タイトル
        public String URL { get; set; }             // URL

    }

    /// <summary>
    /// オプション画面
    /// 読み込み間隔設定データ
    /// </summary>
    public class ReadCycle
    {
        public String Cycle { get; set; }           // 表示間隔
        public int Value { get; set; }              // 読み込み間隔数値データ

        /// <summary>
        /// 読み込み間隔データ設定コンストラクタ
        /// </summary>
        /// <param name="cycle">表示間隔</param>
        /// <param name="value">間隔数値</param>
        public ReadCycle(String cycle, int value)
        {
            this.Cycle = cycle;
            this.Value = value;
        }

        /// <summary>
        /// 読み込み間隔の文字列返却
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Cycle.ToString();
        }
    }
}
