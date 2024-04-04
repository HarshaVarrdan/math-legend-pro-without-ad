using Flexalon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Operations : MonoBehaviour
{
    [SerializeField]
    private GameObject QuestionLayout;
    [SerializeField]
    private GameObject AnswerLayout;
    [Space]
    [SerializeField]
    private GameObject _numbers;
    [SerializeField]
    private GameObject[] _operators;
    [SerializeField] GameObject _showAnswerOpt;

    [SerializeField]
    private Transform _respawnPoint;

    public int Operator = 0;
    public int AnswerNumber = 5;

    [Space]
    [SerializeField]
    private UnityEvent ExecuteOnComplete;
    [SerializeField]
    private UnityEvent ExecuteOnWrong;

    private int Number1;
    private int Number2;
    private int[] Answers;
    private int Answer;

    GameObject temp1;
    GameObject temp2;
    GameObject temp3;
    GameObject temp4;

    public void SpawnAll()
    {
        ThirdPersonController.TPC_Instance.setRespawnPoint(_respawnPoint);
        ProgressIndicator.PI_Instance.RemoveTaskGB(this.gameObject);



        switch (Operator)
        {
            case 0:
                {
                    Debug.Log(this.gameObject.name);
                    Number1 = Random.Range(0, 50);
                    Number2 = Random.Range(0, 50);
                    temp1 = Instantiate(_numbers,new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp1.GetComponent<Numbers>().SetNumber(Number1);
                    temp1.transform.parent = QuestionLayout.transform;
                    temp3 = Instantiate(_operators[Operator], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp3.transform.parent = QuestionLayout.transform;
                    temp2 = Instantiate(_numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp2.GetComponent<Numbers>().SetNumber(Number2);
                    temp2.transform.parent = QuestionLayout.transform;
                    temp4 = Instantiate(_operators[4], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp4.transform.parent = QuestionLayout.transform;
                    
                    temp1.GetComponent<FlexalonInteractable>().enabled = false;
                    temp2.GetComponent<FlexalonInteractable>().enabled = false;
                    
                    Answer = Number1 + Number2;
                    break;
                }
            case 1:
                {
                    Number1 = Random.Range(0, 99);
                    Number2 = Random.Range(0, Number1);
                    temp1 = Instantiate(_numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp1.GetComponent<Numbers>().SetNumber(Number1);
                    temp1.transform.parent = QuestionLayout.transform;
                    temp3 = Instantiate(_operators[Operator], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp3.transform.parent = QuestionLayout.transform;
                    temp2 = Instantiate(_numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp2.GetComponent<Numbers>().SetNumber(Number2);
                    temp2.transform.parent = QuestionLayout.transform;
                    temp4 = Instantiate(_operators[4], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp4.transform.parent = QuestionLayout.transform;

                    temp1.GetComponent<FlexalonInteractable>().enabled = false;
                    temp2.GetComponent<FlexalonInteractable>().enabled = false;

                    Answer = Number1 - Number2;
                    break;
                }
            case 2:
                {
                    Number1 = Random.Range(0, 10);
                    Number2 = Random.Range(0, 10);
                    temp1 = Instantiate(_numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp1.GetComponent<Numbers>().SetNumber(Number1);
                    temp1.transform.parent = QuestionLayout.transform;
                    temp3 = Instantiate(_operators[Operator], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp3.transform.parent = QuestionLayout.transform;
                    temp2 = Instantiate(_numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp2.GetComponent<Numbers>().SetNumber(Number2);
                    temp2.transform.parent = QuestionLayout.transform;
                    temp4 = Instantiate(_operators[4], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp4.transform.parent = QuestionLayout.transform;

                    temp1.GetComponent<FlexalonInteractable>().enabled = false;
                    temp2.GetComponent<FlexalonInteractable>().enabled = false;

                    Answer = Number1 * Number2;
                    break;
                }
            case 3:
                {
                    Number1 = Random.Range(0, 10);
                    Number2 = Random.Range(10, 99);
                    int temp = Number2 * Number1;
                    Debug.LogError(Number1 + " " + Number2 + " " + temp);
                    temp1 = Instantiate(_numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1)  );
                    temp1.GetComponent<Numbers>().SetNumber(temp);
                    temp1.transform.parent = QuestionLayout.transform;
                    temp3 = Instantiate(_operators[Operator], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp3.transform.parent = QuestionLayout.transform;
                    temp2 = Instantiate(_numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp2.GetComponent<Numbers>().SetNumber(Number2);
                    temp2.transform.parent = QuestionLayout.transform;
                    temp4 = Instantiate(_operators[4], new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
                    temp4.transform.parent = QuestionLayout.transform;

                    temp1.GetComponent<FlexalonInteractable>().enabled = false;
                    temp2.GetComponent<FlexalonInteractable>().enabled = false;


                    Answer = temp / Number2;
                    break;
                }
        }

        Array.Resize(ref Answers, AnswerNumber);

        for(int i=0;i<AnswerNumber;i++)
        {
            Answers[i] = Random.Range(0, 99);
        }

        Answers[Random.Range(0, AnswerNumber)] = Answer;

        foreach (var item in Answers)
        {
            GameObject temp = Instantiate(_numbers, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 1));
            Debug.Log(item);
            temp.GetComponent<Numbers>().SetNumber(item);
            temp.transform.parent = AnswerLayout.transform;
        }
        
    }

    public void CheckAnswer()
    {

        Transform item = QuestionLayout.transform.GetChild(QuestionLayout.transform.childCount - 1).transform;

        if(item.gameObject.tag == "Numbers")
        {
            if (item.gameObject.GetComponent<Numbers>().GetNumber() == Answer)
            {
                ExecuteOnComplete.Invoke();
                return;
            }
            else 
            {
                item.transform.parent = AnswerLayout.transform;
                ExecuteOnWrong.Invoke();  
                _showAnswerOpt.SetActive(true); 
            }   
        }
        ExecuteOnWrong.Invoke();
        return;
    }

    public void ShowAnswer() 
    {
        foreach(Transform item in AnswerLayout.transform) 
        {
            if(item.gameObject.GetComponent <Numbers>().GetNumber() == Answer) 
            {
                item.gameObject.transform.parent = QuestionLayout.transform;
            }    
        }
    }
}
