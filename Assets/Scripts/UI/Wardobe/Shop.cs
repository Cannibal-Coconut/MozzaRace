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
    [SerializeField] Button _backButton;

    [SerializeField] Button _previousSkinPageButton;
    [SerializeField] Button _nextSkinPageButton;


    [SerializeField] SkinHolder[] _skinHolders;
    [SerializeField] TextMeshProUGUI _moneyMesh;

    Skin[] _skins;
    int _currentSkinSpage;
    int maxPage = 0;


    CanvasGroup _canvasGroup;
    ProfileInventory _profileInventory;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _profileInventory = FindObjectOfType<ProfileInventory>();
        _profileInventory.AddOnEconomyChangeListener(UpdateMoney);

        Hide();

        CreateSkins();


    }

    private void Start()
    {
        SetButtons();

        SetSkinPage(0);

        
    }

    void CreateSkins()
    {
        List<Skin> skins = new List<Skin>();
        var whiteSkin = new Skin(Color.white, 0);
        whiteSkin.purchased = true;

        skins.Add(whiteSkin);
        skins.Add(new Skin(Color.green, 200));
        skins.Add(new Skin(Color.yellow, 300));
        skins.Add(new Skin(Color.blue, 400));
        skins.Add(new Skin(Color.magenta, 500));

        _skins = skins.ToArray();

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
    }

    void SetButtons()
    {
        _backButton.onClick.AddListener(() =>
        {
            Hide();
        });

        _nextSkinPageButton.onClick.AddListener(() =>
        {
            SetSkinPage(_currentSkinSpage + 1);
        });

        _previousSkinPageButton.onClick.AddListener(() =>
        {
            SetSkinPage(_currentSkinSpage - 1);
        });
    }

    void UpdateMoney()
    {
        _moneyMesh.text = "Skin Points: " + _profileInventory.skinPoints.ToString();
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
