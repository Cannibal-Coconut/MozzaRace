using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovementInterface))]
public class PlayerAnimationHandler : MonoBehaviour
{
    Animator playerAnimator;
    PlayerMovementInterface playerInfo;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerInfo = GetComponent<PlayerMovementInterface>();
        playerInfo.onLaunchPizza += PlayLaunchAnimation;
    }

    // Update is called once per frame
    void Update()
    {
         HandleAnimationParameters();
    }

    private void HandleAnimationParameters(){

      playerAnimator.SetBool("isGrounded", playerInfo.GetGrounded()); 
      playerAnimator.SetBool("isLoadingPizza", playerInfo.GetLoadingPizzaStatus()); 
      

     
      playerAnimator.SetLayerWeight(playerInfo.HasPizzaStatus(), 100);
 

    }

    private void PlayLaunchAnimation(){

        playerAnimator.ResetTrigger("launchPizza");
        playerAnimator.SetTrigger("launchPizza");


    }
}
