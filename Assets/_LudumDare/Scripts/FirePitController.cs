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

    [SerializeField]
    private Material cookedMeatMat;

    private Collider fireCollider;
	private void Start()
	{
        fireCollider = GetComponent<Collider>();
	}

    private float fireTimer;

	void FixedUpdate()
    {
		if (onFire == false && sticks.Count >= numberOfSticksNeeded )
		{
			//Light the fire
			onFire = true;
		}
		else if (onFire == false)
		{
			//unlight the fire
			onFire = false;
			fire.SetActive(false);
		}

		if (onFire)
		{
            StartFire();
    

            foreach (GameObject chunk in meatChunks)
			{


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

    private void StartFire()
    {
        //fireCollider.isTrigger = false;
        fireTimer += Time.deltaTime;

        if (fireTimer >= 0.5f)
        {
            foreach (GameObject stook in sticks)
            {
                stook.GetComponent<ObjectData>().isPickupable = false;
                stook.GetComponent<Rigidbody>().isKinematic = true;
            }

        }
        if (fireTimer >= 1.5)
        {
            fire.SetActive(true);
            fireCollider.isTrigger = false;
        }
    }

}
