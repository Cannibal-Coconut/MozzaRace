using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    
    SceneLoader sceneLoader;

    [SerializeField] private Canvas _mainMenuCanvas;
    [SerializeField] private Canvas _pauseMenuCanvas;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();
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

        _mainMenuCanvas.enabled = true;
        Time.timeScale = 0.0f;
    }
}
