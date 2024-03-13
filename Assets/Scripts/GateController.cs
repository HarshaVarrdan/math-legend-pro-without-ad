using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] GameObject rGate;
    [SerializeField] GameObject lGate;

    [SerializeField] float animTime;
    [SerializeField] Quaternion rTargetRotation;
    [SerializeField] Quaternion lTargetRotation;

    bool isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        rTargetRotation = rGate.transform.rotation * Quaternion.Euler(180, 180, 90);
        lTargetRotation = lGate.transform.rotation * Quaternion.Euler(180, 180, 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            rGate.transform.rotation = Quaternion.Lerp(rGate.transform.rotation, rTargetRotation, animTime * Time.deltaTime);
            lGate.transform.rotation = Quaternion.Lerp(lGate.transform.rotation, lTargetRotation, animTime * Time.deltaTime);
            if (Quaternion.Angle(rGate.transform.rotation, rTargetRotation) < 0.1f && Quaternion.Angle(lGate.transform.rotation, lTargetRotation) < 0.1f)
            {
                // Stop rotating
                isRotating = false;
                Debug.Log("Rotation False");
            }
        }
    }

    public void TriggerAnimation()
    {
        isRotating = true;
    }
}
