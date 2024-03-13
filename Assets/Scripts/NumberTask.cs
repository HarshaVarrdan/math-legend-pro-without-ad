using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Number : MonoBehaviour
{
    [SerializeField] GameObject[] numbersPrefab;

    [SerializeField] int minNumber;
    [SerializeField] int maxNumber;
    [SerializeField] int choiceCount;

    [SerializeField] GameObject minusGO;


    [SerializeField] GameObject parentContainer;
    [SerializeField] GameObject numberlineContainer;
    [SerializeField] GameObject choiceContainer;
    [SerializeField] GameObject choiceparentContainer;


    private void Start()
    {
        for (int i = minNumber; i <= maxNumber; i++)
        {
            GameObject parent = Instantiate(parentContainer, numberlineContainer.transform);
            parent.name = i.ToString();
            
            if (i < 10 && i > -10)
            {
                int temp = (i < 0) ? -i : i;
                Debug.Log(temp);
                GameObject iNumberGO = Instantiate(numbersPrefab[temp], parent.transform);
            }
            else if (i >= 10 || i <= -10)
            {
                int temp = (i < 0) ? -i : i;
                Debug.Log(temp);

                GameObject iNumberGO = Instantiate(numbersPrefab[temp % 10], parent.transform);
                iNumberGO = Instantiate(numbersPrefab[temp / 10], parent.transform);
            }
            if (i < 0)
            {
                Instantiate(minusGO, parent.transform);
            }
        }

        GenerateAnswer();
    }

    void GenerateAnswer() 
    {
        List<int> array = new List<int>();

        for(int i = 0;i < choiceCount; i++) 
        {
            int rand = Random.Range(0, numberlineContainer.transform.childCount);
            GameObject tempGO = numberlineContainer.transform.GetChild(rand).gameObject;
            int temp = int.Parse(tempGO.name);
            array.Add(temp);
            Debug.Log(temp);
            Destroy(numberlineContainer.transform.GetChild(rand).gameObject);

        }
        int x = 0;
        while(x < array.Count) 
        { 
            int temparr = array[Random.Range(0, array.Count)];
            array.Remove(temparr);
            GameObject parent = Instantiate(choiceparentContainer, choiceContainer.transform);
            parent.name = temparr.ToString();
            
            if (temparr < 10 && temparr > -10)
            {
                int temp = (temparr < 0) ? -temparr : temparr;
                Debug.Log(temp);
                GameObject iNumberGO = Instantiate(numbersPrefab[temp], parent.transform);
            }
            else if (temparr >= 10 || temparr <= -10)
            {
                int temp = (temparr < 0) ? -temparr : temparr;
                Debug.Log(temp);

                GameObject iNumberGO = Instantiate(numbersPrefab[temp % 10], parent.transform);
                iNumberGO = Instantiate(numbersPrefab[temp / 10], parent.transform);
            }
            if (temparr < 0)
            {
                Instantiate(minusGO, parent.transform);
            }
        }
    }

    public void Check()
    {
        int prevint = minNumber;
        for (int i = 1; i < numberlineContainer.transform.childCount; i++)
        {
            if (prevint == int.Parse(numberlineContainer.transform.GetChild(i).name) - 1)
            {
                Debug.Log(prevint + " < " + int.Parse(numberlineContainer.transform.GetChild(i).name) + " Correct");
            }
            else
            {
                Debug.Log(numberlineContainer.transform.GetChild(i).name + " is MisPlaced");
                return;
            }

            prevint = int.Parse(numberlineContainer.transform.GetChild(i).name);
        }
    }
}