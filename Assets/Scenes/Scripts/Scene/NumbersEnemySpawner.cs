using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumbersEnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _Numbers;
    [Space]
    [SerializeField]
    private GameObject[] _Enemy;
    [Space]
    [SerializeField]
    private Transform _NumberSpawn;
    [SerializeField]
    private Transform _EnemySpawn;

    GameObject _CurrentWaveNumber;

    int _NumberOfWaves;
    int _CurrentWave;

    bool _Active;

    private void Start()
    {
        _NumberOfWaves = _Numbers.Length;
        _CurrentWave = 0;
        SpawnEnemies(3);
    }

    private void Update()
    {
        if(_Active && _EnemySpawn.transform.childCount == 0 && _CurrentWave < _NumberOfWaves)
        {
            _Active = false;
            _CurrentWave++;
            Invoke("StartWave",5f);
        }
    }

    int EnemyIndex()
    {
        if (_Enemy.Length == 1)
            return 0;
        else
            return(Random.Range(0, _Enemy.Length));
    }

    void SpawnEnemies(int numbers)
    {
        for(int i = 0; i < numbers; i++)
        {
            GameObject temp = Instantiate(_Enemy[EnemyIndex()]);
            temp.transform.parent = _EnemySpawn;
        }
    }

    public void StartWave()
    {
        Destroy(_CurrentWaveNumber);
        _CurrentWaveNumber = Instantiate(_Numbers[_CurrentWave]);
        _CurrentWaveNumber.transform.position = _NumberSpawn.position;
        SpawnEnemies(_CurrentWave + 1);
        _Active = true;
    }
}