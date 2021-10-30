using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DeathScreen : MonoBehaviour, ILiveListener
{
    [Header("Buttons")]
    [SerializeField] Button _coinRestartButton;
    [SerializeField] Button _adContinueButton;

    [Header("Meshes")]
    [SerializeField] TextMeshProUGUI _scoreMesh;
    [SerializeField] TextMeshProUGUI _premiumCoinsMesh;

    CanvasGroup _canvasGroup;

    IngredientInventory _inventory;
    Health _player;
    private MenuManager _menuManager;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _inventory = FindObjectOfType<IngredientInventory>();
        _menuManager = FindObjectOfType<MenuManager>();
        _player = FindObjectOfType<Health>();

        InitializeButtons();

        SetListeners();
    }

    void InitializeButtons()
    {
        _coinRestartButton.onClick.AddListener(RestartWithCoin);
        _adContinueButton.onClick.AddListener(ContinueWithAd);
    }

    void RestartWithCoin() {

        //coin --;
        RestartGame();

    }
     void RestartGame()
    {
        //ResetGame
        _inventory.ResetInventory();
        Hide();
        _player.Live();
    }

    public void OpenMainMenu(){

        RestartGame();
        _menuManager.OpenMainMenu();

    }


    void ContinueWithAd()
    {
        _player.Live();
    }

    public void Display()
    {
        _canvasGroup.gameObject.SetActive(true);

        _scoreMesh.text = "Points: " + _inventory.points.ToString();
        Time.timeScale = 0.0f;

    }

    public void Hide()
    {
        Time.timeScale = 1.0f;
        _canvasGroup.gameObject.SetActive(false);
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
