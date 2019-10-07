using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public string StartScene;

    public AudioSource ass;

    public void StartGame()
    {      
        ScreenFade.SFade.FadeIn(StartScene);
        ScreenFade.SFade.AudioFadeOut(ass, ass.volume);
    }

    public void ShowCredits()
    {
        ScreenFade.SFade.FadeIn("CamCreditScene");
        ScreenFade.SFade.AudioFadeOut(ass, ass.volume);
    }

    public void EndGame()
    {
        Application.Quit();
    }



}
