using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
public class OpeningSceneCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineDollyCart dollyCart;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Material PPMat;

    [SerializeField] [Range(0.0f,1.0f)]
    private float thunderVal;


    public bool isGameStarted = false;

    void Start()
    { 
        dollyCart.m_Speed = 0;
    }

    void Update()
    {
        if (isGameStarted == true)
        {
            dollyCart.m_Speed = speed;
        }


    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, PPMat);
    }

}
