using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public string StartScene;

    void Start()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(StartScene);
    }

    public void ShowCredits()
    { 
    
    }

    public void EndGame()
    {
        Application.Quit();
    }



}
