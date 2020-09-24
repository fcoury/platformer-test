using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    public string levelToLoad;

    public PlayerController thePlayer;
    private CameraController theCamera;
    private LevelManager theLevelManager;

    public float waitToMove, waitToLoad;
    public bool movePlayer;

    public Sprite closedFlag, openFlag;
    public SpriteRenderer theSpriteRenderer;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        theCamera = FindObjectOfType<CameraController>();
        theLevelManager = FindObjectOfType<LevelManager>();
        theSpriteRenderer = GetComponent<SpriteRenderer>();
        theSpriteRenderer.sprite = closedFlag;
    }

    void Update()
    {
        if (movePlayer)
        {
            thePlayer.myRigidbody.velocity = new Vector3(thePlayer.moveSpeed, thePlayer.myRigidbody.velocity.y, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(LevelEndCo());
        }
    }

    public IEnumerator LevelEndCo()
    {
        thePlayer.canMove = false;
        theCamera.followTarget = false;
        theLevelManager.invincible = true;
        thePlayer.myRigidbody.velocity = Vector3.zero;

        theLevelManager.levelMusic.Stop();
        theLevelManager.gameOverMusic.Play();

        yield return new WaitForSeconds(waitToMove);

        movePlayer = true;

        yield return new WaitForSeconds(waitToLoad);

        SceneManager.LoadScene(levelToLoad);
    }
}
