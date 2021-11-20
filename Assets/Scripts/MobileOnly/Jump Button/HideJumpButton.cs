using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class HideJumpButton : MonoBehaviour
{
    [SerializeField] private Button _settingButton;

    private Color _buttonColorBlock;

    private void Start()
    {
        _buttonColorBlock = GetComponent<Button>().colors.normalColor;
        
        _settingButton.onClick.AddListener(HideImage);

        if (!PlatformDetector.IsPlatformMobile())
        {
            gameObject.SetActive(false);
        }
    }

    private void HideImage()
    {
        switch (_buttonColorBlock.a)
        {
            case 255:
                _buttonColorBlock.a = 0;
                break;
            case 0:
                _buttonColorBlock.a = 255;
                break;
        }
    }
}