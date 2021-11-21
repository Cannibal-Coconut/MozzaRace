using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CanvasGroup))]
public class MissionNotification : MonoBehaviour
{
    [Header("References")]
    [SerializeField] MissionHolder _holder;
    [SerializeField] Transform _hiddenPosition;
    [SerializeField] Transform _displayedPosition;

    [Header("Settings")]
    [SerializeField] [Range(1, 5)] float _moveTime;
    [SerializeField] [Range(1, 10)] float _idleTime;


    CanvasGroup _canvasGroup;

    Coroutine _notifyCoroutine;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = false;

        MissionWatcher watcher = FindObjectOfType<MissionWatcher>();

        watcher.onEndedMissionCallback += NotifyCompletedMission;
    }

    void NotifyCompletedMission(Mission mission)
    {
        _holder.SetMission(mission);

        if (_notifyCoroutine != null)
        {
            StopCoroutine(_notifyCoroutine);
        }

        _notifyCoroutine = StartCoroutine(Notify());
    }

    IEnumerator MoveToPosition(Vector3 destination, float duration)
    {
        Vector3 startingPosition = transform.position;

        float elapsedTime = 0;

        while (duration > elapsedTime)
        {
            elapsedTime += Time.deltaTime;

            transform.position = Vector3.Lerp(startingPosition, destination, elapsedTime / duration);

            yield return null;
        }



    }

    IEnumerator Notify()
    {
        _canvasGroup.alpha = 1;

        yield return MoveToPosition(_displayedPosition.position, _moveTime);

        yield return new WaitForSeconds(_idleTime);

        //yield return MoveToPosition(_hiddenPosition.position, _moveTime);

        _canvasGroup.alpha = 0;

    }


}
