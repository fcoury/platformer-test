using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    public int livesToGive;
    public LevelManager theLevelManager;

    void Start()
    {

    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            theLevelManager.AddLives(livesToGive);
            Destroy(gameObject);
        }
    }
}
