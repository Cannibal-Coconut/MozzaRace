using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovementInterface))]



public class PlayerAnimationHandler : MonoBehaviour
{
    
enum FSM {

    Running,
    Landing,
    Loading,
    Spinning,
    Jumping,
    Airborne,
    DoubleJumping,
    Dead,


} 
    [SerializeField]private FSM _state;
    public Animator playerAnimator;
    private PlayerMovementInterface _playerInfo;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        _playerInfo = GetComponent<PlayerMovementInterface>();
        _playerInfo.onLaunchPizza += LaunchPizzaEvent;
        _playerInfo.onReceivePizza += ReceivePizzaEvent; //Receive pizza event
        _playerInfo.onDeath += SetDead;
        _state = FSM.Running;
    }

    // Update is called once per frame
    void Update()
    {

        DecideState();
        PlayCurrentAnimation();
        

    }


    private void PlayCurrentAnimation(){

        switch(_state){

            case FSM.Running:
                PlayAnimation("Running", false, 0f);
            break;
            case FSM.Jumping:
                PlayAnimation("Jumping", false, 0.5f);
               if(AnimationIsFinished(CheckPizzaString("Jumping"), _playerInfo.GetPizzaStatus(), 1.1f))  _state = FSM.Airborne;
            break;
            case FSM.Airborne:
                 PlayAnimation("Airborne", false, 0f);
            break;         
            case FSM.Landing:
                PlayAnimation("Landing", false, 0f);
            break;            
            case FSM.DoubleJumping:
                PlayAnimation("DoubleJump", true, 0f);
                if(AnimationIsFinished("DoubleJump", _playerInfo.GetPizzaStatus(), 1f) || _playerInfo.GetGrounded()) _state = FSM.Running;
            break;
            case FSM.Spinning:
                PlayAnimation("Spinning", true, 0f);
               if(AnimationIsFinished("Spinning", _playerInfo.GetPizzaStatus(), 1f)){
                   if(_playerInfo.GetGrounded()) _state = FSM.Running; 
                    else _state = FSM.Airborne;
               }
            break;
            case FSM.Loading:
                PlayAnimation("RunningLoading", true, 0f);
                if(AnimationIsFinished("Spinning", _playerInfo.GetPizzaStatus(), 1f)){
                   if(_playerInfo.GetGrounded()) _state = FSM.Running; 
                    else _state = FSM.Airborne;
               }
                break;
            case FSM.Dead:
                PlayAnimation("Death", true, 0f);
            break;

        }

    }

 
    
    private void DecideState(){
        if(_state != FSM.Dead){
        if(_state == FSM.Airborne && _playerInfo.GetGrounded())  _state = FSM.Landing;

        if(_playerInfo.GetGrounded()) {
            if(_state == FSM.Landing) if(AnimationIsFinished(CheckPizzaString("Landing"), _playerInfo.GetPizzaStatus(), 1f))  _state = FSM.Running;
            if(_playerInfo.GetLoadingPizzaStatus()){ _state = FSM.Loading;}
        }

        if(!_playerInfo.GetGrounded() &&  (_state == FSM.Running || _state == FSM.Landing || _state == FSM.Loading)) _state = FSM.Jumping;
    
        if((_state == FSM.Jumping || _state == FSM.Airborne) && !_playerInfo.HasDoubleJump() && !_playerInfo.GetGrounded() && _state != FSM.Spinning) _state = FSM.DoubleJumping; 
        } else if(_playerInfo.GetAliveStatus()) { SetLive();}

        
    }

    private void PlayAnimation(string animationName, bool overridePizza, float animationStart){
        
        string pizzaAnimationName = animationName + "Pizza";
        string NoPizzaAnimationName = animationName + "NoPizza";

        if(!overridePizza){

            if(!AnimationIsPlaying(pizzaAnimationName,0)) {
                playerAnimator.Play(pizzaAnimationName, 0, animationStart);
            } 
            
            if(!AnimationIsPlaying(NoPizzaAnimationName,1)) {
                playerAnimator.Play(NoPizzaAnimationName, 1, animationStart);
            } 

        } else {
           
            if(!AnimationIsPlaying(animationName,1)) {
                playerAnimator.Play(animationName, 0, animationStart);
                playerAnimator.Play(animationName, 1, animationStart);
            }

        }

    
    }

    string CheckPizzaString(string animationName){

        if(_playerInfo.GetPizzaStatus() == 0) return animationName + "Pizza";
        else return animationName + "NoPizza";

    }

    private void LaunchPizzaEvent(){
        
        playerAnimator.SetLayerWeight(1, 100);
        playerAnimator.SetLayerWeight(0, 0);
       if(_state != FSM.Dead) _state = FSM.Spinning;
    }

   void ReceivePizzaEvent(){

        playerAnimator.SetLayerWeight(0, 100);
        playerAnimator.SetLayerWeight(1, 0);


   }
    private void SetDead(){

        _state = FSM.Dead;

    }
    public void SetLive(){

        _state = FSM.Running;

    }

    bool AnimationIsPlaying(string stateName, int layer){
            return playerAnimator.GetCurrentAnimatorStateInfo(layer).IsName(stateName);
     }

    bool AnimationIsFinished(string stateName, int layer, float normalizedMaxTime){

       return  playerAnimator.GetCurrentAnimatorStateInfo(layer).IsName(stateName) && playerAnimator.GetCurrentAnimatorStateInfo(layer).normalizedTime >= normalizedMaxTime;

    }

}
