using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flexalon;
using TMPro;
using Unity.VisualScripting;

public class StoneGolem_AI : MonoBehaviour
{
    [SerializeField]
    public Transform _Player;
    [SerializeField]
    public Transform _PlayerBody;
    [SerializeField]
    private float followSpeed;

    private Transform target;
    private Animator _anim;
    

    private void Start()
    {
        if(_Player == null)
        {
            FindPlayer();
        }
        ResetTarget();
    }

    private void Update()
    {
        if(target != null)
        {
            Vector3 TargetPosition = target.position;
            transform.position = Vector3.Lerp(transform.position, TargetPosition, followSpeed * Time.deltaTime);
        }

        if (transform.position != target.position)
        {
            transform.LookAt(target);
        }
        else
        {
            transform.LookAt(_PlayerBody);
        }

    }

    public void SetTarget(Transform tempTarget)
    {
        target = tempTarget;
    }

    public void ResetTarget()
    {
        target = _Player;
    }

    void FindPlayer()
    {
        if(_Player == null && GameObject.FindGameObjectWithTag("Player"))
        {
            Invoke("FindPlayer", 1f);
        }
        else if(_Player == null)
        {
            _Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    float GetLerp()
    {
        return _Player.position.magnitude;
    }

    void SetAnim(float A)
    {
        switch(A)
        {
            case 0f:
                {
                    _anim.SetBool("Float", false);
                    break;
                }
            case 1f:
                {
                    _anim.SetBool("Float", true);
                    break;
                }
            case 2f:
                {
                    _anim.SetTrigger("Cheer");
                    break;
                }
            case 3f:
                {
                    _anim.SetTrigger("Sad");
                    break;
                }
            default:
                {
                    Debug.Log("Animation Does Not Exist");
                    break;
                }
        }
    }
}
