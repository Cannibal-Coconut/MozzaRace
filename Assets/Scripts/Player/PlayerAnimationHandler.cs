using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovementInterface))]
public class PlayerAnimationHandler : MonoBehaviour
{
    private Animator _playerAnimator;
    private PlayerMovementInterface _playerInfo;
    // Start is called before the first frame update
    void Start()
    {
        _playerAnimator = GetComponent<Animator>();
        _playerInfo = GetComponent<PlayerMovementInterface>();
        _playerInfo.onLaunchPizza += PlayLaunchAnimation;
        _playerInfo.onReceivePizza += ResetLaunchTrigger;
    }

    // Update is called once per frame
    void Update()
    {
         HandleAnimationParameters();
    }
    
    private void HandleAnimationParameters(){

      _playerAnimator.SetBool("isGrounded", _playerInfo.GetGrounded()); 
      _playerAnimator.SetBool("isLoadingPizza", _playerInfo.GetLoadingPizzaStatus()); 

      if(_playerInfo.GetPizzaStatus() == 1){
        _playerAnimator.SetBool("hasPizza",false );
        _playerAnimator.SetLayerWeight(_playerInfo.GetPizzaStatus(), 100);
        _playerAnimator.SetLayerWeight(0, 0);
 
      }
        else {
            _playerAnimator.SetBool("hasPizza",true );
            _playerAnimator.SetLayerWeight(_playerInfo.GetPizzaStatus(), 100);
            _playerAnimator.SetLayerWeight(1, 0);
        } 
     
    }

    private void PlayLaunchAnimation(){

        _playerAnimator.SetTrigger("launchPizza");
        

    }

    public void ResetLaunchTrigger(){

        _playerAnimator.ResetTrigger("launchPizza");

    }

   
}
