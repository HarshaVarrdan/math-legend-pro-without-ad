using Flexalon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class AdditionSubtractionLine : MonoBehaviour
{
    [SerializeField] private GameObject QuestionLayout;
    [SerializeField] private GameObject ChoicesLayout;
    [SerializeField] private GameObject answersLayout;
    [SerializeField] private GameObject symbolLayout;
    [SerializeField] private GameObject startNoPos;
    [SerializeField] private GameObject Numbers;

    [Space]
    [SerializeField]
    private Transform _respawnPoint;
    [Space]

    [Space]
    [SerializeField] private bool Addition;
    [SerializeField] private bool Multiplication;
    [SerializeField] private int length;
    [SerializeField] private bool Division;
    int[] evenNo = { 2, 4, 6, 8, 10 };
    int[] oddNo = { 1, 3, 5, 7, 9 };
    [Space]

    [Space]
    [SerializeField] GameObject[] operatorGB;
    [SerializeField] int selectedOperator;
    [Space]

    [SerializeField] private UnityEvent ExecuteOnComplete;
    [SerializeField] private UnityEvent ExecuteOnWrong;

    [SerializeField] private int[] QuestionPattern;
    [SerializeField] private int[] AnswersPattern;
    [SerializeField] private int[] ChoicesPattern;
    [SerializeField] private int[] PlayerChoicesPattern;
    private int StartNumber;

    // Start is called before the first frame update
    void Start()
    {
        Array.Resize(ref QuestionPattern, length);
        Array.Resize(ref AnswersPattern, length + 1);
        Array.Resize(ref ChoicesPattern, length + 2);
        Array.Resize(ref PlayerChoicesPattern, length + 1);

        if (!Addition && !Multiplication && !Division) selectedOperator = 1;

        for(int i =0; i< length; i++) 
        {
            GameObject temp = Instantiate(operatorGB[selectedOperator], this.transform.position, Quaternion.identity);
            temp.transform.parent = symbolLayout.transform;
            temp.transform.rotation = symbolLayout.transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GeneratePatterns()
    {
        if(Addition)
        {
            StartNumber = Random.Range(0, 10);
            AnswersPattern[0] = StartNumber;
            for(int i=0;i<length;i++)
            {
                QuestionPattern[i] = Random.Range(0, 9);
            }
            for(int i=0;i<length;i++)
            {
                AnswersPattern[i+1] = AnswersPattern[i] + QuestionPattern[length - i -1];
            }
        }

        else if (Multiplication) 
        {
            StartNumber = Random.Range(0, 10);
            AnswersPattern[0] = StartNumber;
            for (int i = 0; i < length; i++)
            {
                QuestionPattern[i] = Random.Range(0, 9);
                print(QuestionPattern[i]);
            }
            for (int i = 0; i < length; i++)
            {
                AnswersPattern[i + 1] = AnswersPattern[i] * QuestionPattern[length - i - 1];
            }
        }
        else if (Division)
        {
            StartNumber = PrimeNumber(Random.Range(1, 10));
            AnswersPattern[AnswersPattern.Length - 1] = StartNumber;
            for (int i = QuestionPattern.Length - 1; i >= 0; i--)
            {
                QuestionPattern[i] = Random.Range(1, 10);
                AnswersPattern[i] = AnswersPattern[i + 1] * QuestionPattern[i];  
            }
            StartNumber = AnswersPattern[0];
            AnswersPattern.Reverse();
            QuestionPattern.Reverse();
        }
        else
        {
            StartNumber = Random.Range(90, 99);
            AnswersPattern[0] = StartNumber;
            for (int i = 0; i < length; i++)
            {
                QuestionPattern[i] = Random.Range(0, 9);
            }
            for (int i = 0; i < length; i++)
            {
                AnswersPattern[i + 1] = AnswersPattern[i] - QuestionPattern[length - i - 1];
            }
        }

        for (int i = 0; i < length; i++)
        {
            ChoicesPattern[i] = AnswersPattern[i + 1];
        }

        for (int i = length; i < length + 2; i++)
        {
            ChoicesPattern[i] = Random.Range(0, 99);
        }

        RandomizeArray(ChoicesPattern);
    }

    int PrimeNumber(int n) 
    {
        int  a = 0;
        for (int i = 1; i <= n; i++)
        {
            if (n % i == 0)
            {
                a++;
            }
        }
        if (a <= 2)
        {
            return n;
        }
        else
        {
            return PrimeNumber(n + 1);
        }
    }

    void RandomizeArray<T>(T[] arr)
    {
        System.Random rand = new System.Random();

        int n = arr.Length;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(0, i + 1);

            T temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }
    }

    public void SpawnPattern()
    {
        GeneratePatterns();

        ThirdPersonController.TPC_Instance.setRespawnPoint(_respawnPoint);
        ProgressIndicator.PI_Instance.RemoveTaskGB(this.gameObject);


        for (int i = 0; i < length; i++)
        {
            GameObject temp = Instantiate(Numbers, this.transform.position, Quaternion.identity);
            temp.GetComponent<Numbers>().SetNumber(QuestionPattern[i]);
            temp.transform.parent = QuestionLayout.transform;
            temp.transform.rotation = QuestionLayout.transform.rotation;
            temp.GetComponent<FlexalonInteractable>().enabled = false;
            print(QuestionPattern[i]);
        }

        for (int i = 0; i < length + 2; i++)
        {
            GameObject temp = Instantiate(Numbers, this.transform.position, Quaternion.identity);
            temp.GetComponent<Numbers>().SetNumber(ChoicesPattern[i]);
            temp.transform.parent = ChoicesLayout.transform;
            temp.transform.rotation = ChoicesLayout.transform.rotation;
            print(ChoicesPattern[i]);

        }

        GameObject temp1 = Instantiate(Numbers, startNoPos.transform);
        temp1.GetComponent<Numbers>().SetNumber(StartNumber);
//        temp1.transform.parent = startNoPos.transform;
//        temp1.transform.rotation = startNoPos.transform.rotation;
        temp1.GetComponent<FlexalonInteractable>().enabled = false;


    }

    public void CheckAnswer()
    {
        bool Check = true;
        int j = 0;

        foreach (Transform child in answersLayout.transform)
        {
            PlayerChoicesPattern[j] = child.gameObject.GetComponent<Numbers>().GetNumber();
            j++;
        }

        for (int i = 1; i < length + 1 ; i++)
        {
            Debug.Log(AnswersPattern[i] + " : " + PlayerChoicesPattern[i - 1]);
            if (AnswersPattern[i] != PlayerChoicesPattern[i-1])
            {
                Check = false;
                break;
            }
        }


        if(Check)
        {
            ExecuteOnComplete.Invoke();
        }
        else
        {
            ExecuteOnWrong.Invoke();
        }
    }
}
