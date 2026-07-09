using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ShootingController : MonoBehaviour
{
    public GameObject bulletPrefab;

    //dall'inspector inserisco in input l'azione del controller desiderata
    public InputActionProperty vrAction; 

    private float bulletSpeed = 60f;    
    private Animator animator;

    public AudioSource audioSource;
    public AudioClip shot;
    public AudioClip noBullets;

    private float shotInterval = 1;

    private PlayerManager player;

    public TextMeshProUGUI bulletCounter;




    void Start()
    {
        animator = GetComponentInParent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
    }

    void Update()
    {

        bulletCounter.text = player.GetPlayerBullets().ToString("F0");

        shotInterval += Time.deltaTime;

        if (shotInterval >= 0.5)
        {   
            //controllo contemporaneamente se è stato premuto il tasto destro mouse o 
            if (Input.GetButtonDown("Fire1") || vrAction.action.WasPressedThisFrame())
            {
                shotInterval = 0;
                Spara();
            }
        }
    }

    private void Spara()
    {
        //Debug.Log("Hai sparato!");

        if (bulletPrefab == null) return;

        if (player.GetPlayerBullets() <= 0) {
            audioSource.PlayOneShot(noBullets);
            return;
        }

        animator.SetTrigger("Fire");
        audioSource.PlayOneShot(shot);
        player.PlayerShot();

        // La direzione del proiettile è quella del cotnroller destro SE sono in modalità VR, altrimenti è la stessa della camera
        GameObject rightController = GameObject.FindGameObjectWithTag("Right_Controller");

       Vector3 direzioneProiettile;
        if (rightController == null)
        {
           direzioneProiettile = Camera.main.transform.forward;     
        } else
        {
            direzioneProiettile = rightController.transform.forward;
        }

        // Faccio nascere il proiettile dalla canna della pistola, ma lo orientiamo come "direzioneProiettile"
        GameObject nuovoProiettile = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(direzioneProiettile));

        Rigidbody rb = nuovoProiettile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Spingo il proiettile nella "direzioneProiettile" ad una certa velocità
            rb.linearVelocity = direzioneProiettile * bulletSpeed;
        }

        Destroy(nuovoProiettile, 3f);
    }
}