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

    public List<Transform> grabPoints;
}
