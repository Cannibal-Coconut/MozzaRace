using System;
using System.Collections;
using System.Collections.Generic;
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

    [Space(5)]
    [Header("Back")]
    [SerializeField] Button _backButton;

    [Header("Settings FSM")]

    [SerializeField] private CanvasGroup _volumeCanvas;
    [SerializeField] private CanvasGroup _languageCanvas;
    [SerializeField] private CanvasGroup _miscCanvas;

    [SerializeField] private TextMeshProUGUI _settingTitle;

    CanvasGroup _canvasGroup;

    const string MasterAudioKey = "MasterAudio";
    const string MusicAudioKey = "MusicAudio";
    const string SFXAudioKey = "SFXAudio";

    const float MaxAudioValue = 20;
    const float MinAudioValue = -80;


    public enum SettingsState {
    Volume,
    Language,
    Misc,
} 

    
    private Language _language;

    public Language GetLanguage(){

        return _language;

    }

    public enum Language
    {
        English,
        Spanish,
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

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

    private void SettingsDisplay(SettingsState s){

        switch(s){

            case SettingsState.Volume:
            if(_language ==  Language.English)_settingTitle.text = "Volume"; 
            if(_language ==  Language.Spanish)_settingTitle.text = "Volumen"; 
            _languageCanvas.alpha = 0;
            _languageCanvas.blocksRaycasts = false;
            _miscCanvas.alpha = 0;
            _miscCanvas.blocksRaycasts = false;
            _volumeCanvas.alpha = 1;
            _volumeCanvas.blocksRaycasts = true;

            break;

            case SettingsState.Language:
            if(_language ==  Language.English)_settingTitle.text = "Language"; 
            if(_language ==  Language.Spanish)_settingTitle.text = "Idioma"; 
             _languageCanvas.alpha = 1;
            _languageCanvas.blocksRaycasts = true;
            _miscCanvas.alpha = 0;
            _miscCanvas.blocksRaycasts = false;
            _volumeCanvas.alpha = 0;
            _volumeCanvas.blocksRaycasts = false;
            break;

            case SettingsState.Misc:
            _settingTitle.text = "Misc";
            _languageCanvas.alpha = 0;
            _languageCanvas.blocksRaycasts = false;
            _miscCanvas.alpha = 1;
            _miscCanvas.blocksRaycasts = true;
            _volumeCanvas.alpha = 0;
            _volumeCanvas.blocksRaycasts = false;
            break; 

        }

    }

    public void DisplayVolume(){
        SettingsDisplay(SettingsState.Volume);
    }
    public void DisplayLanguage(){
        SettingsDisplay(SettingsState.Language);
    }
    public void DisplayMisc(){
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
        switch (language)
        {
            case Language.English:
                Debug.Log("English!");
                _language = Language.English;
                break;
            case Language.Spanish:
                Debug.Log("Spanish!");
                _language = Language.Spanish;
                break;
        }
    }

    public void DisplayTutorial()
    {
        Debug.Log("Tutorial!");
    }

}
