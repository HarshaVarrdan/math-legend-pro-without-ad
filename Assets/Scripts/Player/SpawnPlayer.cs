using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayerPrefab;
    [SerializeField]
    private GameObject StartLocation;
    [SerializeField]
    private ParticleSystem SpawnParticals;

    Transform[] Checkpoints;
    Transform LastCheckpoint;

    GameObject CurrentPlayer;
    ParticleSystem PSmemory;

    private void Start()
    {
        SpawnPlayerAtStart();
    }

    public void SpawnPlayerAtStart()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            CurrentPlayer = Instantiate(PlayerPrefab,StartLocation.transform);
            Camera.main.GetComponent<CameraFollow>().SetPlayer(CurrentPlayer);
            spawnParticals(StartLocation.transform);
        }
        else
        {
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            CurrentPlayer = Instantiate(PlayerPrefab, StartLocation.transform);
            Camera.main.GetComponent<CameraFollow>().SetPlayer(CurrentPlayer);
            spawnParticals(StartLocation.transform);
        }
    }

    public void RespawnPlayer()
    {
        Destroy(CurrentPlayer);
        CurrentPlayer = Instantiate(PlayerPrefab, StartLocation.transform);
        Camera.main.GetComponent<CameraFollow>().SetPlayer(CurrentPlayer);
        spawnParticals(StartLocation.transform);
    }

    public void RespawnAtCheckpoint()
    {
        Destroy(CurrentPlayer);
        CurrentPlayer = Instantiate(PlayerPrefab, LastCheckpoint);
        Camera.main.GetComponent<CameraFollow>().SetPlayer(CurrentPlayer);
        spawnParticals(LastCheckpoint);
    }

    public void AddCheckpoint(Transform temp)
    {
        Checkpoints.Append(temp);
        LastCheckpoint = temp;
    }

    private void spawnParticals(Transform temp)
    {
        PSmemory = Instantiate(SpawnParticals, temp.transform);
        PSmemory.Play();
        Invoke("DestroyParticalSystem", 5f);
    }

    private void DestroyParticalSystem()
    {
        Destroy(PSmemory);
    }
}
