using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FractonsGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _Number1Pos;
    [SerializeField]
    private GameObject _Number2Pos;

    [SerializeField]
    private GameObject _Number;
    [SerializeField]
    private GameObject[] _Object;

    [SerializeField]
    private GameObject _layout1;
    [SerializeField]
    private GameObject _layout2;
    [SerializeField]
    private GameObject _Answerlayout;

    [SerializeField]
    private int _MaxSize;

    private int _number1;
    private int _number2;

    [Space]
    [SerializeField]
    private UnityEvent ExecuteOnComplete;
    [SerializeField]
    private UnityEvent ExecuteOnWrong;

    [SerializeField]
    private Transform _respawnPoint;

    private void GenerateNumbers()
    {
        _number1 = Random.Range(0, _MaxSize-1);
        _number2 = _MaxSize - _number1;
        Debug.Log(_number1 + "  " + _number2);
    }

    public void StartTask()
    {
        GenerateNumbers();

        ThirdPersonController.TPC_Instance.setRespawnPoint(_respawnPoint);

        ProgressIndicator.PI_Instance.RemoveTaskGB(this.gameObject);


        GameObject Temp1 = Instantiate(_Number, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
        Temp1.GetComponent<Flexalon.FlexalonInteractable>().Draggable = false;
        Temp1.GetComponent<Numbers>().SetNumber(_number1);
        Temp1.transform.parent = _Number1Pos.transform;
        Temp1.transform.position= _Number1Pos.transform.position;
        Temp1.transform.rotation = _Number1Pos.transform.rotation;

        GameObject Temp2 = Instantiate(_Number, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
        Temp2.GetComponent<Flexalon.FlexalonInteractable>().Draggable = false;
        Temp2.GetComponent<Numbers>().SetNumber(_number2);
        Temp2.transform.parent = _Number2Pos.transform;
        Temp2.transform.position = _Number2Pos.transform.position;
        Temp2.transform.rotation = _Number2Pos.transform.rotation;

        GameObject selObject = _Object[Random.Range(0, _Object.Length)];

        for (int i = 0; i < _number1 + _number2; i++)
        {
            GameObject temp = Instantiate(_Object[Random.Range(0, _Object.Length)]);
            temp.transform.parent = _Answerlayout.transform;
        }
    }

    public void Check()
    {
        bool one = _layout1.transform.childCount == _number1; 
        bool two = _layout2.transform.childCount == _number2;

        if (one && two)
        {
            ExecuteOnComplete.Invoke();
        }
        else
        {
            ExecuteOnWrong.Invoke();
        }
    }

    public void ClearLayout()
    {
        foreach (Transform t in _layout1.transform)
        {
            t.parent = _Answerlayout.transform;
        }
        foreach (Transform t in _layout2.transform)
        {
            t.parent = _Answerlayout.transform;
        }
    }
    public void ShowAnswer()
    {
        foreach(Transform t in _Answerlayout.transform)
        {
            if(_layout1.transform.childCount != _number1) 
            {
                t.parent = _layout1.transform;
            }
            else if (_layout2.transform.childCount != _number2)
            {
                t.parent = _layout2.transform;
            }
        }
    }
}
