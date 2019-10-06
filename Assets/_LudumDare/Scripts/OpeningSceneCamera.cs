using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class OpeningSceneCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineDollyCart dollyCart;

    [SerializeField]
    private float speed;


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
}
