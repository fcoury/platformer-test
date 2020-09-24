using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRotation;
    private Vector3 startLocalScale;

    private Rigidbody2D myRigidbody;

    void Start()
    {
        startPos = transform.position;
        startRotation = transform.rotation;
        startLocalScale = transform.localScale;

        if (GetComponent<Rigidbody2D>() != null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {

    }

    public void ResetObject()
    {
        transform.position = startPos;
        transform.rotation = startRotation;
        transform.localScale = startLocalScale;

        if (myRigidbody != null)
        {
            myRigidbody.velocity = Vector3.zero;
        }

        gameObject.SetActive(true);
    }
}
