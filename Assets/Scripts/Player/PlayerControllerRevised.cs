 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerControllerRevised : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 15.0f;

    private CharacterController characterController;
    private Animator anim;
    private BoxCollider[] swordColliders;
    //public AudioSource swordhit;
    //public AudioSource energy;
    public float delay = 1;
    private int Coins = 0;
    

    public ParticleSystem swordparticles;
    bool isGrounded = true;

    public GameObject swordfab;

    void Start()
    {

        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        swordColliders = GetComponentsInChildren<BoxCollider>();

        //swordhit.gameObject.SetActive(false);

        swordfab.gameObject.SetActive(true);
       
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
            bool isGrounded = true;
        }        
    }
    public void Attack()
    {
        anim.SetTrigger("Attack");
        //swordhit.gameObject.SetActive(true);
        //swordhit.Play();
        swordparticles.Play();
        bool isGrounded = true;
    }
    public void Jump()
    {
        anim.SetTrigger("Jump");
      
    }
    public void EndAttack()
    {
        foreach (var weapon in swordColliders)
        {
            weapon.enabled = false;
            swordparticles.Stop();
            swordfab.gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "coinnew")
        {
            Coins++;
            Destroy(collision.gameObject);
        }
    }
}
