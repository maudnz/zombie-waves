using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject zombie;
    public GameObject SpawnPoint1, SpawnPoint2, SpawnPoint3, SpawnPoint4, SpawnPoint5;
    public TextMeshProUGUI waveCounter;
    public TextMeshProUGUI nextWaveTimer;
    public TextMeshProUGUI playerscore;
    public TextMeshProUGUI playerCredits;
    public GameObject pauseMenuOverlay;

    private PlayerManager player;

    private float actualWave = 1;
    private int x = 0;
    private float nextSpawn = 0f; 
    private float waveIntermissionTimer = 10f;
    private int zombieAlive;

    private int score=0;

    private bool pauseMenu = false;
    private bool isGameOver = false;


    void Start()
    {

        x = 0;
        waveIntermissionTimer = 10f;
        nextSpawn = 0f;

    }


    void Update()
    {


        // Se l'utente preme Esc mette il gioco in pausa
        // (a meno che non sia già morto; in tal caso è già stato chiamato il gameover)
        if (Input.GetKeyDown(KeyCode.Escape))
            PressEsc();



        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        waveCounter.text = "Wave: " + actualWave.ToString("F0");

        playerscore.text = "Score: " + score.ToString("F0");

        playerCredits.text = "Credits: " + player.GetPlayerCredits().ToString("F0");

        // Gestione della generazione degli zombie ad ogni ondata. Ci sono 5 punti di spawn, gli zombie vengono spawnati a gruppi di 5 ogni 10 secondi;
        // il numero di zombie generati ad ogni ondata corrisponde a (5 * numero_ondata_attuale)
        // nel punto di spawn si accende una point light come effetto per circa 2 secondi

        zombieAlive = GameObject.FindGameObjectsWithTag("Zombie").Length;

        if (zombieAlive == 0 && x >= actualWave)
        {
            nextWaveTimer.gameObject.SetActive(true);
            waveIntermissionTimer -= Time.deltaTime;

            nextWaveTimer.text = "Next Wave: " + waveIntermissionTimer.ToString("F0") + "s";

            if (waveIntermissionTimer <= 0)
            {
                actualWave++; 
                x = 0;    
                nextSpawn = 10f; 
                waveIntermissionTimer = 10f;
            }
            return;
        }

        if (x < actualWave)
        {
            nextSpawn += Time.deltaTime;

            if (actualWave == 1 && x == 0)
            {
                nextWaveTimer.gameObject.SetActive(true);
                nextWaveTimer.text = "Next Wave: " + (10f - nextSpawn).ToString("F0") + "s";
            }
            else
            {
                nextWaveTimer.gameObject.SetActive(false);
            }

            if (nextSpawn >= 10f)
            {
                SpawnZombie();
                SetLights(true);
                x++;
                nextSpawn = 0f;
            }
            else if (nextSpawn >= 2f)
            {
                SetLights(false);
            }
        }
        else
        {
            nextWaveTimer.gameObject.SetActive(false);

            if (nextSpawn < 2f)
            {
                nextSpawn += Time.deltaTime;
                if (nextSpawn >= 2f) SetLights(false);
            }
        }

    }



    void SpawnZombie()
    {
        Instantiate(zombie, new Vector3(6.7f, 0f, -38.7702675f), Quaternion.identity);
        Instantiate(zombie, new Vector3(-5f, 0f, -12.7702675f), Quaternion.identity);
        Instantiate(zombie, new Vector3(17.0101013f, 0f, -1.76939964f), Quaternion.identity);
        Instantiate(zombie, new Vector3(-22f, 0f, -29.7702675f), Quaternion.identity);
        Instantiate(zombie, new Vector3(-25f, 0f, 10f), Quaternion.identity);
    }

    void SetLights(bool state)
    {
        SpawnPoint1.SetActive(state);
        SpawnPoint2.SetActive(state);
        SpawnPoint3.SetActive(state);
        SpawnPoint4.SetActive(state);
        SpawnPoint5.SetActive(state);
    }


    public float GetActualWave()
    {
        return actualWave;
    }


    public void IncreaseScore()
    {
        score++;
    }


    private void PressEsc()
    {
        if (pauseMenu && !isGameOver) {
            ResumeGame();

        } else if (!pauseMenu && !isGameOver)
        {
            pauseMenuOverlay.SetActive(true);
            StopGame();
        }

    }


    
    public void StopGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerMovement playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMov != null)
            playerMov.enabled = false;
        GameObject.FindGameObjectWithTag("Gun_Barrel").GetComponent<ShootingController>().enabled = false;
        pauseMenu = true;
        nextWaveTimer.gameObject.GetComponent<AudioSource>().mute = true;
        //AudioListener.volume = 0f;


        NavMeshAgent[] allAgent = FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);
        foreach (NavMeshAgent agent in allAgent)
        {
            agent.isStopped = true;

            if (agent.TryGetComponent<Animator>(out Animator animator))
            {
                animator.speed = 0f;
            }
        }
    }

    public void ResumeGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerMovement playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (playerMov != null)
            playerMov.enabled = true;
        GameObject.FindGameObjectWithTag("Gun_Barrel").GetComponent<ShootingController>().enabled = true;
        pauseMenu = false;
        nextWaveTimer.gameObject.GetComponent<AudioSource>().mute = false;
        pauseMenuOverlay.SetActive(false);


        NavMeshAgent[] allAgent = FindObjectsByType<NavMeshAgent>(FindObjectsSortMode.None);
        foreach (NavMeshAgent agent in allAgent)
        {
            agent.isStopped = false;

            if (agent.TryGetComponent<Animator>(out Animator animator))
            {
                animator.speed = 1f;
            }
        }
    }

    public void IsGameOver()
    {
        isGameOver = true;
    }
}
