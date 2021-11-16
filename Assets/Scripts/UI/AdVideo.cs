using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(CanvasGroup))]
public class AdVideo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] VideoPlayer _videoPlayer;
    [SerializeField] Button _skipVideoButton;

    [Header("Settings")]
    [SerializeField] [Range(0, 20)] float _timeForSkipButton;

    [SerializeField] VideoClip[] _videoClips;

    CanvasGroup _canvasGroup;

    Action _afterVideoAction;

    Coroutine _countdownCoroutine;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        _skipVideoButton.onClick.AddListener(() =>
        {
            StopVideo();
        });

        Hide();
    }

    public void PlayRandomVideo(Action afterVideoAction)
    {

        var clip = _videoClips[UnityEngine.Random.Range(0, _videoClips.Length)];

        PlayVideo(clip, afterVideoAction);
    }

    public void PlayVideo(VideoClip clip, Action afterVideoAction)
    {
        Show();

        _videoPlayer.clip = clip;
        _afterVideoAction = afterVideoAction;

        _videoPlayer.frame = 0;
        _videoPlayer.Play();

        if (_countdownCoroutine != null)
        {
            StopCoroutine(_countdownCoroutine);
        }

        _countdownCoroutine = this.StartCoroutine(VideoCountdown());

        _countdownCoroutine.ToString();

    }

    IEnumerator VideoCountdown()
    {
        /*
        It doesnt work and I dont fucnking know why :D. It just sticks to new WaitForSeconds

        _skipVideoButton.interactable = false;
        //yield return new WaitForSeconds(_timeForSkipButton);
        _skipVideoButton.interactable = true;

        yield return new WaitForSeconds((float)(_videoPlayer.clip.length - _timeForSkipButton));
        
        StopVideo();
        */

        yield return null;
    }

    void StopVideo()
    {
        _videoPlayer.Stop();

        if (_afterVideoAction != null)
        {
            _afterVideoAction.Invoke();

            _afterVideoAction = null;
        }

        Hide();
    }


    public void Hide()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

    }

}
