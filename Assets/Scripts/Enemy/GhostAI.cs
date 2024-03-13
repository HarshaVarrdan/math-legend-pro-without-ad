using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class GhostAI : MonoBehaviour
{


    public float lookRadius = 10f;
    Transform target;
    NavMeshAgent agent;
    private GameObject player;
    Animator animator;
    float distance;
    int idleCycle = 0;
    bool canmove = true;

    [SerializeField] GameObject WeaponCollider;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        target = player.transform;
        distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
            FaceTarget();
            canmove = true;
            if (distance <= agent.stoppingDistance)
            { 
                animator.SetBool("Fly", false);
                attack();

            }
            if (distance > agent.stoppingDistance)
            {
                canmove = true;
                Move();

            }

        }
        else
        {
            animator.SetBool("Fly", false);
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

    public void idle()
    {
        canmove = false;
        idleCycle += 1;
        if (idleCycle == 5)
        {
            StartCoroutine(LongIdle());
        }
    }

    IEnumerator LongIdle()
    {

        animator.SetBool("longIdle", true);
        yield return new WaitUntil(() => canmove == true);
        idleCycle = 0;
        animator.SetBool("longIdle", false);
        StopAllCoroutines();
    }
    
    public void attack()
    {
        animator.SetTrigger("Attack");
        int tempSel = Random.Range(1, 3);
        animator.SetInteger("AtkSel", tempSel);
    }

    public void Move()
    {
        animator.SetBool("Fly",true);
    }

    public void Damage()
    {
        animator.SetTrigger("Hit");
    }

    public void Die()
    {
       
        animator.SetBool("Dead", true);
    }

    public void EnableCollsiion() 
    {
        WeaponCollider.GetComponent<Collider>().enabled = true;
    }

    public void DisableCollision()
    {
        WeaponCollider.GetComponent<Collider>().enabled = false;
    }

    public void FindPlayer() 
    {
        player = GameObject.FindWithTag("Player");
        Debug.Log(player.name);
    }
}






