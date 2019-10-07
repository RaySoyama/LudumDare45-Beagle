using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavemanAnimProxy : MonoBehaviour
{
    [SerializeField]
    private TutorialSystem toot;

    [SerializeField]
    private GameObject StickPrefab;
    
    [SerializeField]
    private GameObject SteakPrefab;
    
    [SerializeField][ReadOnlyField]
    private GameObject SpawnedItem;

    [SerializeField]
    private GameObject manHandJoint;

    [SerializeField]
    private Transform stickSpawnPos;
    
    [SerializeField]
    private Transform stickThrowPos;




    public void SpawnStick()
    {
        SpawnedItem = Instantiate(StickPrefab, manHandJoint.transform);
        SpawnedItem.transform.localPosition = Vector3.zero;
        SpawnedItem.GetComponent<ObjectData>().isPickupable = false;
        SpawnedItem.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void DropStick()
    {
        SpawnedItem.transform.parent = null;
        SpawnedItem.GetComponent<Rigidbody>().isKinematic = false;
        SpawnedItem.transform.position = stickSpawnPos.position;
        SpawnedItem.GetComponent<ObjectData>().isPickupable = true;
    }

    public void PickUpStick()
    {
        SpawnedItem.GetComponent<Rigidbody>().isKinematic = true;
        SpawnedItem.transform.parent = manHandJoint.transform;
        SpawnedItem.transform.position = Vector3.zero;
        SpawnedItem.GetComponent<ObjectData>().isPickupable = false;
    }

    public void ThrowStick()
    {
        //add force to throw
        SpawnedItem.GetComponent<Rigidbody>().isKinematic = false;
        SpawnedItem.transform.parent = null;
        SpawnedItem.GetComponent<ObjectData>().isPickupable = true;
        SpawnedItem.transform.position = stickThrowPos.position;
    }

    public void DestroyStick()
    {
        //Destroy(SpawnedItem);
        SpawnedItem.SetActive(false);
    }

    public void SpawnMeat()
    {
        SpawnedItem = Instantiate(SteakPrefab, manHandJoint.transform);
        SpawnedItem.transform.localPosition = Vector3.zero;
        SpawnedItem.GetComponent<ObjectData>().isPickupable = false;
        SpawnedItem.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void DropMeat()
    {
        SpawnedItem.transform.parent = null;
        SpawnedItem.GetComponent<Rigidbody>().isKinematic = false;
        SpawnedItem.transform.position = stickSpawnPos.position;
        SpawnedItem.GetComponent<ObjectData>().isPickupable = true;
    }
}
