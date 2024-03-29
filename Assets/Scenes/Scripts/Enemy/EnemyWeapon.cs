using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " has been Hit by the Enemy");
        if (other.gameObject.tag == "Player")
        {
            HeartsController.HC_Instance.HeartsDOWN();
            ThirdPersonController.TPC_Instance.GetHit();
            Debug.Log("Enemy Trigger Hit");
        }
    }
}
