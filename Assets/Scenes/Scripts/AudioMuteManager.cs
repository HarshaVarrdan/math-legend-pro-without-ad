using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMuteManager : MonoBehaviour
{

    [SerializeField] AudioSource m_AudioSource;

    bool isMute = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickAudio_B()
    {
        isMute = !isMute;
        m_AudioSource.enabled = !isMute;
    }

}