using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageContext : MonoBehaviour
{

    public Language currentLanguage { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeLanguage(Language.English);
    }

    public void ChangeLanguage(Language language)
    {
        if (language == currentLanguage) return;

        currentLanguage = language;
        foreach (var changeable in FindObjectsOfType<LanguageChangeable>())
        {
            changeable.ChangeLanguage(language);
        }
    }

}
