using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public float moveSpeed;

    private bool canMove;
    private Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (canMove)
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0f);
        }
    }


    void OnBecameVisible()
    {
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillPlane"))
        {
            gameObject.SetActive(false);
        }
    }

    void OnEnable()
    {
        canMove = false;
    }
}
