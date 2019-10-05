﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoofMovement : MonoBehaviour
{
	[SerializeField]
	private float Speed;
    [SerializeField]
	private float rotationSpeed = 8f;


    private GameObject mainCamera;
    public bool cameraForward;
	private Transform dogModel;


    void Start()
    {
        mainCamera = Camera.main.gameObject;
		dogModel = transform.GetChild(0);
    }

    void Update()
    {
        if (cameraForward == true && InputSystem.WoofInput.DirectionalInput.normalized != Vector2.zero)
        {
            //I hope this line of code gets Aids and dies
            Vector3 hori = InputSystem.WoofInput.DirectionalInput.y * new Vector3(mainCamera.transform.forward.normalized.x,0, mainCamera.transform.forward.normalized.z);
            Vector3 vert = InputSystem.WoofInput.DirectionalInput.x * new Vector3(mainCamera.transform.right.normalized.x, 0, mainCamera.transform.right.normalized.z);
            gameObject.transform.Translate((hori+vert) * Speed * Time.deltaTime);

			//Rotate to movement direction.
			dogModel.rotation = Quaternion.LookRotation(Vector3.Lerp(dogModel.forward, (hori + vert), Time.deltaTime * rotationSpeed));

		}
        else 
        {
            gameObject.transform.Translate(new Vector3(InputSystem.WoofInput.DirectionalInput.x,0, InputSystem.WoofInput.DirectionalInput.y) * Speed * Time.deltaTime);
        }



    }
}
