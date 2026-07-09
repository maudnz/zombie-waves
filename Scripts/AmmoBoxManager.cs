using UnityEngine;
using UnityEngine.InputSystem;

public class AmmoBoxManager : MonoBehaviour
{

    private float distance;
    private Transform target;
    private GameObject player;
    private PlayerManager playerEquipment;


    //dall'inspector inserisco in input l'azione del controller desiderata
    public InputActionProperty vrAction;

    public AudioSource audioSource;
    public AudioClip getBullet;
    public AudioClip noMoney;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerEquipment = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();

        if (player != null)
        {
            target = player.transform;
        }
    }


    void Update()
    {
        if (!target) return;

        distance = Vector3.Distance(transform.position, target.position);


        if (distance <= 5f)
        {
            GetComponentInChildren<Light>().intensity = 7f;

            if (Input.GetKeyDown(KeyCode.E) || vrAction.action.WasPressedThisFrame()) 
            {
                if (playerEquipment.GetPlayerCredits()>=100)
                {
                    playerEquipment.SetPlayerBullets();
                    playerEquipment.Pay();
                    audioSource.PlayOneShot(getBullet);
                }
                else
                {
                    audioSource.PlayOneShot(noMoney);
                }
            }
        }
        else
        {
            GetComponentInChildren<Light>().intensity = 0;
        }

    }

}
