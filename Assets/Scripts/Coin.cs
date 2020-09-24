using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public LevelManager levelManager;
    public int coinValue;

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
            levelManager.AddCoins(coinValue);
            gameObject.SetActive(false);
        }
    }
}
