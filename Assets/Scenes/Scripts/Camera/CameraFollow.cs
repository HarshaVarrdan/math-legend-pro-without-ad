using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using Cinemachine;
using TMPro;

public class CameraFollow : MonoBehaviour 
{
    [SerializeField] CinemachineFreeLook freeLookCamera;
    [SerializeField] GameObject player;
	//[SerializeField] CinemachineBrain brain;
    //[SerializeField] Transform target;
	//[SerializeField] float smoothing = 5f;

	//Transform _player;
	//Vector3 offset;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        
        
    }

	public void ChangeTarget(GameObject Target)
	{
        Debug.Log("Called");
        // Disable the Cinemachine FreeLook component
        freeLookCamera.enabled = false;

        // Move the camera to the target position and look at the target
        transform.position = Target.transform.position;
        transform.rotation = Target.transform.rotation;

        // Set the target for freelook
        //freeLookCamera.m_LookAt = Target.transform;

        // Enable the Cinemachine FreeLook component again
        //freeLookCamera.enabled = true;
    }

	public void SetPlayer(GameObject temp)
	{
		
    }

	public void Resettarget()
	{
        // Disable the Cinemachine FreeLook component
        //freeLookCamera.enabled = false;

        // Reset the target for freelook
        freeLookCamera.m_LookAt = player.transform;

        // Enable the Cinemachine FreeLook component again
        freeLookCamera.enabled = true;
    }
}
