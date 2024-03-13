using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntController : MonoBehaviour
{
    
    private bool give = false;
    private bool direct = false;
    private bool longidle = false;
    private bool idle = false;
    private Animator _animator;
    public void Start()
    {
        {

            _animator = this.GetComponent<Animator>();
        }
    }

    public void entGive()
    {
        _animator.SetBool("Give",true);
    }
    public void entDirect()
    {
        _animator.SetBool("Give",false);
        _animator.SetBool("Direct",true);
    }
    public void entLongIdle()
    {
        _animator.SetBool("Direct",false);
        _animator.SetBool("LongIdle",true);
    }
    
}
