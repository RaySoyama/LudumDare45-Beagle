using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField] [ReadOnlyField]
    private Vector2 directionalInput;
    public Vector2 DirectionalInput
    {
        get 
        {
            return directionalInput;
        }
    }

    [SerializeField] [ReadOnlyField]
    private float forwardValue;
    public float ForwadValue
    {
        get 
        {
            return forwardValue;
        }
    }

    [SerializeField][ReadOnlyField]
    private float sidewayValue;
    public float SidewayValue
    {
        get
        {
           return sidewayValue;
        }
    }

    [SerializeField]
    private KeyCode forwardKey = KeyCode.W;
    [SerializeField]
    private KeyCode backwardKey = KeyCode.S;
    [SerializeField]
    private KeyCode rightwardKey = KeyCode.D;
    [SerializeField]
    private KeyCode leftwardKey = KeyCode.A;



    void Update()
    {
        
    }
}
