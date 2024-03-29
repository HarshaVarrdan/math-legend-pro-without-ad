using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;


public class Elf_Rider_AI : MonoBehaviour
{

    [SerializeField] Animator riderAnimator;
    [SerializeField] NavMeshAgent NVM_Agent;
    [SerializeField] GameObject WeaponCollider;

    [SerializeField] UnityEvent onAttack;

    GameObject targetObject;

    public float mintimeGap;
    public float maxtimeGap;
    public float playerDetectRange;
    public float closeAttackOffset;
    public float runningSpeed;
    public float walkSpeed;
    public bool canStopInBetweenContinousAttack;
    public string playerTag;

    float timer;
    float rotationSpeed = 5f;
    float distance;
    float destOffset;
    bool isDead;
    bool isStanding;
    bool canAttack;
    bool canMove;
    bool run;
    bool walk;
    bool isFirst;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        canAttack = true;
        timer = mintimeGap;
        isFirst = true;
        targetObject = GameObject.FindWithTag(playerTag);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = targetObject.transform.position - transform.position;
        directionToPlayer.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        distance = Vector3.Distance(targetObject.transform.position, transform.position);
        Debug.Log("Distance bw player : " + distance);
        if (canAttack)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else if (timer <= 0)
            {
                StartAttack();
            }
        }

        if (canMove)
        {
            NVM_Agent.SetDestination(new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z));
            NVM_Agent.stoppingDistance = destOffset;

            if (walk)
            {
                NVM_Agent.speed = walkSpeed;
            }
            else
            {
                NVM_Agent.speed = runningSpeed;
            }

            riderAnimator.SetBool("Walk", walk);
            riderAnimator.SetBool("Run", run);

        }
        else
        {
            run = false;
            walk = false;
            riderAnimator.SetBool("Walk", walk);
            riderAnimator.SetBool("Run", run);
            canMove = false;
        }
    }

    private void StartAttack()
    {
        canAttack = false;

        Debug.Log("Distance : " + distance);

        if (distance <= playerDetectRange)
        {
            Debug.Log("Called");
            CloseAttack();
        }
    }

    IEnumerator HasReached()
    {
        if (distance > destOffset) canMove = true;
        yield return new WaitWhile(() => distance > destOffset);
        canMove = false;
        onAttack.Invoke();
        riderAnimator.SetTrigger("Attack");
    }

    private void CloseAttack()
    {
        destOffset = closeAttackOffset;
        int rand = UnityEngine.Random.Range(0, 100);
        if (!isFirst)
        {
            if (distance > closeAttackOffset && !canStopInBetweenContinousAttack)
            {
                if (rand < 50)
                {
                    Debug.Log("Attack Finished");
                    ResetTimer();
                    return;
                }
            }
            else if (distance <= closeAttackOffset && canStopInBetweenContinousAttack)
            {
                if (rand < 30)
                {
                    Debug.Log("Attack Finished");
                    ResetTimer();
                    return;
                }
            }
        }

        isFirst = false;

        if (distance > playerDetectRange / 2)
        {
            run = true;
            walk = false;
        }
        else
        {
            walk = true;
            run = false;
        }

        StartCoroutine(HasReached());

    }
    private void ResetTimer()
    {
        timer = UnityEngine.Random.Range(mintimeGap, maxtimeGap);
        Debug.Log("New Timer : " + timer);
        canAttack = true;
        run = false;
        walk = false;
        isFirst = true;
    }

    public void Die()
    {
        isDead = !isDead;
        if (isDead)
        {
            canAttack = false;
            canMove = false;
        }
        else canAttack = true;
        riderAnimator.SetBool("isDead", isDead);
    }

    public void Stand()
    {
        isStanding = !isStanding;
        riderAnimator.SetBool("Stand", isStanding);
    }

    public void Damage()
    {
        riderAnimator.SetTrigger("Damage");
    }

    public void Victory()
    {
        riderAnimator.SetTrigger("Victory");
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
        if (targetObject == null) targetObject = GameObject.FindWithTag(playerTag);

    }
}

