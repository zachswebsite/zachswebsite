using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoCannon : MonoBehaviour
{
    public GameObject prefab;
    protected GameObject cannonBall;
    protected Rigidbody2D rbd;
    public Vector2 vec = new Vector2(-30,0);

    void Start()
    {
        InvokeRepeating("LaunchProjectile", 2.0f, 0.3f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void LaunchProjectile()
    {
        cannonBall = Instantiate<GameObject>(prefab);
        rbd = cannonBall.GetComponent<Rigidbody2D>();
        rbd.velocity = vec;
        Destroy(cannonBall,2.0f);
    }
}
