using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;
using UnityEngine.Events;

public class PatternGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject Numbers;
    [SerializeField]
    private GameObject Questions;
    [SerializeField]
    private GameObject Answers;
    [SerializeField]
    private int ValueLimit;
    [SerializeField]
    private int LengthLimit;
    [SerializeField]
    private int AnswerLimit;
    [SerializeField]
    private int PatternStart;

    [Space]
    [SerializeField]
    private UnityEvent ExecuteOnComplete;

    private int PatternMultiplyer;
    private int PatternLength;
    private int PatternAnswerLength;
    private int[] PatternDisplay;
    private int[] PatternAnswers;
    private int PatterncorrectAnswer;
    private int PatternLimit;

    private bool PatternOperatorAddition;
    // Start is called before the first frame update
    void Start()
    {
        PatternLength = LengthLimit;
        PatternAnswerLength = AnswerLimit;
        PatternOperatorAddition = true;
    }

    private void GeneratePatternMultiplyer()
    {
        PatternMultiplyer = Random.Range(0, PatternLength);
        //PatternOperatorAddition = Random.value > 0.5f;
        PatternOperatorAddition = true;
    }

    private void GeneratePattern()
    {
        GeneratePatternMultiplyer();
        Array.Resize(ref PatternDisplay, PatternLength);
        Array.Resize(ref PatternAnswers, PatternAnswerLength);
        if(PatternOperatorAddition)
        {
            for (int i = 0; i < PatternLength; i++)
            {
                PatternDisplay[i] = (PatternStart + (i * PatternMultiplyer));
                PatterncorrectAnswer = (PatternStart + ((i + 1) * PatternMultiplyer));
            }
        }
        else
        {
            for (int i = 0; i < PatternLength; i++)
            {
                PatternDisplay[i] = PatternStart * (i * PatternMultiplyer);
                PatterncorrectAnswer = PatternStart + ((i + 1) * PatternMultiplyer);
            }
        }
        Debug.Log(PatterncorrectAnswer);
        for(int i = 0; i < PatternAnswerLength; i++)
        {
            PatternAnswers[i] = Random.Range(PatternStart, ValueLimit);
        }
        PatternAnswers[Random.Range(0,PatternAnswerLength)] = PatterncorrectAnswer;
    }

    public void SpawnPattern()
    {
        GeneratePattern();

        for(int i = 0; i < PatternLength; i++)
        {
            GameObject temp = Instantiate(Numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
            temp.GetComponent<Numbers>().SetNumber(PatternDisplay[i]);
            temp.transform.parent = Questions.transform;
        }
        SpawnAnswers();
    }

    public void SpawnAnswers()
    {
        Debug.Log("Not Spawning");
        for (int j = 0; j < PatternAnswerLength; j++)
        {
            GameObject temp = Instantiate(Numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
            temp.GetComponent<Numbers>().SetNumber(PatternAnswers[j]);
            temp.transform.parent = Answers.transform;
        }
    }

    public void CheckAnswer()
    {
        foreach (Transform child in Questions.transform)
        {
            Debug.Log("checked" + child.gameObject.GetComponent<Numbers>().GetNumber());
            if (child.gameObject.GetComponent<Numbers>().GetNumber() == PatterncorrectAnswer)
            {
                ExecuteOnComplete.Invoke();
                return;
            }
        }
        Debug.Log("Wrong");
    }

}
