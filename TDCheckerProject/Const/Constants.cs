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
        public const short BELL_NO_SOUND = 0;
        public const short BELL_PLAY_CHIME = 1;
        public const short BELL_PLAY_BELL_RING = 2;
        public const short BELL_PLAY_MUSIC = 3;

        /// <summary>
        /// Option 初回取得件数
        /// </summary>
        public const int OPTION_NO_READING = 0;
        public const int OPTION_ALL_READING = 1;
        public const int OPTION_SPECIFIED_NUMBER_READ = 2;

        /// <summary>
        /// Option 読み込みリスト数
        /// </summary>
        public const int DEFAULT_READLISTNUM = 10;
        public const int DEFAULT_READCYCLE = 1;


        /// <summary>
        /// ログファイル名
        /// </summary>
        public const String LOG_TDNET = "TDnet";

        /// <summary>
        /// フィルタボタンON
        /// </summary>
        public const String BTN_FILTER_ON = "設定中";
        /// <summary>
        /// フィルタボタンOFF
        /// </summary>
        public const String BTN_FILTER_DEFAULT = "フィルタ";

        /// <summary>
        /// 銘柄コードコピーメッセージ
        /// </summary>
        public const String MSG_CODE_COPY = "銘柄コードをコピーしました。";

        /// <summary>
        /// グリッド表示モード
        /// </summary>
        public const int GRID_MODE_NOMAL= 0;
        public const int GRID_MODE_KEYWORD = 1;
        public const int GRID_MODE_AUTOSELECT = 2;


    }
}
