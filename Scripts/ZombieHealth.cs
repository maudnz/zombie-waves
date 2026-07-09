
using UnityEngine;
using UnityEngine.UI;


public class ZombieHealth : MonoBehaviour
{
   
    public AudioSource audioSource;
    public AudioClip spawn;
    public AudioClip zombieHit;
    public AudioClip death;
    //public AudioClip headshot_kill;

    private Slider zombieHitSlider;

    private Animator animator;

    private int zombieHealth = 100;
    private float sliderInterval = 0;

    PlayerManager player2;

    private bool isCollision = false;

    GameManager game2;



    void Start()
    {
        audioSource.PlayOneShot(spawn);

        zombieHealth = 100;

        animator = GetComponent<Animator>();


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject sliderFind = GameObject.FindGameObjectWithTag("ZombieHealthSlider");
        GameObject game = GameObject.FindGameObjectWithTag("GameManager");

        bool nonull = (player != null) && (sliderFind != null) && (game != null);
        if (nonull)
        {
            player2 = player.GetComponent<PlayerManager>();

            //passando true come parametro, mi trova anche componenti in gameobject figli disattivati
            zombieHitSlider = sliderFind.GetComponentInChildren<Slider>(true);

            game2 = game.GetComponent<GameManager>();
        }

    }


    void Update()
    {
        if (isCollision)
        {
            sliderInterval += Time.deltaTime;

            if (sliderInterval >= 2f)
            {
                zombieHitSlider.gameObject.SetActive(false);
                isCollision = false;
                sliderInterval = 0;
            }
        }
    }


    private bool isDead = false;
    public void ChangeHealth()
    {
        if (isDead) return;

        if (zombieHealth > 0)
        {
            zombieHealth -= 34;
            audioSource.PlayOneShot(zombieHit);
            animator.SetTrigger("Hit");

            zombieHitSlider.value = zombieHealth;
            ShowZombieSlider();

            // Controllo reale della morte
            if (zombieHealth <= 0)
            {
                isDead = true;
                Died();
            }
        }
    }
    private void Died()
    {
        animator.SetTrigger("death");
        audioSource.PlayOneShot(death);
        ShowZombieSlider();
        player2.AddKillCredits();
        game2.IncreaseScore();
        Destroy(gameObject, 2f);
    }


    private void ShowZombieSlider()
    {
        zombieHitSlider.gameObject.SetActive(true);
        isCollision = true;
    }


    public float GetHealth ()
    {
        return zombieHealth;
    }
}