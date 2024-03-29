using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdPersonController : MonoBehaviour
{
    public CharacterController _controller;
    public Transform _cam;
    public Animator _anim;
    public GameObject _sword;
    public Joystick _joystick;
    public Transform respawnAt;
    public AudioSource AudioSource;
    private AudioSource HurtAudioClip;

    public float _speed = 6;

    public float _turnsmoothtime = 0.1f;
    float turnSmoothvelocity;
    bool _canattack = true;

    public float jumpHeight = 1.0f;
    bool _canjump = false;
    bool jump = false;

    private Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 Move_Direction;
    public float mGravity = -30.0f;


    public static ThirdPersonController TPC_Instance;

    private void Awake()
    {
        
        if(TPC_Instance == null) 
        {
            TPC_Instance = this;
        }
    }

    private void Start()
    {
        _joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
        _cam = Camera.main.transform;
        HurtAudioClip = AudioSource.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        float horizontal, vertical;

        if (Input.GetAxisRaw("Horizontal") == 0 && _joystick.Horizontal != 0)
            horizontal = _joystick.Horizontal;
        else if(Input.GetAxisRaw("Horizontal") != 0 && _joystick.Horizontal == 0) 
            horizontal = Input.GetAxisRaw("Horizontal");
        else
            horizontal = 0;

        if (Input.GetAxisRaw("Vertical") == 0 && _joystick.Vertical != 0)
            vertical = _joystick.Vertical;
        else if(Input.GetAxisRaw("Vertical") != 0 && _joystick.Vertical == 0)
            vertical = Input.GetAxisRaw("Vertical");
        else
            vertical = 0;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothvelocity, _turnsmoothtime);
            transform.rotation = Quaternion.Euler(0, angle, 0);
            Vector3 movedirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            Move_Direction = movedirection;
            _controller.Move(movedirection.normalized * _speed * Time.deltaTime);
            _anim.SetBool("IsWalking", true);
        }
        else
        {
            _anim.SetBool("IsWalking", false);
        }


        if ( (Input.GetButtonDown("Jump") || jump) && _canjump)
        {
            _canjump = false;   
            Debug.LogError("Jump Called");
            jump = false;
            mVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * mGravity);
        }

        // apply gravity.
        mVelocity.y += mGravity * Time.deltaTime;
        _controller.Move(mVelocity * Time.deltaTime);
        Debug.LogWarning(mVelocity.y + " : " + _controller.isGrounded);

        if (_controller.isGrounded && mVelocity.y < 0)
        {
            _canjump = true;
            Debug.LogWarning("Player is Grounded");
            mVelocity.y = 0f;
        }
        else
        {
            _canjump = false;
            return;
        }

        
    }

    public void Attack()
    {
        if (_canattack)
        {
            _canattack = false;
            _sword.GetComponent<BoxCollider>().enabled = true;
            _anim.SetTrigger("Attack");
            Invoke("EndAttack", 1f);
        }
    }

    private void EndAttack()
    {
        _canattack = true;
        _sword.GetComponent<BoxCollider>().enabled = false;
    }

    public void GetHit() 
    {
        _anim.SetTrigger("Damage");
        HurtAudioClip.Play();
    }

    public void Jump() 
    {
        jump = true;
    }

    public Vector3 GetDirection()
    {
        return Move_Direction;
    }
    public void StopPlayer ()
    {
        _speed = 0;
    }
    
    public void StartPlayer ()
    {
        _speed = 6;
    }

    public void setRespawnPoint(Transform point) 
    {
        respawnAt = point;
    }

    public void Respawn() 
    {
        Debug.LogError("Respawn Called" + respawnAt.position);
        transform.position = new Vector3(respawnAt.position.x, respawnAt.position.y, respawnAt.position.z);
        HeartsController.HC_Instance.HeartsDOWN();
    }

    public Transform GetRepawnPoint() 
    {
        return respawnAt;
    }
}
