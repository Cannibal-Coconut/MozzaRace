using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    
    SceneLoader sceneLoader;

    private void Start() {
        sceneLoader = FindObjectOfType<SceneLoader>();

        Debug.Log(Application.platform.GetType());
    }

    public void OpenShop(){

        Debug.Log("Shop!");

    }

    public void OpenWardrobe(){

        
        Debug.Log("Wardrobe!");

    }

    public void OpenSettings(){


        Debug.Log("Settings!");

    }

    public void InitGame(){

        Debug.Log("Playing!");
        //TNESTING
        sceneLoader.LoadNextScene();

    }


    
    
}
