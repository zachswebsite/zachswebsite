using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class jumpKingScript : MonoBehaviour
{  
    public Animator animator;
    public PhysicsMaterial2D material;
    public float walkSpeed = 30;
    public AudioSource jumpSound;
    public AudioSource landSound;
    private float moveInput;
    public float velocity;
    public float yVelocity;
    public float maxWalkSpeed = 3;
    public float maxHorizontalJumpSpeed = 4;
    public bool jumping;
    public bool  isGrounded;
    public bool falling;
    public float horizJumpSpeed = 15;
    public float jumpMultiplier = 15;
    public bool chargingUpJump;
    public float maxJumpValue = 0.6f;
    public float jumpChargeDirection;
    private Rigidbody2D rb;
    public LayerMask groundMask;
    public bool canJump = true;
    public float jumpValue = 0.0f;
    public PhysicsMaterial2D bounceMat, normalMat;
    public GameController gameController;
    private Vector3 rightRotation = new Vector3(0,0,0);
    private Vector3 leftRotation = new Vector3(0,180,0);
    private BoxCollider2D boxCollider2D;
    public bool inCutscene;
    public bool startedCutscene;
    public bool deathState;
    private bool inAir;
    public float extraHeight;
    private int checkPointCount;
    public bool isPaused = false;
    public Slider overallVolumeSlider;
    public Slider musicSlider;
    public bool testing;
    /*
    Gwen did the UI coding for this script as well

    Zach Willson did the rest, player movement, collision detection,animations, scene transitions
    */
void Start()
    {
        checkPointCount = 0;
        extraHeight = 0.05f;
        jumping = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        inCutscene = false;
        if(!testing){
            transform.position = new Vector2(-2.9f,-3.65f);
        }

    }
void Update()
    {
        if(!isPaused){
        jumpChargeDirection = Input.GetAxisRaw("Horizontal");
        material = rb.sharedMaterial;
        velocity = rb.velocity.x;
        yVelocity = rb.velocity.y;
        if(transform.position.y > 15){
            if(yVelocity < -10 ){
                inCutscene = true;
            }
        }
        else if(yVelocity < -13){
            //start thought bubble UI
            inCutscene = true;
        }
        moveInput = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("xVelocity",Mathf.Abs(moveInput));

        //isGrounded = Physics2D.OverlapBox(new Vector2(gameObject.transform.position.x,
            //gameObject.transform.position.y - 0.5f),
            //new Vector2(0.9f,0.4f),0f,groundMask);
        isGrounded = isGroundedFunc();
        if(!isGrounded){
            inAir = true;
            rb.sharedMaterial = bounceMat;
            animator.SetBool("InAir",true);
        }else{
            rb.velocity = Vector3.zero;
            if(inAir){
                //play noise
                landSound.Play();
                animator.SetBool("InAir",false);
                inAir = false;
            }
        }
        if(isGrounded && inCutscene && !startedCutscene){
            animator.SetBool("deathState",true);
            Invoke("cutScene",1);
            startedCutscene = true;
        }
        if(yVelocity>0 || yVelocity < 0){
            //if still detecting wall collision as grounded, change to call ienumerator here and delay for .1 seconds and check to see if velocity still 0.
            //do nothing now, used to set grounded value

        }else{
            isGrounded = true;
            jumping = false;
        }
        
        if(Input.GetKeyDown("space") && isGrounded && canJump && !inCutscene){
            jumpValue = 0;
            chargingUpJump = true;
            animator.SetBool("ChargingJump",true);
            rb.velocity = new Vector2(0.0f,rb.velocity.y);
        }
        if(Input.GetKey("space") && isGrounded && canJump && !inCutscene && chargingUpJump){
            //here update the max jump value
            if(jumpValue < maxJumpValue){
                jumpValue += Time.deltaTime/1.5f;
            }
            //also display user UI
            gameController.DisplayUserUI();
        }
        if(Input.GetKeyUp("space") && isGrounded && canJump && !inCutscene && chargingUpJump){
            chargingUpJump = false;
            animator.SetBool("ChargingJump",false);
            isGrounded = false;
            jumping = true;
            rb.sharedMaterial = bounceMat;
            //perform add force get quick jump start, and horizontal speed start
            rb.AddForce(Vector2.up * jumpMultiplier * jumpValue);
            jumpChargeDirection = Input.GetAxisRaw("Horizontal");
            if(jumpChargeDirection != 0){
                if(jumpChargeDirection == 1){
                    //move right
                    rb.AddForce(Vector2.right * horizJumpSpeed);
                    transform.rotation = Quaternion.Euler(rightRotation);
                }
                else if(jumpChargeDirection == -1){
                    //move left
                    rb.AddForce(Vector2.left * horizJumpSpeed);
                    transform.rotation = Quaternion.Euler(leftRotation);
                }
            }
            gameController.StopDisplayingUserUI();
            //reset jumpValue
            jumpValue = 0;
            jumpSound.Play();
        }
        //if player grounded allow movement
        
        if(isGrounded && !chargingUpJump && !inCutscene){
            if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)){
                //right movement
                transform.position += Time.deltaTime * walkSpeed * Vector3.right;
                transform.rotation = Quaternion.Euler(rightRotation);
                animator.SetBool("Walking",true);
            }
            else if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)){
                //right movement
                transform.position += Time.deltaTime * walkSpeed * Vector3.left;
                transform.rotation = Quaternion.Euler(leftRotation);  
                animator.SetBool("Walking",true);             
            }else{
                animator.SetBool("Walking",false);
            }
        }else{
            animator.SetBool("Walking",false);
        }
        if(isGrounded){
            rb.sharedMaterial = normalMat;
        }else{
            rb.sharedMaterial = bounceMat;
        }
        }else{
            jumpSound.volume = overallVolumeSlider.value;
            landSound.volume = overallVolumeSlider.value;
        }

    }
    private bool isGroundedFunc(){
        if(rb.velocity.y > 0.1f){
            return false;
        }
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size-new Vector3(.1f,0,0),0f, Vector2.down,extraHeight, groundMask);
        /* Debugging visualizer for the boxcast
        Color rayColor;
        if(raycastHit.collider != null){
            rayColor = Color.green;
        }else{
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x-.1f,0), Vector2.down * (boxCollider2D.bounds.extents.y+extraHeight), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x-.1f,0), Vector2.down * (boxCollider2D.bounds.extents.y+extraHeight), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x-.1f, boxCollider2D.bounds.extents.y + extraHeight), Vector2.down * (boxCollider2D.bounds.extents.x-.2f), rayColor);
        */
        return raycastHit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log(other.name);
        if(other.name == "Checkpoint1" && checkPointCount < 1){
            gameController.hitCheckpoint();
            checkPointCount++;
        }else if(other.name == "Checkpoint2" && checkPointCount < 2){
            gameController.hitCheckpoint();
            checkPointCount++;
        }else if(other.name == "StageFinish"){
            //transition to scene2, final stage!
            gameController.StageFinished();
        }
    }

    private void cutScene(){
        gameController.displayThoughtsUI();
        startedCutscene = true;
        animator.SetBool("deathState",false);
    }


    private void OnCollisionEnter2D(Collision2D other) {
        //Debug.Log(other.GetContact(0).normal);
        if(other.GetContact(0).normal == new Vector2(0.0f,1.0f)){
            rb.sharedMaterial = normalMat;
        }else{
            rb.sharedMaterial = bounceMat;
        }
        //ContactPoint2D.normal returns 2d vector, the surface normal of other object
        //surface normal represents tangential vector, so can tell if flat ground 
    }

}
