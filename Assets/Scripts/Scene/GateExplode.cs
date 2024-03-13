using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateExplode : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particleSystem;

    public float Timer = 1f;

    public void OpenGate()
    {
        ParticleSystem temp = Instantiate(_particleSystem);
        temp.Play();
        Invoke("DestroyGate", Timer);
    }

    private void DestroyGate()
    {
        Debug.Log("invoked");
        this.gameObject.SetActive(false);
    }
}
