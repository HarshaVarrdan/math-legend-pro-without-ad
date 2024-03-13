using UnityEngine;
using UnityEngine.AI;

public class HobbitAI : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    public GameObject player;
    Animator animator;
    float distance;
    public float[] TimeBtwAnim;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform;
        Animationtiming();
        distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            movement();
            agent.SetDestination(target.position);
            FaceTarget();

            if (distance <= agent.stoppingDistance)
            {
                animator.SetBool("Near", true);
                animator.SetBool("CanAttack", true);
                attack();

            }
            else if (distance >= agent.stoppingDistance)
            {
                
                animator.SetBool("CanAttack", false);
                
            }
       
        }
        else
        {
            animator.SetBool("Near", false);
            animator.SetBool("CanChase", false);
            idle();
            



        }

    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
   public void movement()
    {
        animator.SetBool("CanChase", true);
    }
    public void attack()
    {


            
            int atksel = Random.Range(1,4);
            animator.SetInteger("AttackSelection", atksel);
           // animator.SetInteger("IdleCycle", 2);


    }
    public void idle()
    {
        int idlesel = Random.Range(1, 3);
        animator.SetInteger("IdleSelector", idlesel);
    }
    public void Animationtiming()
    {

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hobbit_Idle_long"))
        {
            Delay(TimeBtwAnim[0]);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hobbit_Idle"))
        {
            Delay(TimeBtwAnim[1]);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dwarf Attack Animationn"))
        {
            Delay(TimeBtwAnim[2]);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hobbit_Attack"))
        {
            Delay(TimeBtwAnim[3]);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Hobbit Run Attack"))
        {
            Delay(TimeBtwAnim[3]);
        }

    }
    public void Delay(float n)
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        Invoke(nameof(Agentback), n);
    }
    public void Agentback()
    {
        agent.isStopped = false;
    }
    public void FindPlayer()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
    }
}

