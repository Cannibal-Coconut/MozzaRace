using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class Settings : MonoBehaviour
{

    [Header("References")]
    [SerializeField] AudioMixer _audioMixer;

    [Header("Sliders")]
    [SerializeField] Slider _masterAudioSlider;
    [SerializeField] Slider _musicAudioSlider;
    [SerializeField] Slider _sfxAudioSlider;

    [Space(10)]
    [Header("Buttons")]
    [Header("Languages")]
    [SerializeField] Button _englishButton;
    [SerializeField] Button _spanishButton;

    [Header("Tutorial")]
    [SerializeField] Button _tutorialButton;
    [SerializeField] Tutorial _tutorial;

    [Space(5)]
    [Header("Back")]
    [SerializeField] Button _backButton;

    [Header("Settings FSM")]

    [SerializeField] private CanvasGroup _volumeCanvas;
    [SerializeField] private CanvasGroup _languageCanvas;
    [SerializeField] private CanvasGroup _miscCanvas;

    [SerializeField] private TextMeshProUGUI _settingTitle1;
    [SerializeField] private TextMeshProUGUI _settingTitle2;
    [SerializeField] private TextMeshProUGUI _settingTitle3;

    CanvasGroup _canvasGroup;

    const string MasterAudioKey = "MasterAudio";
    const string MusicAudioKey = "MusicAudio";
    const string SFXAudioKey = "SFXAudio";

    const float MaxAudioValue = 1f;
    const float MinAudioValue = 0.001f;

    static bool _displayJumpButton = true;

    LanguageContext _languageContext;

    public enum SettingsState
    {
        Volume,
        Language,
        Misc,
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _languageContext = FindObjectOfType<LanguageContext>();

        SetSliders();
        SetButtons();
        DisplayVolume();
    }

    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;


    }

    private void SettingsDisplay(SettingsState s)
    {

        switch (s)
        {

            case SettingsState.Volume:

                _settingTitle1.enabled = true;
                _settingTitle2.enabled = false;
                _settingTitle3.enabled = false;
                _languageCanvas.alpha = 0;
                _languageCanvas.blocksRaycasts = false;
                _miscCanvas.alpha = 0;
                _miscCanvas.blocksRaycasts = false;
                _volumeCanvas.alpha = 1;
                _volumeCanvas.blocksRaycasts = true;

                break;

            case SettingsState.Language:

                _settingTitle1.enabled = false;
                _settingTitle2.enabled = true;
                _settingTitle3.enabled = false;
                _languageCanvas.alpha = 1;
                _languageCanvas.blocksRaycasts = true;
                _miscCanvas.alpha = 0;
                _miscCanvas.blocksRaycasts = false;
                _volumeCanvas.alpha = 0;
                _volumeCanvas.blocksRaycasts = false;
                break;

            case SettingsState.Misc:
                _settingTitle1.enabled = false;
                _settingTitle2.enabled = false;
                _settingTitle3.enabled = true;
                _languageCanvas.alpha = 0;
                _languageCanvas.blocksRaycasts = false;
                _miscCanvas.alpha = 1;
                _miscCanvas.blocksRaycasts = true;
                _volumeCanvas.alpha = 0;
                _volumeCanvas.blocksRaycasts = false;
                break;

        }

    }

    public void DisplayVolume()
    {
        SettingsDisplay(SettingsState.Volume);
    }
    public void DisplayLanguage()
    {
        SettingsDisplay(SettingsState.Language);
    }
    public void DisplayMisc()
    {
        SettingsDisplay(SettingsState.Misc);
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;

    }

    void SetSliders()
    {
        SetSlider(_masterAudioSlider, SetMasterVolume, MasterAudioKey);
        SetSlider(_musicAudioSlider, SetMusicVolume, MusicAudioKey);
        SetSlider(_sfxAudioSlider, SetSFXVolume, SFXAudioKey);
    }

    void SetButtons()
    {

        //LANGUAGES/////////////////////////////
        _englishButton.onClick.AddListener(() =>
        {
            SetLanguage(Language.English);
        });

        _spanishButton.onClick.AddListener(() =>
        {
            SetLanguage(Language.Spanish);
        });
        ////////////////////////////////////////

        _tutorialButton.onClick.AddListener(() =>
        {
            DisplayTutorial();
        });

        _backButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    void SetSlider(Slider slider, UnityAction<float> onValueChanged, string groupKey)
    {
        slider.onValueChanged.AddListener(onValueChanged);

        slider.minValue = MinAudioValue;
        slider.maxValue = MaxAudioValue;
        
        float value;
        _audioMixer.GetFloat(groupKey, out value);
        value = Mathf.Pow(10,value/20);
        slider.value = value;
    }

    public void SetMasterVolume(float newVolume)
    {
        _audioMixer.SetFloat(MasterAudioKey, Mathf.Log10(newVolume)*20);
    }

    public void SetMusicVolume(float newVolume)
    {
        _audioMixer.SetFloat(MusicAudioKey, Mathf.Log10(newVolume)*20);
    }

    public void SetSFXVolume(float newVolume)
    {
        _audioMixer.SetFloat(SFXAudioKey, Mathf.Log10(newVolume)*20);
    }

    public void SetLanguage(Language language)
    {
        switch (language)
        {
            case Language.English:
                Debug.Log("English!");
                _languageContext.ChangeLanguage(Language.English);
                break;
            case Language.Spanish:
                Debug.Log("Spanish!");
                _languageContext.ChangeLanguage(Language.Spanish);
                break;
        }
    }

    public void DisplayTutorial()
    {
        _tutorial.Display();
    }

}
