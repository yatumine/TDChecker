using System;
using System.Threading.Tasks;
using TDChecker.Properties;

namespace COMMON.Utility
{
    static class SoundUtility
    {
        public enum SOUND_TYPE 
        {
            e_NO_SOUND = 0,
            e_PLAY_CHIME,
            e_PLAY_BELL_RING,
            e_PLAY_PLAY_MUSIC,
        };

        /// <summary>
        /// サウンド再生
        /// </summary>
        /// <param name="p_SoundType"></param>
        static public void CallSound( SOUND_TYPE p_SoundType )
        {
            switch ( p_SoundType )
            {
                case SOUND_TYPE.e_NO_SOUND:
                    break;
                case SOUND_TYPE.e_PLAY_CHIME:
                    Microsoft.SmallBasic.Library.Sound.PlayChimes();
                    break;
                case SOUND_TYPE.e_PLAY_BELL_RING:
                    Microsoft.SmallBasic.Library.Sound.PlayBellRing();
                    break;
                case SOUND_TYPE.e_PLAY_PLAY_MUSIC:
                    Microsoft.SmallBasic.Library.Sound.Play(Settings.Default.BellSoundFile);
                    break;
                default:
                    break;
            }

            return;
        }


        static public async void PlayTimerAsync(String pFilePath, int pPlaySec)
        {
            int iSec = pPlaySec * 1000;
            try
            {
                Microsoft.SmallBasic.Library.Sound.Play(pFilePath);
                await Task.Delay(iSec);
                Microsoft.SmallBasic.Library.Sound.Stop(pFilePath);
            }
            catch (Exception)
            {
                Microsoft.SmallBasic.Library.Sound.Play(pFilePath);
            }

            return;
        }
    }
}
