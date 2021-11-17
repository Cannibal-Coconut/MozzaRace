using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(IngredientInventory))]

public class PlayerMovementInterface : MonoBehaviour
{

  //References to other player scripts which contain important information this script listens to
    private PlayerJump _playerJump;
    private Health _playerHealth;
    private PlayerAttack _playerAttack;

    private int _pizzaStatus;
  
    enum PizzaStatus{

    hasPizza,
    noPizza

    }

    public delegate void OnLaunchPizza();

    public event OnLaunchPizza onLaunchPizza;

    public delegate void OnReceivePizza();

    public event OnLaunchPizza onReceivePizza;


  
    public delegate void Death();

    public event Death onDeath;

    private void Start() {
    
        _playerJump = GetComponent<PlayerJump>();
        _playerHealth = GetComponent<Health>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerHealth.AddDeadListener(IsDead);

    }

//Get grounded state
  public bool GetGrounded(){

      return _playerJump.GetIsGrounded();;

  }


//Get if player is holding loading
  public bool GetLoadingPizzaStatus(){

    //if is loading
    if(_playerAttack.isAttackStarted) {
      return true;
      } else return false; 

  }

//Get launch pizza trigger
  public void LaunchPizzaTrigger(){

    onLaunchPizza();

  }

//get if player has pizza
  public void  SetPizzaStatus(bool hasPizza){

    if(hasPizza) {_pizzaStatus = (int)PizzaStatus.hasPizza; OnReceivePizzaEvent(); }else {_pizzaStatus = (int)PizzaStatus.noPizza; }

  } 

  public int GetPizzaStatus(){

    return _pizzaStatus;

  }


//get double jump trigger 
  public bool HasDoubleJump(){

      return !_playerJump.GetDoubleJump();

  }

public void OnReceivePizzaEvent(){

    onReceivePizza();


}

public void IsDead(){

  onDeath();

}



public bool GetAliveStatus(){
  
    return _playerHealth.GetAlive();

}


  
}
