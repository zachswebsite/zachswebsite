using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicScript : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioSource ambience;

    public AudioClip[] stage1Music;
    public AudioClip[] stage2Music;
    public AudioClip rainSounds;
    public AudioClip natureSounds;
    public bool stage1;
    public bool stage2;
    public bool playingStage1Music;
    public bool playingStage2Music;
    private int audiotracker1 = 0, audiotracker2 = 0;
    public bool gameStarted;
    public float sfxVolume;
    private float musicVolumeBeforeFade;
    private float sfxVolumeBeforeFade;
    private float ambienceVolumeBeforeFade;

    /*
    Karl Watkins (primarily)
    & 
    Zach Willson wrote Start() and Update() together
    */
     private void Start()
     {
         gameStarted = false;
         stage1 = true;
         ambience.clip = rainSounds;
         playingStage1Music = true;
         DontDestroyOnLoad(this.gameObject);
         _audioSource.clip = stage1Music[0];
         PlayMusic();
         sfxVolume = .15f;
         _audioSource.volume = .2f;
         ambience.volume = .14f;
         SceneManager.LoadScene("StartScreen");
     }
 
     private void Update() {
         /*
        if(SceneManager.GetActiveScene().name == "Stage2"){
            stage2 = true;
            stage1 = false;
        }else{
            stage1 = true;
            stage2 = false;
        }*/
        if(SceneManager.GetActiveScene().name == "SampleScene"){
            gameStarted = true;
        }else if(SceneManager.GetActiveScene().name == "StartScreen"){
            gameStarted = false;
            ambience.Stop();
        }
        if(playingStage1Music && stage2){
            //slowly turn off current music, then start next playlist (can happen naturally)
            sfxVolumeBeforeFade = sfxVolume;
            musicVolumeBeforeFade = _audioSource.volume;
            ambienceVolumeBeforeFade = ambience.volume;
            StartCoroutine(StartFade(_audioSource,3.0f,0,0));
            StartCoroutine(StartFade(ambience,3.0f,0,1));
            playingStage1Music = false;
            playingStage2Music = true;
        }
        if(!_audioSource.isPlaying){
            if(stage1){
                audiotracker1++;
                if(audiotracker1 >= stage1Music.Length){
                    audiotracker1 = 0;
                }
                _audioSource.clip = stage1Music[audiotracker1];
            }else{
                audiotracker2++;
                if(audiotracker2 >= stage2Music.Length){
                    audiotracker2 = 0;
                }
                _audioSource.clip = stage2Music[audiotracker2];
            }
            _audioSource.Play();
        }
        if(!ambience.isPlaying && gameStarted){
            if(stage1){
                ambience.clip = rainSounds;
            }else{
                ambience.clip = natureSounds;
            }
            ambience.Play();
        }
    }

    /*
    Karl Watkins wrote the following methods
    */
    public void UpdateSFXvolume(float value){
        sfxVolume = value;
    }
    public void SwitchStage(){
        if(stage1){
            stage1 = false;
            stage2 = true;
            ambience.clip = natureSounds;
        }else{
            stage1 = true;
            stage2 = false;
            ambience.clip = rainSounds;
        }
    }
     public void PlayMusic()
     {
         if (_audioSource.isPlaying) return;
         _audioSource.Play();
     }
 
     public void StopMusic()
     {
         _audioSource.Stop();
     }

     public void StopAmbience(){
         ambience.Stop();
     }

     public void PlayAmbience(){
         if(ambience.isPlaying) return;
         ambience.Play();
     }



    /*
    Zach Willson wrote this method with the help of Karl Watkins
    */
    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume,int flag)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        audioSource.Stop();
        SwitchStage();      
        //check flag to reset volumes
        if(flag == 0){
            audioSource.volume = musicVolumeBeforeFade;
        }else{
            audioSource.volume = .05f; //preset ambience volume because birds are loud for some reason.
        }
        yield break;
    }

}
