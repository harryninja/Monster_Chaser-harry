using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveForce = 10f;
    public float jumpForce = 11f;
    private float movementx;
    private SpriteRenderer sr;
    private Rigidbody2D myBody;
    private Animator anim;
    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string JUMP_ANIMATION = "Jump";
    private string ENEMY_TAG = "Enemy";
    private bool isGrounded;


    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
    }

    private void FixedUpdate()
    {
        PlayerJump();
    }

    void PlayerMoveKeyboard()
    {
        movementx = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementx, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void AnimatePlayer()
    {

        if (movementx > 0)
        {

            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementx < 0)
        {

            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else
        {

            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        } 
        if( Input.GetButtonDown("Jump")) {
         anim.SetBool(JUMP_ANIMATION, true);
        } else {
             anim.SetBool(JUMP_ANIMATION, false);
        }
 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
           
        }

        if(collision.gameObject.CompareTag(ENEMY_TAG)) {
Destroy(gameObject);
        }
  
    }
      private void OnTriggerEnter2D(Collider2D collision)
  {
   if(collision.CompareTag(ENEMY_TAG)) {
       Destroy(gameObject);
   }   
  }
}
