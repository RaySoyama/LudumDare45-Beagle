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
    private bool isRunning;
    public bool IsRunning
    {
        get 
        {
            return isRunning;
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


    [SerializeField] [ReadOnlyField]
    private bool isEmote;
    public bool IsEmote
    {
        get 
        {
            return isEmote;
        }
    }

    [SerializeField] [ReadOnlyField]
    private bool isBark;
    public bool IsBark
    {
        get
        {
            return isBark;
        }
    }


    [SerializeField] [ReadOnlyField]
    private bool isMenuDown;
    public bool IsMenuDown
    {
        get 
        {
            return isMenuDown;
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
    private KeyCode runKey = KeyCode.LeftShift;
	[SerializeField]
	private int grabKeyMouseIdx = 0;
    [SerializeField]
	private int duckKeyMouseIdx = 1;
    [SerializeField]
    private KeyCode zoomInKey = KeyCode.LeftControl;
    [SerializeField]
    private KeyCode zoomOutKey = KeyCode.LeftShift;
    [SerializeField]
    private KeyCode emoteKey = KeyCode.E;
    [SerializeField]
    private KeyCode barkKey = KeyCode.Space;
    [SerializeField]
    private KeyCode menuKey = KeyCode.Escape;




    [SerializeField]
    private string joystickHorizontal = "Horizontal";
    [SerializeField]
    private string joystickVertical = "Vertical";
    [SerializeField]
    private string runButton = "RightBumper";
    [SerializeField]
    private string grabButton = "XboxA";
    [SerializeField]
    private string duckButton = "LeftTrigger";
    [SerializeField]
    private string zoomInButton = "LeftBumper";
    [SerializeField]
    private string zoomOutButton = "RightBumper";
    
    [SerializeField]
    private string emoteButton = "XboxX";
    [SerializeField]
    private string barkButton = "XboxB";
    
    [SerializeField]
    private string menuButton = "Start";
    


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
        if (Input.GetAxis(joystickHorizontal) != 0.0f || Input.GetAxis(joystickVertical) != 0.0f || Input.GetButton(runButton) || 
            Input.GetButton(grabButton) || Input.GetAxis(duckButton) != 0.0f || Input.GetButton(zoomInButton) || 
            Input.GetButton(zoomOutButton) || Input.GetButton(emoteButton) || Input.GetButton(barkButton)|| Input.GetButtonDown(menuButton))
        {
            inputMode = InputMode.Controller;
        }

        if (Input.GetKey(forwardKey) || Input.GetKey(backwardKey) || Input.GetKey(rightwardKey) ||
            Input.GetKey(leftwardKey) || Input.GetKey(runKey) || Input.GetMouseButton(grabKeyMouseIdx) || Input.GetMouseButton(duckKeyMouseIdx) ||
           Input.GetKey(barkKey) || Input.GetKey(emoteKey) || Input.GetKey(zoomInKey) || Input.GetKey(zoomOutKey) || Input.GetKeyDown(menuKey)) //||Input.GetKey()
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
        
        isRunning = Input.GetButton(runButton);

        duckValue = Input.GetAxis(duckButton);

        if (duckValue >= 0.1f)
        {
            isDucking = true;
        }
        else 
        {
            isDucking = false;        
        }

        //Input.GetButton(emoteButton) || Input.GetAxis(barkButton) != 0.0f

        isEmote = Input.GetButton(emoteButton);

        isBark = Input.GetButtonDown(barkButton);



        isMenuDown = Input.GetButtonDown(menuButton);

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


        isRunning = Input.GetKey(runKey);


        isGrabbing = Input.GetMouseButton(grabKeyMouseIdx) || Input.GetKey(KeyCode.J);
        isDucking = Input.GetMouseButton(duckKeyMouseIdx) || Input.GetKey(KeyCode.K);

        //isDucking = Input.GetKey(KeyCode.K);
        //isGrabbing = Input.GetKey(KeyCode.J);


        if (isDucking == true)
        {
            duckValue = 1;
        }
        else
        {
            duckValue = 0;
        }

       isBark  = Input.GetKeyDown(barkKey);
       isEmote = Input.GetKey(emoteKey);


        isZoomingIn = Input.GetKey(zoomInKey);
        isZoomingOut = Input.GetKey(zoomOutKey);

        isMenuDown = Input.GetKeyDown(menuKey);

        directionalInput = RawInput.normalized;
    }
}
