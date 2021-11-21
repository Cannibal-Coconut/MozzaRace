using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DeathScreen : MonoBehaviour, ILiveListener
{
    [Header("References")]
    [SerializeField] AdVideo _adVideo;

    [Header("Buttons")]
    [SerializeField] Button _coinContinueButton;
    [SerializeField] Button _adContinueButton;
    [SerializeField] Button _restartButton;

    [Header("Meshes")]
    [SerializeField] TextMeshProUGUI _matchScoreMesh;
    [SerializeField] TextMeshProUGUI _pointsMesh;
    [SerializeField] TextMeshProUGUI _premiumCostMesh;

    CanvasGroup _canvasGroup;

    IngredientInventory _inventory;
    Health _player;
    MenuManager _menuManager;
    ProfileInventory _profileInventory;

    LanguageContext _languageContext;

    DifficultyLevel _currentLevel;
    private ChangePizza _minigameMonitor;

    private bool _hasNotDied;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _inventory = FindObjectOfType<IngredientInventory>();
        _profileInventory = FindObjectOfType<ProfileInventory>();
        _languageContext = FindObjectOfType<LanguageContext>();
        _menuManager = FindObjectOfType<MenuManager>();
        _minigameMonitor= FindObjectOfType<ChangePizza>();
        _player = FindObjectOfType<Health>();

        InitializeButtons();

        SetListeners();
        _hasNotDied = true;
        enabled = true;
    }

    void InitializeButtons()
    {
        _coinContinueButton.onClick.AddListener(ContinueWithPoints);
        _adContinueButton.onClick.AddListener(ContinueWithAd);
        _restartButton.onClick.AddListener(RestartGame);

    }

    void ContinueWithPoints()
    {

        if (_profileInventory)
        {
            if (_profileInventory.matchPoints >= _currentLevel.reviveCost)
            {
                _profileInventory.RemoveMatchPoints(_currentLevel.reviveCost);
                _player.Live();
            }
        }
    }

    void RestartGame()
    {
        //ResetGame
        SetHasNotDied(true);
        _inventory.ResetInventory();
        _profileInventory.PassMatchPointsToSkinPoints();
        Hide();
        _player.Live();
    }

    public void OpenMainMenu()
    {
        _profileInventory.PassMatchPointsToSkinPoints();
        _menuManager.ReturnToMainMenu();
    }

    void ContinueWithAd()
    {
        _adVideo.PlayRandomVideo(_player.Live);
    }

    public void Display()
    {
        _menuManager.DisablePauseButton();

        string pointsText = "Points: ";
        string totalText = "Total: ";
        switch (_languageContext.currentLanguage)
        {
            case Language.English:
                pointsText = "Points: ";
                totalText = "Total: ";
                break;
            case Language.Spanish:
                pointsText = "Puntos: ";
                totalText = "Total: ";
                break;
            default:
                break;
        }
        _matchScoreMesh.text = pointsText + _profileInventory.matchPoints.ToString();
        _pointsMesh.text = totalText + (_profileInventory.points + _profileInventory.matchPoints).ToString();
        _premiumCostMesh.text = _currentLevel.reviveCost.ToString();

        StartCoroutine(DeathAnimationWaiter());
    }

    public void SetDifficultyLevel(DifficultyLevel level)
    {
        _currentLevel = level;
    }

    public void SetHasNotDied(bool b) {

        _hasNotDied = b;

    }
    private IEnumerator DeathAnimationWaiter()
    {

        yield return new WaitForSeconds(1.5f);
        //Init Pizza Time
        if(_inventory.finishedOrders > 1 && _hasNotDied) _minigameMonitor.enabled = true;
        else DeathPostMinigame();
        
    }

    public void DeathPostMinigame(){

        _canvasGroup.alpha = 1f;
        Time.timeScale = 1f;
        _canvasGroup.blocksRaycasts = true;

    }
    public void Hide()
    {
        // Time.timeScale = 1.0f;
        _menuManager.EnablePauseButton();
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnLive()
    {

        Hide();

    }

    public void OnDead()
    {
        Display();
    }

    public void SetListeners()
    {
        var player = FindObjectOfType<Health>();
        if (player)
        {
            player.AddLiveListener(OnLive);
            player.AddDeadListener(OnDead);
        }


    }

}
