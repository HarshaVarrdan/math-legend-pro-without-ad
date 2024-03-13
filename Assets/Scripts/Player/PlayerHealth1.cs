using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth1 : MonoBehaviour
{

    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit = 2f;
    //[SerializeField] Slider healthSlider;

    private float timer = 0f;
    private CharacterController characterController;
    private Animator anim;
    private int currentHealth;
    private AudioSource audio;
    private ParticleSystem blood;
    //public GameObject Gameover;
   

    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value < 0)
                currentHealth = 0;
            else
                currentHealth = value;
        }
    }

    void Awake()
    {
        //Assert.IsNotNull(healthSlider);
    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();
        blood = GetComponentInChildren<ParticleSystem>();
        //Gameover.gameObject.SetActive(false);
        Time.timeScale = 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    /*void OnTriggerEnter(Collider other)
    {

        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {

            if (other.tag == "Weapon")
            {
                takeHit();
                timer = 0;
            }
        }
    }*/
    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "river")
        {
            takeHit();
        }
        if (collision.gameObject.tag == "apple")
        {
            addhealth();
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "falldownsurface")
        {
            killPlayer();            
        }

    }

    public void takeHit()
    {
        if (currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Hurt");
            currentHealth -= 10;
            //healthSlider.value = currentHealth;
            audio.PlayOneShot(audio.clip);
            blood.gameObject.SetActive(true);                     
        }

        if (currentHealth <= 0)
        {
            
            killPlayer();
        }        
    }
    public void addhealth()
    {
        if (currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            anim.Play("Attack");
            currentHealth += 10;
            //healthSlider.value = currentHealth;
            audio.PlayOneShot(audio.clip);
            
        }

        if (currentHealth == 100)
        {

            currentHealth = 100;
        }
    }

    public void Getblock()
    {
        if (currentHealth > 0)
        {
            GameManager.instance.PlayerHit(currentHealth);
            currentHealth -= 10;
            //healthSlider.value = currentHealth;
            
        }

        if (currentHealth <= 0)
        {
            
        }
    }
    void killPlayer()
    {        
        GameManager.instance.PlayerHit(currentHealth);
        characterController.enabled = false;
        audio.PlayOneShot(audio.clip);
        //Gameover.gameObject.SetActive(true);        
    }

    public void PowerUpHealth() 
    {
        if (currentHealth <= 70)
        {
            CurrentHealth += 30;
        }
        else if (currentHealth < startingHealth)
        {
            CurrentHealth = startingHealth;
        }
        //healthSlider.value = currentHealth;
    }

    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1; //Resume Game..
        }
    }

}
