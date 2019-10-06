using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    public enum ObjectType
    {
        SmallStick,
        BigStick,
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
