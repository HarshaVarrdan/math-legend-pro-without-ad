using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [Space]
    [SerializeField]
    private UnityEvent OnSwitch;

    bool active;

    private void Start()
    {
        active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided");
        if(other.gameObject.tag == "Player" && active)
        {
            //Camera.main.gameObject.GetComponent<CameraFollow>().ChangeTarget(target);
            active= false;
            if(OnSwitch != null)
                OnSwitch.Invoke();
        }
        else
        {
            //Debug.Log("not player");
        }
    }

}
