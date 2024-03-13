using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamage : MonoBehaviour
{

    [SerializeField] float DamageAmt = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Sword Hit");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "Enemy") 
        {
            other.gameObject.GetComponent<EnemyHP>().Damage(DamageAmt);
            Debug.Log("Sword Trigger Hit");
        }
    }



}
