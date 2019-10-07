using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenFade : MonoBehaviour
{

    public static ScreenFade SFade;

    [SerializeField][Range(0.0f, 5.0f)]
    private float transitionDuraion;

    [SerializeField]
    private Image img;

    void Start()
    {
        if (SFade == null)
        {
            SFade = this;
        }

        Object.DontDestroyOnLoad(this);
        FadeOut();
    }

    private void Update()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FadeOut();
    }

    private Coroutine check;

    public void FadeIn(string newScene)
    {
        if (check == null)
        {
            check = StartCoroutine(FadeInCor(newScene));
        }
    }

    public void FadeOut()
    {
        if (check == null)
        {
            check = StartCoroutine(FadeOutCor());
        }
    }

    private Coroutine AudioCheck;
    public void AudioFadeOut(AudioSource ass, float startVol )
    {
        if (AudioCheck == null)
        {
            AudioCheck = StartCoroutine(AudioFadeOutCor(ass,startVol));
        }
    }





    private IEnumerator FadeInCor(string newScene)
    {
        float n = 0;
        img.gameObject.SetActive(true);
        while (n < transitionDuraion)
        {
            n += Time.deltaTime;

            img.color = new Color(0, 0, 0, n / transitionDuraion);
            
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(newScene);
        check = null;
    }   
    
    private IEnumerator FadeOutCor()
    {
        float n = 0;

        while (n < transitionDuraion)
        {
            n += Time.deltaTime;

            img.color = new Color(0, 0, 0, 1.0f - (n / transitionDuraion));
            yield return new WaitForEndOfFrame();
        }
        img.gameObject.SetActive(false);
        check = null;
    }


    private IEnumerator AudioFadeOutCor(AudioSource ass, float startVol)
    {
        float n = 0;

        while (n < transitionDuraion)
        {
            n += Time.deltaTime;

            ass.volume = startVol - (n / transitionDuraion);
            yield return new WaitForEndOfFrame();
        }

        AudioCheck = null;
    }
}
