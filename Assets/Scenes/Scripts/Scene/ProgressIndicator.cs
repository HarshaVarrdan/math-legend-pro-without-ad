using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ProgressIndicator : MonoBehaviour
{

    [SerializeField] Transform StartPoint;
    [SerializeField] Transform EndPoint;

    [SerializeField] Slider progresSlider;
    [SerializeField] GameObject directionIndicatorArrow;

    GameObject player;

    public List<GameObject> taskObjects = new List<GameObject>();
    public GameObject nearestObject;

    public Camera mainCamera;
    public float detectionRadius = 200f;

    float totaldistance;

    public static ProgressIndicator PI_Instance;

    private void Awake()
    {
        if(PI_Instance == null)
        {
            PI_Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tempTaskObjects = GameObject.FindGameObjectsWithTag("Task");

        taskObjects.AddRange(tempTaskObjects);

        totaldistance = Vector3.Distance(StartPoint.position, EndPoint.position);
        player = GameObject.FindWithTag("Player");
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        progresSlider.value = -(((Vector3.Distance(EndPoint.position, player.transform.position) * 100) / totaldistance) -100);
        Debug.Log(progresSlider.value);

        nearestObject = FindNearestObject(taskObjects);

        if (nearestObject != null)
        {
            Vector3 direction = nearestObject.transform.position - mainCamera.transform.position;

            directionIndicatorArrow.transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        }
    }


    GameObject FindNearestObject(List<GameObject> objects)
    {
        GameObject nearestObject = null;
        float nearestDistance = Mathf.Infinity;
        Vector3 playerPosition = (ThirdPersonController.TPC_Instance.respawnAt != null)? ThirdPersonController.TPC_Instance.respawnAt.position : player.transform.position;

        foreach (GameObject obj in objects)
        {
            float distance = Vector3.Distance(obj.transform.position, playerPosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestObject = obj;
            }
        }

        // Check if the nearest object is within the detection radius
        if (nearestObject != null && nearestDistance <= detectionRadius)
        {
            return nearestObject;
        }

        return null;
    }


    public void RemoveTaskGB(GameObject gb)
    {
        taskObjects.Remove(gb);
    }

}
