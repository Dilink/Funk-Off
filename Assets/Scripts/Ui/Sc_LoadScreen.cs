using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Sc_LoadScreen : MonoBehaviour
{
    public static Sc_LoadScreen Instance;

    [Header("PARAMETERS")]
    public float transitionTime = 1f;

    [Header("Loadscreen elements")]
    public GameObject Loadscreen;
    public RectTransform LoadscreenRect;
    private string sceneToLoad;


    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

    }


    // ---------------------
    // LOADSCREEN FUNCTIONS
    // ---------------------

    public void LoadThisScene(string scene)
    {
        EnableOrDisableLoadScreen();
        sceneToLoad = scene;
        LoadscreenRect.anchoredPosition = new Vector2(-2225, 0);
        LoadscreenRect.DOAnchorPosX(0, 0.8f, false);
        Invoke("HideLoadscreen", 0.8f);
    }

    public void HideLoadscreen()
    {
        SceneManager.LoadScene(sceneToLoad);
        LoadscreenRect.DOAnchorPosX(2225, 0.8f, false);
        Invoke("EnableOrDisableLoadScreen", transitionTime);
    }

    // System
    private void EnableOrDisableLoadScreen()
    {


        if (Loadscreen.activeInHierarchy)
        {
            Loadscreen.SetActive(false);
        }
        else
        {
            Loadscreen.SetActive(true);
        }
    }

}
