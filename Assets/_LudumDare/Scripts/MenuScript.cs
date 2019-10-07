using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public string StartScene;

    public AudioSource ass;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void StartGame()
    {      
        ScreenFade.SFade.FadeIn(StartScene);
        ScreenFade.SFade.AudioFadeOut(ass, ass.volume);
        Cursor.visible = false;
    }

    public void ShowCredits()
    {
        ScreenFade.SFade.FadeIn("CamCreditScene");
        ScreenFade.SFade.AudioFadeOut(ass, ass.volume);
        Cursor.visible = false;
    }

    public void EndGame()
    {
        Application.Quit();
    }



}
