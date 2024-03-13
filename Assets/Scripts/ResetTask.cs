using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResetTask : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Layouts;
    [Space]
    [SerializeField]
    private UnityEvent OnReset;


    public void ResetTasks()
    {
        foreach (GameObject temp in Layouts)
        {
            foreach(Transform temp1 in temp.transform)
            {
                Destroy(temp1.gameObject);
            }
        }
        Debug.Log("Reset");

        if(OnReset != null)
            OnReset.Invoke();
    }
}
