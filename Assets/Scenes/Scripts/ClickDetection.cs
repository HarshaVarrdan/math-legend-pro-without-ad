using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetection : MonoBehaviour
{

    [SerializeField] GameObject placinglayout;

    [SerializeField] bool isOperator;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetLayout() 
    {
        return placinglayout;
    }

    public void ToGetLayout() 
    {
        if (isOperator) 
        {
            if(this.transform.parent.GetComponent<ClickDetection>().GetLayout().transform.childCount != 0) 
            {
                this.transform.parent.GetComponent<ClickDetection>().GetLayout().transform.GetChild(0).gameObject.transform.parent = this.transform.parent.gameObject.transform;
            }
            transform.parent = this.transform.parent.GetComponent<ClickDetection>().GetLayout().transform;
        }
    }
    
}
