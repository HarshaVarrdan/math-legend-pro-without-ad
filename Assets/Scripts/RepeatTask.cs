using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RepeatTask : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnRepeatTask;
    [Space]
    [SerializeField]
    private int RepeatTimes;
    [Space]
    [SerializeField]
    private UnityEvent Oncomplete;

    private int TaskCounter = 0;

    private void Start()
    {
        TaskCounter = 0;
    }

    public void StartTask()
    {
        TaskCounter++;
        OnRepeatTask.Invoke();
        Debug.Log("Started");
    }

    public void CompleteTask()
    {
        if(TaskCounter != RepeatTimes)
        {
            Invoke("StartTask", 1f);
            Debug.Log("Invoked");
        }     
        else
            Oncomplete.Invoke();
    }
}
