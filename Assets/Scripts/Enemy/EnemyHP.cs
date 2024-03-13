using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField] float MaxHealth;

    float Health;
    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(Health <= 0)
        {
            GetComponent<Animator>().SetBool("Dead", true);
            Invoke("DestroyObject", 2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "weapon")
        {
            Health--;
            Debug.Log(Health);
        }
        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Hit");
    }

    public void Damage(float hit) 
    {
        Health -= hit;
        Debug.Log(Health);
        GetComponent<Animator>().SetTrigger("Hit");
        

    }

    private void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
