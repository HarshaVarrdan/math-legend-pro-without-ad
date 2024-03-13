using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexSpawner : MonoBehaviour
{
    public void Spawner(GameObject _object, int _number)
    {
        for(int i = 0; i < _number; i++)
        {
            GameObject temp = Instantiate(_object);
            temp.transform.parent = this.transform;
        }
            
    }
    public void Spawner(GameObject _object , int _number, bool check)
    {
        GameObject temp = Instantiate(_object);
        temp.GetComponent<Numbers>().SetNumber(_number);
        temp.transform.parent = this.transform;
    }
}
