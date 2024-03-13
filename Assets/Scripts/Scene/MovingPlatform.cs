using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public int Movedistance;
    public int interval;
    public float speed;
    public float time;

    private Vector3 Upposition;
    private Vector3 Downposition;
    private Vector3 targetposition;

    private bool active;
    private bool ismoving;

    private float elapsedTime;


    private void Start()
    {
        Upposition= transform.position;
        Downposition = transform.position + new Vector3(0, this.transform.position.y - Movedistance, 0);
    }

    private void Update()
    {
        if(active)
        {
            if(ismoving)
            {
                MoveToTargetPosition();
            }
        }
    }

    private void Moveup()
    {
        targetposition = Upposition;
        ismoving = true;
    }

    private void Movedown()
    {
        targetposition = Downposition;
        ismoving = true;
    }

    private void MoveToTargetPosition()
    {
        elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(elapsedTime / time);

        transform.position = Vector3.Lerp(transform.position, targetposition, t * speed);

        if (t >= 1f)
        {
            ismoving = false;
            if(targetposition == Upposition)
            {
                Invoke("Movedown", interval);
            }
            else
            {
                Invoke("Moveup", Random.Range(0,5));
            }
        }
    }

    public void StartMovement()
    {
        active= true;
        Movedown();
    }
}
