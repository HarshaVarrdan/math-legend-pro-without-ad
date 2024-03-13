using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsController : MonoBehaviour
{
    [SerializeField]
    private Image[] HeartContainers;
    [SerializeField]
    private GameObject GameLose;
    [SerializeField]
    private Sprite FullHeart = null;
    [SerializeField]
    private Sprite EmptyHeart = null;

    public static HeartsController HC_Instance;
    private bool onetime= false;

    private int hearts;
    


    private void Awake()
    {
        if(HC_Instance == null) 
        {
            HC_Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var heart in HeartContainers)
        {
            heart.overrideSprite = FullHeart;
            Debug.Log(heart.gameObject.name);
        }
        hearts = HeartContainers.Length;
        
    }

    void Update()
    {
        if(hearts == 0)
        {
            Debug.Log("YOU DIED");
            if (!onetime)
            {
                GameLose.GetComponent<UIManager>().ShowPlayerLoseUI();
                onetime = true;
            }
        }
    }

    private void UpdateHearts()
    {
        int i = 0;
        while(i<HeartContainers.Length)
        {
            if(i<(HeartContainers.Length - hearts ))
                HeartContainers[i].overrideSprite= EmptyHeart;
            else
                HeartContainers[i].overrideSprite = FullHeart;
            
            i++;
        }
    }

    public void HeartsUP()
    {
        hearts++;
        UpdateHearts();
        Debug.Log("" + hearts);
    }

    public void HeartsDOWN()
    {
        hearts--;
        UpdateHearts();
        Debug.Log("" + hearts);
    }

    
    public void HeartsFull()
    {
        hearts=HeartContainers.Length;
        UpdateHearts();
        Debug.Log("" + hearts);
    }

}
