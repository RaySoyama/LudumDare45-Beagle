using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckList : MonoBehaviour
{

    public static CheckList ShitList;

    [SerializeField]
    private GameObject menuCanvas;

    [SerializeField]
    private Text checkListText;
   
    
    public bool checkListOpen = false;

    public bool isTutorialComplete = false;
    
    public bool isSticksCollected = false;

    public bool isManCalled = false;

    public bool isMeatCooked = false;

    public bool isGoodBoi = false;




    void Start()
    {
        if (ShitList == null)
        {
            ShitList = this;
        }

        checkListOpen = false;
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if(InputSystem.WoofInput.IsMenuDown == true)
        {
            ToggleCheckList();
        }


    }

    private void UpdateList()
    {

        if (isTutorialComplete == false)
        {
            checkListText.text = $"Current Task: Complete Tutorial";
        }
        else if (isSticksCollected == false)
        {
            checkListText.text = $"Current Task: \nGather 5 Sticks in the Fire Pit";
        }
        else if (isManCalled == false)
        {
            checkListText.text = $"Current Task: Bark for Attention";
        }
        else if (isMeatCooked == false)
        {
            checkListText.text = $"Current Task: Cook Meat";
        }
        else if (isGoodBoi == false)
        { 
            checkListText.text = $"Current Task: Be A Good Boi\nSit n' Speak";
        }
    }

    public void ToggleCheckList()
    {
        UpdateList();

        if (checkListOpen == true)
        {
            CloseCheckList();
        }
        else
        { 
            OpenCheckList();
        }
    }

    private void OpenCheckList()
    {
        menuCanvas.SetActive(true);
        checkListOpen = true;
        Time.timeScale = 0.4f;


    }

    private void CloseCheckList()
    {
        menuCanvas.SetActive(false);
        checkListOpen = false;
        Time.timeScale = 1;
    }



}
