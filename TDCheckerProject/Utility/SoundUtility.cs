using System;
using System.Threading.Tasks;
using TDChecker.Properties;

namespace COMMON.Utility
{
    static class SoundUtility
    {
        /// <summary>
        /// 再生音形式
        /// </summary>
        public enum SoundType : int
        {
            e_NoSound = 0,
            e_PlayChime,
            e_PlayRing,
            e_PlayMusic,
        };

        /// <summary>
        /// 再生秒数
        /// </summary>
        public enum PlaySec : int
        {
            e_10_Sec = 10000,
        };

        /// <summary>
        /// サウンド再生
        /// </summary>
        /// <param name="soundType">再生音形式（SoundType）</param>
        static public void CallSound( SoundType soundType )
        {
            switch ( soundType )
            {
                case SoundType.e_NoSound:
                    break;
                case SoundType.e_PlayChime:
                    Microsoft.SmallBasic.Library.Sound.PlayChimes();
                    break;
                case SoundType.e_PlayRing:
                    Microsoft.SmallBasic.Library.Sound.PlayBellRing();
                    break;
                case SoundType.e_PlayMusic:
                    Microsoft.SmallBasic.Library.Sound.Play(Settings.Default.BellSoundFile);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 指定ファイル再生
        /// </summary>
        /// <param name="filePath">再生ファイルPath</param>
        /// <param name="playSec">再生秒数</param>
        static public async void PlayTimerAsync(String filePath, PlaySec playSec)
        {
            Microsoft.SmallBasic.Library.Sound.Play(filePath);
            await Task.Delay((int)playSec);
            Microsoft.SmallBasic.Library.Sound.Stop(filePath);
            return;
        }
    }
}
