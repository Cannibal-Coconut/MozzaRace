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
    [SerializeField] Button _coinRestartButton;
    [SerializeField] Button _adContinueButton;

    [Header("Meshes")]
    [SerializeField] TextMeshProUGUI _scoreMesh;
    [SerializeField] TextMeshProUGUI _premiumCoinsMesh;

    [Header("Settings")]
    [SerializeField] [Range(0, 10)] int _premiumCost;

    CanvasGroup _canvasGroup;

    IngredientInventory _inventory;
    Health _player;
    MenuManager _menuManager;
    ProfileInventory _profileInventory;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _inventory = FindObjectOfType<IngredientInventory>();
        _profileInventory = FindObjectOfType<ProfileInventory>();
        _menuManager = FindObjectOfType<MenuManager>();
        _player = FindObjectOfType<Health>();

        InitializeButtons();

        SetListeners();

        enabled = true;
    }

    void InitializeButtons()
    {
        _coinRestartButton.onClick.AddListener(RestartWithCoin);
        _adContinueButton.onClick.AddListener(ContinueWithAd);
    }

    void RestartWithCoin()
    {

        if (_profileInventory)
        {
            if (_profileInventory.premiumCoins >= _premiumCost)
            {
                _profileInventory.RemovePremiumCoins(_premiumCost);
                RestartGame();
            }
        }
    }

    void RestartGame()
    {
        //ResetGame
        _inventory.ResetInventory();
        Hide();
        _player.Live();
    }

    public void OpenMainMenu()
    {

        _menuManager.ReturnToMainMenu();

    }


    void ContinueWithAd()
    {
        _adVideo.PlayRandomVideo(_player.Live);
    }

    public void Display()
    {
        _menuManager.DisablePauseButton();
        _scoreMesh.text = "Points: " + _inventory.points.ToString();
       
        StartCoroutine(DeathAnimationWaiter());
    }

    private IEnumerator DeathAnimationWaiter(){

        yield return new WaitForSeconds(1.5f);
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }    



    public void Hide()
    {
        Time.timeScale = 1.0f;
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
