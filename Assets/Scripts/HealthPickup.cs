using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToGive;
    private LevelManager theLevelManager;

    void Start()
    {
        theLevelManager = FindObjectOfType<LevelManager>();
        Debug.Log("The level manager: " + theLevelManager);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("HealthPickup triggered " + other);
        if (other.CompareTag("Player"))
        {
            theLevelManager.HealPlayer(healthToGive);
            Destroy(gameObject);
        }
    }
}
