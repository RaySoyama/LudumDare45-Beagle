using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform dog;

	private float xOffset = 4f;
	private float yOffset = 4f;
	private float cameraSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//placeholder
		transform.LookAt(dog);

		transform.position = Vector3.Lerp(transform.position, new Vector3(dog.position.x - xOffset, dog.position.y + yOffset, dog.position.z), Time.deltaTime * cameraSpeed);

	}
}