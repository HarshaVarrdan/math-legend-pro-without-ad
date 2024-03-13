using UnityEngine;
using UnityEngine.AI;

public class OrcTankerAI : MonoBehaviour
{
    private static readonly int _canRun = Animator.StringToHash("CanRun");
    private static readonly int _canAttack2 = Animator.StringToHash("CanAttack2");
    private static readonly int _canAttack1 = Animator.StringToHash("CanAttack1");
    public float lookRadius = 10f;
    private GameObject _player;
    [SerializeField] private float[] timeBtwAnim;
    private float _timerAttack;

    [SerializeField] private GameObject weaponCollider1;

    [SerializeField] private GameObject weaponCollider2;

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
        AnimationTime();
        _distance = Vector3.Distance(_target.position, transform.position);
        if (_distance <= lookRadius)
        {
            _agent.SetDestination(_target.position);
            FaceTarget();

            if (_distance <= _agent.stoppingDistance)
            {
                _animator.SetBool(_canRun, false);
                Attack();
            }
            else if (_distance > _agent.stoppingDistance)
            {
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
            var attackSelection = Random.Range(1, 2);
            if (attackSelection == 1)
            {
                _animator.SetTrigger(_canAttack1);
                _timerAttack = 0;
            }

            if (attackSelection == 2)
            {
                _animator.SetTrigger(_canAttack2);
                _timerAttack = 0;
            }
        }
    }

    public void EnableCollision()
    {
        weaponCollider1.GetComponent<Collider>().enabled = true;
        weaponCollider2.GetComponent<Collider>().enabled = true;
    }

    public void DisableCollision()
    {
        weaponCollider1.GetComponent<Collider>().enabled = false;
        weaponCollider2.GetComponent<Collider>().enabled = false;
    }

    private void AnimationTime()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")) Delay(timeBtwAnim[0]);
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("AttackTanker")) Delay(timeBtwAnim[1]);
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Die")) Delay(timeBtwAnim[2]);
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("L_hurt")) Delay(timeBtwAnim[3]);
       
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