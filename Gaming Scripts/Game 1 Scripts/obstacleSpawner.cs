using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacleSpawner : MonoBehaviour
{
    public GameObject player;
    public float timeToSpawn = 3f;
    public float spawnDistance = 5;
    private float timer = 0;
    private float spawnCount = 1;
    public float height = 1;
    public GameObject prefab;
    protected GameObject obstacle;
    // Start is called before the first frame update
    void Start()
    {
       // obstacle = Instantiate<GameObject>(prefab);
       // obstacle.transform.position = transform.position + new Vector3(0,Random.Range(-height,height)-0.5f,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > timeToSpawn){
            spawnCount++;
            if(spawnCount >= 45){
                if(player.transform.position.x >210){
                obstacle = Instantiate<GameObject>(prefab);
                obstacle.transform.position = transform.position + new Vector3((spawnCount*(spawnDistance-2))+80,Random.Range(-height-0.5f,height+0.5f)-0.5f,0);
                }else{
                    spawnCount --;
                }
            }else if(spawnCount >= 20){
                if(player.transform.position.x > 115){
                    obstacle = Instantiate<GameObject>(prefab);
                    obstacle.transform.position = transform.position + new Vector3((spawnCount*(spawnDistance-1.5f))+60,Random.Range(-height-0.4f,height+0.4f)-0.5f,0);
                }else{
                    spawnCount --;
                }
            }else if(spawnCount >= 11){
                obstacle = Instantiate<GameObject>(prefab);
                obstacle.transform.position = transform.position + new Vector3((spawnCount*(spawnDistance-1))+40,Random.Range(-height-0.3f,height+0.3f)-0.5f,0);
            }else if(spawnCount >= 4){
                obstacle = Instantiate<GameObject>(prefab);
                obstacle.transform.position = transform.position + new Vector3((spawnCount*(spawnDistance-0.5f))+20,Random.Range(-height-0.2f,height+0.2f)-0.5f,0);  
                //Destroy(obstacle,50); 
            }else{
                obstacle = Instantiate<GameObject>(prefab);
                obstacle.transform.position = transform.position + new Vector3(spawnCount*spawnDistance,Random.Range(-height,height)-0.5f,0);
            }
        timer = 0;
        }
    timer += Time.deltaTime;
    }
}
