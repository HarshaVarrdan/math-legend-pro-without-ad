using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Numbers : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _Numbers;

    public int _NumberValue;

    public void SetNumber(int Temp)
    {
        _NumberValue = Temp;
        if(Temp < 10)
        {
            GameObject temp1 = Instantiate(_Numbers[Temp], this.transform.position + (new Vector3(0,0,-0.3f)),new Quaternion(0,0,0,1));
            temp1.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp1.transform.parent = this.transform;
            //Debug.Log(Temp);
        }
        if(Temp >= 10 && Temp < 100)
        {
            //Debug.Log(Temp + " " + Temp / 10 + " " + Temp % 10);
            GameObject temp1 = Instantiate(_Numbers[Temp / 10], this.transform.position + (new Vector3(0.35f, 0, -0.3f)), new Quaternion(0, 0, 0, 1));
            GameObject temp2 = Instantiate(_Numbers[Temp % 10], this.transform.position + (new Vector3(-0.35f, 0, -0.3f)), new Quaternion(0, 0, 0, 1));
            temp1.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp2.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp1.transform.parent = this.transform;
            temp2.transform.parent = this.transform;
        }
        if (Temp >= 100 && Temp < 1000)
        {
            GameObject temp1 = Instantiate(_Numbers[Temp / 100], this.transform.position + (new Vector3(0.70f, 0, -0.3f)), new Quaternion(0, 0, 0, 1));
            GameObject temp2 = Instantiate(_Numbers[(Temp / 10) % 10], this.transform.position + (new Vector3(0.0f, 0, -0.3f)), new Quaternion(0, 0, 0, 1));
            GameObject temp3 = Instantiate(_Numbers[(Temp % 100) % 10], this.transform.position + (new Vector3(-0.70f, 0, -0.3f)), new Quaternion(0, 0, 0, 1));
            temp1.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp2.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp3.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp1.transform.parent = this.transform;
            temp2.transform.parent = this.transform;
            temp3.transform.parent = this.transform;
        }
        if(Temp >= 1000) 
        {
            Debug.LogError(Temp / 1000 + " " + (Temp / 10) %100  + " " + (Temp / 100)%10 + " " + Temp % 1000 + " ");

            GameObject temp1 = Instantiate(_Numbers[Temp / 1000], this.transform.position + (new Vector3(1.00f, 0, -0.3f)), new Quaternion(0, 0, 0, 1));
            GameObject temp2 = Instantiate(_Numbers[(Temp / 10) % 100], this.transform.position + (new Vector3(0.35f, 0, -0.3f)), new Quaternion(0, 0, 0, 1));
            GameObject temp3 = Instantiate(_Numbers[(Temp / 100) % 10], this.transform.position + (new Vector3(-0.35f, 0, -0.3f)), new Quaternion(0, 0, 0, 1));
            GameObject temp4 = Instantiate(_Numbers[(Temp % 1000)], this.transform.position + (new Vector3(-1.00f, 0, -0.3f)), new Quaternion(0, 0, 0, 1));
            temp1.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp2.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp3.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp4.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            temp1.transform.parent = this.transform;
            temp2.transform.parent = this.transform;
            temp3.transform.parent = this.transform;
            temp4.transform.parent = this.transform;

        }
    }


    public int GetNumber()
    {
        //Debug.Log(_NumberValue);
        return _NumberValue;
    }
}
