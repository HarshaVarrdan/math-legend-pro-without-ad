using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDestination : MonoBehaviour
{
    [SerializeField]
    Vector3 DestinationVector;
    [SerializeField]
    float Speed;

    Transform StartPosition;
    //Transform Destination;
    float Counter;
    //float Endtime;
    bool Active;

    private void Awake()
    {
        Vector3 temp = transform.position;
        StartPosition = transform;
        Active = false;
        DestinationVector += temp;
    }

    private void Update()
    {
        //Debug.Log(transform.position);
        //Debug.Log(Destination.position);
        if (Active && transform.position != DestinationVector)
        {
            Counter += Time.deltaTime;
            float LerpPosition = Counter / Speed;
            //Debug.Log(LerpPosition);
            transform.position = Vector3.Lerp(StartPosition.position, DestinationVector, LerpPosition);
        }
    }

    public void StartMovement()
    {
        Active = true;
        Counter = 0;
        //Endtime = Time.deltaTime + Speed;
    }
}
