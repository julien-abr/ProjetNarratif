using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Team02
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown languageDropdown;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider readSpeedSlider;
        [SerializeField] private Toggle instantTextToggle;
        [SerializeField] private Button backButton;

        [Header("Menu Objects")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;
        private AudioMixer audioMixer;
        private RapBattlesSO rapBattlesSo;

        private void Start()
        {
            audioMixer = Resources.Load<AudioMixer>("Team02/Audio/Team02_Mixer");
            rapBattlesSo = Resources.Load<RapBattlesSO>("Team02/RapBattles");

            GameOptions.InitilizeOptions();

            if (languageDropdown != null)
            {
                languageDropdown.value = GameOptions.language;
                rapBattlesSo.ChangeLanguage(GameOptions.language);
                languageDropdown.onValueChanged.AddListener(GameOptions.SetLanguage);
            }

            if (musicVolumeSlider != null)
            {
                musicVolumeSlider.value = GameOptions.musicVolume;
                //audioMixer.SetFloat("MusicVolume", GameOptions.musicVolume);
                musicVolumeSlider.onValueChanged.AddListener(GameOptions.SetMusicVolume);
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

            backButton.onClick.AddListener(BackToMenu);
        }

        #region mainMenu
        public void StartGame()
        {
            SceneManager.LoadScene(1);
        }

        public void Options()
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
        }

        public void Quit()
        {
            Application.Quit();
        }
        #endregion mainMenu

        #region optionsMenu
        private void BackToMenu()
        {
            optionsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        #endregion optionsMenu
    }
}