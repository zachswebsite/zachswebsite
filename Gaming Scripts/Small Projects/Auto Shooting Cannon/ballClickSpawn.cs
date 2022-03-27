using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballClickSpawn : MonoBehaviour
{
    Camera mainCamera;
    public GameObject prefab;
    Vector3 screenToWorld;
    protected GameObject cannonBall;
    protected Rigidbody2D rbd;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Vector3 mouseClick = new Vector3(Input.mousePosition.x,Input.mousePosition.y,mainCamera.transform.position.z*-1);
            screenToWorld = mainCamera.ScreenToWorldPoint(mouseClick);

            cannonBall = Instantiate<GameObject>(prefab);
            cannonBall.transform.position = screenToWorld;
            rbd = cannonBall.GetComponent<Rigidbody2D>();
            rbd.velocity = new Vector2(Random.Range(-20.0f,20.0f),Random.Range(-20.0f,20.0f));
            Destroy(cannonBall,2.0f);
        }
    }
}
