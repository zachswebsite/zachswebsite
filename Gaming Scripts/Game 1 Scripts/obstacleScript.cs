using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleScript : MonoBehaviour
{
    GameObject gameManager;
    gameManagerScript gms;
    public float currentCheckpointX;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gms = gameManager.GetComponent<gameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
       //get rid of gameobjects once player has made last checkpoint
        if(this.transform.position.x < gms.checkXY.x-10){
            Destroy(gameObject);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        /*
        if(other.gameObject.tag == "Player"){
            Debug.Log("WE HIT GAME OVER");
            Debug.Log("Game Object: "+other.gameObject.tag);
            Debug.Log("Hit collision type: "+this.GetComponent<BoxCollider2D>().isTrigger);
            gms.GameOver();
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other) {
        gms.AddPoint();
    }
}
