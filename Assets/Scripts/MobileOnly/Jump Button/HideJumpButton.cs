using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HideJumpButton : MonoBehaviour
{
    [SerializeField] private Button settingButton;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();

        settingButton.onClick.AddListener(HideImage);

        if (!PlatformDetector.IsPlatformMobile())
        {
            gameObject.SetActive(false);
        }
    }

    private void HideImage()
    {
        var tempColor = image.color;
        switch (tempColor.a)
        {
            case 0.0f:
                tempColor.a = 0.6f;
                break;
            case 0.6f:
                tempColor.a = 0.0f;
                break;
        }

        image.color = tempColor;
    }
}