using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Shop : MonoBehaviour
{
    [Header("References")]
    [Header("Buttons")]
    [SerializeField]
    Button _backButton;

    [SerializeField] Button _previousSkinPageButton;
    [SerializeField] Button _nextSkinPageButton;

    [SerializeField] Button _buySkinButton;
    [SerializeField] private TMP_Text _textBuyButton;

    [SerializeField] SkinHolder[] _skinHolders;
    [SerializeField] TextMeshProUGUI _moneyMesh;

    [SerializeField] private LanguageContext languageContext;

    Skin[] _skins;
    int _currentSkinSpage;
    int maxPage = 0;
    SkinHolder _selectedSkinHolder;

    CanvasGroup _canvasGroup;
    ProfileInventory _profileInventory;

    [SerializeField] Image buyingSprite;
    [SerializeField] Sprite unBoughtSprite;
    [SerializeField] Sprite boughtSprite;

    SoundSettingManager sound;
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        sound = FindObjectOfType<SoundSettingManager>();
        Hide();

        _profileInventory = FindObjectOfType<ProfileInventory>();
        _profileInventory.AddOnEconomyChangeListener(UpdateMoney);
        CreateSkins();
    }

    private void Start()
    {
        SetButtons();

        SetSkinPage(0);

 
        UpdateMoney();

        SkinInProperty();
    }

    public void SelectSkinHolder(SkinHolder skinHolder)
    {
        if (_selectedSkinHolder)
        {
            _selectedSkinHolder.Deselect();
        }

        _selectedSkinHolder = skinHolder;

        _selectedSkinHolder.Select();
        SkinInProperty();
    }

    void BuySkin()
    {
        if (_selectedSkinHolder != null)
        {
            if (!_selectedSkinHolder.skin.purchased)
            {
                if (_profileInventory.points >= _selectedSkinHolder.skin.value)
                {
                    _profileInventory.RemovePoints(_selectedSkinHolder.skin.value);

                    if (!_selectedSkinHolder.skin.purchased)
                    {
                        _selectedSkinHolder.skin.purchased = true;

                        _profileInventory.AddBoughtSkin(_selectedSkinHolder.skin);

                        switch (languageContext.currentLanguage)
                        {
                            case Language.Spanish:
                                _textBuyButton.SetText("Comprado");
                                break;
                            case Language.English:
                                _textBuyButton.SetText("Bought");
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        buyingSprite.sprite = boughtSprite;
                        sound.PlaySpendMoney();
                    }

                }
                else
                {
                    sound.PlayPizzaTimeError();
                }
            }
            else
            {
                sound.PlayPizzaTimeError();
            }
        }
    }

    void SkinInProperty()
    {
        if (_selectedSkinHolder == null) return;

        switch (languageContext.currentLanguage)
        {
            case Language.Spanish:
                _textBuyButton.SetText(_selectedSkinHolder.skin.purchased ? "Comprado" : "Comprar?");
                break;
            case Language.English:
                _textBuyButton.SetText(_selectedSkinHolder.skin.purchased ? "Bought" : "Buy?");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_selectedSkinHolder.skin.purchased) buyingSprite.sprite = boughtSprite;
        else buyingSprite.sprite = unBoughtSprite;


    }

    void CreateSkins()
    {

        switch (languageContext.currentLanguage)
        {
            case Language.Spanish:
                _textBuyButton.SetText("Comprar?");
                break;
            case Language.English:
                _textBuyButton.SetText("Buy?");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }


        List<Skin> skins = new List<Skin>();

        skins.Add(new Skin(SkinEnum.GREEN, 200));
        skins.Add(new Skin(SkinEnum.YELLOW, 300));
        skins.Add(new Skin(SkinEnum.BLUE, 400));
        skins.Add(new Skin(SkinEnum.MAGENTA, 500));
        skins.Add(new Skin(SkinEnum.RED, 500));
        skins.Add(new Skin(SkinEnum.CYAN, 500));
        skins.Add(new Skin(SkinEnum.ORANGE, 500));
        skins.Add(new Skin(SkinEnum.PURPLE, 1000));

        _skins = skins.ToArray();

        foreach (var shopSkin in _skins)
        {
            foreach (var profileSkin in _profileInventory.skins)
            {
                if (shopSkin.skinID == profileSkin.skinID)
                {
                    shopSkin.purchased = true;
                }
            }
        }

        maxPage = 0;
        int skinCount = _skins.Length;

        while (skinCount > 0)
        {
            maxPage++;

            skinCount -= maxPage * _skinHolders.Length;
        }
    }

    void SetSkinPage(int pageNumber)
    {
        int skinsPerPage = _skinHolders.Length;

        if (pageNumber >= maxPage)
        {
            pageNumber = 0;
        }
        else if (pageNumber < 0)
        {
            pageNumber = maxPage - 1;
        }

        _currentSkinSpage = pageNumber;

        for (int i = 0; i < _skinHolders.Length; i++)
        {
            _skinHolders[i].Hide();
        }

        for (int i = 0; i < skinsPerPage; i++)
        {
            if (i + pageNumber * skinsPerPage >= _skins.Length) break;

            _skinHolders[i].SetSkin(_skins[pageNumber * skinsPerPage + i]);
            _skinHolders[i].Show();
        }

        SelectSkinHolder(_skinHolders[0]);
    }

    void SetButtons()
    {
        _backButton.onClick.AddListener(() => { Hide(); });

        _nextSkinPageButton.onClick.AddListener(() => { SetSkinPage(_currentSkinSpage + 1); });

        _previousSkinPageButton.onClick.AddListener(() => { SetSkinPage(_currentSkinSpage - 1); });

        _buySkinButton.onClick.AddListener(BuySkin);
    }

    void UpdateMoney()
    {
        _moneyMesh.text = _profileInventory.points.ToString();
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