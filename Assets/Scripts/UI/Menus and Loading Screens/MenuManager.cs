using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    SceneLoader sceneLoader;

    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _pauseMenuCanvas;
    [SerializeField] private Button _pauseMenuButton;
    [SerializeField] private Button _wardrobeButton;
    [SerializeField] private GameStateManager _game;


    [SerializeField] private IngredientInventory _inventory;
    [SerializeField] private Health _player;
    [SerializeField] private Spawner _spawner;


    [SerializeField] private Settings _settings;
    [SerializeField] private Shop _shop;
    [SerializeField] private Wardrobe _wardrobe;

    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PizzaCutManager _pizzaCutManager;

    SceneLoader _sceneLoader;

    private void Awake()
    {
        _game = FindObjectOfType<GameStateManager>();
        _player = FindObjectOfType<Health>();
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _spawner = FindObjectOfType<Spawner>();
    }

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        _inventory = FindObjectOfType<IngredientInventory>();
        _player = FindObjectOfType<Health>();

        _pauseMenuCanvas.enabled = false;
        OpenMainMenu();
    }

    public void OpenShop()
    {
        _shop.Display();
    }


    public void OpenWardrobe()
    {
        _wardrobe.Display();
        _wardrobeButton.gameObject.SetActive(false);
    }

    public void OpenSettings()
    {
        _settings.Show();
    }

    public void InitGame()
    {
        _playerManager.enabled = true;
        _mainMenuCanvas.enabled = false;
        _game.SetGamePos();
        ResetGame();
        ResumeGame();
    }

    public void OpenPauseMenu()
    {
        _playerManager.enabled = false;
        _pauseMenuCanvas.enabled = true;
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        _playerManager.enabled = true;
        Time.timeScale = 1.0f;
        _pauseMenuCanvas.enabled = false;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenMainMenu()
    {
        _playerManager.enabled = true;
        _mainMenuCanvas.enabled = true;
        Time.timeScale = 0.0f;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        sceneLoader.LoadScene(3);
    }

    public void ResetGame()
    {
        _playerManager.enabled = true;
        _inventory.ResetInventory();
        _player.Live();
    }


    public void RestartButton()
    {
        _playerManager.enabled = true;
        _inventory.ResetInventory();
        _spawner.StopSpawn();
        _player.Live();
    }

    public void DisablePauseButton()
    {
        _pauseMenuButton.enabled = false;
    }

    public void EnablePauseButton()
    {
        _pauseMenuButton.enabled = true;
    }

    public void EnableWardrobeButton()
    {
        _wardrobeButton.gameObject.SetActive(true);
    }

    public void PlayButtonSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.MENUPOP, 1f);
    }
}