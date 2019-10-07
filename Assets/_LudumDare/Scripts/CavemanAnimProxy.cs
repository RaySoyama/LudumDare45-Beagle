using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavemanAnimProxy : MonoBehaviour
{
    [SerializeField]
    private TutorialSystem toot;

    [SerializeField]
    private GameObject StickPrefab;
    
    [SerializeField][ReadOnlyField]
    private GameObject SpawnedStick;

    [SerializeField]
    private GameObject manHandJoint;

    [SerializeField]
    private Transform stickSpawnPos;




    public void SpawnStick()
    {
        SpawnedStick = Instantiate(StickPrefab, manHandJoint.transform);
        SpawnedStick.GetComponent<ObjectData>().isPickupable = false;
    }

    public void DropStick()
    {
        SpawnedStick.transform.parent = null;
        SpawnedStick.transform.position = stickSpawnPos.position;
        SpawnedStick.GetComponent<ObjectData>().isPickupable = true;
    }

    public void PickUpStick()
    { 
    
    }

    public void ThrowStick()
    { 
    
    }


}
