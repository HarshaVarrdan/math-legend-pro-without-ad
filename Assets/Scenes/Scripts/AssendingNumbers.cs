using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

public class AssendingNumbers : MonoBehaviour
{
    [SerializeField]
    private GameObject _Layout;
    [SerializeField]
    private GameObject _Numbers;
    [SerializeField]
    private GameObject _BackgroundCube;

    [SerializeField]
    private Transform _respawnPoint;

    public bool _Decending;
    public bool _SpawnNumbers;

    public int _PatternLength;
    public int _ValueLimit;

    private int[] _pattern;
    private int[] _patternAnswers;

    public Color _CorrectColor;

    [Space]
    [SerializeField]
    private UnityEvent ExecuteOnComplete;
    [SerializeField]
    private UnityEvent ExecuteOnWrong;

    void GeneratePatters()
    {
        Array.Resize(ref _pattern, _PatternLength);
        for (int i = 0; i < _PatternLength; i++)
        {
            _pattern[i] = Random.Range(1, _ValueLimit);
        }
    }

    public void SpawnPattern()
    {
        GeneratePatters();

        ThirdPersonController.TPC_Instance.setRespawnPoint(_respawnPoint);

        ProgressIndicator.PI_Instance.RemoveTaskGB(this.gameObject);

        for (int i = 0; i < _pattern.Length; i++)
        {
            GameObject temp = Instantiate(_Numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
            temp.GetComponent<Numbers>().SetNumber(_pattern[i]);
            temp.transform.parent = _Layout.transform;
            temp.transform.rotation = _Layout.transform.rotation;
        }
    }
    
    public void Check()
    {
        int j = 0;
        bool _correct = true;

        if (_SpawnNumbers)
            Array.Resize(ref _patternAnswers, _PatternLength);
        else
            Array.Resize(ref _patternAnswers, _Layout.transform.childCount);

        foreach (Transform child in _Layout.transform)
        {
            _patternAnswers[j] = child.gameObject.GetComponent<Numbers>().GetNumber();
            j++;
        }

        for (int i = 0; i < (_patternAnswers.Length-1); i++)
        {
            if(_Decending)
            {
                if (_patternAnswers[i] <= _patternAnswers[i + 1])
                {
                    //Debug.Log(_patternAnswers[i - 1] + " " + _patternAnswers[i] + (_patternAnswers[i - 1] <= _patternAnswers[i]));
                    //Debug.Log("ok");
                    _correct = true;
                    _BackgroundCube.GetComponent<Renderer>().material.color = _CorrectColor;
                }
                else
                {
                    //Debug.Log("NOT ok");
                    _correct = false;
                    break;
                }
            }
            else
            {
                if (_patternAnswers[i] >= _patternAnswers[i + 1])
                {
                    //Debug.Log(_patternAnswers[i - 1] + " " + _patternAnswers[i] + (_patternAnswers[i - 1] <= _patternAnswers[i]));
                    //Debug.Log("ok");
                    _correct = true;
                    _BackgroundCube.GetComponent<Renderer>().material.color = _CorrectColor;

                }
                else
                {
                    //Debug.Log("NOT ok");
                    _correct = false;
                    break;
                }
            }
        }

        if (_correct)
        {
            ExecuteOnComplete.Invoke();
        }
        else
        {
            Debug.Log("Wrong");
            ExecuteOnWrong.Invoke();
        }
    }
}
