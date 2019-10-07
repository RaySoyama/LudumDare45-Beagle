using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpeningSceneCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineDollyCart dollyCart;

    [SerializeField]
    private float speed;

    [SerializeField]
    private Material PPMat;

    [SerializeField] [Range(0.0f,1.0f)]
    private float thunderVal;

    [SerializeField]
    private List<float> EffectValue = new List<float>();

    [SerializeField][ReadOnlyField]
    private int barkCount;

    [SerializeField]
    private Animator dogAnim;

    [SerializeField]
    private Animator manAnim;

    public bool isGameStarted = false;

    [SerializeField]
    private Image barkIcon;

    [SerializeField]
    private Sprite controllerBark; 

    [SerializeField]
    private Sprite keyboardBark;

    public AudioSource ass;



    [SerializeField]
    private GameObject BlackScreen;

    private Coroutine IHateThis;

    private Coroutine BarkCour;


    void Start()
    { 
        dollyCart.m_Speed = 0;
        isGameStarted = false;
        PPMat.SetFloat("_Effect", 222);
        barkIcon.gameObject.SetActive(false);
        BlackScreen.SetActive(false);
    }

    void Update()
    {
        dogAnim.SetBool("isSitting", true);

        if (Time.timeSinceLevelLoad >= 3.0f)
        {
            isGameStarted = true;
        }

        if (isGameStarted == true)
        {
            dollyCart.m_Speed = speed;

            //hard coded
            if (dollyCart.m_Position >= 17.11724)
            {
                barkIcon.gameObject.SetActive(true);

                if (InputSystem.WoofInput.inputMode == InputSystem.InputMode.Controller)
                {
                    barkIcon.sprite = controllerBark;
                }
                else
                { 
                    barkIcon.sprite = keyboardBark;
                }
                


                if (InputSystem.WoofInput.IsBark == true)
                {
                    if (IHateThis == null)
                    { 
                        IHateThis = StartCoroutine(IHateYou());
                    }

                    if (BarkCour == null)
                    {
                        BarkCour = StartCoroutine(Bork());
                    }  

                }
            }



        }


    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, PPMat);
    }

    private IEnumerator IHateYou()
    {
        yield return new WaitForSeconds(1);
        manAnim.SetLayerWeight(1, 1);
        manAnim.SetTrigger("Look");
    }

    private IEnumerator Bork()
    {
        dogAnim.SetTrigger("Bark");
        yield return new WaitForSeconds(0.2f);

        PPMat.SetFloat("_Effect", EffectValue[barkCount]);
        barkCount++;

        if (barkCount == EffectValue.Count)
        {
            BlackScreen.SetActive(true);
            StartCoroutine(ToScene());
        }
        else
        { 
            yield return new WaitForSeconds(0.8f);
            BarkCour = null;
        }
    }


    private IEnumerator ToScene()
    {
        yield return new WaitForSeconds(1);
        ScreenFade.SFade.FadeIn("RayDevScene");
        ScreenFade.SFade.AudioFadeOut(ass,ass.volume);

    }


}
