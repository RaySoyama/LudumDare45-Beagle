using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public enum ObjectType
    {
        SmallStick,
        BigStick,
		MeatChunk,
		CookedMeat
    }

    [SerializeField]
    private ObjectType type;
    public ObjectType Type
    {
        get
        {
            return type;
        }
    }

    [SerializeField]
    private bool holdInMouth = true;

    public bool HoldInMouth
    {
        get 
        {
            return holdInMouth;
        }
    }

    public bool isCurrentlyInMouth = false;

    public bool isPickupable = true;

    public List<Transform> grabPoints;

    public float timeInFire = 0;

}
