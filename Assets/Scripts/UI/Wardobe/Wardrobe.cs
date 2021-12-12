using UnityEngine;
using UnityEngine.UI;

public class Wardrobe : MonoBehaviour
{
    [Header("References")]
    [Header("Buttons")]
    [SerializeField] Button _backButton;

    [SerializeField] Button _previousSkinPageButton;
    [SerializeField] Button _nextSkinPageButton;

    [SerializeField] SkinHolder[] _skinHolders;

    int _currentSkinSpage;
    int maxPage = 0;

    CanvasGroup _canvasGroup;
    [SerializeField] CanvasGroup _extraCanvas;
    ProfileInventory _profileInventory;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _profileInventory = FindObjectOfType<ProfileInventory>();

        Hide();

    }

    private void Start()
    {
        SetButtons();

        SetSkinPage(0);
        
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


        for (int i = 0; i < _skinHolders.Length; i++)
        {
            _skinHolders[i].Hide();
        }

        
        if (_profileInventory.skins.Count == 0) return;
        
        _currentSkinSpage = pageNumber;

        for (int i = 0; i < skinsPerPage; i++)
        {
            if (i + pageNumber * skinsPerPage >= _profileInventory.skins.Count) break;
            Debug.Log("Set skin" + _profileInventory.skins[pageNumber * skinsPerPage + i].skinID);
            _skinHolders[i].SetSkin(_profileInventory.skins[pageNumber * skinsPerPage + i]);
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

    public void Display()
    {
        int skinCount = _profileInventory.skins.Count;

        while (skinCount > 0)
        {
            maxPage++;

            skinCount -= maxPage * _skinHolders.Length;
        }

        maxPage++;

        SetSkinPage(0);

        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _extraCanvas.alpha = 1;
        _extraCanvas.blocksRaycasts = true;
    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        _extraCanvas.alpha = 0;
        _extraCanvas.blocksRaycasts = false;
        
    }




}
