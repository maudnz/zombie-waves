using UnityEngine;
using UnityEngine.AI; //NavMeshAgent

public class ZombieBehavior : MonoBehaviour
{
    

    //public float rotationSpeed = 3f;
    public float attackCooldown = 1.5f;

    private Transform target;
    private Animator animator;
    private NavMeshAgent agent;        
    private PlayerManager playerHealth;
    private GameManager game;

    private float nextAttackTime = 0f; 
    private float distance;



    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        animator.SetBool("isWalking", true);

        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        animator.SetFloat("WalkSpeed", 3f + 1/(game.GetActualWave()) );

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;

            playerHealth = player.GetComponent<PlayerManager>();
        }
    }

    void Update()

    {
        // ----------- Pathfinding ---------

        if (!target) return;

        // Dico al navmesh agent dove si trova il player
        agent.SetDestination(target.position);

       /* // Invece di fare (target.position - transform.position) in linea retta,
        // dirigo lo zombie agni frame verso il punto successivo calcolato da navmesh per evitare gli ostacoli
        Vector3 dir = agent.steeringTarget - transform.position;
        Vector3 dir = agent.nextPosition - transform.position; */


        //agent.updatePosition = false;
        //agent.updateRotation = false;


         /* dir.y = 0f;
         if (dir != Vector3.zero)
         {
             transform.rotation = Quaternion.Slerp(
                 transform.rotation,
                 Quaternion.LookRotation(dir),
                 rotationSpeed * Time.deltaTime
             );
          } */



        distance = Vector3.Distance(transform.position, target.position);
        if (distance > agent.stoppingDistance)
        {
            animator.SetBool("isWalking", true);
        }
        else
        { 
            // il giocatore è oltre la stoppingdistance
            animator.SetBool("isWalking", false);

            if (Time.time > nextAttackTime)
            {
                animator.SetTrigger("Attack");

                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }

    // Funzione chiamata direttamente dall'animazione (Animation Event)
    // se lo zombie colpisce e nel momento preciso dell'attimo preciso il player si trova ancora otlre la stoppingdistance, allora subisce il danno
    public void CheckPlayerDamage()
    {
        if (distance <= agent.stoppingDistance) 
        {
            playerHealth.UpdateHealth();
        }
    }

}