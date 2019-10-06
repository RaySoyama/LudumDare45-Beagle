using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    //public Transform dog;

    public static CameraController CamController;

    [SerializeField]
    private CinemachineBrain camBrain;

    [SerializeField]
    private CinemachineVirtualCamera feildCam;

    [SerializeField]
    private CinemachineVirtualCamera caveCam;

    [SerializeField]
    private float defaultFOV = 40;
    
    [SerializeField]
    private float zoomInFOV = 25;
    
    [SerializeField]
    private float zoomOutFOV = 50;

    [SerializeField]
    private float lerpSpeed = 1;


    void Start()
    {
        if (CamController == null)
        {
            CamController = this;
        }
    }

    void Update()
    {

        if (InputSystem.WoofInput.IsZoomingIn == true)
        {
            ZoomIn();
        }
        else if (InputSystem.WoofInput.IsZoomingOut == true)
        {
            ZoomOut();
        }
        else 
        {
            ZoomDefault();
        }
    }


    public void EnterCave()
    {
        caveCam.Priority = 11;
    }

    public void ExitCave()
    { 
        caveCam.Priority = 9;    
    }

    public void ZoomIn()
    {
        caveCam.m_Lens.FieldOfView = Mathf.Lerp(caveCam.m_Lens.FieldOfView, zoomInFOV, lerpSpeed * Time.deltaTime);
        feildCam.m_Lens.FieldOfView = Mathf.Lerp(feildCam.m_Lens.FieldOfView, zoomInFOV, lerpSpeed * Time.deltaTime);
    }

    public void ZoomOut()
    {
        caveCam.m_Lens.FieldOfView = Mathf.Lerp(caveCam.m_Lens.FieldOfView, zoomOutFOV, lerpSpeed * Time.deltaTime);
        feildCam.m_Lens.FieldOfView = Mathf.Lerp(feildCam.m_Lens.FieldOfView, zoomOutFOV, lerpSpeed * Time.deltaTime);
    }

    public void ZoomDefault()
    {
        caveCam.m_Lens.FieldOfView = Mathf.Lerp(caveCam.m_Lens.FieldOfView, defaultFOV, lerpSpeed * Time.deltaTime);
        feildCam.m_Lens.FieldOfView = Mathf.Lerp(feildCam.m_Lens.FieldOfView, defaultFOV, lerpSpeed * Time.deltaTime);
    }
}