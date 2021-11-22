using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Tutorial : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Sprite _pcTutorial;
    [SerializeField] Sprite _mobileTutorial;

    [SerializeField] Image _pcImage;
    [SerializeField] Image _mobileImage;

    [SerializeField] Button _backButton;
    [SerializeField] Button _mobileBackButton;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        Hide();

        if (PlatformDetector.IsPlatformMobile())
        {
            _pcImage.gameObject.SetActive(false);
            _backButton.gameObject.SetActive(false);

            _mobileImage.enabled = true;
            _mobileBackButton.enabled = true;
        }
        else
        {
            _pcImage.enabled = true;
            _backButton.enabled = true;
            
            _mobileBackButton.gameObject.SetActive(false);
            _mobileImage.gameObject.SetActive(false);
        }

        _backButton.onClick.AddListener(() =>
        {
            Hide();
        });
        
        _mobileBackButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    public void Display()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }
}
