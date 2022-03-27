using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas playerUI;
    private int currentCheckpoint;
    public Animator secondFireAnimator;
    public Animator firstFireAnimator;
    public Image arrowButtonImage;
    public Slider jumpSlider;
    public GameObject jumpObject;
    public jumpKingScript playerScript;
    public MusicScript musicScript;
    public GameObject player1;
    private Rigidbody2D rb;
    public Camera mainCamera;
    public GameObject background;
    public Text thoughtBubbleText;
    public Canvas thoughtsUI;
    public Canvas positiveThoughtsUI;
    public Canvas howToPlayCanvas;
    public Canvas pauseMenuCanvas;
    public float parallaxSpeed = 8f;
    public bool parallaxON = false;
    public bool UserUIon = false;
    public float displacement;
    public string[] negativeThoughts;
    public string[] positiveThoughts;
    private Vector3 leftRotation = new Vector3(0,0,-90);
    private Vector3 rightRotation = new Vector3(0,0,90);
    public Light2D checkpointLight1;
    public Light2D checkpointLight2;
    public bool followPlayer2;
    public ParticleSystem rain;
    public Image thoughtBubble;
    public Image negativeThoughtBubble;
    public Text thoughtBubbleText2;
    private bool positiveThoughtsUIon;

    public bool firstAnimation = true;
    public bool secondAnimation = true;
    public Image firstLanding;
    public Image secondLanding;
    public Image firstArrow;
    public Image secondArrow;
    public GameObject firstJumpBar;
    public GameObject secondJumpBar;
    public Image walkingSprite;
    private Vector3 rightRotationNew = new Vector3(0,0,0);
    private Vector3 leftRotationNew = new Vector3(0,180,0);
    public Image arrowKeyRight;
    public Image arrowKeyLeft;
    private Color yellowHighlight = new Color(255f/255f, 253f/255f, 153f/255f);
    private Color whiteColor = new Color(1, 1, 1);
    public Slider musicVolume;
    public Slider ambienceVolume;
    public Slider sfxVolume;
    public AudioSource jumpSound;
    public AudioSource landSound;
    

    /* 
    Gwen did the Start() 
    including creating the arrays for thought texts, as well as UI manipulation
    */
    void Start()
    {
        rb = player1.GetComponent<Rigidbody2D>();
        //playerScript = GameObject.Find("GameController").GetComponent<jumpKingScript>();
        currentCheckpoint = 0;
        negativeThoughts = new string[12];
        positiveThoughts = new string[6];
        negativeThoughts[0] = "Why do I keep failing...";
        negativeThoughts[1] = "I'm so useless.. I can't do anything right...";
        negativeThoughts[2] = "I'm just a burden to everyone...";
        negativeThoughts[3] = "It's like there's a constant storm in my head...";
        negativeThoughts[4] = "Will it ever end???";
        negativeThoughts[5] = "If I don't fix my situation soon, I'll be abandoned...";
        negativeThoughts[6] = "All this effort is futile... I can't change anything...";
        negativeThoughts[7] = "Of course this would happen to me...";
        negativeThoughts[8] = "What's the point of trying again...";
        negativeThoughts[9] = "WHY?!?!...";
        negativeThoughts[10] = "What a cruel joke...";
        positiveThoughts[0] = "I can do this...";
        positiveThoughts[1] = "I'm doing it!!!";
        positiveThoughts[2] = "I'm glad I have my family for support... not everyone has that...";
        positiveThoughts[3] = "If I can just keep trying, I no I can make it";
        positiveThoughts[4] = "Maybe my hard work is paying off...";
        positiveThoughts[5] = "I'm making progress... And I feel a bit better..";
        playerUI.enabled = false;
        mainCamera = Camera.main;
        thoughtsUI.enabled = false;
        checkpointLight1.enabled = false;
        checkpointLight2.enabled = false;
        mainCamera.transform.position = new Vector3(0,0,-10);
        followPlayer2 = true;
        positiveThoughtsUIon = false;
        positiveThoughtsUI.enabled = false; 
        howToPlayCanvas.enabled = false;
        pauseMenuCanvas.enabled = false;
        displacement = 1.4f;
        musicScript = (MusicScript)FindObjectOfType(typeof(MusicScript));
        musicVolume.value = musicScript._audioSource.volume;
        ambienceVolume.value = musicScript.ambience.volume;
        sfxVolume.value = musicScript.sfxVolume;
        
    }


    /*
    Gwen did the UI coding for the rest of this script (anything related to canvas.enabled)
    */
    /*
    Zach Willson did the rest, manipulating player location/movement, manipulating emission rate over time, parallax, etc.
    Benton and Karl helped Zach with lots of this, specifically parallax and camera movemement.
    */
    /*This script is was copied and pasted for GameController2. Only a few changes are in that, primarily starting off it launches 
    the player as if jumping into the level from the previous level
    */
    // Update is called once per frame
    void Update()
    {
        if(pauseMenuCanvas.enabled){
            //check for volume updates
            musicScript._audioSource.volume = musicVolume.value;
            musicScript.ambience.volume = ambienceVolume.value;
            jumpSound.volume = sfxVolume.value/2;
            landSound.volume = sfxVolume.value*3;
            musicScript.UpdateSFXvolume(sfxVolume.value);

        }
        
        if(rain.isEmitting){
            var rainEmitter = rain.emission;
            rainEmitter.rateOverTime = (float)Mathf.Round((float)(200 - (3.14 * (player1.transform.position.y+4))));
            //rain.emission = 200 - (3.14 * (player1.transform.position.y+4));
        }
        if(howToPlayCanvas.enabled){
            if(firstAnimation){
            StartCoroutine(SwitchAnimations());
            }
            if(secondAnimation){
                StartCoroutine(SwitchAnimations2());
            }   
        }
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseMenuCanvas.enabled || howToPlayCanvas.enabled){
                BackButtonClicked();
            }else{
                playerScript.isPaused = true;
                displayPauseMenu(); 
            }
        }
        if(player1.transform.position.y >= 45.6){
            followPlayer2 = false;
        }else{
            followPlayer2 = true;
        }
        if(currentCheckpoint>0){
            checkCheckpoints();
        }
        if(parallaxON){
            if(followPlayer2){
            background.transform.position = new Vector3(0,((player1.transform.position.y)/parallaxSpeed)+12,0);
            }
            if(player1.transform.position.y >= -3.1f && followPlayer2){
                mainCamera.transform.position = new Vector3(0,player1.transform.position.y+3.1f,-10);
            }
        }
        if(positiveThoughtsUIon){
            thoughtBubble.transform.position = player1.transform.position + new Vector3(2,3,0);
        }
        if(UserUIon){
            jumpObject.transform.position = player1.transform.position + new Vector3(0,displacement,0);
            jumpSlider.value = playerScript.jumpValue;
             
            if(playerScript.jumpChargeDirection == 1){
                //display right arrow
                arrowButtonImage.enabled = true;
                arrowButtonImage.transform.rotation = Quaternion.Euler(new Vector3(0,0,180));
            }
            else if(playerScript.jumpChargeDirection == -1){
                //display left arrow
                arrowButtonImage.enabled = true;
                arrowButtonImage.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
            }else{
                arrowButtonImage.enabled = true;
                arrowButtonImage.transform.rotation = Quaternion.Euler(new Vector3(0,0,270));
            }
            
        }
    }

    public void MenuButtonClicked(){
        SceneManager.LoadScene("StartScreen");
    }

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
    walkingSprite.transform.rotation = Quaternion.Euler(rightRotationNew);
    //set right arrow key color
    arrowKeyRight.color = yellowHighlight;
    arrowKeyLeft.color = whiteColor;
    yield return new WaitForSeconds(2.5f);
    walkingSprite.transform.rotation = Quaternion.Euler(leftRotationNew);
    arrowKeyLeft.color = yellowHighlight;
    arrowKeyRight.color = whiteColor;
    yield return new WaitForSeconds(2.5f);
    secondAnimation = true;
}

    public void displayPauseMenu(){
        pauseMenuCanvas.enabled = true;
        //Time.timeScale = 0;
    }

    public void StageFinished(){
        musicScript.SwitchStage();
        SceneManager.LoadScene("Stage2");
    }
    public void BackButtonClicked(){
        if(pauseMenuCanvas.enabled){
            pauseMenuCanvas.enabled = false;
            playerScript.isPaused = false;
            //Time.timeScale = 1;
        }else{
            howToPlayCanvas.enabled = false;
            pauseMenuCanvas.enabled = true;
        }
    }

    public void HowToPlayClicked(){
        howToPlayCanvas.enabled = true;
        pauseMenuCanvas.enabled = false;
    }
    public void hitCheckpoint(){
        currentCheckpoint++;
        displayPositiveThoughts();
        if(currentCheckpoint == 1){
            firstFireAnimator.SetBool("FireCaptured",true);
            checkpointLight1.enabled = true;
        }
        else if(currentCheckpoint == 2){
            secondFireAnimator.SetBool("FireCaptured",true);
            firstFireAnimator.SetBool("FireCaptured",false);
            checkpointLight1.enabled = false;
            checkpointLight2.enabled = true;
        }else if(currentCheckpoint == 3){
            
        }
    }

    public void checkCheckpoints(){
        if(player1.transform.position.y < 15 && currentCheckpoint == 1){
            //teleport player back above fireplace
            player1.transform.position = new Vector2(0,15.5f);
        }else if(player1.transform.position.y < 33 && currentCheckpoint == 2){
            player1.transform.position = new Vector2(1.5f,34.2f);
        }
    }

    public void DisplayUserUI(){
        playerUI.enabled = true;
        UserUIon = true;
    }
    public void StopDisplayingUserUI(){
        playerUI.enabled = false;
        UserUIon = false;
    }
   
    public void displayThoughtsUI(){
        thoughtBubbleText.text = "";
        thoughtsUI.enabled = true;
        StartCoroutine(TypeSentence(negativeThoughts[Random.Range(0,11)],thoughtBubbleText));
        playerUI.enabled = false;
    }

    public void displayPositiveThoughts(){
        positiveThoughtsUIon = true;
        positiveThoughtsUI.enabled = true;
        StartCoroutine(TypeSentence(positiveThoughts[Random.Range(0,6)],thoughtBubbleText2));
    }

    public void stopDisplayingThoughts(){
        thoughtsUI.enabled = false;
        playerScript.inCutscene = false;
        playerScript.startedCutscene = false;
        playerScript.deathState = false;
        playerScript.animator.SetBool("deathState",false);
    }
    public void stopDisplayingPositiveThoughts(){
        positiveThoughtsUI.enabled = false;
        positiveThoughtsUIon = false;
        
    }

    /*
    Benton helped Zach 
    with this IEnumerator
    */
    IEnumerator TypeSentence(string sentence,Text textToChange){
        textToChange.text = "";
        foreach(char letter in sentence){
            //display letter, time delay it
            textToChange.text += letter;
            yield return new WaitForSeconds(.075f);
        }
        if(textToChange.name == thoughtBubbleText.name){
            //Debug.Log("Invoked stop playing negative");
            Invoke("stopDisplayingThoughts",1.6f);
        }else{
            //Debug.Log("invoked stop play Positive");
            Invoke("stopDisplayingPositiveThoughts",1.6f);
        }
    }
}
