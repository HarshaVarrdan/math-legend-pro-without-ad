using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    public UnityEvent onFunctionExecute;

    public void triggertask()
    {
        if (onFunctionExecute != null)
        {
            onFunctionExecute.Invoke();
        }
    }
}
