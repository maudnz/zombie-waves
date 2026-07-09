using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bloodEffectPrefab;


    // il metodo viene chiamato automaticamente quando il proiettile sbatte contro un collider
    void OnCollisionEnter(Collision hitObject)
    {
        // qui controllo se si tratta del collider di uno zombie
        if (hitObject.collider.CompareTag("Zombie"))
        {
            shot(hitObject);
        }
        else
        {
            return;
        }
    }


    void shot(Collision hitObject)
    {
        // trovo il punto esatto del contatto fisico
        ContactPoint contact = hitObject.contacts[0];

       
        if (bloodEffectPrefab != null)
        {
            //istamzio le particelle di sangue nel punto d'impatto
            GameObject sangue = Instantiate(bloodEffectPrefab, contact.point, Quaternion.identity);
            Destroy(sangue, 1.5f);
        }

        // applico il danno allo zombie colpito
        ZombieHealth zombie = hitObject.transform.GetComponent<ZombieHealth>();
        if (zombie != null)
        {
            zombie.ChangeHealth();
        }
    }

}
