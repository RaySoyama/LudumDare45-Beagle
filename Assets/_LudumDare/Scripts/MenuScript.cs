using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    public string StartScene;

    public AudioSource ass;

    public bool isJapanese;
    public Font JpFont;
    public Font EnFont;

    public Text startText;
    public Text languageText;
    public Text creditText;
    public Text quitText;



    private void Start()
    {
        isJapanese = false;
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


    public void ToggleLanguage()
    {
        if (isJapanese == true)//make english
        {
            startText.font = EnFont;
            startText.text = "Start";

            languageText.font = JpFont;
            languageText.text = "日本";

            creditText.font = EnFont;
            creditText.text = "Credits";

            quitText.font = EnFont;
            quitText.text = "Quit";

            isJapanese = false;
        }
        else 
        {
            startText.font = JpFont;
            startText.text = "はじめ";

            languageText.font = EnFont;
            languageText.text = "Eng";

            creditText.font = JpFont;
            creditText.text = "チーム";

            quitText.font = JpFont;
            quitText.text = "やめる";

            isJapanese = true;
        }

        ScreenFade.SFade.isJapanese = isJapanese;

    }


}
