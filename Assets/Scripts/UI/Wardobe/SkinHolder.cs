using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class SkinHolder : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Button _button;
    [SerializeField] Image _image;

    [Header("Settings")]
    [SerializeField] HolderType _holderType;

    PlayerSkin _playerSkin;

    CanvasGroup _canvasGroup;
    Skin _skin;
    ProfileInventory _profileInventory;

    public enum HolderType
    {
        Shop,
        Wardrobe

    }

    private void Awake()
    {
        _playerSkin = FindObjectOfType<PlayerSkin>();

        _canvasGroup = GetComponent<CanvasGroup>();

        _profileInventory = FindObjectOfType<ProfileInventory>();



        switch (_holderType)
        {
            case HolderType.Shop:
                _button.onClick.AddListener(() =>
                {
                    Purchase();
                });
                break;
            case HolderType.Wardrobe:
                _button.onClick.AddListener(() =>
                {
                    Equip();
                });
                break;
        }

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

    public void SetSkin(Skin skin)
    {
        _skin = skin;

        _image.color = _skin.color;


    }

    public void Equip()
    {
        if (_skin.purchased)
        {
            Debug.Log("SKIN!");
            _playerSkin.SetSkin(_skin);
        }
    }

    public void Purchase()
    {
        if (!_skin.purchased)
        {
            if (_profileInventory.skinPoints >= _skin.value)
            {
                _profileInventory.RemoveSkinPoints(_skin.value);

                if (!_skin.purchased)
                {
                    _skin.purchased = true;

                    _profileInventory.skins.Add(_skin);
                }

            }
        }

    }





}
