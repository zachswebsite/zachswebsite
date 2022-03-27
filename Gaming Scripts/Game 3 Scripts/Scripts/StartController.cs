using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas introCanvas;
    public Text volumeText; 
    public Text musicVolumeText;
    public Canvas settingsCanvas;
    public Canvas howToPlayCanvas;
    public bool firstAnimation = true;
    public bool secondAnimation = true;
    public Image firstLanding;
    public Image secondLanding;
    public Image firstArrow;
    public Image secondArrow;
    public GameObject firstJumpBar;
    public GameObject secondJumpBar;
    public Image walkingSprite;
    public Canvas cutSceneIntro;
    public Text introTextCutScene;
    private Vector3 rightRotation = new Vector3(0,0,0);
    private Vector3 leftRotation = new Vector3(0,180,0);
    public Image arrowKeyRight;
    public Image arrowKeyLeft;
    private Color yellowHighlight = new Color(255f/255f, 253f/255f, 153f/255f);
    private Color whiteColor = new Color(1, 1, 1);
    public GameObject musicGameObject;
    public MusicScript musicScript;
    public Slider musicVolume;
    public Slider ambienceVolume;
    public Slider sfxVolume;


    /*
    Benton
    & 
    Zach
    did the Start() and update() together
    */
    void Start()
    {
        cutSceneIntro.enabled = false;
        introCanvas.enabled = true;
        settingsCanvas.enabled = false;
        howToPlayCanvas.enabled = false;
        musicScript = (MusicScript)FindObjectOfType(typeof(MusicScript));
        musicVolume.value = musicScript._audioSource.volume;
        ambienceVolume.value = musicScript.ambience.volume;
        sfxVolume.value = musicScript.sfxVolume;
    }

    // Update is called once per frame
    void Update()
    {
        if(settingsCanvas.enabled){
            musicScript._audioSource.volume = musicVolume.value;
            musicScript.ambience.volume = ambienceVolume.value;
            musicScript.UpdateSFXvolume(sfxVolume.value);
        }
        if(firstAnimation){
            StartCoroutine(SwitchAnimations());
        }
        if(secondAnimation){
            StartCoroutine(SwitchAnimations2());
        }

        
    }

    /*
    Benton did the methods below
    */
    public void StartButtonClicked(){
        //do stuff
        introCanvas.enabled = false;
        //SceneManager.MoveGameObjectToScene(musicGameObject,SceneManager.GetSceneByBuildIndex(0));
        //load text into canvas. 
        cutSceneIntro.enabled = true;
        StartCoroutine(TypeSentence("This game is one that attempts to capture the process of going through life with PTSD. Throughout the game, you will face multiple challenges in your journey. You may stumble and fall, but the real challenge is never giving up. This game emulates the re-wiring of the brain via healing that allows one to process and relive the world through a different lens than they once might have.",introTextCutScene));
    }
    public void SettingsButtonClicked(){
        //do stuff
        introCanvas.enabled = false;
        settingsCanvas.enabled = true;
    }
    public void BackButtonClicked(){
        if(settingsCanvas.enabled){
            settingsCanvas.enabled = false;
            introCanvas.enabled = true;
        }else{
            howToPlayCanvas.enabled = false;
            settingsCanvas.enabled = true;
        }
    }
    public void HowToPlayClicked(){
        howToPlayCanvas.enabled = true;
        settingsCanvas.enabled = false;
    }

    public void AdjustingMainVolume(){

    }
    public void AdjustingMusicVolume(){

    }

/* 
Zach Willson wrote the IEnumerators
*/
    private IEnumerator SwitchAnimations()
{
    firstAnimation = false;
    //play first animation
    firstLanding.enabled = true;
    firstArrow.enabled = true;
    firstJumpBar.SetActive(true);
    secondArrow.enabled = false;
    secondLanding.enabled = false;
    secondJumpBar.SetActive(false);
   yield return new WaitForSeconds(3);
    firstLanding.enabled = false;
    firstArrow.enabled = false;
    firstJumpBar.SetActive(false);
    secondArrow.enabled = true;
    secondLanding.enabled = true;
    secondJumpBar.SetActive(true);
   //play 2nd animation
   yield return new WaitForSeconds(3);
   //switch values again for re-run
   firstAnimation = true;
/// etc etc etc.
}

private IEnumerator SwitchAnimations2(){
    secondAnimation = false;
    walkingSprite.transform.rotation = Quaternion.Euler(rightRotation);
    //set right arrow key color
    arrowKeyRight.color = yellowHighlight;
    arrowKeyLeft.color = whiteColor;
    yield return new WaitForSeconds(2.5f);
    walkingSprite.transform.rotation = Quaternion.Euler(leftRotation);
    arrowKeyLeft.color = yellowHighlight;
    arrowKeyRight.color = whiteColor;
    yield return new WaitForSeconds(2.5f);
    secondAnimation = true;
}

IEnumerator TypeSentence(string sentence,Text textToChange){
        textToChange.text = "";
        foreach(char letter in sentence){
            //display letter, time delay it
            textToChange.text += letter;
            yield return new WaitForSeconds(.02f);
        }
        
            Invoke("startGame",4.0f);
}

public void startGame(){
    SceneManager.LoadScene("SampleScene");
}
        
}
