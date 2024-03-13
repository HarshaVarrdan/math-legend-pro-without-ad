using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Score : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Text;
    [Space]
    [SerializeField]
    private UnityEvent OnIncrease;
    [Space]
    [SerializeField]
    private UnityEvent OnDecrease;

    private int CurrentScore;
    
    // Update is called once per frame
    void Update()
    {
        Text.text = ""+CurrentScore;        
    }

    public void IncreaseScore()
    {
        CurrentScore += 1;
        if(OnIncrease != null)
            OnIncrease.Invoke();
    }
    public void IncreaseScore(int temp)
    {
        CurrentScore += temp;
        if (OnIncrease != null)
            OnIncrease.Invoke();
    }

    public void DecreaseScore()
    {
        CurrentScore -= 1;
        if (OnDecrease != null)
            OnDecrease.Invoke();
    }
    public void DecreaseScore(int temp)
    {
        CurrentScore -= temp;
        if(OnDecrease != null)
            OnDecrease.Invoke();
    }
}
