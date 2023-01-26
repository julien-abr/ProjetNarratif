using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace Team02
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown languageDropdown;
        [SerializeField] private Slider musicVolumeSlider;
        //[SerializeField] private Slider readSpeedSlider;
        //[SerializeField] private Toggle instantTextToggle;
        [SerializeField] private Button backButton;

        [Header("Menu Objects")]
        [SerializeField] private GameObject mainMenu;
        [SerializeField] private GameObject optionsMenu;
        private AudioMixer audioMixer;
        private RapBattlesSO rapBattlesSo;

        [Header("Title")]
        [SerializeField] private RectTransform west;
        [SerializeField] private RectTransform contenders;

        [Header("For animations")]
        [SerializeField] private RectTransform play;
        [SerializeField] private RectTransform options;
        [SerializeField] private RectTransform quit;
        [SerializeField] private Image fonduAuNoir;

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

            //if (readSpeedSlider != null)
            //{
            //    readSpeedSlider.value = GameOptions.readSpeed;
            //    readSpeedSlider.onValueChanged.AddListener(GameOptions.SetReadingSpeed);
            //}
            //
            //if (instantTextToggle != null)
            //{
            //    instantTextToggle.isOn = GameOptions.isInstantText;
            //    instantTextToggle.onValueChanged.AddListener(GameOptions.SetInstantText);
            //}

            backButton.onClick.AddListener(BackToMenu);
            StartCoroutine(EnterAnimation());
        }

        #region mainMenu
        public void StartGame()
        {
            StartCoroutine(FadeToStart());
        }

        private IEnumerator FadeToStart()
        {
            fonduAuNoir.DOColor(new Color(0, 0, 0, 1), 1);
            yield return new WaitForSeconds(2);
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

        #region animations
        private IEnumerator EnterAnimation()
        {
            Vector2 westStartPos = west.anchoredPosition;
            Vector2 contendersStartPos = contenders.anchoredPosition;
            west.anchoredPosition = westStartPos - 1500 * Vector2.left;
            contenders.anchoredPosition = contendersStartPos + 1500 * Vector2.left;
            Vector2 playStartPos = play.anchoredPosition;
            Vector2 optionsStartPos = options.anchoredPosition;
            Vector2 quitStartPos = quit.anchoredPosition;
            play.anchoredPosition = playStartPos + 1000 * Vector2.left;
            options.anchoredPosition = optionsStartPos + 1000 * Vector2.left;
            quit.anchoredPosition = quitStartPos + 1000 * Vector2.left;
            yield return new WaitForSeconds(1);
            west.DOAnchorPos(westStartPos, .5f).SetEase(Ease.OutBack, 1);
            yield return new WaitForSeconds(.25f);
            contenders.DOAnchorPos(contendersStartPos, .5f).SetEase(Ease.OutBack, 1);
            yield return new WaitForSeconds(.25f);
            play.DOAnchorPos(playStartPos, .5f).SetEase(Ease.OutBack, 1);
            yield return new WaitForSeconds(.25f);
            options.DOAnchorPos(optionsStartPos, .5f).SetEase(Ease.OutBack, 1);
            yield return new WaitForSeconds(.25f);
            quit.DOAnchorPos(quitStartPos, .5f).SetEase(Ease.OutBack, 1);
        }
        #endregion
    }
}