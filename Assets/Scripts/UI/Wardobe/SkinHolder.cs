using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class SkinHolder : MonoBehaviour
{

    [Header("References")]
    [SerializeField] Button _button;
    [SerializeField] Image _image;
    [SerializeField] Image _frameImage;

    [SerializeField] TextMeshProUGUI _priceText;

    [Header("Settings")]
    [SerializeField] HolderType _holderType;

    [Header("Settings")]
    [SerializeField] Sprite _selectedSprite;
    [SerializeField] Sprite _unselectedSprite;

    SkinHandler _playerSkin;
    Shop _shop;

    CanvasGroup _canvasGroup;
    public Skin skin { get; private set; }
    ProfileInventory _profileInventory;

    public enum HolderType
    {
        Shop,
        Wardrobe

    }

    private void Awake()
    {
        _playerSkin = FindObjectOfType<SkinHandler>();
        _shop = FindObjectOfType<Shop>();

        _canvasGroup = GetComponent<CanvasGroup>();

        _profileInventory = FindObjectOfType<ProfileInventory>();

        switch (_holderType)
        {
            case HolderType.Shop:
                _button.onClick.AddListener(() =>
                {
                    _shop.SelectSkinHolder(this);
                });
                break;
            case HolderType.Wardrobe:
                _button.onClick.AddListener(() =>
                {
                    _playerSkin.SetSkin(skin);
                });
                break;
        }

    }

    public void Select()
    {
        _frameImage.sprite = _selectedSprite;
    }

    public void Deselect()
    {
        _frameImage.sprite = _unselectedSprite;
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
        this.skin = skin;

        this._image.sprite = skin.sprite;

        if (_priceText)
        {
            _priceText.text = skin.value.ToString();
        }

    }

    public void Equip()
    {
        if (skin.purchased)
        {
            Debug.Log("SKIN!");
            _playerSkin.SetSkin(skin);
        }
    }






}
