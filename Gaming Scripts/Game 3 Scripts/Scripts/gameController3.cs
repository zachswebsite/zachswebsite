using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
Zach Willson coded this
this is the final scene, and it just launches the player from lower right-middle of the screen to the ending platform 
*/
public class gameController3 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player1;
    public Vector2 startingPos = new Vector2(3.25f,-4.5f);
    public float maxJumpValue = .6f;
    public float jumpMultiplier = 1100;
    public float horizJumpSpeed = 200;
    private Rigidbody2D rb;
    void Start()
    {
        //player starting jump onto platform
        rb = player1.GetComponent<Rigidbody2D>();
        player1.transform.position = startingPos;
        rb.AddForce(Vector2.up * jumpMultiplier * maxJumpValue);
        rb.AddForce(Vector2.left * horizJumpSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
