using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormBoss : MonoBehaviour
{
    [SerializeField]
    private float _MaxHealth;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private Animator _anim;
    [SerializeField]
    private GameObject _FireOrigin;

    [Space]
    [SerializeField]
    private ParticleSystem _FireBall;
    [SerializeField]
    private ParticleSystem _FireBreath;

    private bool _underground;
    private bool _idle;
    private bool _dead;
    private bool _largeAttack;
    private bool _taunt;
    private int _attack;
    private int _attackCount;
    private float _health;

    private void Start()
    {
        _health = _MaxHealth;
        if(_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
        GoUnderground();
    }

    private void Update()
    {
        if(_health < (_MaxHealth/5))
            GoUnderground();
        else if (_health < (_MaxHealth / 3))
            AttackLongSweep();
        else if (_health < 2 * (_MaxHealth / 3))
            AttackLongStraight();

        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void FixedUpdate()
    {
        
    }

    float Distance()
    {
        return Vector3.Distance(this.transform.position, _player.transform.position);
    }

    void Attack()
    {
        if (_underground)
            return;
        else
        {
            if(Distance()<10)
                Attack01();
            else if(Distance()<20)
                Attack02();
            else if(Distance()<30)
                Attack04();
        }

        Invoke("Attack", 5f);
    }

    void Attack01()
    {
        _anim.SetBool("Attack01",true);
        Invoke("StopAttack", 1f);
    }

    void Attack02()
    {
        _anim.SetBool("Attack02", true);
        Invoke("StopAttack", 1f);
    }

    void Attack03()
    {
        _anim.SetBool("Attack03", true);
        Invoke("StopAttack", 1f);
    }

    void Attack04()
    {
        _anim.SetBool("Attack04", true);
        Invoke("StopAttack", 1f);
    }

    void AttackLongStraight()
    {
        _anim.SetBool("AttackLongStraight", true);
        Invoke("StopAttack", 1f);
    }

    void AttackLongSweep()
    {
        _anim.SetBool("AttackLongSweep", true);
        Invoke("StopAttack", 1f);
    }

    void StopAttack()
    {
        _anim.SetBool("Attack01", false);
        _anim.SetBool("Attack02", false);
        _anim.SetBool("Attack03", false);
        _anim.SetBool("Attack04", false);
        _anim.SetBool("AttackLongStraight", false);
        _anim.SetBool("AttackLongSweep", false);
        _anim.SetBool("VictoryDirect", false);
        _anim.SetBool("DeathDirect", false);
    }

    void DirectionControll(GameObject _target)
    {
        Vector3 direction = _target.transform.position - transform.position;
        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f * Time.deltaTime);       
    }

    private void GoUnderground()
    {
        _underground = true;
        _anim.SetBool("Underground", true);
    }

    private void ReduceHealth(int temp)
    {
        _health -= temp;
        if (_health <= 0)
            Death();
    }

    private void Death()
    {
        _anim.SetBool("Death", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            ReduceHealth(1);
        }
    }

    public void StartBattle()
    {
        _underground = false;
        _anim.SetBool("Underground", false);
        Invoke("Attack", 10f);
    }

    public void DirectVictory()
    {
        _anim.SetBool("VictoryDirect", true);
        Invoke("StopAttack", 5f);
    }

    public void DeathDirect()
    {
        _anim.SetBool("DeathDirect", true);
        Invoke("StopAttack", 5f);
    }
}
