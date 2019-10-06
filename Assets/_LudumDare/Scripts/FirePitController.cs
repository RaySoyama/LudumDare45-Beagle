using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePitController : MonoBehaviour
{
    [SerializeField][ReadOnlyField]
	private List<GameObject> sticks = new List<GameObject>();
    
    [SerializeField][ReadOnlyField]
	private List<ObjectData> meatChunks = new List<ObjectData>();
	public int numberOfSticksNeeded = 3;

	[ReadOnlyField]
	public bool onFire = false;
	
    public GameObject fire;

    [SerializeField]
    private Material cookedMeatMat;
    [SerializeField]
    private Material burnedMeatMat;

    [SerializeField]
    private float timeToCook = 3.0f;

    [SerializeField]
    private float timeToBurn = 6.0f;



    [SerializeField]
    private GameObject innerFireCollider;

    [SerializeField]
    private Collider outerFireCollider;


    Coroutine fireCoroutine;
	private void Start()
	{
        fire.SetActive(false);
        innerFireCollider.SetActive(false);
	}


	void FixedUpdate()
    {
		if (sticks.Count >= numberOfSticksNeeded )
		{
            //Light the fire
            if (fireCoroutine == null)
            { 
               fireCoroutine = StartCoroutine(StartFire());
            }

			onFire = true;
		}

		if (onFire)
		{
            if (sticks.Count > 0)
            {
                if (fireCoroutine == null)
                {
                    fireCoroutine = StartCoroutine(StartFire());
                }
            }

            foreach (ObjectData chunk in meatChunks)
			{
                chunk.timeInFire += Time.deltaTime;

                if (chunk.timeInFire > timeToCook)
                {
                    chunk.gameObject.GetComponentInChildren<Renderer>().material = cookedMeatMat;
                 
                    if (chunk.timeInFire > timeToBurn)
                    {
                        chunk.gameObject.GetComponentInChildren<Renderer>().material = burnedMeatMat;
                    }

                }

			}

		}

    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Pickup"))
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
				if (!meatChunks.Contains(other.GetComponent<ObjectData>()))
				{
					meatChunks.Add(other.GetComponent<ObjectData>());
                    Debug.Log("meat added");
				}
			}
		}


	}

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            ObjectData obj = other.gameObject.GetComponent<ObjectData>();

            if (obj.Type == ObjectData.ObjectType.MeatChunk)
            {
                if (!meatChunks.Contains(other.GetComponent<ObjectData>()))
                {
                    meatChunks.Add(other.GetComponent<ObjectData>());
                    Debug.Log("meat added");
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
                if (onFire == true)
                {
                    return;
                }

                if (sticks.Contains(other.gameObject))
                {
                    sticks.Remove(other.gameObject);
                }
            }
            else if (obj.Type == ObjectData.ObjectType.MeatChunk)
            {
                if (meatChunks.Contains(other.GetComponent<ObjectData>()))
                {
                    meatChunks.Remove(other.GetComponent<ObjectData>());
                    Debug.Log("meat removed");
                }
            }
			
		}
	}

    private IEnumerator StartFire()
    {
        //fireCollider.isTrigger = false;


        yield return new WaitForSeconds(1.5f);

        foreach (GameObject stook in sticks)
        {
            stook.GetComponent<ObjectData>().isPickupable = false;
            stook.GetComponent<Rigidbody>().isKinematic = true;
        }
        
        sticks.Clear();


        yield return new WaitForSeconds(0.5f);

            fire.SetActive(true);
            innerFireCollider.SetActive(true);

        fireCoroutine = null;
        yield return null;
    }
}
