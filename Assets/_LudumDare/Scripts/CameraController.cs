using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    //public Transform dog;

    public static CameraController CamController;

    [SerializeField]
    private CinemachineVirtualCamera feildCam;

    [SerializeField]
    private CinemachineVirtualCamera caveCam;


    void Start()
    {
        if (CamController == null)
        {
            CamController = this;
        }
    }

    void Update()
    {
		//placeholder
		//transform.LookAt(dog);

		//transform.position = Vector3.Lerp(transform.position, new Vector3(dog.position.x - xOffset, dog.position.y + yOffset, dog.position.z), Time.deltaTime * cameraSpeed);
	}


    public void EnterCave()
    {
        caveCam.Priority = 11;
    }

    public void ExitCave()
    { 
        caveCam.Priority = 9;    
    }

}