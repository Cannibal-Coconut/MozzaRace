using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSettingManager : MonoBehaviour
{
       [SerializeField] private AudioSource sfxAudio;
       [SerializeField] private AudioSource musicSource;

        [SerializeField] private AudioClip PizzaTimeSong;
        [SerializeField] private AudioClip TipTimeSong;
        [SerializeField] private AudioClip MainThemeSong;
        public bool loopMusic;

        private float previousMusicTime;

        private void Start() {
            PlayMainTheme();
        }

    public void PlayPizzaTime(){
        //if(loopMusic == true) previousMusicTime = musicSource.time;
        loopMusic = false;
        musicSource.clip = PizzaTimeSong;
        musicSource.volume = 0.3f;
        musicSource.time = 0.0f;
        musicSource.Play();
    }
    public void PlayTipTime(){
      //  if(loopMusic == true) previousMusicTime = musicSource.time;
        loopMusic = false;
        musicSource.clip = TipTimeSong;
        musicSource.volume = 1.5f;
        musicSource.time = 0.0f;
        musicSource.Play();

    }

    public void PlayMainTheme(){
        loopMusic = true;
        musicSource.clip = MainThemeSong;
        musicSource.time = previousMusicTime;
        musicSource.volume = 0.3f;
        musicSource.Play();
    }



    private void Update() {

        if(!musicSource.isPlaying){
        
            PlayMainTheme();
        
        }

        musicSource.loop = loopMusic;

    }
    
    public bool SFXIsPlaying(){

        return sfxAudio.isPlaying;

    }

    #region sfx

      public void PlayButtonSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.MENUPOP, 1f, sfxAudio);
    }

     public void PlayKnifeAlert()
    {
        SoundManager.PlaySound(SoundManager.Sound.KNIFEBEEP, 1f, sfxAudio);
    }
     public void PlayRollerAlert()
    {
        SoundManager.PlaySound(SoundManager.Sound.ROLLERBEEP, 1f, sfxAudio);
    }
     public void PlayFinishedOrder()
    {
        SoundManager.PlaySound(SoundManager.Sound.FINISHEDORDER, 1f, sfxAudio);
    }
     public void PlayPizaTimeCut()
    {
        SoundManager.PlaySound(SoundManager.Sound.PIZZATIMECUT, 1f, sfxAudio);
    }
     public void PlayKnifeThrow()
    {
        SoundManager.PlaySound(SoundManager.Sound.KNIFETHROW, 1f, sfxAudio);
    }
     public void PlayGameOver()
    {
        
        SoundManager.PlaySound(SoundManager.Sound.GAMEOVER, 1.5f, sfxAudio);
    }
     public void PlaySpendMoney()
    {
        SoundManager.PlaySound(SoundManager.Sound.MONEYSPENT, 1f, sfxAudio);
    }
     public void PlayRollerHit()
    {
        SoundManager.PlaySound(SoundManager.Sound.ROLLERHIT, 1f, sfxAudio);
    }
     public void PlayPizzaLaunch()
    {
        SoundManager.PlaySound(SoundManager.Sound.PIZZALAUNCH, 1f, sfxAudio);
    }
     public void PlayCoinPickup()
    {
        SoundManager.PlaySound(SoundManager.Sound.COIN, 0.5f, sfxAudio);
    }
     public void PlayStepSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.STEP, 1f, sfxAudio);
    }
     public void PlayPizzaTimeCorrect()
    {
        SoundManager.PlaySound(SoundManager.Sound.PIZZATIMECORRECT, 1f, sfxAudio);
    }
     public void PlayPizzaTimeError()
    {
        SoundManager.PlaySound(SoundManager.Sound.PIZZATIMEERROR, 0.5f, sfxAudio);
    }
     public void PlayIngredientPickup()
    {
        SoundManager.PlaySound(SoundManager.Sound.INGREDIENTPICKUP, 1f, sfxAudio);
    }
     public void PlayJumpSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.JUMP, 0.5f, sfxAudio);
    }
      
      



    #endregion




}


