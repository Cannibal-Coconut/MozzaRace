using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

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

    [Space(5)]
    [Header("Back")]
    [SerializeField] Button _backButton;

    CanvasGroup _canvasGroup;
    LanguageContext _languageContext;

    const string MasterAudioKey = "MasterAudio";
    const string MusicAudioKey = "MusicAudio";
    const string SFXAudioKey = "SFXAudio";

    const float MaxAudioValue = 20;
    const float MinAudioValue = -80;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _languageContext = FindObjectOfType<LanguageContext>();

        SetSliders();
        SetButtons();
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

        slider.value = value;
    }

    public void SetMasterVolume(float newVolume)
    {
        _audioMixer.SetFloat(MasterAudioKey, newVolume);
    }

    public void SetMusicVolume(float newVolume)
    {
        _audioMixer.SetFloat(MusicAudioKey, newVolume);
    }

    public void SetSFXVolume(float newVolume)
    {
        _audioMixer.SetFloat(SFXAudioKey, newVolume);
    }

    public void SetLanguage(Language language)
    {
        _languageContext.ChangeLanguage(language);
    }

    public void DisplayTutorial()
    {
        Debug.Log("Tutorial!");
    }

}
