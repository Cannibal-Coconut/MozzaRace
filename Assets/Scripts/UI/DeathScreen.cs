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
        _menuManager.ResumeGame();
    }
     void RestartGame()
    {
        //ResetGame
        _inventory.ResetInventory();
        Hide();
        _player.Live();
    }

    public void OpenMainMenu(){

        _menuManager.ReturnToMainMenu();

    }


    void ContinueWithAd()
    {
        _player.Live();
        _menuManager.ResumeGame();
    }

    public void Display()
    {
        StartCoroutine(DeathAnimationWaiter());
    }

    private IEnumerator DeathAnimationWaiter(){

        yield return new WaitForSeconds(1.5f);
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        _menuManager.DisablePauseButton();
        _scoreMesh.text = "Points: " + _inventory.points.ToString();
        Time.timeScale = 0.0f;


    }    

    public void Hide()
    {
        _menuManager.EnablePauseButton();
        _canvasGroup.alpha = 0f;
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
