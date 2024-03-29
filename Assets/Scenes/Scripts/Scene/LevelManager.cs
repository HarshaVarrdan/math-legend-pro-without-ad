using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] _TasksUI;

    public void ChangeUI(int temp)
    {
        foreach (var task in _TasksUI)
        {
            task.SetActive(false);
        }
        _TasksUI[temp].SetActive(true);
    }

    public void StartNext(GameObject temp)
    {
        temp.GetComponentInParent<Operations>().SpawnAll();
    }
}
