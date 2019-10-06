using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    public GameObject JawEnd;

    [SerializeField][ReadOnlyField]
    private List<GameObject> avaliblePickups = new List<GameObject>();
    [SerializeField][ReadOnlyField]
    private List<ObjectData> avaliblePickupData = new List<ObjectData>();


    public GameObject pickupIndicator;

    
    [SerializeField][ReadOnlyField]
    private GameObject objectInMouth;
    [SerializeField][ReadOnlyField]
    private GameObject objectInMouthGrabPoint;



    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Break();
        }


        if (avaliblePickups.Count != 0)
        {
            float closestDistance = 69696969696969;
            GameObject closestGrabpoint = null;
            ObjectData closestObjectData = null;

            foreach (ObjectData pickup in avaliblePickupData)
            {
                foreach (Transform grabPoint in pickup.grabPoints)
                {
                    float distanceDogToPoint = Vector3.Distance(transform.position, grabPoint.position);

                    if (distanceDogToPoint < closestDistance)
                    {
                        closestDistance = distanceDogToPoint;
                        closestObjectData = pickup;
                        closestGrabpoint = grabPoint.gameObject;
                    }
                }


            }

            //only show when nothing in mouth
            if (objectInMouth == null)
            {
                pickupIndicator.SetActive(true);
                pickupIndicator.transform.position = new Vector3(closestGrabpoint.transform.position.x, closestGrabpoint.transform.position.y + 0.1f, closestGrabpoint.transform.position.z);
            }

            if (InputSystem.WoofInput.IsGrabbed == true)
            {
                if (objectInMouth == null)
                {
                    PutObjectInMouth(closestObjectData, closestGrabpoint);
                }
                else
                {
                    //Chillin in mouth
                    HoldObjectInMouth();
                }
            }
            else if (objectInMouth != null)
            {
                DropObjectInMouth();
            }





        }
        else 
        { 
            pickupIndicator.SetActive(false);
        }

        if (objectInMouth != null)
        {
            if (InputSystem.WoofInput.IsGrabbed == false)
            {
                DropObjectInMouth();
            }

            pickupIndicator.SetActive(false);
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup") == true && avaliblePickups.Contains(other.gameObject) != true)
        {
            avaliblePickups.Add(other.gameObject);
            avaliblePickupData.Add(other.GetComponent<ObjectData>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Pickup") == true)
        {
            avaliblePickups.Remove(other.gameObject);
            avaliblePickupData.Remove(other.GetComponent<ObjectData>());
        }
    }

    private void PutObjectInMouth(ObjectData closestObjectData, GameObject grabPoint)
    {
        objectInMouth = closestObjectData.gameObject;
        objectInMouthGrabPoint = grabPoint;

        if (closestObjectData.HoldInMouth == true)
        {
            objectInMouth.gameObject.GetComponent<Rigidbody>().useGravity = false;
            objectInMouth.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            objectInMouth.gameObject.GetComponent<Collider>().isTrigger = true;

            objectInMouth.transform.parent = JawEnd.transform;
            objectInMouth.transform.localPosition = objectInMouthGrabPoint.transform.localPosition * -1;
            objectInMouth.transform.localEulerAngles = objectInMouthGrabPoint.transform.localEulerAngles;
        }

    }

    private void HoldObjectInMouth()
    {
        //objectInMouth.transform.localPosition = objectInMouthGrabPoint.transform.localPosition;
        //objectInMouth.transform.position = JawEnd.transform.position - objectInMouthGrabPoint.transform.localPosition;
    }

    private void DropObjectInMouth()
    {
        objectInMouth.GetComponent<Rigidbody>().useGravity = true;
        objectInMouth.GetComponent<Rigidbody>().isKinematic = false;
        objectInMouth.gameObject.GetComponent<Collider>().isTrigger = false;
        objectInMouth.transform.parent = null;
        objectInMouth = null;
        objectInMouthGrabPoint = null;
    }

}
