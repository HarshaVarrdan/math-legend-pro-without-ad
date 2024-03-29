using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisiondetect : MonoBehaviour
{

    [SerializeField] GameObject targetLayout;

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
        if(other.gameObject.tag == "Greater" || other.gameObject.tag == "Lesser" || other.gameObject.tag == "equals") 
        {
            other.gameObject.transform.parent = targetLayout.transform;
        }
    }
}
