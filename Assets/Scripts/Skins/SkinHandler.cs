using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class SkinHandler : MonoBehaviour
{
   public PlayerAnimationHandler player;
  public enum SkinEnum{


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

  [SerializeField] Sprite[] skinSprites = new Sprite[System.Enum.GetValues(typeof(SkinEnum)).Length];
  public Sprite GetSprite (SkinEnum skin){
      return skinSprites[((int)skin)];
    } 

  public SkinEnum currentSkin;


  [SerializeField] public AnimatorOverrideController[] playerAnimators = new AnimatorOverrideController[System.Enum.GetValues(typeof(SkinEnum)).Length];


  private void Awake() {
      player = FindObjectOfType<PlayerAnimationHandler>();
      currentSkin = SkinEnum.DEFAULT;
  }



  public void SetSkin(Skin skin){
      player = FindObjectOfType<PlayerAnimationHandler>();
      Debug.Log(skin.skin);
      player.playerAnimator.runtimeAnimatorController = playerAnimators[((int)skin.skin)];
  }



}
