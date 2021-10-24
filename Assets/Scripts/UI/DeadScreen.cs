using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class DeadScreen : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] Button _coinRestartButton;
    [SerializeField] Button _adRestartButton;

    [Header("Meshes")]
    [SerializeField] TextMeshProUGUI _scoreMesh;
    [SerializeField] TextMeshProUGUI _premiumCoinsMesh;

    CanvasGroup _canvasGroup;

    IngredientInventory _inventory;
    Health _player;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _inventory = FindObjectOfType<IngredientInventory>();

        InitializeButtons();

        _player = FindObjectOfType<Health>();
        if (_player)
        {
            _player.onDead += Display;
            _player.onLive += Hide;
        }
    }

    void InitializeButtons()
    {
        _coinRestartButton.onClick.AddListener(RestartWithCoin);
        _adRestartButton.onClick.AddListener(RestartWithAd);
    }

    void RestartWithCoin()
    {
        _player.Live();
    }

    void RestartWithAd()
    {
        _player.Live();
    }

    public void Display()
    {
        _canvasGroup.alpha = 1;

        _scoreMesh.text = "Points: " + _inventory.points.ToString();

    }

    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }



}
