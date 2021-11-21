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

    [SerializeField] Image _image;

    [SerializeField] Button _backButton;

    CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        Hide();

        if (PlatformDetector.IsPlatformMobile())
        {
            _image.sprite = _mobileTutorial;
        }
        else
        {
            _image.sprite = _pcTutorial;
        }

        _backButton.onClick.AddListener(() =>
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
