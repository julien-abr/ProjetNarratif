using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VNCreator
{
    public static class GameOptions
    {
        public static float musicVolume = 0.5f;
        public static float sfxVolume = 0.5f;
        public static float readSpeed = 0.5f;
        public static int language = 0;
        public static bool isInstantText = false;

        public static void InitilizeOptions()
        {
            if (PlayerPrefs.HasKey("Language"))
                language = PlayerPrefs.GetInt("Language");

            LocalizeText.Instance.SetLanguage(GetLanguage(language));

            if (PlayerPrefs.HasKey("MusicVolume"))
                musicVolume = PlayerPrefs.GetFloat("MusicVolume");
            if (PlayerPrefs.HasKey("SfxVolume"))
                sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            if (PlayerPrefs.HasKey("ReadSpeed"))
                readSpeed = PlayerPrefs.GetFloat("ReadSpeed");
            if (PlayerPrefs.HasKey("InstantText"))
                isInstantText = PlayerPrefs.GetInt("InstantText") == 1 ? true : false;
        }

        public static void SetLanguage(int index)
        {
            language = index;
            LocalizeText.Instance.SetLanguage(GetLanguage(language));
            PlayerPrefs.SetInt("Language", index);
        }

        public static void SetMusicVolume(float index)
        {
            musicVolume = index;
            PlayerPrefs.SetFloat("MusicVolume", index);
        }

        public static void SetSFXVolume(float index)
        {
            sfxVolume = index;
            PlayerPrefs.SetFloat("SfxVolume", index);
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

        private static string GetLanguage(int index)
        {
            switch(index)
            {
                case 0:
                    return "English";
                case 1:
                    return "French";
            }
            return "English";
        }
    }
}
