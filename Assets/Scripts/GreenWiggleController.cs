using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenWiggleController : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public Rigidbody2D myRigidbody;
    public bool movingRight;
    public float moveSpeed;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (movingRight && transform.position.x > rightPoint.position.x)
        {
            movingRight = false;
        }

        if (!movingRight && transform.position.x < leftPoint.position.x)
        {
            movingRight = true;
        }

        if (movingRight)
        {
            myRigidbody.velocity = new Vector3(moveSpeed, myRigidbody.velocity.y, 0);
        }
        else
        {
            myRigidbody.velocity = new Vector3(-moveSpeed, myRigidbody.velocity.y, 0);
        }
    }
}
