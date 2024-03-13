using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Dragon_AI : MonoBehaviour
{

    [SerializeField] float destOffset;

    [SerializeField] GameObject flameEmitterParent;
    [SerializeField] GameObject FlamePrefab;
    
    [SerializeField] NavMeshAgent NVM_Agent;
    [SerializeField] Animator dragonAnimator;

    [SerializeField] UnityEvent onCloseAttack;
    [SerializeField] UnityEvent onFlameAttack;
    
    GameObject targetObject;

    public float mintimeGap;
    public float maxtimeGap;
    public float attackRange;
    public float flameTiming;
    public string playerTag;

    bool canAttack;
    bool canMove;
    bool isFlying = false;
    bool isDead = false;
    float rotationSpeed = 5f;
    float timer;
    float distance;
    float nextTimeGap;

    // Start is called before the first frame update
    void Start()
    {
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

        /*crossProduct = Vector3.Cross(transform.forward, directionToPlayer);
        float turnSide = Mathf.Sign(crossProduct.y);

        if(crossProduct.y > 0f) 
        {
            dragonAnimator.SetBool("canTurn", true);
            if (turnSide > 0)
            {
                dragonAnimator.SetTrigger("TurnR");
            }
            else if (turnSide < 0)
            {
                dragonAnimator.SetTrigger("TurnL");
            }
        }
        else 
        {
            dragonAnimator.SetBool("canTurn", false);
        }*/

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

                dragonAnimator.SetBool("Walk", true);
            }
            else 
            {
                canMove = false;
                dragonAnimator.SetBool("Walk", false);
            }
        }
    }

    public void StartAttack() 
    {
        canAttack = false;
        nextTimeGap = Random.Range(mintimeGap, maxtimeGap + 1f);

        Debug.Log("Timer new Time : " + nextTimeGap);

        timer = nextTimeGap;

        Debug.Log("Distance : " + distance);

        if (distance < attackRange)
        {

            int rand = Random.Range(100, -100);

            if (rand < 0)
            {
                CloseAttack();
            }
            else if(rand > 0) 
            {
                FlameAttack();
            }
            else 
            {
                ResetTimer();
            }
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

        dragonAnimator.SetTrigger("CloseAttack");
        onCloseAttack.Invoke();
        canAttack = true;

    }

    public void FlameAttack()
    {
        StartCoroutine(EmitFlame());
        dragonAnimator.SetTrigger("FlameAttack");
        onFlameAttack.Invoke();
    }

    public IEnumerator EmitFlame() 
    {
        GameObject flame = Instantiate(FlamePrefab, flameEmitterParent.transform);

        yield return new WaitForSeconds(flameTiming);
        dragonAnimator.SetTrigger("EndFlameAttack");
        Destroy(flame);
        canAttack = true;
    }

    public void Fly() 
    {
        isFlying = !isFlying;
        dragonAnimator.SetBool("Fly", isFlying);
    }

    public void Die() 
    {
        isDead = !isDead;
        dragonAnimator.SetBool("Dead", isDead);
    }

    public void Damage()
    {
        dragonAnimator.SetTrigger("Damage");
    }

    public void FindPlayer() 
    {
        if (targetObject == null) targetObject = GameObject.FindWithTag(playerTag);
    }
}
