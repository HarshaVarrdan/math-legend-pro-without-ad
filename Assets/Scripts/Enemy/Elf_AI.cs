using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Elf_AI : MonoBehaviour
{

    [SerializeField] Animator elfAnimator;
    [SerializeField] NavMeshAgent NVM_Agent;

    [SerializeField] GameObject arrowPrefab;
    [SerializeField] GameObject arrowLaunchObject;
    [SerializeField] GameObject WeaponCollider;

    GameObject targetObject;

    public float mintimeGap;
    public float maxtimeGap;
    public float playerDetectRange;
    public float shootAttackRange;
    public float closeAttackRange;
    public int minAttackTime;
    public int maxAttackTime;
    public string playerTag;

    float idleTimer;
    float timer;
    float rotationSpeed = 5f;
    float distance;
    float destOffset;
    int no_of_attacks;
    bool isIdle;
    bool isDead;
    bool canAttack;
    bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        canAttack = true;
        timer = mintimeGap;
        targetObject = GameObject.FindWithTag(playerTag);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToPlayer = targetObject.transform.position - transform.position;
        directionToPlayer.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        Debug.Log(targetRotation + " " + Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime));

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

        if (isIdle)
        {
           idleTimer += Time.deltaTime;
        }

        if (canMove)
        {
            if (distance > destOffset)
            {
                NVM_Agent.SetDestination(new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, targetObject.transform.position.z));
                NVM_Agent.stoppingDistance = destOffset;

                elfAnimator.SetBool("Walk", true);
            }
            else
            {
                canMove = false;
                elfAnimator.SetBool("Walk", false);
            }
        }
    }

    private void StartAttack()
    {
        canAttack = false;

        Debug.Log("Distance : " + distance);

        if(distance <= playerDetectRange)
        { 
            if(distance <= closeAttackRange) 
            {
                CloseAttack();
                return;
            }

            int choice = UnityEngine.Random.Range(-100, 100);
            Debug.Log(choice);
            if(choice >= 0) 
            {
                no_of_attacks = UnityEngine.Random.Range(minAttackTime, maxAttackTime);
                destOffset = UnityEngine.Random.Range(shootAttackRange - 2, shootAttackRange);
                ShootAttack();
            }
            else 
            {
                CloseAttack();
            }
        }
        else
        {
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        timer = UnityEngine.Random.Range(mintimeGap, maxAttackTime);
        canAttack = true;
    }

    public void ShootAttack()
    {
        no_of_attacks -= 1; 
        if(no_of_attacks > 0) 
        {
            StartCoroutine(HasReached("Shoot"));
        }
        else 
        {
            ResetTimer();
        }
    }

    IEnumerator HasReached(string animName)
    {
        canMove = true;
        yield return new WaitWhile(() => distance > destOffset);

        elfAnimator.SetTrigger(animName);
    }

    private void CloseAttack()
    {
        destOffset = closeAttackRange;
        int rand = UnityEngine.Random.Range(0, 100);
        if(rand < 30) 
        {
            ResetTimer();
            return;
        }
        StartCoroutine(HasReached("Attack"));

    }

    public void Arrow() 
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowLaunchObject.transform.position, Quaternion.identity);
        arrow.GetComponent<Arrow_Manager>().SetParameters(targetObject.transform.position);
    }

    public void Damage() 
    {
        elfAnimator.SetTrigger("Damage");
    }

    public void Die() 
    {
        isDead = !isDead;
        elfAnimator.SetBool("Dead", isDead);
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
