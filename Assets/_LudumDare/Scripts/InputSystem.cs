using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public enum InputMode
    {
        Null,
        Keyboard,
        Controller
    }


    public static InputSystem WoofInput;


    [SerializeField] [ReadOnlyField]
    private Vector2 directionalInput;
    public Vector2 DirectionalInput
    {
        get
        {
            return directionalInput;
        }
    }

    public float ForwadValue
    {
        get
        {
            return directionalInput.y;
        }
    }

    public float SidewayValue
    {
        get
        {
            return directionalInput.x;
        }
    }

    [SerializeField] [ReadOnlyField]
    private bool isGrabbing = false;
    public bool IsGrabbing
    {
        get
        {
            return isGrabbing;
        }
    }


    [SerializeField] [ReadOnlyField]
    private bool isZoomingIn = false;
    public bool IsZoomingIn
    {
        get 
        {
            return isZoomingIn;
        }
    }
   
    [SerializeField] [ReadOnlyField]
    private bool isZoomingOut = false;
    public bool IsZoomingOut
    {
        get
        {
            return isZoomingOut;
        }
    }


    [SerializeField] [ReadOnlyField]
    private bool isDucking = false;

    public bool IsDucking
    {
        get
        {
            return isDucking;
        }

    }

    [SerializeField] [ReadOnlyField]
    private float duckValue;
    public float DuckValue
    {

        get
        {
            return duckValue;
        }
    }


    [SerializeField]
    private KeyCode forwardKey = KeyCode.W;
    [SerializeField]
    private KeyCode backwardKey = KeyCode.S;
    [SerializeField]
    private KeyCode rightwardKey = KeyCode.D;
    [SerializeField]
    private KeyCode leftwardKey = KeyCode.A;
	[SerializeField]
	private KeyCode grabKey = KeyCode.Space;
    [SerializeField]
    private KeyCode zoomInKey = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode zoomOutKey = KeyCode.LeftControl;



    [SerializeField]
    private string joystickHorizontal = "Horizontal";
    [SerializeField]
    private string joystickVertical = "Vertical";
    [SerializeField]
    private string grabButton = "XboxA";
    [SerializeField]
    private string duckButton = "LeftTrigger";
    [SerializeField]
    private string zoomInButton = "LeftBumper";
    [SerializeField]
    private string zoomOutButton = "RightBumper";

    public InputMode inputMode;

    //25 50
    void Start()
    {
        if (WoofInput == null)
        {
            WoofInput = this;
        }
    }


    void Update()
    {
        if (Input.GetAxis(joystickHorizontal) != 0.0f || Input.GetAxis(joystickVertical) != 0.0f || 
            Input.GetButton(grabButton) || Input.GetAxis(duckButton) != 0.0f || Input.GetButton(zoomInButton) || Input.GetButton(zoomOutButton))
        {
            inputMode = InputMode.Controller;
        }

        if (Input.GetKey(forwardKey) || Input.GetKey(backwardKey) || Input.GetKey(rightwardKey) ||
            Input.GetKey(leftwardKey) || Input.GetKey(grabKey)|| Input.GetKey(zoomInKey) || Input.GetKey(zoomOutKey)) //||Input.GetKey()
        {
            inputMode = InputMode.Keyboard;
        }

        if (inputMode == InputMode.Controller)
        {
            ControllerInputLoop();
        }
        else if (inputMode == InputMode.Keyboard)
        {
            KeyboardInputLoop();
        }


    }

    private void ControllerInputLoop()
    {
        directionalInput = new Vector2(Input.GetAxis(joystickHorizontal), Input.GetAxis(joystickVertical)).normalized;
        
        isGrabbing = Input.GetButton(grabButton);
        
        duckValue = Input.GetAxis(duckButton);

        if (duckValue >= 0.1f)
        {
            isDucking = true;
        }
        else 
        {
            isDucking = false;        
        }

        isZoomingIn = Input.GetButton(zoomInButton);
        isZoomingOut = Input.GetButton(zoomOutButton);

    }

    private void KeyboardInputLoop()
    {
        Vector2 RawInput = Vector2.zero;

        if ((Input.GetKey(forwardKey)) == true)
        {
            RawInput.y += 1.0f;
        }
        if ((Input.GetKey(backwardKey)) == true)
        {
            RawInput.y -= 1.0f;
        }
        if ((Input.GetKey(rightwardKey)) == true)
        {
            RawInput.x += 1.0f;
        }
        if ((Input.GetKey(leftwardKey)) == true)
        {
            RawInput.x -= 1.0f;
        }

     
        isGrabbing = Input.GetKey(grabKey);

        isZoomingIn = Input.GetKey(zoomInKey);
        isZoomingOut = Input.GetKey(zoomOutKey);


        directionalInput = RawInput.normalized;
    }
}
