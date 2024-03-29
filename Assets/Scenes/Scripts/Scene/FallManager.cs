using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Enemy;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class FallManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] CinemachineFreeLook CFL;
    [SerializeField] StoneGolem_AI golemAI;
    [SerializeField] EventTrigger Sword_ET;

    private GameObject player = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered " + other.gameObject.name);

        if(other.gameObject.CompareTag("Player")) 
        {
            Debug.Log("Player Respawn Triggered");
            Debug.LogError("Player Respawn Triggered");
            //ThirdPersonController.TPC_Instance.Respawn();

            Transform temp = ThirdPersonController.TPC_Instance.GetRepawnPoint();

            Destroy(GameObject.FindWithTag("Player"));

            player = Instantiate(playerPrefab, temp.position,Quaternion.identity);

            CFL.Follow = player.transform;
            CFL.LookAt = player.transform.GetChild(0);

            golemAI._Player = player.transform.GetChild(1);
            golemAI._PlayerBody = player.transform.GetChild(0);

            golemAI.ResetTarget();

            player.GetComponent<ThirdPersonController>().setRespawnPoint(temp);

            HeartsController.HC_Instance.HeartsDOWN();

           // Sword_ET.triggers += player.GetComponent<ThirdPersonController>().Attack();

            Invoke("ReferEnemy", 1f);
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnPointerClickDelegate((PointerEventData)data); });
            Sword_ET.triggers.Add(entry);

        }
    }

    private void OnPointerClickDelegate(PointerEventData data)
    {
        player.GetComponent<ThirdPersonController>().Attack();
    }

    void ReferEnemy() 
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log("player refer called");

        foreach (GameObject enemy in enemies)
        {
            Debug.Log(enemy.name);
            if (enemy.GetComponent<GhostAI>())
            {
                enemy.GetComponent<GhostAI>().FindPlayer();
            }

            else if (enemy.GetComponent<GolemAI>())
            {
                enemy.GetComponent<GolemAI>().FindPlayer();
            }

            else if (enemy.GetComponent<Paladin_AI>())
            {
                enemy.GetComponent<Paladin_AI>().FindPlayer();
            }

            else if (enemy.GetComponent<OrcTankerAI>())
            {
                enemy.GetComponent<OrcTankerAI>().FindPlayer();
            }

            else if (enemy.GetComponent<OrcSoldierAI>())
            {
                enemy.GetComponent<OrcSoldierAI>().FindPlayer();
            }

            else if (enemy.GetComponent<HobbitAI>())
            {
                enemy.GetComponent<HobbitAI>().FindPlayer();
            }

            else if (enemy.GetComponent<GriffinAI>())
            {
                enemy.GetComponent<GriffinAI>().FindPlayer();
            }

            else if (enemy.GetComponent<Elf_Rider_AI>())
            {
                enemy.GetComponent<Elf_Rider_AI>().FindPlayer();
            }

            else if (enemy.GetComponent<Elf_AI>())
            {
                enemy.GetComponent<Elf_AI>().FindPlayer();
            }

            else if (enemy.GetComponent<Dragon_AI>())
            {
                enemy.GetComponent<Dragon_AI>().FindPlayer();
            }
        }
    }
}
