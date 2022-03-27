using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public GameObject gm;
    GameControllerScript gcs;
    //this gives us access to the main script, so we
    //can pass information to and from it.
    public float speed;
    //measures left and right movement, tuned for 
    //specific sprite image of flute
    public int playerNumber;
    //number 1 for first player, 2 for 2nd player.
    private int playerLocation = 0;
    public int notesAvailable = 7;
    //this will mark the players location on the 
    //instrument, vital for passing info to gameScript

    // Start is called before the first frame update
    void Start()
    {
        playerLocation = 0;
        gcs = gm.GetComponent<GameControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if(gcs.GameRunning()){
        if(gcs.getPlayerMove() == 2 && playerNumber == 2){
            //if player 2's turn, allow movements/input
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                if(playerLocation > 0){
                    playerLocation--;
                    transform.position += Vector3.left * speed;
                }
            }

            if(Input.GetKeyDown(KeyCode.RightArrow)){
                if(playerLocation < notesAvailable){
                    playerLocation++;
                    transform.position += Vector3.right * speed;
                }
            }
            
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                gcs.AddTune(playerLocation);
            }
            if(Input.GetKeyDown(KeyCode.RightShift)){
                gcs.CoroutineTune();
            }
        }
        if(gcs.getPlayerMove() == 1 && playerNumber == 1){
            //if player 1's turn, allow movements/input
            if(Input.GetKeyDown(KeyCode.A)){
                if(playerLocation > 0){
                    playerLocation--;
                    transform.position += Vector3.left * speed;
                }
            }

            if(Input.GetKeyDown(KeyCode.D)){
                if(playerLocation < notesAvailable){
                    playerLocation++;
                    transform.position += Vector3.right * speed;
                }
            }
            
            if(Input.GetKeyDown(KeyCode.W)){
                gcs.AddTune(playerLocation);
            }
            if(Input.GetKeyDown(KeyCode.LeftShift)){
                gcs.CoroutineTune();
            }
        }
        }
    }

    public void resetLocation(){
        playerLocation = 0;
    }

}
