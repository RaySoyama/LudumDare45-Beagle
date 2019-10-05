﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
	private Transform cameraTransform;
	private Transform dog;

	private List<Transform> pickupList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
		cameraTransform = FindObjectsOfType<Camera>()[0].transform;
		dog = transform.parent.transform;
	}

    // Update is called once per frame
    void Update()
    {
		//find all pickups in radius and "select" the one closest to the dog
		//set position to that object's center and set scale to be that object's

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
			//transform.localScale = new Vector3(closest.lossyScale.x, closest.lossyScale.y, transform.lossyScale.z);
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}

		transform.LookAt(cameraTransform);

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
}
