using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;

public class GriffinAI : MonoBehaviour
{
    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    public GameObject player;
    Animator animator;
    int RandAtk = 0;
    int FlyWalk = 0;
    float distance;
    public float[] TimeBtwAgentStop;
    public float[] TimeBtwAgentRotation;
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        target = player.transform;
        IdleAnim();
        distance = Vector3.Distance(target.position, transform.position);
        Animationtiming();

        if (distance <= lookRadius)
        {
            animator.SetBool("IsChasing", true);
            animator.SetBool("IsNear", false);


            FlyWalk = Random.Range(1, 3);
            FaceTarget();

            if (FlyWalk == 1)
            {
                RunAnim();
            }

            if (FlyWalk == 2)
            {
                FlyAnim();
            }



            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {

                animator.SetBool("CanAttack", true);
                animator.SetBool("IsNear", true);

                RandAtk = Random.Range(2, 4);
                Attacks(RandAtk);



            }

        }
        else
        {
            animator.SetBool("IsChasing", false);



        }

    }
    void FaceTarget() // auto rotation
    {
        
        Vector3 targetDir = target.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.SignedAngle(targetDir, forward, Vector3.up);
        animator.SetFloat("Turn", angle);
        if (angle < -30.0F)
        {
            print("turn left");
            print(angle);
            animator.SetInteger("CanRotate", 1);
        }
        else if (angle > 30.0F)
        {
            print("turn right");
            print(angle);
            animator.SetInteger("CanRotate", 1);
        }
        else
        {
            print("forward");
            print(angle);
            animator.SetInteger("CanRotate", 0);
        }
    }
    public void OnDrawGizmosSelected() // range indication in editor
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
    //states for animation
    public void RunAnim()
    {

        animator.SetInteger("MovementType", 1);
        // agent.isStopped = false;
    }

    public void IdleAnim()
    {
        int idle = Random.Range(1, 3);
        animator.SetInteger("IdleSelection", idle);


    }
    public void Attacks(int n)
    {
        animator.SetInteger("AttacksSelection", n);
        animator.SetInteger("IdleCycle", 2);

    }

    public void FlyAnim()
    {
        animator.SetInteger("MovementType", 2);

        if (distance <= agent.stoppingDistance || animator.GetBool("IsChasing") == false)

        {
            animator.SetBool("CanLand", true);

        }
    }

    // adjusting time btw each animation and navmesh agent activation
    public void Animationtiming() 
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Griffin_Stand"))
        {
            Delay(TimeBtwAgentStop[0]);
            StartCoroutine("NotRotate", TimeBtwAgentRotation[0]);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Griffin_Idle"))
        {
            Delay(TimeBtwAgentStop[1]);
            StartCoroutine("NotRotate", TimeBtwAgentRotation[1]);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Griffin_Attack_fly"))
        {
            Delay(TimeBtwAgentStop[2]);
            StartCoroutine("NotRotate", TimeBtwAgentRotation[2]);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Griffin_Attack_ground"))
        {
            Delay(TimeBtwAgentStop[3]);
            StartCoroutine("NotRotate", TimeBtwAgentRotation[3]);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Griffin Power Attack"))
        {
            Delay(TimeBtwAgentStop[4]);
            StartCoroutine("NotRotate", TimeBtwAgentRotation[4]);
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
        
        agent.isStopped=false;

    }

    IEnumerator NotRotate(int Seconds)
    {
        agent.angularSpeed = 0;
        yield return new WaitForSeconds(Seconds);
        agent.angularSpeed = 120;
        StopCoroutine("NotRotate");
    }

    public void FindPlayer()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
    }
}