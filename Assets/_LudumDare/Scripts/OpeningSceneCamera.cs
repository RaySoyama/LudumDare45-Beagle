using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

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

    [SerializeField][ReadOnlyField]
    private float barkTime;

    public bool isGameStarted = false;

    [SerializeField]
    private GameObject barkIcon;

    [SerializeField]
    private GameObject BlackScreen;

    private Coroutine IHateThis;

    void Start()
    { 
        dollyCart.m_Speed = 0;
        isGameStarted = false;
        PPMat.SetFloat("_Effect", 222);
        barkIcon.SetActive(false);
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
                barkTime += Time.deltaTime;

                barkIcon.SetActive(true);

                if (InputSystem.WoofInput.IsBark == true)
                {
                    if (IHateThis == null)
                    { 
                        IHateThis = StartCoroutine(IHateYou());
                    }

                    dogAnim.SetTrigger("Bark");

                    if (barkTime >= 1.0f / 15.0f)
                    {
                        if (barkCount == EffectValue.Count)
                        {
                            //Load New Scene



                        }
                        else
                        {

                            barkTime -= 1.0f / 15.0f;

                            PPMat.SetFloat("_Effect", EffectValue[barkCount]);
                            barkCount++;

                            if (barkCount == EffectValue.Count)
                            {
                                barkTime = 0;
                                BlackScreen.SetActive(true);
                                StartCoroutine(ToScene());

                            }
                        }

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
        dogAnim.SetTrigger("Bark");
    }
    
    private IEnumerator ToScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("RayDevScene");
    }

}
