using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camarota : MonoBehaviour
{

    public float rotationSpeed = 10f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotationAmount = -touch.deltaPosition.x * rotationSpeed * Time.deltaTime;
                transform.Rotate(Vector3.up, rotationAmount, Space.World);
            }
        }
    }

}