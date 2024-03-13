using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroVideoController : MonoBehaviour
{

    public float waitTime;

    public int nextLevelIndex;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() 
    {
        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(nextLevelIndex);
    }

}
