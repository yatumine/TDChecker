using System;

namespace COMMON.Const
{
    public class Constants
    {
        /// <summary>
        /// フォーマット定義
        /// </summary>
        public static readonly String YYYYMMDD = "yyyyMMdd";
        public static readonly String YYYYMMDD_SLASH = "yyyy/MM/dd";
        public static readonly String YYYYMMDDHHMM = "yyyyMMddHHmm";
        public static readonly String YYYYMMDDHHMM_SLASH = "yyyy/MM/dd HH:mm";
        public static readonly String YYYYMMDDHHMMSS = "yyyyMMddHHmmss";
        public static readonly String YYYYMMDDHHMMSS_SLASH = "yyyy/MM/dd HH:mm:ss";
        public static readonly String YYYYMMDDHHMMSSFFF_SLASH = "yyyy/MM/dd HH:mm:ss.fff";

        /// <summary>
        /// ベル音
        /// </summary>
        public const short BellNoSound = 0;
        public const short BellPlayChime = 1;
        public const short BellPlayBellRing = 2;
        public const short BellPlayMusic = 3;

        /// <summary>
        /// Option 初回取得件数
        /// </summary>
        public const int OptionNoReading = 0;
        public const int OptionAllReading = 1;
        public const int OptionSpecifiedNumberRead = 2;

        /// <summary>
        /// Option 読み込みリスト数
        /// </summary>
        public const int DefaultReadingNum = 10;
        public const int DefaultReadCycle = 1;

        /// <summary>
        /// フィルタボタンON
        /// </summary>
        public const String BtnFilterOn = "設定中";
        /// <summary>
        /// フィルタボタンOFF
        /// </summary>
        public const String BtnFilterDefault = "フィルタ";

        /// <summary>
        /// 銘柄コードコピーメッセージ
        /// </summary>
        public const String MsgCodeCopy = "銘柄コードをコピーしました。";

        /// <summary>
        /// グリッド表示モード
        /// </summary>
        public const int GridModeNomal= 0;
        public const int GridModeKeyWord = 1;
        public const int GridModeAutoSelect = 2;
    }
}
