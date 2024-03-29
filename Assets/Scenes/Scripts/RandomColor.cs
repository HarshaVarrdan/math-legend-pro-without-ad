using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    void Start()
    {
        Renderer rendererComponent = GetComponent<Renderer>();

        if (rendererComponent != null)
        {
            Color randomColor = Random.ColorHSV();
            rendererComponent.material.color = randomColor;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
    }
}
