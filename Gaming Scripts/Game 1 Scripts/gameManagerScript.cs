using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManagerScript : MonoBehaviour
{
    public GameObject player;
    public AudioSource pointAudio;
    public Sprite pause;
    public Sprite play;
    public AudioSource gameOverAudio;
    public GameObject tutCanvas;
    public AudioSource myMusic;
    public AudioClip og;
    public Animator animator;
    public AudioClip slow;
    public AudioClip fast;
    public AudioClip uke;
    public GameObject gameOverCanvas;
    public GameObject runningScoreCanvas;
    public GameObject winningCanvas;
    public GameObject gameOver;
    public GameObject winCondition;
    public GameObject checkpointButton;
    private Text score;
    private Text score2;
    private string gameStatus = "start";
    private Image playButton;
    private int checkpointScore;
    private bool checkpointed = false;
    private Time checkpointTime;
    public Vector2 checkXY;
    private int scoreCount;
    // Start is called before the first frame update
    void Start()
    {
        myMusic.loop = true;
        player = GameObject.FindWithTag("Player");
        score = GameObject.FindWithTag("score").GetComponent<Text>();
        score2 = GameObject.FindWithTag("score2").GetComponent<Text>();
        playButton = GameObject.FindWithTag("PlayButton").GetComponent<Image>();
        Time.timeScale = 0.01f;
        gameOver.SetActive(false);
        winningCanvas.SetActive(false);
        runningScoreCanvas.SetActive(false);
        if(checkpointed == false){
            checkpointButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.x > 215){
            gameStatus = "win";
            winning();
        }
    }
    public void winning(){
        Time.timeScale = 0;
        winningCanvas.SetActive(true);
        runningScoreCanvas.SetActive(true);
    }
    public void checkPoint(){
        if(checkpointed == true){
            //send back to coordinates, and unfreeze game. Infinite round only starts when player gets close to X position of infinite round
            player.transform.position = checkXY;
            Time.timeScale = 1;
            gameStatus = "start";
            gameOverCanvas.SetActive(false);
            gameOver.SetActive(false);
            runningScoreCanvas.SetActive(true);
            scoreCount = checkpointScore;
        }
    }
    public void hitCheckpoint(Vector2 position){
        Debug.Log("checkpoint hit, x = "+position.x+" and y = "+position.y);
        checkXY = position;
        checkpointed = true;
        checkpointScore = scoreCount;
    }
    public void buttonClicked(){

        
        /*startButton.enabled = true;*/
        if(gameStatus == "start"){
            scoreCount = 0;
            runningScoreCanvas.SetActive(true);
            Time.timeScale = 1;
            gameOverCanvas.SetActive(false);
            tutCanvas.SetActive(false);
            if(checkpointed == false){
            checkpointButton.SetActive(false);
        }
            }else if(gameStatus == "end"){
                Replay();
            }
        }
    public void GameOver(){
        gameOverAudio.Play();
        Time.timeScale = 0;
        gameStatus = "end";
        gameOverCanvas.SetActive(true);
        gameOver.SetActive(true);
        runningScoreCanvas.SetActive(false);
        if(checkpointed == false){
            checkpointButton.SetActive(false);
        }else{
            checkpointButton.SetActive(true);
        }
    }

    public void AddPoint(){
        scoreCount ++;
        pointAudio.Play();
        score2.text = scoreCount.ToString();
        score.text = scoreCount.ToString();
    }

    public void Replay(){
        SceneManager.LoadScene(0);
    }

    public void OgSong(){
        if(myMusic.isPlaying){
            myMusic.Stop();
        }
        myMusic.clip = og;
        myMusic.Play();
    }

    public void SlowBeat(){
        if(myMusic.isPlaying){
            myMusic.Stop();
        }
        myMusic.clip = slow;
        myMusic.Play();
    }

    public void FasterBeat(){
        if(myMusic.isPlaying){
            myMusic.Stop();
        }
        myMusic.clip = fast;
        myMusic.Play();
    }

    public void Uke(){
        if(myMusic.isPlaying){
            myMusic.Stop();
        }
        myMusic.clip = uke;
        myMusic.Play();
    }

    public void PlayPause(){
        if(myMusic.isPlaying){
            myMusic.Pause();
            playButton.sprite = pause;
        }else{
            myMusic.Play();
            playButton.sprite = play;
        }
    }
}
