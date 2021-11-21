using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [RequireComponent(typeof(PizzaCutManager))] 

public class ChangePizza : MonoBehaviour
{      

    private float _pizzaCuttingTotalRemainingTime {get;set;}
    private float _pizzaCuttingTimer {get;set;}
    private int _remainingPizzas {get;set;}
    private int _cutPizzas {get;set;}
    private int _finishedOrders;
    private float _finishedOrderReviveThreshhold;

    private IngredientInventory _orderInventory;
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

    private void Awake()
    {
        _pizzaCutManager = GetComponent<PizzaCutManager>();
        _orderInventory = FindObjectOfType<IngredientInventory>();
        _player = FindObjectOfType<PlayerManager>();
        _playerHealth = FindObjectOfType<Health>();
        _death = FindObjectOfType<DeathScreen>();

        correct.SetActive(false);
        failed.SetActive(false);
        _random = new System.Random();
        InstantiatePizza();
        StartCoroutine(PizzaCutMinigame());
    }

    private void InstantiatePizza()
    {
        switch (_random.Next(1, 4))
        {
            case 1:
                _currentPizza = Instantiate(pizza4, Vector3.zero, Quaternion.Euler(0,0,UnityEngine.Random.Range(0.0f, 360.0f)));
                break;
            case 2:
                _currentPizza =Instantiate(pizza6, Vector3.zero, Quaternion.Euler(0,0,UnityEngine.Random.Range(0.0f, 360.0f)));
                break;
            case 3:
               _currentPizza = Instantiate(pizza8, Vector3.zero, Quaternion.Euler(0,0,UnityEngine.Random.Range(0.0f, 360.0f)));
                break;
        }
    }

    public void ChangeVisiblePizza(PizzaCutCheck visiblePizza, bool result)
    {
        Destroy(visiblePizza.gameObject);
        _remainingPizzas--;

        StartCoroutine(ShowResult(result));
    }       

    public IEnumerator PizzaCutMinigame(){
        

        yield return new WaitForSeconds(_pizzaCuttingTotalRemainingTime);
        this.enabled = false;

    }

    private void UpdateMinigame() {

        while(minigameState){
            if(_cutPizzas >=  (int) _finishedOrderReviveThreshhold && _finishedOrders > 1) {

                //RevivePlayer
                _playerHealth.Live();
                this.enabled = false;
            }
       
        } 

        if(_cutPizzas >=  (int) _finishedOrderReviveThreshhold) return;
        _death.DeathPostMinigame();
        this.enabled = false;

    }

    private IEnumerator ShowResult(bool result)
    {
        if (result)
            correct.SetActive(true);
        else
            failed.SetActive(true);

        yield return new WaitForSeconds(1);
        
        correct.SetActive(false);
        failed.SetActive(false);
        
        InstantiatePizza();
    }

    private void OnEnable() {

        _pizzaCutManager.enabled = true;
        _player.enabled = false;
        InitMinigame();
    }

    private void InitMinigame(){

        _finishedOrders = _orderInventory.finishedOrders;
        _remainingPizzas = _finishedOrders;
        _finishedOrderReviveThreshhold = _finishedOrders * 0.8f;
        minigameState = true;
        Debug.Log("Cut " + _finishedOrderReviveThreshhold + " to Revive!");
        UpdateMinigame();

    }

    private void CalculateTotalPizzaTime(){

        _pizzaCuttingTotalRemainingTime = 5f;

    }

    private void ResetMinigame(){

        minigameState = false;
        _pizzaCuttingTimer = _pizzaCuttingTotalRemainingTime;
        _cutPizzas = 0;
        _remainingPizzas = 0;
        Destroy(_currentPizza);

    }   

    private void OnDisable() {
        ResetMinigame();
        _player.enabled = true;
        _pizzaCutManager.enabled = false;
    }
}