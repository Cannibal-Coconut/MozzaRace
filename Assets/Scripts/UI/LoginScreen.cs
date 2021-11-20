using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreen : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TMP_InputField _nameField;
    [SerializeField] TMP_InputField _passwordField;

    [Space(5)]
    [SerializeField] Button _loginButton;
    [SerializeField] Button _signInButton;
    [SerializeField] Button _continueWithoutProfileButton;


    ProfileInventory _profileInventory;

    private void Awake()
    {
        _profileInventory = FindObjectOfType<ProfileInventory>();

        SetButtons();
    }

    void SetButtons()
    {
        _loginButton.onClick.AddListener(LogIn);
        _signInButton.onClick.AddListener(SignIn);
        _continueWithoutProfileButton.onClick.AddListener(ContinueWithNoProfile);
    }

    private void StartGame()
    {
        Debug.Log("Start Game!");
    }

    void ContinueWithNoProfile()
    {

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
