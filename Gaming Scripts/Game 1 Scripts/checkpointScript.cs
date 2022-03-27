using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpointScript : MonoBehaviour
{
    GameObject gameManager;
    gameManagerScript gms;
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        gms = gameManager.GetComponent<gameManagerScript>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        gms.hitCheckpoint(this.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
