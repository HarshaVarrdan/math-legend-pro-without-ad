using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] EnemyList;
    [SerializeField]
    private Transform EnemySpawn;
    [SerializeField]
    private int EnemyCount;
    [SerializeField]
    private UnityEvent Oncomplete;

    private bool Active = false;

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        if(EnemySpawn.transform.childCount == 0 && Active)
        {
            Active = false;
            if(Oncomplete != null)
                Oncomplete.Invoke();
        }
    }

    public void SpawnEnemy()
    {
        for(int i = 0; i < EnemyCount; i++)
        {
            GameObject temp= Instantiate(EnemyList[EnemyIndex()],EnemySpawn);
            //temp.transform.parent = EnemySpawn;
            //temp.transform.position = EnemySpawn.position;
        }
        Active = true;
    }

    int EnemyIndex()
    {
        if (EnemyList.Length == 1)
            return 0;
        else
            return Random.Range(0, EnemyList.Length);
    }
}
