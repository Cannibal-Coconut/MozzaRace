using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HideJumpButton : MonoBehaviour
{
    private Image _image;

    [SerializeField] private Button _button;

    private void Awake()
    {
        _image = GetComponent<Image>();
        
        _button.onClick.AddListener(HideImage);

        if (!PlatformDetector.IsPlatformMobile())
            gameObject.SetActive(false);
    }

    private void HideImage()
    {
        _image.enabled = !_image.enabled;
    }
}