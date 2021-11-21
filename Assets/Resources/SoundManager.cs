using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public static class SoundManager {
    // Start is called before the first frame update
    public enum Sound {

      MENUPOP,
      KNIFEBEEP,
      ROLLERBEEP,
      FINISHEDORDER,
      PIZZATIMECUT,
      KNIFETHROW,
      GAMEOVER,
      MONEYSPENT,
      ROLLERHIT,
      PIZZALAUNCH,
      COIN,
      STEP,//
      PIZZATIMECORRECT,
      PIZZATIMEERROR,
      INGREDIENTPICKUP,
      JUMP,
  
      
    }

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    
    public static void PlaySound(Sound sound, float vol, AudioSource source ) {

        if (oneShotGameObject == null) {
            oneShotGameObject = new GameObject("OneShotSound");
            oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            if(source != null) oneShotAudioSource.outputAudioMixerGroup = source.outputAudioMixerGroup;
        
        }
        oneShotAudioSource.PlayOneShot(GetAudioClip(sound), vol);
        
    }

    private static AudioClip GetAudioClip(Sound sound) {
        foreach(GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArryay) {
            
            if (soundAudioClip.sound == sound) {
                return soundAudioClip.audioClip;
            }

        }

        Debug.LogError("Sound" + sound + " not found!");
        return null;
        

    }

}
