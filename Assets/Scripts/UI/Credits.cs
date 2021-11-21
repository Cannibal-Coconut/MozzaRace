using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Credits : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Button _backButton;
    [SerializeField] Button _showButton;
    [Space(5)]
    [SerializeField] Button _twitterButton;
    [SerializeField] Button _instagramButton;
    [SerializeField] Button _youtubeButton;
    [SerializeField] Button _itchioButton;
    [SerializeField] Button _webButton;

    [SerializeField] CanvasGroup _canvasGroup;

    const string TwitterURL = "https://twitter.com/Canniba1Coconut";
    const string InstagramURL = "https://www.instagram.com/mozzarace/";
    const string YoutubeURL = "https://www.youtube.com/channel/UCxQY_jKOws48sSrO1a-99cw";
    const string ItchioURL = "https://cannibal-coconut.itch.io";
    const string WebURL = "https://cannibal-coconut.github.io";

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        Hide();

        SetButtons();
    }

    void SetButtons()
    {
        _backButton.onClick.AddListener(Hide);
        _showButton.onClick.AddListener(Show);

        _twitterButton.onClick.AddListener(OpenTwitter);
        _instagramButton.onClick.AddListener(OpenInstagram);
        _youtubeButton.onClick.AddListener(OpenYoutube);
        _itchioButton.onClick.AddListener(OpenItchio);
        _webButton.onClick.AddListener(OpenWeb);

    }

    void OpenURL(string url)
    {
        Application.OpenURL(url);
    }

    void OpenTwitter()
    {
        OpenURL(TwitterURL);
    }

    void OpenInstagram()
    {
        OpenURL(InstagramURL);
    }
    void OpenYoutube()
    {
        OpenURL(YoutubeURL);
    }
    void OpenItchio()
    {
        OpenURL(ItchioURL);
    }
    void OpenWeb()
    {
        OpenURL(WebURL);
    }



    void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

    }



}
