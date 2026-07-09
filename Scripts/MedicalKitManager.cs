using UnityEngine;
using UnityEngine.InputSystem;

public class MedicalKitManager : MonoBehaviour
{

    private float distance;
    private Transform target;
    private GameObject player;
    private PlayerManager playerEquipment;

    //dall'inspector inserisco in input l'azione del controller desiderata
    public InputActionProperty vrAction;


    public AudioSource audioSource;
    public AudioClip getHealth;
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
            GetComponentInChildren<Light>().intensity = 1f;

            if (Input.GetKeyDown(KeyCode.E) || vrAction.action.WasPressedThisFrame())
            {
                if (playerEquipment.GetPlayerCredits() >= 100)
                {
                    playerEquipment.SetMaxPlayerHealth();
                    playerEquipment.Pay();
                    audioSource.PlayOneShot(getHealth);
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
