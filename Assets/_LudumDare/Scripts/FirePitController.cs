using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePitController : MonoBehaviour
{
    [SerializeField][ReadOnlyField]
	private List<GameObject> sticks = new List<GameObject>();
    
    [SerializeField][ReadOnlyField]
	private List<GameObject> meatChunks = new List<GameObject>();
	public int numberOfSticksNeeded = 3;

	[ReadOnlyField]
	public bool onFire = false;
	
    public GameObject fire;

	public GameObject cookedMeat;

	private void Start()
	{
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

		if (onFire)
		{
			foreach (GameObject chunk in meatChunks)
			{
				GameObject newChunk = Instantiate(cookedMeat, transform.position, Quaternion.identity);
				newChunk.GetComponent<Rigidbody>().AddForce(new Vector3(0.1f, 1f, 0.1f));
				Destroy(chunk);
			}

			meatChunks = new List<GameObject>();

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
			else if(obj.Type == ObjectData.ObjectType.MeatChunk)
			{
				if (!meatChunks.Contains(other.gameObject))
				{
					meatChunks.Add(other.gameObject);
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
				else if (obj.Type == ObjectData.ObjectType.MeatChunk)
				{
					if (meatChunks.Contains(other.gameObject))
					{
						meatChunks.Remove(other.gameObject);
					}
				}
			}
		}
	}
}
