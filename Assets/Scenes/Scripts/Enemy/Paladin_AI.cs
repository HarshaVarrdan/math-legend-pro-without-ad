using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Paladin_AI : MonoBehaviour
{

    [SerializeField] float destOffset;
    
    [SerializeField] NavMeshAgent NVM_Agent;
    [SerializeField] Animator paladinAnimator;

    [SerializeField] UnityEvent onAttack;

    GameObject targetObject;

    public float mintimeGap;
    public float maxtimeGap;
    public float attackRange;
    public float moveSpeed;
    public string playerTag;

    bool canAttack;
    bool canMove;
    bool isDead = false;
    float rotationSpeed = 5f;
    float timer;
    float distance;
    float nextTimeGap;

    // Start is called before the first frame update
    void Start()
    {
        NVM_Agent.speed = moveSpeed;
        canAttack = true;
        timer = maxtimeGap;
        targetObject = GameObject.FindWithTag(playerTag);
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 crossProduct = Vector3.zero;

        Vector3 directionToPlayer = targetObject.transform.position - transform.position;
        directionToPlayer.y = 0f; 

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        distance = Vector3.Distance(transform.position, targetObject.transform.position);

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
            if(distance > destOffset) 
            {
                NVM_Agent.SetDestination(new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z));
                NVM_Agent.stoppingDistance = destOffset;

                paladinAnimator.SetBool("Walk", true);
            }
            else 
            {
                canMove = false;
                paladinAnimator.SetBool("Walk", false);
            }
        }
    }

    public void StartAttack() 
    {
        canAttack = false;
        nextTimeGap = Random.Range(mintimeGap, maxtimeGap + 0.1f);

        Debug.Log("Timer new Time : " + nextTimeGap);

        timer = nextTimeGap;

        Debug.Log("Distance : " + distance);

        if (distance < attackRange && !isDead)
        {
            CloseAttack();
        }
        else { canAttack = true; }

    }

    public void ResetTimer() 
    {
        timer = maxtimeGap;
        canAttack = true;
    }

    public void CloseAttack() 
    {
        StartCoroutine(HasReached());
    }


    IEnumerator HasReached() 
    {
        canMove = true;
        yield return new WaitWhile(() => distance > destOffset);

        paladinAnimator.SetTrigger("Attack");
        onAttack.Invoke();
        canAttack = true;

    }

    public void Die() 
    {
        isDead = !isDead;
        paladinAnimator.SetBool("isDead", isDead);
    }

    public void Damage() 
    {
        paladinAnimator.SetTrigger("Damage");
    }

    public void Guard() 
    {
        paladinAnimator.SetTrigger("Guard");
    }

    public void FindPlayer()
    {
        if (targetObject == null) targetObject = GameObject.FindGameObjectWithTag(playerTag);
    }
}
