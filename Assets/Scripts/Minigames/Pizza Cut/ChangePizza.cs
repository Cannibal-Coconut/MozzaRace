using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PizzaCutManager))]

public class ChangePizza : MonoBehaviour
{

    private float _pizzaCuttingTotalRemainingTime { get; set; }
    public int remainingPizzas { get; set; }
    private int _goodPizzas { get; set; }
    private int _finishedOrders;
    private float _finishedOrderReviveThreshhold;

    private IngredientInventory _orderInventory;
    private PizzaTimeCanvas _pizzaCanvas;
    private PlayerManager _player;
    private Health _playerHealth;

    private GameObject _currentPizza;

    [SerializeField] private GameObject pizza4;
    [SerializeField] private GameObject pizza6;
    [SerializeField] private GameObject pizza8;

    [SerializeField] private GameObject correct;
    [SerializeField] private GameObject failed;

    public bool minigameState;

    private System.Random _random;

    private PizzaCutManager _pizzaCutManager;

    private DeathScreen _death;

    public Action onGoodPizzaCut;

    SoundSettingManager sound;
    private void Awake()
    {
        sound = FindObjectOfType<SoundSettingManager>();
        _pizzaCutManager = GetComponent<PizzaCutManager>();
        _orderInventory = FindObjectOfType<IngredientInventory>();
        _player = FindObjectOfType<PlayerManager>();
        _pizzaCanvas = FindObjectOfType<PizzaTimeCanvas>();
        _playerHealth = FindObjectOfType<Health>();
        _death = FindObjectOfType<DeathScreen>();

        correct.SetActive(false);
        failed.SetActive(false);
        _random = new System.Random();

        minigameState = false;
        this.enabled = false;
        _pizzaCutManager.enabled = false;
    }

    private void InstantiatePizza()
    {
        if (minigameState)
        {
            switch (_random.Next(1, 4))
            {
                case 1:
                    _currentPizza = Instantiate(pizza4, Vector3.zero, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0.0f, 360.0f)));
                    break;
                case 2:
                    _currentPizza = Instantiate(pizza6, Vector3.zero, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0.0f, 360.0f)));
                    break;
                case 3:
                    _currentPizza = Instantiate(pizza8, Vector3.zero, Quaternion.Euler(0, 0, UnityEngine.Random.Range(0.0f, 360.0f)));
                    break;
            }
        }
    }

    public void ChangeVisiblePizza(PizzaCutCheck visiblePizza, bool result)
    {
        Destroy(visiblePizza.gameObject);
        remainingPizzas--;
        if (result)
        {
            _goodPizzas++;

            if (onGoodPizzaCut != null)
            {
                onGoodPizzaCut.Invoke();
            }
        }
        Debug.Log("You have to cut " + remainingPizzas + " remaining pizzas");
        StartCoroutine(ShowResult(result));
    }

    public IEnumerator PizzaCutMinigameTimer()
    {

        _pizzaCanvas.SetPizzaTimer(_pizzaCuttingTotalRemainingTime);
        yield return new WaitForSeconds(_pizzaCuttingTotalRemainingTime);
        if (minigameState)
        {
            LoseMinigame();
            Debug.Log("You just time lost");

        }


    }

    private void Update()
    {


        if (_goodPizzas >= (int)_finishedOrderReviveThreshhold && _finishedOrders > 1)
        {

            //RevivePlayer
            WinMinigame();


        }

        if (remainingPizzas < 0)
        {

            LoseMinigame();
            Debug.Log("You just ran out of pizzas!");
        }

    }

    public void WinMinigame()
    {

        _playerHealth.Live();
        this.enabled = false;


    }

    public void LoseMinigame()
    {

        _death.DeathPostMinigame();
        this.enabled = false;


    }

    private IEnumerator ShowResult(bool result)
    {
        if (result){
            correct.SetActive(true);
            sound.PlayPizzaTimeCorrect();
            }
        else {
            failed.SetActive(true);
            sound.PlayPizzaTimeError();
        }
        yield return new WaitForSeconds(0.5f);

        correct.SetActive(false);
        failed.SetActive(false);

        InstantiatePizza();
    }

    private void OnEnable()
    {

        sound.PlayPizzaTime();
        _pizzaCutManager.enabled = true;
        _player.enabled = false;
        InitMinigame();
    }

    private void InitMinigame()
    {
        _death.SetHasNotDied(false);
        minigameState = true;
        InstantiatePizza();
        if (_orderInventory.finishedOrders <= 50)
        {
            _finishedOrders = _orderInventory.finishedOrders;
        }
        else _finishedOrders = 50;
        CalculateTotalPizzaTime();
        _pizzaCanvas.Display();
        StartCoroutine(PizzaCutMinigameTimer());
        remainingPizzas = _finishedOrders;
        _finishedOrderReviveThreshhold = _finishedOrders * 0.5f;
        Debug.Log("Cut " + _finishedOrderReviveThreshhold + " to Revive!");
        Debug.Log("You have completed " + _finishedOrders + " orders!");

    }

    private void CalculateTotalPizzaTime()
    {

        if (_finishedOrders >= 50)
        {

            _pizzaCuttingTotalRemainingTime = 30;

        }
        else _pizzaCuttingTotalRemainingTime = (int)20 * Mathf.Log10(_finishedOrders);

        Debug.Log("Total pizza time : " + _pizzaCuttingTotalRemainingTime);
    }

    private void ResetMinigame()
    {

        Destroy(_currentPizza);
        minigameState = false;
        remainingPizzas = 0;
        _pizzaCanvas.Hide();
    }

    private void OnDisable()
    {
        sound.PlayMainTheme();
        ResetMinigame();
        _player.enabled = true;
        _pizzaCutManager.enabled = false;
    }

    public float GetPizzaCuttingTotalRemainingTime()
    {

        return _pizzaCuttingTotalRemainingTime;

    }
     public float GetFinishedOrders(){

        return _finishedOrders;

    }
}