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

    [SerializeField] private IngredientInventory _inventory;
    [SerializeField] private Health _player;
    [SerializeField] private Spawner _spawner;



    [SerializeField] Settings _settings;
    [SerializeField] Shop _shop;
    [SerializeField] Wardrobe _wardrobe;

    SceneLoader _sceneLoader;

    private void Awake()
    {
        _inventory = FindObjectOfType<IngredientInventory>();
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

    public void OpenShop(){

        Debug.Log("Shop!");
        _shop.Display();
        //LoadShop
    }


    public void OpenWardrobe(){

        _wardrobe.Display();
    }

    public void OpenSettings(){


        Debug.Log("Settings!");
        _settings.Show();

    }

    public void InitGame(){

        Debug.Log("Playing!");
       _mainMenuCanvas.enabled = false;
       ResetGame();
       ResumeGame();


    }

    public void OpenPauseMenu(){

        _pauseMenuCanvas.enabled = true;
        Time.timeScale = 0.0f;
    }

    public void ResumeGame(){

        Time.timeScale = 1.0f;
        _pauseMenuCanvas.enabled = false;
    }

    
    public void QuitGame(){

        Application.Quit();

    }
    
    public void OpenMainMenu(){
        _mainMenuCanvas.enabled = true;
        Time.timeScale = 0.0f;
    }   


    public void ReturnToMainMenu(){
        Time.timeScale = 1.0f;
        sceneLoader.LoadScene(2);
    }

    public void ResetGame(){
        
        _inventory.ResetInventory();
        _player.Live();
        
        
    }


    public void RestartButton(){
        
        _inventory.ResetInventory();
        _spawner.StopSpawn();
        _player.Live();


    }
    public void DisablePauseButton(){

        _pauseMenuButton.enabled = false;

    }
    public void EnablePauseButton(){
        _pauseMenuButton.enabled = true;


    }

    public void PlayButtonSound(){

        SoundManager.PlaySound(SoundManager.Sound.MENUPOP, 1f);

    }
}
