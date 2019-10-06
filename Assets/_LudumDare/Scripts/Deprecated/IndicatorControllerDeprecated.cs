using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorControllerDeprecated : MonoBehaviour
{

    private GameObject selected;

    private float distanceThreshold;

    private Transform cameraTransform;
    private Transform dog;

    private List<Transform> pickupList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = FindObjectsOfType<Camera>()[0].transform;
        dog = transform.parent.transform;

        //distanceThreshold


    }

    // Update is called once per frame
    void Update()
    {

        //if dog gets too far go back to it.
        if (Vector3.Distance(transform.position, dog.position) > distanceThreshold)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            transform.position = dog.transform.position;
            selected = null;
        }
        else
        {
            Transform closest = null;
            float closestDist = 0f;

            //Find closest pickup in radius
            foreach (Transform pick in pickupList)
            {
                float dist = Vector3.Distance(pick.position, dog.position);

                if (closest)
                {
                    if (dist < closestDist)
                    {
                        closest = pick;
                        closestDist = dist;
                    }
                }
                else
                {
                    closest = pick;
                    closestDist = dist;
                }
            }

            if (closest)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                transform.position = closest.position;
                selected = closest.gameObject;
                //transform.localScale = new Vector3(closest.lossyScale.x, closest.lossyScale.y, transform.lossyScale.z);
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                transform.position = dog.transform.position;
                selected = null;
            }

            transform.LookAt(cameraTransform);
        }
        //transform.Rotate(transform.forward);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.tag == "Pickup")
        {
            pickupList.Add(other.transform);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            if (pickupList.Contains(other.transform))
            {
                pickupList.Remove(other.transform);
            }

        }

    }

    public GameObject GetSelected()
    {
        return selected;
    }

}