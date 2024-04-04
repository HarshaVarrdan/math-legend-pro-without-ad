using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CompNumb : MonoBehaviour
{
    [SerializeField]
    private GameObject NumbersPrefab;
    [SerializeField]
    private GameObject LeftLayout;
    [SerializeField] 
    private GameObject RightLayout;
    [SerializeField]
    private GameObject AnswersLayout;
    [SerializeField]
    private GameObject OperatorLayout;
    [SerializeField] 
    private GameObject _showAnswerOpt;
    [SerializeField]
    private GameObject[] Operators;

    [SerializeField]
    private UnityEvent OnCompleteExecute;
    [SerializeField]
    private UnityEvent OnWrongExecute;
    
    [SerializeField]
    private Transform _respawnPoint;

    private int LeftNumber;
    private int RightNumber;

    public void StartTask()
    {
        ThirdPersonController.TPC_Instance.setRespawnPoint(_respawnPoint);

        ProgressIndicator.PI_Instance.RemoveTaskGB(this.gameObject);


        Debug.Log("Called");
        LeftNumber = Random.Range(0, 99);
        RightNumber = Random.Range(0, 99);

        GameObject temp1 = Instantiate(NumbersPrefab);
        temp1.GetComponent<Flexalon.FlexalonInteractable>().Draggable = false;
        temp1.GetComponent<Numbers>().SetNumber(LeftNumber);
        temp1.transform.parent = LeftLayout.transform;

        GameObject temp2 = Instantiate(NumbersPrefab);
        temp2.GetComponent<Flexalon.FlexalonInteractable>().Draggable = false;
        temp2.GetComponent<Numbers>().SetNumber(RightNumber);
        temp2.transform.parent = RightLayout.transform;

        foreach(GameObject temp in Operators)
        {
            GameObject temp3 = Instantiate(temp);
            temp3.transform.parent = OperatorLayout.transform;
        }
    }

    public void CheckAnswer()
    {
        if(AnswersLayout.transform.childCount == 0) 
        {
            return;
        }

        if(LeftNumber < RightNumber)
        {
            if (AnswersLayout.transform.GetChild(0).gameObject.tag == "Greater")
            {
                OnCompleteExecute.Invoke();
            }
            else 
            {
                _showAnswerOpt.SetActive(true);
                OnWrongExecute.Invoke();
                Debug.Log("Wrong1");
            }
        }
        else if(LeftNumber > RightNumber)
        {
            if (AnswersLayout.transform.GetChild(0).gameObject.tag == "Lesser")
            {
                OnCompleteExecute.Invoke();
            }
            else
            {
                OnWrongExecute.Invoke();
                _showAnswerOpt.SetActive(true);
                Debug.Log("Wrong2");
            }
        }
        else if (LeftNumber == RightNumber)
        {
            if (AnswersLayout.transform.GetChild(0).gameObject.tag == "equals")
            {
                OnCompleteExecute.Invoke();
            }
            else
            {
                OnWrongExecute.Invoke();
                _showAnswerOpt.SetActive(true);
                Debug.Log("Wrong3");
            }
        }
    }


    public void ShowAnswer() 
    {
        if (AnswersLayout.transform.childCount != 0)
        {
            AnswersLayout.transform.GetChild(0).transform.parent = OperatorLayout.transform;
        }
        if (LeftNumber < RightNumber)
        {
            foreach(Transform item in OperatorLayout.transform) 
            {
                if(item.gameObject.tag == Operators[0].tag) 
                {
                    item.parent = AnswersLayout.transform;
                }
            }
        }
        else if (LeftNumber > RightNumber)
        {
            foreach (Transform item in OperatorLayout.transform)
            {
                if (item.gameObject.tag == Operators[1].tag)
                {
                    item.parent = AnswersLayout.transform;
                }
            }
        }
        else if (LeftNumber == RightNumber)
        {
            foreach (Transform item in OperatorLayout.transform)
            {
                if (item.gameObject.tag == Operators[2].tag)
                {
                    item.parent = AnswersLayout.transform;
                }
            }
        }
    }
}

