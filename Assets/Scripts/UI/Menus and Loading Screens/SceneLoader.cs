using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    //private async load operation
    private AsyncOperation sceneLoadAsyncOperation;
    //SceneIndex saved to load after loading screen
    private int savedNextSceneIndex;
    //const loading screen index to call when loading screen
    private const int LOADING_SCREEN_INDEX = 1; 

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    #region TESTING

    private void Start() {
        StartCoroutine(StartingAnimation());
    }

    private IEnumerator StartingAnimation(){

        yield return new WaitForSeconds(1.5f);
        LoadScene(2);

    }



    #endregion




    //public async load operation getter
    public AsyncOperation GetAsyncLoadOp(){

        return sceneLoadAsyncOperation;

    }

     //public next loadable scene index getter
    public int  GetSavedNextSceneIndex(){

        return savedNextSceneIndex;

    }



    //Public function to load scene from index 
    public void LoadScene(int nextSceneIndex){
        
        //load asynchronously from an index using coroutines
        savedNextSceneIndex = nextSceneIndex;
        StartCoroutine(LoadSceneAsync(LOADING_SCREEN_INDEX));

    }


    public void LoadNextScene(){

        LoadScene(SceneManager.GetActiveScene().buildIndex +1);

    }

    public void LoadPreviousScene(){

        LoadScene(SceneManager.GetActiveScene().buildIndex -1);

    }


    //Coroutine
    public IEnumerator LoadSceneAsync(int asyncSceneIndex){

        //save load operation for progress bars/data
        sceneLoadAsyncOperation = SceneManager.LoadSceneAsync(asyncSceneIndex);
        
        //wait until loading is done
        while(!sceneLoadAsyncOperation.isDone){
        yield return null;
        }
    }

}


    
