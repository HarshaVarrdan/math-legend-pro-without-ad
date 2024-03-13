using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager02 : MonoBehaviour
{
     [SerializeField]
    GameObject[] _TasksUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void ChangeUI(int temp)
    {
        foreach (var task in _TasksUI)
        {
            task.SetActive(false);
        }
        _TasksUI[temp].SetActive(true);
    }
}
