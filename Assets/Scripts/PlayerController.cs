using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private float activeMoveSpeed;
    public bool canMove;

    public float jumpSpeed;

    public Rigidbody2D myRigidbody;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private bool isGrounded;
    public Animator myAnim;

    public Vector3 respawnPos;
    public LevelManager theLevelManager;
    public GameObject stompBox;

    public float knockbackForce;
    public float knockbackLength;
    private float knockbackCounter;

    public float invincibilityLength;
    private float invincibilityCount;

    public AudioSource jumpSound;
    public AudioSource hurtSound;

    private bool onPlatform;
    public float onPlatformSpeedModifier;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        respawnPos = transform.position;
        theLevelManager = FindObjectOfType<LevelManager>();
        activeMoveSpeed = moveSpeed;
        canMove = true;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (knockbackCounter <= 0 && canMove)
        {

            if (onPlatform)
            {
                activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;
            }
            else
            {
                activeMoveSpeed = moveSpeed;
            }

            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                myRigidbody.velocity = new Vector3(activeMoveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                myRigidbody.velocity = new Vector3(-activeMoveSpeed, myRigidbody.velocity.y, 0f);
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                myRigidbody.velocity = new Vector3(0f, myRigidbody.velocity.y, 0f);
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
                jumpSound.Play();
            }
        }

        if (knockbackCounter > 0) {
            knockbackCounter -= Time.deltaTime;
            if (transform.localScale.x > 0)
            {
                myRigidbody.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
            else {
                myRigidbody.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
        }

        if (invincibilityCount <= 0)
        {
            theLevelManager.invincible = false;
        }
        else
        {
            invincibilityCount -= Time.deltaTime;
        }

        Debug.Log("Speed " + myRigidbody.velocity.x + " Grounded: " + isGrounded);
        myAnim.SetFloat("Speed", Mathf.Abs(myRigidbody.velocity.x));
        myAnim.SetBool("Grounded", isGrounded);

        if (myRigidbody.velocity.y < 0)
        {
            stompBox.SetActive(true);
        }
        else
        {
            stompBox.SetActive(false);
        }
    }

    public void Knockback()
    {
        knockbackCounter = knockbackLength;
        invincibilityCount = invincibilityLength;
        theLevelManager.invincible = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player Triggered " + other);
        if (other.CompareTag("KillPlane"))
        {
            theLevelManager.Respawn();
        }

        if (other.CompareTag("Checkpoint"))
        {
            respawnPos = other.transform.position;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = other.transform;
            onPlatform = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            transform.parent = null;
            onPlatform = false;
        }
    }

}
