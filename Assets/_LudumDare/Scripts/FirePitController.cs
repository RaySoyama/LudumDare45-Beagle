using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePitController : MonoBehaviour
{
	private List<GameObject> sticks = new List<GameObject>();
	public int numberOfSticksNeeded = 3;
	[ReadOnlyField]
	public bool onFire = false;
	GameObject fire;

	private void Start()
	{
		fire = transform.GetChild(0).gameObject;
	}

	void FixedUpdate()
    {
		if (sticks.Count >= numberOfSticksNeeded)
		{
			//Light the fire
			onFire = true;
			fire.SetActive(true);
		}
		else
		{
			//unlight the fire
			onFire = false;
			fire.SetActive(false);
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Pickup")
		{
			ObjectData obj = other.gameObject.GetComponent<ObjectData>();
			if (obj.Type == ObjectData.ObjectType.SmallStick || obj.Type == ObjectData.ObjectType.BigStick)
			{
				if (!sticks.Contains(other.gameObject))
				{
					sticks.Add(other.gameObject);
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Pickup")
		{
			ObjectData obj = other.gameObject.GetComponent<ObjectData>();
			if (obj.Type == ObjectData.ObjectType.SmallStick || obj.Type == ObjectData.ObjectType.BigStick)
			{
				if (sticks.Contains(other.gameObject))
				{
					sticks.Remove(other.gameObject);
				}
			}
		}
	}
}
