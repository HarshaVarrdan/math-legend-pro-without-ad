using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlatformManager : MonoBehaviour
{

    [SerializeField] GameObject[] movingPlatforms;


    // Start is called before the first frame update
    void Start()
    {
        TriggerMovingPlatform();
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    public void TriggerMovingPlatform()
    {
        //if (movingPlatforms.Length == 0) return;
        foreach(Transform child in this.transform) 
        {
            child.gameObject.GetComponent<MovingPlatform>().StartMovement();
        }
    }
}
