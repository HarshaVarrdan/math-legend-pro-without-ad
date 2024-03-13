using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Arrow_Manager : MonoBehaviour
{
    [SerializeField] UnityEvent onAttack;
    
    [SerializeField] float moveSpeed;
    [SerializeField] string playerTagString;
    public Vector3 attackLoc;
    bool canAttack;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack) 
        {
            transform.position = Vector3.MoveTowards(transform.position, attackLoc, moveSpeed);
            if(transform.position == attackLoc) 
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void SetParameters(Vector3 Loc) 
    {
        attackLoc = Loc;
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTagString) 
        {
            onAttack.Invoke();
        }
        Destroy(this.gameObject);
    }
}
