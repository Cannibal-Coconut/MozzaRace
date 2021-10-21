using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    
    SceneLoader mySceneLoader;

    private void Start() {
        mySceneLoader = FindObjectOfType<SceneLoader>();

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
        mySceneLoader.LoadNextScene();

    }


    
    
}
