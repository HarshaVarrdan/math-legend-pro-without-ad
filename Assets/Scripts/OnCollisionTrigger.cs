using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCollisionTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnCollisionEnterCall;
    [SerializeField]
    private UnityEvent OnCollisionExitCall;
    [SerializeField]
    private string CollisionTag;
    [Space]
    [SerializeField]
    private bool OnStartExecute;

    private void Start()
    {
        if(OnStartExecute)
        {
            OnCollisionEnterCall.Invoke();
            OnCollisionExitCall.Invoke();
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(OnCollisionEnterCall != null && other.gameObject.tag == CollisionTag)
            OnCollisionEnterCall.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if(OnCollisionExitCall != null && other.gameObject.tag == CollisionTag)
            OnCollisionExitCall.Invoke();
    }

    public void CageDestroy()
    {
        Invoke("Cage",2f);
    }

    void Cage()
    {
        Destroy(this.gameObject);
    }

}
