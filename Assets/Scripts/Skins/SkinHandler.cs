using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SkinHandler : MonoBehaviour
{
   public Animator playerAnimator;
  public enum Skin{


    DEFAULT,
    BLUE,
    CYAN,
    GREEN,
    MAGENTA,
    ORANGE,
    PURPLE,
    RED,
    YELLOW,
  }
public enum Animations{

  AIRBORNEPIZZA,
  AIRBORNENOPIZZA,
  JUMPINGPIZZA,
  JUMPINGNOPIZZA,
  LANDINGPIZZA,
  LANDINGNOPIZZA,
  RUNNINGPIZZA,
  RUNNINGNOPIZZA,
  CHARGING,
  FLIP,
  SPIN,

}


  public Animation[,] skinAnimations = 
  new Animation[System.Enum.GetValues(typeof(Skin)).Length,System.Enum.GetValues(typeof(Animations)).Length];


  private void Awake() {
    


  }


  public void SetSkin(){



  }

    private void Update() {
      AnimatorClipInfo[] clipInfo;
      AnimatorStateInfo animStateInfo = playerAnimator.GetCurrentAnimatorStateInfo(0);
      clipInfo = playerAnimator.GetCurrentAnimatorClipInfo(0);
    }

}
