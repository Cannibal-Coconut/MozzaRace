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

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
        _inventory = FindObjectOfType<IngredientInventory>();
        _player= FindObjectOfType<Health>();
        _pauseMenuCanvas.enabled = false;
        OpenMainMenu();

    }

    public void OpenShop(){

        Debug.Log("Shop!");
        //LoadShop
    }

    public void OpenWardrobe(){

        
        Debug.Log("Wardrobe!");
        //LoadWardrobe
    }

    public void OpenSettings(){


        Debug.Log("Settings!");
        //OpenSettings

    }

    public void InitGame(){

        Debug.Log("Playing!");
       Time.timeScale =1.0f;
       _mainMenuCanvas.enabled = false;
       ResetGame();
       ResumeGame();


    }

    public void OpenPauseMenu(){
        
        Time.timeScale = 0.0f;
        _pauseMenuCanvas.enabled = true;

    }

    public void ResumeGame(){

        _pauseMenuCanvas.enabled = false;
        Time.timeScale = 1.0f;
    }

    
    public void QuitGame(){

        Application.Quit();

    }
    
    public void OpenMainMenu(){
        
        ResetGame();
        _mainMenuCanvas.enabled = true;
        
        Time.timeScale = 0.0f;
    }   

    public void ResetGame(){

        _player.HurtPlayer(10000);
        _inventory.ResetInventory();
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
