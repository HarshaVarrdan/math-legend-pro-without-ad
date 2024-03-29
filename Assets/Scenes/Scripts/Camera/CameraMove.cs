using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{

    [SerializeField]
    private GameObject[] Position;
    [SerializeField]
    private GameObject manager;

    private GameObject target;

    private int currentposition = 0;
    private int LastPosition;

    public float moveTime = 1.0f;
    private float currentTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        LastPosition = Position.Length;
        target = Position[0];
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        float t = Mathf.Clamp01(currentTime / moveTime);

        if( this.transform.position != target.transform.position)
        {
            transform.position = Vector3.Lerp(this.transform.position, target.transform.position, t);
        }
        
    }

    public void MoveForward()
    {
        if(currentposition < (LastPosition - 1))
        {
            currentposition++;
            target = Position[currentposition];
            manager.GetComponent<LevelManager>().ChangeUI(currentposition);
            manager.GetComponent<LevelManager>().StartNext(Position[currentposition]);
            manager.GetComponent<Clock>().StartTimer();
        }
        else
        {
            Debug.Log("At Final Position");
        }
    }

    public void MoveBackward()
    {
        if (currentposition > 0)
        {
            currentposition--;
            target = Position[currentposition];
            manager.GetComponent<LevelManager>().ChangeUI(currentposition);
        }
        else
        {
            Debug.Log("At Starting Position");
        }
    }

    public int GetPosition()
    {
        return currentposition;
    }
}
