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
            checkListText.text = $"Complete Tutorial";
        }
        else if (isSticksCollected == false)
        {
            checkListText.text = $"Collect 5 Sticks to the Fire Pit";
        }
        else if (isManCalled == false)
        {
            checkListText.text = $"Bark for Attention";
        }
        else if (isMeatCooked == false)
        {
            checkListText.text = $"Cook Meat";
        }
        else if (isGoodBoi == false)
        { 
            checkListText.text = $"Be A Good Boi\nSit n Bark";
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
        Time.timeScale = 0;


    }

    private void CloseCheckList()
    {
        menuCanvas.SetActive(false);
        checkListOpen = false;
        Time.timeScale = 1;
    }



}
