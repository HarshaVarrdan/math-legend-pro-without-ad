using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EvenOddNumbers : MonoBehaviour
{
    [SerializeField]
    private GameObject NumbersPrefab;
    [SerializeField]
    private GameObject LeftLayout;
    [SerializeField]
    private GameObject RightLayout;
    [SerializeField]
    private GameObject SpawnLayout;
    [SerializeField]
    private GameObject _showAnswerOpt;

    [SerializeField]
    private Transform _respawnPoint;

    public int NumberLimit;

    [SerializeField]
    private UnityEvent OnCompleteExecute;
    [SerializeField]
    private UnityEvent OnWrongExecute;

    private int[] NumbersList;

    public void StartTask()
    {
        ThirdPersonController.TPC_Instance.setRespawnPoint(_respawnPoint);
        ProgressIndicator.PI_Instance.RemoveTaskGB(this.gameObject);

        Debug.Log("Called");
        GeneratePattern();
        for(int i = 0; i < NumberLimit; i++)
        {
            GameObject temp = Instantiate(NumbersPrefab);
            temp.GetComponent<Numbers>().SetNumber(NumbersList[i]);
            temp.transform.parent = SpawnLayout.transform;
        }
    }

    public void Check()
    {
        bool check = true;
        if(LeftLayout.transform.childCount == 0) 
        {
            //_showAnswerOpt.SetActive(true);
            //OnWrongExecute.Invoke();
            //Debug.Log("WRONG");
            check = false;
        }
        foreach (Transform item in LeftLayout.transform)
        {
            int temp1 = item.gameObject.GetComponent<Numbers>().GetNumber();
            if(temp1%2 != 0)
                check = false;

        }

        foreach (Transform item in RightLayout.transform)
        {
            int temp1 = item.gameObject.GetComponent<Numbers>().GetNumber();
            if (temp1 % 2 == 0)
                check = false;

        }

        if(check)
        {
            OnCompleteExecute.Invoke();
        }
        else
        {
            _showAnswerOpt.SetActive(true);
            OnWrongExecute.Invoke();
            Debug.Log("WRONG");
        }

    }

    public void ClearLayout() 
    {
        foreach (Transform item in LeftLayout.transform)
        {
            item.parent = SpawnLayout.transform;
        }

        foreach (Transform item in RightLayout.transform)
        {
            item.parent = SpawnLayout.transform;
        }
    }

    public void ShowAnswer() 
    {
        foreach (Transform item in LeftLayout.transform)
        {
            int temp1 = item.gameObject.GetComponent<Numbers>().GetNumber();
            if (temp1 % 2 != 0)
                item.parent = RightLayout.transform;
        }

        foreach (Transform item in RightLayout.transform)
        {
            int temp1 = item.gameObject.GetComponent<Numbers>().GetNumber();
            if (temp1 % 2 == 0)
                item.parent = LeftLayout.transform;
        }
        foreach (Transform item in SpawnLayout.transform)
        {
            int temp1 = item.gameObject.GetComponent<Numbers>().GetNumber();
            if (temp1 % 2 == 0)
                item.parent = LeftLayout.transform;
            if (temp1 % 2 != 0)
                item.parent = RightLayout.transform;
        }

    }

    private void GeneratePattern()
    {
        int even,odd;
        int n = NumberLimit;

        if (NumberLimit % 2 == 0)
        {
            even = NumberLimit / 2;
            odd = NumberLimit / 2;
        }
        else
        {
            even = NumberLimit / 2;
            odd = (NumberLimit / 2) + 1;
        }

        Array.Resize(ref NumbersList, NumberLimit);
        for (int i = 0; i < even; i++)
        {
            int temp = Random.Range(0, 99);
            
            if (temp % 2 == 0)
                NumbersList[i] = temp;
            else
                NumbersList[i] = temp + 1;
        }

        for (int i = 0; i < odd; i++)
        {
            int temp = Random.Range(0, 99);
            if (temp % 2 == 0)
                NumbersList[i + even] = temp +1;
            else
                NumbersList[i + even] = temp;
        }
        
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            int temp = NumbersList[k];
            NumbersList[k] = NumbersList[n];
            NumbersList[n] = temp;
        }

    }

    
}

