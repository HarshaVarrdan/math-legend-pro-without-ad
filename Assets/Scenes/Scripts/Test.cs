using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    int Score = 0;
    // Start is called before the first frame update
    void Start()
    {
        Score = PlayerPrefs.GetInt("Score");
    }

    // Update is called once per frame
    void Update()
    {
        Score++;
        Debug.Log(Score);
        PlayerPrefs.SetInt("Score", Score);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
