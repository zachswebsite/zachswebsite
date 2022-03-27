using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCode : MonoBehaviour
{
    public GameObject prefab;
    protected GameObject cannonBall;
    protected Rigidbody2D rbd;
    public Vector2 vec = new Vector2(-30,0);

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown ("Jump") == true)
        {

            // Instantiate is the method that clones your prefabs
            cannonBall = Instantiate<GameObject>(prefab);
            rbd = cannonBall.GetComponent<Rigidbody2D>();
            rbd.velocity = vec;
            Destroy(cannonBall,2.0f);
        }
    }
}
