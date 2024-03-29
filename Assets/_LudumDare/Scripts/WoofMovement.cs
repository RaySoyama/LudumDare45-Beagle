﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoofMovement : MonoBehaviour
{
	[SerializeField]
	private float currentSpeed;
	[SerializeField]
	private float defaultSpeed;
    [SerializeField]
	private float defaultAnimSpeed;

	[SerializeField]
	private float runSpeed;
    [SerializeField]
	private float runAnimSpeed;
    


    [SerializeField]
	private float rotationSpeed = 8f;


    private GameObject mainCamera;
    public bool cameraForward;
	private Transform dogModel;
	public Transform mouthPoint;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private float headDownSpeed;


    private Coroutine BorkCour;
	void Start()
    {
        mainCamera = Camera.main.gameObject;
		//dependent on the model being the first child bad
		dogModel = transform.GetChild(0);
		//dependent on the indicator being the second child bad
	}

    void Update()
    {
        dogModel.transform.localEulerAngles = new Vector3(0, dogModel.transform.localEulerAngles.y, dogModel.transform.localEulerAngles.z);

        if (InputSystem.WoofInput.IsEmote == true)
        {
            anim.SetBool("isSitting",true);
            anim.SetBool("isWalking", false);
        }
        else if (cameraForward == true && InputSystem.WoofInput.DirectionalInput.normalized != Vector2.zero)
        {
            //I hope this line of code gets Aids and dies
            Vector3 hori = InputSystem.WoofInput.DirectionalInput.y * new Vector3(mainCamera.transform.forward.normalized.x,0, mainCamera.transform.forward.normalized.z);
            Vector3 vert = InputSystem.WoofInput.DirectionalInput.x * new Vector3(mainCamera.transform.right.normalized.x, 0, mainCamera.transform.right.normalized.z);
            gameObject.transform.Translate((hori+vert) * currentSpeed * Time.deltaTime);

			//Rotate to movement direction.
			dogModel.rotation = Quaternion.LookRotation(Vector3.Lerp(dogModel.forward, (hori + vert), Time.deltaTime * rotationSpeed));

            anim.SetBool("isWalking", true);

            if (InputSystem.WoofInput.IsRunning == true)
            {
                currentSpeed = runSpeed;
                anim.speed = runAnimSpeed;
            }
            else 
            {
                currentSpeed = defaultSpeed;
                anim.speed = defaultAnimSpeed;
            }

		}
        else 
        {
            anim.SetBool("isWalking", false);
            //gameObject.transform.Translate(new Vector3(InputSystem.WoofInput.DirectionalInput.x,0, InputSystem.WoofInput.DirectionalInput.y) * Speed * Time.deltaTime);
        }

        if (InputSystem.WoofInput.IsEmote == false)
        {
            anim.SetBool("isSitting", false);
        }


        if (InputSystem.WoofInput.IsDucking == true)
        {
            if (InputSystem.WoofInput.inputMode == InputSystem.InputMode.Controller)
            {
                anim.SetLayerWeight(2, InputSystem.WoofInput.DuckValue);
            }
            else 
            {
                anim.SetLayerWeight(2, Mathf.Lerp(anim.GetLayerWeight(2), 1, headDownSpeed * Time.deltaTime));
            }
        }
        else
        {
            anim.SetLayerWeight(2, Mathf.Lerp(anim.GetLayerWeight(2), 0, headDownSpeed * Time.deltaTime));
        }

        if (InputSystem.WoofInput.IsBark == true)
        {
            if (BorkCour == null)
            {
                BorkCour = StartCoroutine(Bork());
                anim.SetTrigger("Bark");           
            }

        }


    }

    //test
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AreaTrigger") == true)
        {
            CameraController.CamController.EnterCave();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AreaTrigger") == true)
        {
            //Hard Coded

            CameraController.CamController.ExitCave();
        }
    }

    private IEnumerator Bork()
    {
        anim.SetTrigger("Bark");
        yield return new WaitForSeconds(0.6f);
        BorkCour = null;
        
    }


}
