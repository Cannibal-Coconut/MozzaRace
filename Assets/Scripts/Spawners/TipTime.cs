using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TipTime : MonoBehaviour
{
    private const float TIP_TIME_DURATION = 10;
    private const float TIP_TIME_BANNER_DURATION = 3;
    private int _tipTimePointThreshhold;
    private CanvasGroup _TipTimeCanvas;
    private ProfileInventory _inventoryPoints;
    private Spawner _presetSpawner;
    private int _currentPoints;
    private int _offsetPoints;
    private bool _tipTimeMode;


    Health _player;

    [SerializeField] private Spawnable _tipJarSpawnable;


    [SerializeField] private Spawnable[] _coinPresetSpawnables;

    private void Start()
    {
        _inventoryPoints = FindObjectOfType<ProfileInventory>();
        _player = FindObjectOfType<Health>();
        _presetSpawner = FindObjectOfType<Spawner>();
        _TipTimeCanvas = GetComponent<CanvasGroup>();


        _player.AddDeadListener(OnDeath);

        _inventoryPoints.onUpdateCurrentPoints += UpdateCurrentPoints;
        _offsetPoints = 0;
        _tipTimePointThreshhold = 1000;

        _TipTimeCanvas.alpha = 0;
    }

    private void UpdateCurrentPoints()
    {

        _currentPoints = _inventoryPoints.matchPoints;

        if ((_currentPoints - _offsetPoints) >= _tipTimePointThreshhold)
        {

            _offsetPoints = _currentPoints;
            TriggerTipTimeSpawnPreset();
            Debug.Log("CurrentPoints = " + _currentPoints + " offsetPoints = " + _offsetPoints);
            if (_offsetPoints >= (_tipTimePointThreshhold * 2))
            {

                _tipTimePointThreshhold = _tipTimePointThreshhold * 4;

            }

        }

    }

    private void TriggerTipTimeSpawnPreset()
    {
        //Spawn Jar
        _presetSpawner.Spawn(_tipJarSpawnable);

    }

    private void OnDeath()
    {

        _TipTimeCanvas.alpha = 0;
        _tipTimeMode = false;

    }

    public void TriggerTipTime()
    {

        _tipTimeMode = true;
        _TipTimeCanvas.alpha = 1;
        _presetSpawner.StopSpawn();
        _presetSpawner.StartSpawn();
        StartCoroutine(TipTimeBannerShowtime());
        //Spawn Tips
        _presetSpawner.Spawn(_coinPresetSpawnables[Random.Range(0, _coinPresetSpawnables.Length)]);
    }

    private IEnumerator TipTimeBannerShowtime()
    {

        yield return new WaitForSeconds(TIP_TIME_BANNER_DURATION);
        _TipTimeCanvas.alpha = 0;
        yield return new WaitForSeconds(TIP_TIME_DURATION - TIP_TIME_BANNER_DURATION);
        _tipTimeMode = false;

    }

    public float GetTipTime()
    {
        return TIP_TIME_DURATION;
    }

    public bool GetTipTimeModeStatus()
    {
        return _tipTimeMode;
    }

}
