using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CheckList : MonoBehaviour
{
    [SerializeField]
    private GameObject menuCanvas;

    [SerializeField]
    private Text checkListText;
   
    
    [SerializeField]
    private bool checkListOpen = false;
    void Start()
    {
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

    public void ToggleCheckList()
    {
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
