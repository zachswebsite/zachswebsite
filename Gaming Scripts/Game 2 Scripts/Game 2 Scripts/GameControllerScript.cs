using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControllerScript : MonoBehaviour
{

    //controls - shift to recreate sound, A,D, left arrow, right arrow for movement. 
    //down arrow and 'S' for inputting/blowing, may change to up arrow and 'W'

    List<int> tuneToMatch = new List<int>();
    List<int> tuneGuess = new List<int>();
    
    public GameObject flute;
    public AudioSource[] notes = new AudioSource[9];
    public GameObject startCanvas;
    public GameObject rulesCanvas;
    public int timer = 20;
    public GameObject gameRunningCanvas;
    public Image player1Text;
    public Image winImage;
    public Image congrats;
    public Image player2Text;
    public Image createTunePrompt;
    public Button backButton;
    public Image mimicTunePrompt;
    public Image pixelX;
    public Text noteCountText;
    public Image notesLeftImage;
    public Text countDownText;
    private Image backImage;
    playerScript pl1Script;
    playerScript pl2Script;
    public GameObject player1;
    //x = 1.1 y = .48 good starting position
    public Vector3 startPos = new Vector3(1.1f,0.48f,0.0f);
    public Vector3 timeOutPos = new Vector3(1.1f,2.3f,0.0f);
    public Vector3 flutePos = new Vector3(3.19f,-1.87f,0.0f);
    public GameObject player2;
    private bool player1Move = true;
    private bool player1Turn = true;
    private int noteCount = 2;
    private int moveTracker = 0;
    private int roundCount = 0;
    private int currentNoteCount = 0;
    private IEnumerator coroutine;
    public int secondsToFadeOut = 3;
    private bool gameStarted = false;
    private SpriteRenderer pl1Sr;
    private SpriteRenderer pl2Sr;
    private SpriteRenderer fluteSr;
    private Vector3 menuPosPl1;
    private Vector3 menuPosPl2;
    
    //noteCountText.text = noteCount - currentNoteCount.ToString();

    //use moveTracker variable and player1Turn to tell what type of display to put up, 
    //and which list to move the 'tunes' into.
    // Start is called before the first frame update
    void Start()
    {
        menuPosPl1 = player1.transform.position;
        menuPosPl2 = player2.transform.position;
        notes[8].Play();
        congrats.enabled = false;
        pl1Sr = player1.GetComponent<SpriteRenderer>();
        pl2Sr = player2.GetComponent<SpriteRenderer>();
        fluteSr = flute.GetComponent<SpriteRenderer>();

        backImage = backButton.GetComponent<Image>();
        backImage.enabled = false;
        backButton.enabled = false;
        winImage.enabled = false;
        mimicTunePrompt.enabled = false;
        createTunePrompt.enabled = false;
        player2Text.enabled = false;
        player1Text.enabled = false;
        pixelX.enabled = false;
        rulesCanvas.SetActive(false);
        gameRunningCanvas.SetActive(false);
        startCanvas.SetActive(true);
        pl1Script = player1.GetComponent<playerScript>();
        pl2Script = player2.GetComponent<playerScript>();
        coroutine = PlayTune();
    }

    // Update is called once per frame
    void Update()
    {
        noteCountText.text = (noteCount - currentNoteCount).ToString();
        countDownText.text = timer.ToString();
        if(moveTracker == 4){
            roundCount ++;
            noteCount = 2+roundCount;
            moveTracker = 0;
            tuneGuess.Clear();
            tuneToMatch.Clear();
        }
        //below code is mainly for UI display
        if(gameStarted){
        if(moveTracker == 0 || moveTracker == 2){
            //create a tune move, now decide player
            mimicTunePrompt.enabled = false;
            createTunePrompt.enabled = true;
            if(player1Move == true){
                //player 1 creating a tune
                player2Text.enabled = false;
                player1Text.enabled = true;
            }else{
                //player 2 creating a tune
                player2Text.enabled = true;
                player1Text.enabled = false;
            }
        }else if(moveTracker == 1 || moveTracker == 3){
            //guess a tune move, now decide player
            mimicTunePrompt.enabled = true;
            createTunePrompt.enabled = false;
            if(player1Move == true){
                //player 1 guessing a tune
                player2Text.enabled = false;
                player1Text.enabled = true;
            }else{
                //player 2 guessing a tune
                player2Text.enabled = true;
                player1Text.enabled = false;
            }
        }
        }
    }
    private IEnumerator FadeAudioOut(){
 
        // Check Music Volume and Fade Out
        while (notes[8].volume > 0.01f)
        {
            notes[8].volume -= Time.deltaTime / secondsToFadeOut;
            yield return null;
        }
 
        // Make sure volume is set to 0
        notes[8].volume = 0;
 
        // Stop Music
        notes[8].Stop();
        
    }
    public void MenuButton(){
        //go back to main menu
        notes[8].volume = 1;
        player1.transform.position = menuPosPl1;
        player2.transform.position = menuPosPl2;
        gameRunningCanvas.SetActive(false);
        startCanvas.SetActive(true);
        rulesCanvas.SetActive(false);
        if(notes[8].isPlaying){
            //nothing
        }else{
            notes[8].Play();
        }
        fluteSr.enabled = true;
        pl1Sr.enabled = true;
        pl2Sr.enabled = true;
    }
    public void StartGame(){
        StartCoroutine(FadeAudioOut());
        player1.transform.position = startPos;
        player2.transform.position = timeOutPos;
        //method's for start screen, and game intro.
        tuneToMatch.Clear();
        timer = 30;
        tuneGuess.Clear();
        gameStarted = true;
        startCanvas.SetActive(false);
        gameRunningCanvas.SetActive(true);
        ResetVars();
        backImage.enabled = false;
        notesLeftImage.enabled = true;
        noteCountText.enabled = true;
        winImage.enabled = false;
    }

    public void RulesScreen(){
        //method to bring up the rules UI
        startCanvas.SetActive(false);
        rulesCanvas.SetActive(true);
        fluteSr.enabled = false;
        pl1Sr.enabled = false;
        pl2Sr.enabled = false;
    }

    public void PlayTuneToMatch(){
        tuneToMatch.ForEach(delegate(int tune){
            notes[tune].Play();
        });
    }
    public void CoroutineTune(){
        StartCoroutine(PlayTune());
    }
    private IEnumerator Congratulations(){
        float quickTimer = 1.0f;
        congrats.enabled = true;
        gameStarted = false;
        mimicTunePrompt.enabled = false;
        createTunePrompt.enabled = false;
        while(quickTimer>=0){
            yield return new WaitForSeconds(0.75f);
            quickTimer--;
        }
        congrats.enabled = false;
        gameStarted = true;
    }
    private IEnumerator CountdownTimer(int round){
        timer = 30 + roundCount;
        yield return new WaitForSeconds(3.0f);
        while(moveTracker == round && timer > 0){
            timer--;
            yield return new WaitForSeconds(1.0f);
        }
        //lose state, ran out of time, put code here for that
        if(moveTracker == round){
            PlayerLose();
            gameStarted = false;
        }
        
    }
    private IEnumerator RedX(){
        float quickTimer = 1.0f;
        pixelX.enabled = true;
        while(quickTimer>=0){
            yield return new WaitForSeconds(0.75f);
            quickTimer--;
        }
        pixelX.enabled = false;
    }
    public void PlayerLose(){
        notesLeftImage.enabled = false;
        noteCountText.enabled = false;
        backButton.enabled = true;
        backImage.enabled = true;
        if(getPlayerMove() == 1){
            //player 2 wins
            winImage.enabled = true;
            player2Text.enabled = true;
            player1Text.enabled = false;
            createTunePrompt.enabled = false;
            mimicTunePrompt.enabled = false;
            gameStarted = false;
        }else{
            //player `1 wins
            winImage.enabled = true;
            player2Text.enabled = false;
            player1Text.enabled = true;
            createTunePrompt.enabled = false;
            mimicTunePrompt.enabled = false;
            gameStarted = false;
        }
    }

    public bool GameRunning(){
        return gameStarted;
    }
    private IEnumerator PlayTune(){
        int count = 0;
        yield return new WaitForSeconds(.3f);
        while(count < tuneToMatch.Count){
            notes[tuneToMatch[count]].Play();
            count++;
            yield return new WaitForSeconds(1.0f);
        }
    }
    public void AddTune(int tune){
        notes[tune].Play();
        if(currentNoteCount < noteCount){
            currentNoteCount++;
            if(moveTracker == 0 || moveTracker == 2){
                tuneToMatch.Add(tune);
            }else{
                GuessTune(tune);
            }
            if(currentNoteCount == noteCount){
                //1 and 3 are flags for 'guessing' turns, doesn't matter the player
                //now at the end of a guessing turn, 
                //decide if the guess matches, if so continue

                /*
                if(moveTracker == 1 || moveTracker == 3){
                    if(tuneToMatch.Equals(tuneGuess);
                    for(int i=0;i<tuneToMatch.Count;i++){

                    }
                }*/


                //player reached max note additions,
                //change to other player
                ChangePlayerMove();
                
                moveTracker++;
                if(moveTracker == 2){
                    ChangePlayerMove();
                    tuneToMatch.Clear();
                    tuneGuess.Clear();
                }
                currentNoteCount = 0;
                if(moveTracker == 1 || moveTracker == 3){
                    //start countdown coroutine here, go while moveTracker == input
                    StartCoroutine(CountdownTimer(moveTracker));
                }
            }
        }
    }
    public void ResetVars(){
        roundCount = 0;
        noteCount = 2;
        tuneToMatch.Clear();
        tuneGuess.Clear();
        player1Move = true;
        player1Turn = true;
        currentNoteCount = 0;
        moveTracker = 0;
    }
    public void GuessTune(int guess){
        //check if the guess is correct, if it is, add to list of guesses, 
        //if it is not correct, send signal to show, and reset currentNoteCount
        int index = 0;
        if(currentNoteCount == 0){
            index = 0;
        }else{
            index = currentNoteCount-1;
        }
        if(guess == tuneToMatch[index]){
            //guess is correct, so just add to list of guesses
            tuneGuess.Add(guess);
            if(currentNoteCount == noteCount){
                StartCoroutine(Congratulations());
            }
        }else{
            //guess is incorrect, restart guessing sequence
            tuneGuess.Clear();
            currentNoteCount = 0;
            StartCoroutine(RedX());
            //maybe set a big red x visible, and have an audio cue.
        }
    }
    public void ChangePlayerMove(){
        if(player1Move == true){
            player1Move = false;
            pl1Script.resetLocation();
            pl2Script.resetLocation();
            player1.transform.position = timeOutPos;
            player2.transform.position = startPos;
        }else{
            player1Move = true;
            pl1Script.resetLocation();
            pl2Script.resetLocation();
            player1.transform.position = startPos;
            player2.transform.position = timeOutPos;
        }
    }
    public int getPlayerTurn(){
        if(player1Turn == true){
            return 1;
        }else{
            return 2;
        }
    }
    public int getPlayerMove(){
        if(player1Move == true){
            return 1;
        }else{
            return 2;
        }
    }
    public int getNoteCount(){
        return noteCount;
    }
    public int getRoundCount(){
        return roundCount;
    }
    
}
