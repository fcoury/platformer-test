using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public float waitToRespawn;
    public PlayerController thePlayer;
    public GameObject deathExplosion;

    public int coinCount;
    public int healthCount;
    public int bonusLifeThreshold = 100;
    private int coinBonusCount;
    public AudioSource coinSound;

    public Text coinText;
    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int maxHealth;

    private bool respawning;
    public ResetOnRespawn[] objectsToReset;
    public bool invincible;
    public int startingLives;
    public int currentLives;
    public Text livesText;
    public AudioSource levelMusic;
    public AudioSource gameOverMusic;

    public GameObject gameOverScreen;

    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
        coinText.text = "COINS: 0";
        healthCount = maxHealth;
        objectsToReset = FindObjectsOfType<ResetOnRespawn>();
        currentLives = startingLives;
        livesText.text = currentLives.ToString();
    }

    void Update()
    {
        if (healthCount <= 0 && !respawning)
        {
            respawning = true;
            Respawn();
        }
    }

    public void Respawn()
    {
        currentLives--;
        livesText.text = currentLives.ToString();

        if (currentLives > 0)
        {
            StartCoroutine(RespawnCo());
        }
        else
        {
            levelMusic.Stop();
            gameOverMusic.Play();
            thePlayer.gameObject.SetActive(false);
            gameOverScreen.gameObject.SetActive(true);
        }
    }

    public IEnumerator RespawnCo()
    {
        thePlayer.gameObject.SetActive(false);
        Instantiate(deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation);

        yield return new WaitForSeconds(waitToRespawn);

        thePlayer.transform.position = thePlayer.respawnPos;
        healthCount = maxHealth;
        respawning = false;
        UpdateHeartMeter();
        for (int i = 0; i < objectsToReset.Length; i++)
        {
            objectsToReset[i].ResetObject();
        }
        ResetCoins();
        thePlayer.gameObject.SetActive(true);
    }

    public void ResetCoins()
    {
        coinCount = 0;
        coinBonusCount = 0;
        coinText.text = "COINS: " + coinCount;
    }

    public void AddCoins(int coinsToAdd)
    {
        coinCount += coinsToAdd;
        coinBonusCount += coinsToAdd;
        coinSound.Play();
        coinText.text = "COINS: " + coinCount;
        if (coinBonusCount >= bonusLifeThreshold)
        {
            AddLives(1);
            coinBonusCount -= bonusLifeThreshold;
        }
    }

    public void AddLives(int livesToAdd)
    {
        currentLives++;
        coinSound.Play();
        livesText.text = currentLives.ToString();
    }

    public void HurtPlayer(int damageToTake)
    {
        if (!invincible)
        {
            healthCount -= damageToTake;
            UpdateHeartMeter();

            thePlayer.hurtSound.Play();
            thePlayer.Knockback();
        }
    }

    public void HealPlayer(int healthToGive)
    {
        healthCount += healthToGive;
        if (healthCount > 6)
        {
            healthCount = 6;
        }
        coinSound.Play();
        UpdateHeartMeter();
    }

    public void UpdateHeartMeter()
    {
        switch (healthCount)
        {
            case 6:
                heart3.sprite = heartFull;
                heart2.sprite = heartFull;
                heart1.sprite = heartFull;
                break;
            case 5:
                heart3.sprite = heartHalf;
                heart2.sprite = heartFull;
                heart1.sprite = heartFull;
                break;
            case 4:
                heart3.sprite = heartEmpty;
                heart2.sprite = heartFull;
                heart1.sprite = heartFull;
                break;
            case 3:
                heart3.sprite = heartEmpty;
                heart2.sprite = heartHalf;
                heart1.sprite = heartFull;
                break;
            case 2:
                heart3.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart1.sprite = heartFull;
                break;
            case 1:
                heart3.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart1.sprite = heartHalf;
                break;
            case 0:
                heart3.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart1.sprite = heartEmpty;
                break;
        }
    }
}
