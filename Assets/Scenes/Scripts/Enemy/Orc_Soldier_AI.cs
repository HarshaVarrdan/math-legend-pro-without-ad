using UnityEngine;
using UnityEngine.AI;


public class OrcSoldierAI : MonoBehaviour
{
    private static readonly int _canRun = Animator.StringToHash("CanRun");
    private static readonly int _canAttack = Animator.StringToHash("CanAttack");
    private static readonly int _attackFrenzy = Animator.StringToHash("AttackFrenzy");
    private static readonly int _attackNormal = Animator.StringToHash("AttackNormal");
    public float lookRadius = 10f;
    [SerializeField]
    private GameObject weaponCollider;

    GameObject _player;
    [SerializeField] private float[] timeBtwAnim;

    private float _timerAttack;
    private NavMeshAgent _agent;
    private Animator _animator;
    private float _distance;
    private Transform _target;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        if (_player == null)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }


    private void Update()
    {
        _target = _player.transform;
        AnimationTiming();
        _distance = Vector3.Distance(_target.position, transform.position);
        if (_distance <= lookRadius)
        {
            _agent.SetDestination(_target.position);
            FaceTarget();

            if (_distance <= _agent.stoppingDistance)
            {
                _animator.SetBool(_canAttack, true);
                _animator.SetBool(_canRun, false);
                Attack();
            }
            else if (_distance > _agent.stoppingDistance)
            {
                _animator.SetBool(_canAttack, false);
                _animator.SetBool(_canRun, true);
            }
        }
        else
        {
            _animator.SetBool(_canRun, false);
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void FaceTarget()
    {
        var direction = (_target.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
    }

    public void Attack()
    {
        _timerAttack += Time.deltaTime;
        if (_timerAttack >= 4f)
        {
            var attackSelection = Random.Range(1, 3);
            if (attackSelection == 1)
            {
                _animator.SetTrigger(_attackNormal);
                _timerAttack = 0;
            }

            if (attackSelection == 2)
            {
                _animator.SetTrigger(_attackFrenzy);
                _timerAttack = 0;
            }
        }
    }
    
    public void EnableCollision() 
    {
        weaponCollider.GetComponent<Collider>().enabled = true;
    }

    public void DisableCollision()
    {
        weaponCollider.GetComponent<Collider>().enabled = false;
    }

    private void AnimationTiming()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) Delay(timeBtwAnim[0]);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("orcSoldierAttack1")) Delay(timeBtwAnim[1]);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("orcSoldierAttack2")) Delay(timeBtwAnim[2]);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Die")) Delay(timeBtwAnim[3]);

        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) Delay(timeBtwAnim[4]);
    }

    private void Delay(float n)
    {
        _agent.velocity = Vector3.zero;
        _agent.isStopped = true;
        Invoke(nameof(AgentBack), n);
    }

    public void AgentBack()
    {
        _agent.isStopped = false;
    }

    public void FindPlayer()
    {
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
    }
}