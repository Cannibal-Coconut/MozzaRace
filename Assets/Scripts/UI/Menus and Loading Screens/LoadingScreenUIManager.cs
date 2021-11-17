using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class LoadingScreenUIManager : MonoBehaviour
{
    
    private AsyncOperation loadingAsyncOp;
    private SceneLoader sceneLoader;
    public Slider progressBar;
    public TextMeshProUGUI loadingText;
    private void Start()
    {
        //find sceneloader and start fake loading time to prevent screen from flashing too quickly in pc 
        sceneLoader = FindObjectOfType<SceneLoader>();
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
        loadingAsyncOp = sceneLoader.GetAsyncLoadOp();
        yield return new WaitForSeconds(2f);
        //Load next scene
        StartCoroutine(sceneLoader.LoadSceneAsync(sceneLoader.GetSavedNextSceneIndex()));
        loadingAsyncOp = sceneLoader.GetAsyncLoadOp();

   
    }
}
