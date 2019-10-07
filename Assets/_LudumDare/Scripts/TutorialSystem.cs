﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSystem : MonoBehaviour
{
    /*
     * 1.Movement key icon
     * 2.Villager making noise
     * 3.Dog in range of vilager
     * 4.Villager Icon Sit
     * 5.Villager Icon Bark
     * 6.Spawn Stick
     * 7.Pickup Icons
     * 8.Throw Stick
     * 9.Run Icon
     * 10. Detect Stick Back
     * 11.Give meat
    s */

    [System.Serializable]
    private struct Icons
    {
        public Sprite ControllerIcon;
        public Sprite KeyboardIcon;
    }

    private enum Tutorial
    {
        Idle,
        Sit,
        Bark,
        SpawnStick,
        PickUp,
        GetStickBack,
        ThrowStick,
        Run,
        GiveMeat,
        End
    }

    [SerializeField]
    private float clapDuration;

    [SerializeField]
    private Animator manAnim;

    [SerializeField]
    private Animator dogAnim;

    [SerializeField]
    private Image TutorialIcons;

    [SerializeField]
    private Image WalkIcons;

    [SerializeField]
    private GameObject TutorialBoundries;

    [SerializeField]
    private PickupController pickupCont;


    [SerializeField]
    private Icons movementIcons;

    [SerializeField]
    private Icons sitIcons;

    [SerializeField]
    private Icons barkIcons;

    [SerializeField]
    private Icons pickupIcons;

    [SerializeField]
    private Icons runIcons;




    [SerializeField] [ReadOnlyField]
    private List<Tutorial> TutorialAction = new List<Tutorial>();

    [SerializeField][ReadOnlyField]
    private bool dogInRange;

    [SerializeField][ReadOnlyField]
    private bool stickInRange;


    private Coroutine ClapCour;

    void Start()
    {
        TutorialAction = new List<Tutorial>() { Tutorial.Idle,Tutorial.Sit,Tutorial.Bark,
                                                Tutorial.SpawnStick,Tutorial.PickUp,Tutorial.ThrowStick,Tutorial.GetStickBack,
                                                Tutorial.Run,Tutorial.GiveMeat,Tutorial.End};


    }

    void Update()
    {
        switch (TutorialAction[0])
        {
            case Tutorial.Idle:
                IdleUpdate();
                break;
            case Tutorial.Sit:
                SitUpdate();
                break;
            case Tutorial.Bark:
                BarkUpdate();
                break;
            case Tutorial.SpawnStick:
                SpawnStickUpdate();
                break;
            case Tutorial.PickUp:
                PickUpUpdate();
                break;
            case Tutorial.GetStickBack:
                GetStickBackUpdate();
                break;
            case Tutorial.ThrowStick:
                ThrowStickUpdate();
                break;
            case Tutorial.Run:
                RunUpdate();
                break;
            case Tutorial.GiveMeat:
                GiveMeatUpdate();
                break;
            case Tutorial.End:
                EndUpdate();
                break;

        }
    }


    private void IdleUpdate()
    {
        WalkIcons.gameObject.SetActive(true);
        TutorialIcons.gameObject.SetActive(false);

        if (InputSystem.WoofInput.inputMode == InputSystem.InputMode.Controller)
        {
            WalkIcons.sprite = movementIcons.ControllerIcon;
        }
        else
        {
            WalkIcons.sprite = movementIcons.KeyboardIcon;
        }

        //animation shit
        //humm or make noise

        //check for dog
        if (dogInRange == true)
        {
            TutorialIcons.gameObject.SetActive(true);
            WalkIcons.gameObject.SetActive(false);
            TutorialAction.RemoveAt(0);
        }
    }

    private void SitUpdate()
    {
        //Spawn Icon
        if (InputSystem.WoofInput.inputMode == InputSystem.InputMode.Controller)
        {
            TutorialIcons.sprite = sitIcons.ControllerIcon;
        }
        else
        {
            TutorialIcons.sprite = sitIcons.KeyboardIcon;
        }

        //detect sitting
        if (dogAnim.GetBool("isSitting") == true)
        {
            if (ClapCour == null)
            {
                ClapCour = StartCoroutine(ClapCoroutine(true));
            }
        }
    }

    private void BarkUpdate()
    {
        //Spawn Icon
        if (InputSystem.WoofInput.inputMode == InputSystem.InputMode.Controller)
        {
            TutorialIcons.sprite = barkIcons.ControllerIcon;
        }
        else
        {
            TutorialIcons.sprite = barkIcons.KeyboardIcon;
        }

        if (InputSystem.WoofInput.IsBark == true)
        {
            if (ClapCour == null)
            {
                ClapCour = StartCoroutine(ClapCoroutine(true));
            }
        }
    }

    private void SpawnStickUpdate()
    {
        //Spawn Stick, Play Animation;
        //wait till animation ends
        Debug.Log("Spawn Stick Action");
        TutorialAction.RemoveAt(0);
    }

    private void PickUpUpdate()
    {
        //Spawn Icon
        if (InputSystem.WoofInput.inputMode == InputSystem.InputMode.Controller)
        {
            TutorialIcons.sprite = pickupIcons.ControllerIcon;
        }
        else
        {
            TutorialIcons.sprite = pickupIcons.KeyboardIcon;
        }

        if (pickupCont.HasSomethingInMouth == true)
        {
            if (ClapCour == null)
            {
                ClapCour = StartCoroutine(ClapCoroutine(true));
            }

        }


    }

    private void GetStickBackUpdate()
    {
        //go into crouch animation

        //detect stick avalible
        if (pickupCont.HasSomethingInMouth == false && stickInRange == true)
        {
            //pick up
            Debug.Log("Get Stick back Action");


            //go to throw update
            TutorialAction.RemoveAt(0);
        }

    }

    private void ThrowStickUpdate()
    {
        //throw stick animation
        //Launch stick
        //wait till animation
        Debug.Log("Throw Stick Action");

        TutorialAction.RemoveAt(0);
    }

    private void RunUpdate()
    {
        //Spawn Icon
        if (InputSystem.WoofInput.inputMode == InputSystem.InputMode.Controller)
        {
            TutorialIcons.sprite = runIcons.ControllerIcon;
        }
        else
        {
            TutorialIcons.sprite = runIcons.KeyboardIcon;
        }

        if (InputSystem.WoofInput.IsRunning == true)
        {
            if (ClapCour == null)
            {
                ClapCour = StartCoroutine(ClapCoroutine(true));
            }
        }

    }

    private void GiveMeatUpdate()
    {
        //play spawn meat anim,
        //drop meat

        Debug.Log("Spawn Meat Action");

        if (ClapCour == null)
        {
            ClapCour = StartCoroutine(ClapCoroutine(true));
        }

    }

    private void EndUpdate()
    {
        //throw some icons
        //disable tutorial Boundries
        Debug.Log("End Action");
        TutorialBoundries.SetActive(false);
    }


    private IEnumerator ClapCoroutine(bool nextAction)
    {
        //set clap anim on
        manAnim.SetBool("isClapping", true);


        yield return new WaitForSeconds(clapDuration);
        manAnim.SetBool("isClapping", false);

        //set clap anim off

        

        ClapCour = null;

        if (nextAction == true)
        { 
            TutorialAction.RemoveAt(0);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            dogInRange = true;
        }
        else if (other.CompareTag("Pickup") == true)
        {
            stickInRange = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            dogInRange = true;
        }
        else if (other.CompareTag("Pickup") == true)
        {
            stickInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            dogInRange = false;
        }
        else if (other.CompareTag("Pickup") == true)
        {
            stickInRange = false;
        }

    }


}