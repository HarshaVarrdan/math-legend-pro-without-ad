using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrimeNumberGame : MonoBehaviour
{
    public GameObject[] primeNumberPrefabs;
    public GameObject container;
    public Transform prime;
    public GameObject GridLayout;
    


    //[SerializeField]
    //private GameObject[] number;

    //private int score;
    private int primeNumber;
    private bool numberFound;

    void Start()
    {
        //score = 0;
        numberFound = false;
        GeneratePrimeNumber();

        
    }

    void GeneratePrimeNumber()
    {
        
        
            int num = Random.Range(2, 100); // Generate a random number between 2 and 100
            if (IsPrime(num))
            {
            if (num < 10)
            {
                Instantiate(primeNumberPrefabs[num], new Vector3(0, 0, -0.3f), new Quaternion(0, 180, 0, 1),prime);
                Debug.Log(num);
            }
            if (num >= 10)
            {
                Instantiate(primeNumberPrefabs[num / 10], new Vector3(-0.35f, 0, -0.3f), new Quaternion(0, 180, 0, 1),prime);
                Instantiate(primeNumberPrefabs[num % 10], new Vector3(0.35f, 0, -0.3f), new Quaternion(0, 180, 0, 1),prime);
                Debug.Log(num / 10 + " " + num % 10);
            }
           
            
            int firstNumberValue = Random.Range(0, 99);
            Debug.Log(firstNumberValue);
            container.transform.localPosition = new Vector3(-4f, 0f, 9.698f);
            if (firstNumberValue >= 10)
            {
                int tensPlaceValue = firstNumberValue / 10;
                GameObject tensPlaceObj = Instantiate(primeNumberPrefabs[tensPlaceValue], container.transform);
                tensPlaceObj.transform.localPosition = new Vector3( 0.35f, 0f, 10f);
                //tensPlaceObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                int onesPlaceValue = firstNumberValue % 10;
                GameObject onesPlaceObj = Instantiate(primeNumberPrefabs[onesPlaceValue], container.transform);
                onesPlaceObj.transform.localPosition = new Vector3(-0.35f, 0f, 10f);
                //onesPlaceObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
            else
            {
                GameObject productObj = Instantiate(primeNumberPrefabs[firstNumberValue], container.transform);
                productObj.transform.localPosition = new Vector3(0.35f, 0f, 10f);
                //productObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            }
            //Instantiate(primeNumberPrefabs[randomIndex], new Vector3(0f, 0f, 0f), Quaternion.identity);


        }
        else
        {
            GeneratePrimeNumber();
        }

        
    }

    public void checkvalue()
    {

        if (GridLayout.transform.GetChild(0).name=="prime")
        {
            Debug.Log("correct");
        }
        else
        {
            Debug.Log("wrong");
        }
                

    }


    bool IsPrime(int n)
    {       
        if (n <= 1)
            return false;

        for (int i = 2; i <= n / 2; i++)
        {
            if (n % i == 0)
                return false;
            //Debug.Log("correct");
        }

        return true;
    }

    // Handle player interactions with the prime number prefab
    /*void OnMouseDown()
    {
        if (!numberFound)
        {
            // Increase the score, set the numberFound flag to true, and generate a new prime number.
            score++;
            numberFound = true;
            GeneratePrimeNumber();
            
        }
    }*/
    /*private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        if (collision.gameObject.name == "Ques")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            
        }
    }*/

    
    }

