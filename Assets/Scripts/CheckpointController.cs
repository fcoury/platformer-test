using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public Sprite flagClosed;
    public Sprite flagOpen;

    private SpriteRenderer theSpriteRenderer;

    public bool checkPointActive;

    void Start()
    {
        theSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            theSpriteRenderer.sprite = flagOpen;
            checkPointActive = true;
        }
    }
}
