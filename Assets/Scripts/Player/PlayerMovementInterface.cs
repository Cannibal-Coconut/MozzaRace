using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerAttack))]
[RequireComponent(typeof(Inventory))]
public class PlayerMovementInterface : MonoBehaviour
{

    private PlayerJump playerJump;
    private Health playerHealth;
    private PlayerAttack playerAttack;
    private Inventory playerInventory;

    public delegate void OnLaunchPizza();

    public event OnLaunchPizza onLaunchPizza;


    private void Start() {
        
        playerJump = GetComponent<PlayerJump>();
        playerHealth = GetComponent<Health>();
        playerAttack = GetComponent<PlayerAttack>();
        playerInventory = GetComponent<Inventory>();
    }

  public bool GetGrounded(){

      return playerJump.GetIsGrounded();;

  }


  public bool GetLoadingPizzaStatus(){

    //  return _isLoadingPizza;
    return false;
  }

  public bool GetLaunchingPizzaStatus(){

      //return _isLaunchingPizza;
    return false;
  }

  public int HasPizzaStatus(){
      //if has pizza
      if(false) return 0; else return 1;

  } 

  public bool HasDoubleJump(){

      return playerJump.GetDoubleJump();

  }


  
}
