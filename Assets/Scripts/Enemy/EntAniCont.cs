using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntAniCont : MonoBehaviour
{

    Animator animator;

    bool CanIdle;
    bool CanLongIdle1 = false;
    bool CanLongIdle2;
    bool CanWalk;
    bool IsDead;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void Idle()
    {
        CanIdle = !CanIdle;
        animator.SetBool("Idle", CanIdle);

    }
    public void LongIdle()
    {
        CanLongIdle1 = !CanLongIdle1;
        int i = 1;
        animator.SetInteger("Loop", i);
        i = ++i;
        animator.SetBool("LongIdle1", CanLongIdle1);
        
        Debug.Log(i);
        Debug.Log(CanLongIdle1);
        

    }
    public void LongIdle2()
    {
        CanLongIdle2 = !CanLongIdle2;
        animator.SetBool("LongIdle2", CanLongIdle2);
    }
    public void Attack()
    {
        
        animator.SetTrigger("Attack");
    }
    public void Damage()
    {

        animator.SetTrigger("Damage");
    }
    public void Death()
    {
        IsDead = !IsDead;
        animator.SetBool("Death", IsDead);
    }

    public void Walk()
    {
        CanWalk = !CanWalk;
        animator.SetBool("Walk", CanWalk);
    }

}
