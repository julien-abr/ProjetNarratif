using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VNCreator
{
    public class VNCreator_OptionsMenu : MonoBehaviour
    {
        public TMP_Dropdown languageDropdown;
        public Slider musicVolumeSlider;
        public Slider sfxVolumeSlider;
        public Slider readSpeedSlider;
        public Toggle instantTextToggle;
        public Button backButton;

        [Header("Menu Objects")]
        public GameObject optionsMenu;
        public GameObject mainMenu;

        void Start()
        {
            GameOptions.InitilizeOptions();

            if (languageDropdown != null)
            {
                languageDropdown.value = GameOptions.language;
                languageDropdown.onValueChanged.AddListener(GameOptions.SetLanguage);
            }

            if(musicVolumeSlider != null)
            {
                musicVolumeSlider.value = GameOptions.musicVolume;
                musicVolumeSlider.onValueChanged.AddListener(GameOptions.SetMusicVolume);
            }
            if (sfxVolumeSlider != null)
            {
                sfxVolumeSlider.value = GameOptions.sfxVolume;
                sfxVolumeSlider.onValueChanged.AddListener(GameOptions.SetSFXVolume);
            }
            if (readSpeedSlider != null)
            {
                readSpeedSlider.value = GameOptions.readSpeed;
                readSpeedSlider.onValueChanged.AddListener(GameOptions.SetReadingSpeed);
            }
            if (instantTextToggle != null)
            {
                instantTextToggle.isOn = GameOptions.isInstantText;
                instantTextToggle.onValueChanged.AddListener(GameOptions.SetInstantText);
            }

            backButton.onClick.AddListener(Back);
        }

        void Back()
        {
            mainMenu.SetActive(true);
            optionsMenu.SetActive(false);
        }
    }
}
