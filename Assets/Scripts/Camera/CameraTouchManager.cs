using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

public class CameraTouchManager : MonoBehaviour
{

    private float InitSpeedX, InitSpeedY;
    public CinemachineFreeLook cFL;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(cFL.m_XAxis.m_MaxSpeed);
        InitSpeedY = cFL.m_XAxis.m_MaxSpeed;
        InitSpeedX = cFL.m_YAxis.m_MaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0) 
        {
            foreach(Touch touch in Input.touches) 
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    cFL.m_YAxis.m_MaxSpeed = 0;
                    cFL.m_XAxis.m_MaxSpeed = 0;
                }
                else
                {
                    Debug.Log("Hovering Over UI");
                    cFL.m_YAxis.m_MaxSpeed = InitSpeedX;
                    cFL.m_XAxis.m_MaxSpeed = InitSpeedY;
                }
            }
        }
    }
}
