using UnityEngine;
using UnityEngine.Audio;

namespace Team02
{
    public static class GameOptions
    {
        public static float musicVolume = -30f;
        public static float readSpeed = 0.5f;
        public static int language = 0;
        public static bool isInstantText = false;
        private static AudioMixer audioMixer;
        private static RapBattlesSO rapBattlesSo;

        public static void InitilizeOptions()
        {
            if (PlayerPrefs.HasKey("Language"))
                language = PlayerPrefs.GetInt("Language");
            if (PlayerPrefs.HasKey("MusicVolume"))
                musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            if (PlayerPrefs.HasKey("SfxVolume"))
                readSpeed = PlayerPrefs.GetFloat("ReadSpeed");
            if (PlayerPrefs.HasKey("InstantText"))
                isInstantText = PlayerPrefs.GetInt("InstantText") == 1 ? true : false;

            audioMixer = Resources.Load<AudioMixer>("Audio/AudioMixer");
            rapBattlesSo = Resources.Load<RapBattlesSO>("Team02/RapBattles");
        }

        public static void SetLanguage(int index)
        {
            language = index;
            rapBattlesSo.ChangeLanguage(index);
            PlayerPrefs.SetInt("Language", index);
        }

        public static void SetMusicVolume(float index)
        {
            musicVolume = index;
            PlayerPrefs.SetFloat("MusicVolume", index);
            audioMixer.SetFloat("MusicVolume", index);
        }

        public static void SetReadingSpeed(float index)
        {
            readSpeed = index;
            PlayerPrefs.SetFloat("ReadSpeed", index);
        }

        public static void SetInstantText(bool index)
        {
            isInstantText = index;
            PlayerPrefs.SetInt("InstantText", index ? 1 : 0);
        }
    }
}
