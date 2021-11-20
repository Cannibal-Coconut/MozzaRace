using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HideJumpButtonSetting : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private Button _button;

    private bool isActive;

    private void Start()
    {
        _button = GetComponent<Button>();

        isActive = false;

        _button.onClick.AddListener(SetText);

        if (!PlatformDetector.IsPlatformMobile())
            gameObject.SetActive(false);
    }

    private void SetText()
    {
        text.SetText(!isActive ? "Hide Jump Button: ON" : "Hide Jump Button: OFF");
        isActive = !isActive;
    }
}