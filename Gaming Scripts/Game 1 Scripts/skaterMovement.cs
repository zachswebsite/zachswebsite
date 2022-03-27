using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skaterMovement : MonoBehaviour
{
   public float speed = 2;
   private float playerPoints = 0;
   public ParticleSystem rocket1;
   public ParticleSystem rocket2;
   GameObject gm;
   protected GameObject obstacle;
   Rigidbody2D rgbd;
   public GameObject prefab;
   public Animator animator;
   gameManagerScript gms;
   
    // Flap force
    public float force = 300;

    // Use this for initialization
    void Start () {    
        animator.SetBool("GameOver",false);
        gm = GameObject.FindGameObjectWithTag("GameManager");
        gms = gm.GetComponent<gameManagerScript>();
        // Fly towards the right
        //GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        rgbd = GetComponent<Rigidbody2D>();
        

    }
   
    // Update is called once per frame
    void Update () {
        // Flap
        /*rgbd.constraints = RigidbodyConstraints2D.None;*/
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space)){
            rgbd.constraints = RigidbodyConstraints2D.None;
            rgbd.constraints = RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
            animator.SetBool("Jump",true);
            rocket1.Pause();
            rocket2.Pause();
        }else if(Input.GetKey(KeyCode.LeftShift)){
            rgbd.constraints = RigidbodyConstraints2D.None;
            rgbd.constraints = RigidbodyConstraints2D.FreezeRotation;
            animator.SetBool("Ducking",true);
            rocket1.Pause();
            rocket2.Pause();
        }else if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D)){
            animator.SetBool("Station",true);
            rocket1.Play();
            rocket2.Play();
            rgbd.constraints = RigidbodyConstraints2D.FreezePositionY;
        }else if(GetComponent<Rigidbody2D>().velocity.y < 0){
            rgbd.constraints = RigidbodyConstraints2D.None;
            rgbd.constraints = RigidbodyConstraints2D.FreezeRotation;
            //going down
            animator.SetBool("Down",true);
            rocket1.Pause();
            rocket2.Pause();
        }
        else{
            rgbd.constraints = RigidbodyConstraints2D.None;
            rgbd.constraints = RigidbodyConstraints2D.FreezeRotation;
            animator.SetBool("Jump",false);
            animator.SetBool("Station",true);
            animator.SetBool("Ducking",false);
            animator.SetBool("Down",false);
            animator.SetBool("Station",false);
            rocket1.Pause();
            rocket2.Pause();
        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
        
        if(other.gameObject.name == "mario pipe"){
            Debug.Log("Game Over!");
            Debug.Log("Total Points = "+playerPoints);
            animator.SetBool("GameOver",true);
            gms.GameOver();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //player gets point
        playerPoints ++;
    }
}
