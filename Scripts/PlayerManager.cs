using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour 
{

    private int playerBullets = 50;
    private int playerCredits = 0;

    public Slider playerHealthSlider;
    public GameObject gameoverOverlay;

    private GameObject game;
    private GameManager game2;

    public AudioSource audioSource;
    public AudioClip playerHit;
    public AudioClip gameover;



    void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager");
        game2 = game.GetComponent<GameManager>();
    }


    // Questa funzione verrà chiamata quando lo zombie attacca il giocatore (controlla se viene verito o ucciso)
    private bool isDead = false;
    public void UpdateHealth()
    {
        if (isDead) return;

        if (playerHealthSlider.value > 0)
        {
            playerHealthSlider.value -= 34;
            audioSource.PlayOneShot(playerHit);

            if (playerHealthSlider.value <= 0)
            {
                isDead = true;
                Died();
            }
        }
    }
    private void Died()
    {
        audioSource.PlayOneShot(gameover);
        gameoverOverlay.SetActive(true);
        game2.IsGameOver();
        game2.StopGame();
    }


    public int  GetPlayerCredits()
    {
        return playerCredits;
    }

    public void Pay()
    {
        playerCredits -= 100;
    }
    public void SetPlayerBullets()
    {
        playerBullets += 50;
    }

    public void SetMaxPlayerHealth()
    {
        playerHealthSlider.value = 100;
    }


    public void AddKillCredits()
    {
        playerCredits += 20;
    }


    public void PlayerShot ()
    {
        playerBullets--;
    }

    public int GetPlayerBullets()
    {
        return playerBullets;
    }



}
