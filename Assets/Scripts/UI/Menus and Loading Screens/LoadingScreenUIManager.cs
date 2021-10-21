using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingScreenUIManager : MonoBehaviour
{
    
    private AsyncOperation loadingAsyncOp;
    private SceneLoader mySceneLoader;
    public Slider progressBar;
    public TextMeshProUGUI loadingText;
    private void Start()
    {
        //find sceneloader and start fake loading time to prevent screen from flashing too quickly in pc 
        mySceneLoader = FindObjectOfType<SceneLoader>();
        StartCoroutine(FakeLoadingTimeWait());
    }

    private void Update()
    {
        //Loading progress bar + % text
        float  progressValue = Mathf.Clamp01(loadingAsyncOp.progress/0.9f);
        progressBar.value = progressValue;
        loadingText.text = Mathf.Round(progressValue * 100) + "%";
    }

    private IEnumerator FakeLoadingTimeWait(){

        //Load prev asyng op to prevent nullpointer
        loadingAsyncOp = mySceneLoader.GetAsyncLoadOp();
        yield return new WaitForSeconds(0.4f);
        //Load next scene
        StartCoroutine(mySceneLoader.LoadSceneAsync(mySceneLoader.GetSavedNextSceneIndex()));
        loadingAsyncOp = mySceneLoader.GetAsyncLoadOp();

   
    }
}
