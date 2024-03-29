using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    //private GameObject Gate;
    private Vector3 ClosedPosition;
    private Vector3 OpenedPosition;
    private Vector3 TargetPosition;

    public float OpenDistance;
    public float time = 1f;

    private float elapsedTime = 0.0f;

    private bool opened;
    private bool closed;

    // Start is called before the first frame update
    void Start()
    {
        opened= false;

        //float x = transform.position.x;
        //float y = transform.position.y;
        //float z = transform.position.z;
        //Debug.Log("" + x + " " + y + " " + z);

        OpenedPosition = transform.position + new Vector3(0,OpenDistance, 0);
        ClosedPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / time);

        if (transform.position != TargetPosition)
        {
            Debug.Log("Moving");
            transform.position = Vector3.Lerp(transform.position, TargetPosition, t);
        }
    }

    public void ToggleGate()
    {
        if(opened)
        {
            opened = false;
            TargetPosition = ClosedPosition;
        }
        else
        {
            opened = true;
            TargetPosition = OpenedPosition;
        }
    }
}
