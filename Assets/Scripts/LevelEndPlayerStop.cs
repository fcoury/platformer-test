using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPlayerStop : MonoBehaviour
{
    private LevelEnd theLevelEnd;
    private PlayerController thePlayer;
    private bool triggered = false;

    void Start()
    {
        theLevelEnd = transform.parent.GetComponent<LevelEnd>();
    }

    void Update()
    {
        if (triggered)
        {
            Debug.Log("Stopping player...");
            theLevelEnd.thePlayer.myRigidbody.velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("LevelEndStop triggered by " + other);
        if (other.CompareTag("Player"))
        {
            theLevelEnd.theSpriteRenderer.sprite = theLevelEnd.openFlag;
            theLevelEnd.thePlayer.myAnim.SetBool("Celebrating", true);
            theLevelEnd.movePlayer = false;
            triggered = true;
        }
    }
}
