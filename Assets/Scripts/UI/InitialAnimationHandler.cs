using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialAnimationHandler : MonoBehaviour
{

private SceneLoader _sceneLoader;

private void Awake() {
    _sceneLoader = FindObjectOfType<SceneLoader>();
}

private void Start() {
    _sceneLoader.StartingAnim();
}

}
