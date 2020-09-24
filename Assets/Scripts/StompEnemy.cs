using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompEnemy : MonoBehaviour
{
    public GameObject deathExplosion;
    public float bounceForce;
    public Rigidbody2D playerRigidbody;

    void Start()
    {
        playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            Instantiate(deathExplosion, other.transform.position, other.transform.rotation);
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, bounceForce, 0);
        }
    }
}
