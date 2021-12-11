using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LoginScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_InputField _nameField;
    [SerializeField] TMP_InputField _passwordField;

    [Space(5)]
    [SerializeField] Button _loginButton;
    [SerializeField] Button _signInButton;
    [SerializeField] Button _continueWithoutProfileButton;

    [SerializeField] Button _spanishButton;
    [SerializeField] Button _engButton;

    ProfileInventory _profileInventory;
    SceneLoader _sceneLoader;
    CanvasGroup _canvasGroup;

    LanguageContext _languageContext;
    private void Awake()
    {
        _profileInventory = FindObjectOfType<ProfileInventory>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _languageContext = FindObjectOfType<LanguageContext>();
        _passwordField.inputType = TMP_InputField.InputType.Password;

        SetButtons();
    }

    private void Start()
    {
        Show();
    }

    void SetButtons()
    {
        _loginButton.onClick.AddListener(LogIn);
        _signInButton.onClick.AddListener(SignIn);
        _continueWithoutProfileButton.onClick.AddListener(ContinueWithNoProfile);
          _spanishButton.onClick.AddListener(() =>
        {
            _languageContext.ChangeLanguage(Language.Spanish);
        });
          _engButton.onClick.AddListener(() =>
        {
            _languageContext.ChangeLanguage(Language.English);
        });
    }

    private void StartGame()
    {
        Debug.Log("Start Game!");
        _sceneLoader.LoadScene(3);
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
    }

    void ContinueWithNoProfile()
    {
        _profileInventory.AddBoughtSkin(new Skin(SkinHandler.SkinEnum.DEFAULT, 0));

        StartGame();
    }

    void LogInFailure()
    {
        Debug.Log("Couldnt Log in");
    }

    void SignInFailure()
    {
        Debug.Log("Couldnt Sign in!");

    }

    void LogIn()
    {
        if (_nameField.text.Length != 0 && _passwordField.text.Length != 0)
        {
            _profileInventory.LogInFromDatabase(_nameField.text, _passwordField.text, StartGame, LogInFailure);
        }
    }

    void SignIn()
    {
        if (_nameField.text.Length != 0 && _passwordField.text.Length != 0)
        {
            _profileInventory.SignInFromDatabase(_nameField.text, _passwordField.text, StartGame, SignInFailure);
        }
    }


}
