using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Actions : MonoBehaviour
{
    private CharacterController characterController;
    [SerializeField] private float moveSpeed = 15f;
    private Animator anim;
    private BoxCollider[] swordColliders;
    public Joystick joystick;
    public GameObject swordfab;
    public GameObject MainCamera;
    //public GameObject M2;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        if(joystick == null)
            joystick = FindObjectOfType<Joystick>();
        //M2.gameObject.SetActive(false);
        //MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();

        //swordhit.gameObject.SetActive(false);

        swordfab.gameObject.SetActive(true);
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float x = joystick.Horizontal;
        float y = joystick.Vertical;
        Vector3 movement;

        var rigidbody = GetComponent<Rigidbody>();
        movement = ((MainCamera.transform.forward * y * moveSpeed) + (MainCamera.transform.right * x * moveSpeed));
        movement.y = 0;
        rigidbody.velocity = movement;

        if (x != 0 && y != 0)
        {
            float targetAngle = (Mathf.Atan2(x,y) * Mathf.Rad2Deg);
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }

        if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }       
    }
    public void Menu()
    {
        SceneManager.LoadScene("NewMenu");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "card1")
        {
            //M2.gameObject.SetActive(true);
            Debug.Log("Sratte");
        }
    }
    public void loadlevel1()
    {
        SceneManager.LoadScene("one");
    }

    public void BeginAttack()
    {
        foreach (var weapon in swordColliders)
        {
            weapon.enabled = true;
            anim.SetTrigger("Attack");
            //swordhit.gameObject.SetActive(true);
            //swordhit.Play();
            swordfab.gameObject.SetActive(true);
            //bool isGrounded = true;
        }
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
        //swordhit.gameObject.SetActive(true);
        //swordhit.Play();
        //swordparticles.Play();
        //bool isGrounded = true;
    }

    public void EndAttack()
    {
        foreach (var weapon in swordColliders)
        {
            weapon.enabled = false;
            //swordparticles.Stop();
            swordfab.gameObject.SetActive(false);
        }
    }
}

