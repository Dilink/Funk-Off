using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Ma_UiManager : MonoBehaviour
{
    //   [SerializeField] Mb_PlayerCard[] allPlayerUi;
    [Header("PARAMETERS")]
    public float FunkBarFillSpeed = 0.5f;
    public Gradient funkBarGradient;

    [Space]
    [Header("Turnsbar elements")]
    public TMP_Text TurnsbarText;

    [Header("Movebar elements")]
    [SerializeField] TextMeshProUGUI moveLeftText;

    [Header("Patternsbar elements")]
    public List <RectTransform> PatternsbarElements;
    public Image[] PatternsbarIconsImg;
    public Image[] PatternsbarMultipliersImg;
    public Image[] PatternsbarCancelMarkersImg;
    public TMP_Text[] PatternsbarMultipliersTexts;

    private bool isPaternShaking;

    [Header("Funkbar elements")]
    public Image FunkbarFillImg;
    public List<Image> FunkbarMasksImg;
    public RectTransform FunkbarFillRect;
    public GameObject funkbarMasks;

    //[Header("PlayersStateBar elements")]


    [Header("PauseMenus elements")]
    public GameObject PauseMenu;
    public Image PauseMenubackgroundImg;
    public RectTransform PauseMainRect;
    public RectTransform PauseSettingsRect;
    public RectTransform PauseQuitConfirmRect;

    [Header("EndgameScreen elements")]
    public GameObject EndGameScreen;
    public RectTransform EndGameScreen_winRect;
    public RectTransform EndGameScreen_looseRect;

    [Header("UiCharacter")]
    public Image[] AllCharacterUi;

    private void Reset()
    {
        // PlayerCards elements
        //allPlayerUi = FindObjectsOfType<Mb_PlayerCard>();

        // Turnsbar elements
        TurnsbarText = GameObject.Find("TurnsBar_TextTurnsCount").GetComponent<TMP_Text>();

        // Patternsbar elements
        //PatternsbarElements = GameObject.Find("PatternsBar_elements").GetComponentsInChildren<GameObject>();
        //PatternsbarIconsImg = GameObject.Find("").GetComponentsInChildren<Image>();
        //PatternsbarMultipliersImg = GameObject.Find("PatternsBar_Multipliers").GetComponentsInChildren<Image>();
        //PatternsbarCancelMarkersImg = GameObject.Find("PatternsBar_CancelMarkers").GetComponentsInChildren<Image>();
        //PatternsbarMultipliersTexts = GameObject.Find("PatternsBar_MultipliersTexts").GetComponentsInChildren<TMP_Text>();

        // Funkbar elements
        FunkbarFillImg = GameObject.Find("Funkbar_Fill").GetComponent<Image>();
        FunkbarFillRect = GameObject.Find("Funkbar_Fill").GetComponent<RectTransform>();

        // PlayerStateBar elements

        // Pausemenu elements
        PauseMenu = GameObject.Find("PauseMenu");
        PauseMenubackgroundImg = GameObject.Find("PauseMenu_BackgroundBlack").GetComponent<Image>();
        PauseMainRect = GameObject.Find("PauseMain").GetComponent<RectTransform>();
        PauseSettingsRect = GameObject.Find("PauseSettings").GetComponent<RectTransform>();
        PauseQuitConfirmRect = GameObject.Find("QuitConfirm").GetComponent<RectTransform>();

        // EndgameScreen elements
        EndGameScreen = GameObject.Find("EndGameScreen");
        EndGameScreen_winRect = GameObject.Find("EndGameScreen_Win").GetComponent<RectTransform>();
        EndGameScreen_looseRect = GameObject.Find("EndGameScreen_Loose").GetComponent<RectTransform>();
}

    private void Awake()
    {
        //OLD MOVEMENT SYSTEM
      /*  for (int i = 0; i < allPlayerUi.Length; i++)
            allPlayerUi[i].playerAssigned = GameManager.Instance.allPlayers[i];*/
    }
    // ---------------------
    // TURNSBAR FUNCTIONS
    // ---------------------

    public void TESTUpdateTurns()
    {
        UpdateTurnsbarText(1, 1);
    }

    // Update the text of the Turnsbar to display current turn / Max turns
    public void UpdateTurnsbarText(int currentTurn, int maxTurn)
    {
        //Animation
        Sequence moveSeq = DOTween.Sequence();
        moveSeq.Append(TurnsbarText.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f));
        moveSeq.Append(TurnsbarText.transform.DORotate(new Vector3(0, 0, 20), 0.1f));
        moveSeq.PrependInterval(0.1f);
        moveSeq.Append(TurnsbarText.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        moveSeq.Append(TurnsbarText.transform.DORotate(new Vector3(0, 0, 0), 0.1f));

        TurnsbarText.text = (maxTurn - currentTurn).ToString();
    }

    // ---------------------
    // PATTERNSBAR FUNCTIONS
    // ---------------------

    public void RemovePattern(int emplacement, bool isPatternDestroyed=false)
    {
        StartCoroutine(RemovePatternAnimations(emplacement, isPatternDestroyed));
    }

    private IEnumerator RemovePatternAnimations(int emplacement, bool isPatternDestroyed)
    {
        if (!isPatternDestroyed)
        {
            PatternsbarMultipliersImg[emplacement].transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.2f).SetEase(Ease.OutCirc);
            PatternsbarMultipliersImg[emplacement].transform.DOLocalMoveY(PatternsbarElements[emplacement].anchoredPosition.y + 85, 0.2f, false).SetEase(Ease.OutCirc);
            PatternsbarMultipliersTexts[emplacement].transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.2f).SetEase(Ease.OutCirc);
            PatternsbarMultipliersTexts[emplacement].transform.DOLocalMoveY(PatternsbarElements[emplacement].anchoredPosition.y + 85, 0.2f, false).SetEase(Ease.OutCirc);

            PatternsbarElements[emplacement].transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.2f).SetEase(Ease.OutCirc);
            PatternsbarElements[emplacement].transform.DOLocalMoveY(PatternsbarElements[emplacement].anchoredPosition.y + 75, 0.2f, false).SetEase(Ease.OutCirc);

            yield return new WaitForSeconds(0.2f);

            //PatternsbarMultipliersImg[emplacement].transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.OutElastic);
            //PatternsbarMultipliersTexts[emplacement].transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.OutElastic);

            PatternsbarElements[emplacement].transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.OutElastic);

            yield return new WaitForSeconds(0.3f);

            PatternsbarMultipliersImg[emplacement].transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.InCubic);
            PatternsbarMultipliersImg[emplacement].transform.DOLocalMoveY(PatternsbarElements[emplacement].anchoredPosition.y - 85, 0.18f, false).SetEase(Ease.InCubic);
            PatternsbarMultipliersTexts[emplacement].transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.InCubic);
            PatternsbarMultipliersTexts[emplacement].transform.DOLocalMoveY(PatternsbarElements[emplacement].anchoredPosition.y - 85, 0.18f, false).SetEase(Ease.InCubic);

            PatternsbarElements[emplacement].transform.DOLocalMoveY(PatternsbarElements[emplacement].anchoredPosition.y - 275, 0.25f, false).SetEase(Ease.InCubic);
        }
        else
        {
            Debug.LogError("TODO: animation cancel");
        }
    }

    public void MovePatterns(int emplacement)
    {
        // Déplace le pattern concerné d'une case vers la gauche
        if(PatternsbarElements[emplacement].anchoredPosition.x >= -350)
        {
            // Déplacement du pattern
            PatternsbarElements[emplacement].transform.DOLocalMoveX(PatternsbarElements[emplacement].anchoredPosition.x - 200, 0.4f, false).SetEase(Ease.InOutQuart);

            // Couleur du background et scale, pour qu'il se reset après avoir été sur la case grise
            PatternsbarElements[emplacement].localScale = new Vector3(1, 1, 1);
            PatternsbarElements[emplacement].GetChild(1).GetComponent<Image>().color = Color.white;
        }
        else // Si le pattern concerné est le plus à gauche, le renvoit sur la case grise
        {
            RespawnPattern(emplacement);
        }
    }

    public void RespawnPattern(int emplacement)
    {
        // Remet le pattern sur la case grise
        PatternsbarElements[emplacement].anchoredPosition = new Vector2(600, 0);

        // Change le background et le scale pour qu'il s'adapte à la case grise 
        PatternsbarElements[emplacement].localScale = new Vector3(0, 0, 0);
        PatternsbarElements[emplacement].transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.4f).SetEase(Ease.OutBack);
        PatternsbarElements[emplacement].GetChild(1).GetComponent<Image>().color = Color.grey;

        // Réarrange la liste pour qu'elle match le nouvel ordre visuel
        RectTransform temp = PatternsbarElements[emplacement];
        PatternsbarElements.RemoveAt(emplacement);
        PatternsbarElements.Add(temp);
    }

    // Update the Icon of the patternsbar
    public void UpdatePatternsBarIcon(int emplacement,Sc_Pattern pattern)
    {
        PatternsbarElements[emplacement].GetChild(2).GetComponent<Image>().sprite = pattern.sprite;
    }

    // MULTIPLIERS

    // Update the multipliers visuals
    public void UpdateMultiplierIcon(int emplacement, Color color, Color colorbkg, string text)
    {
        // Animation
        PatternsbarMultipliersImg[emplacement].transform.localScale = new Vector3(0, 0, 0);
        PatternsbarMultipliersTexts[emplacement].transform.localScale = new Vector3(0, 0, 0);
        PatternsbarMultipliersImg[emplacement].transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutElastic);
        PatternsbarMultipliersTexts[emplacement].transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutElastic);

        // Color and text
        PatternsbarMultipliersImg[emplacement].color = color;
        PatternsbarElements[emplacement].GetChild(0).GetComponent<Image>().color = colorbkg;
        PatternsbarMultipliersTexts[emplacement].text = text;
        PatternsbarMultipliersTexts[emplacement].color = Color.black;
    }

    // Remove the multiplier visual
    public void RemoveMultiplierIcon(int emplacement)
    {
        UpdateMultiplierIcon(emplacement, Color.clear, GameManager.Instance.comboManager.colorNone , "x1");
        PatternsbarMultipliersTexts[emplacement].color = Color.clear;
    }

    public void RemoveAllMultiplierIcon()
    {
        GameManager.Instance.UpdateFeedBackAutourGrid(0);

        for (int i =0; i<PatternsbarMultipliersImg.Length; i++)
        {
            UpdateMultiplierIcon(i, Color.clear, GameManager.Instance.comboManager.colorNone, "x1");
            PatternsbarMultipliersTexts[i].color = Color.clear;
        }
    }

    // Update the cancel marker visuals
    public void UpdateCancelMarkerIcon(int emplacement, bool active)
    {
        PatternsbarElements[emplacement].GetChild(3).GetComponent<Image>().color = active ? new Color(0.88f, 0.11f, 0.59f, 1.0f) : Color.clear;
    }

    // ---------------------
    // FUNKBAR FUNCTIONS
    // ---------------------

    public void GetAllMasksImages()
    {
        Transform[] masks = funkbarMasks.GetComponentsInChildren<Transform>();

        for(int i = 0; i < masks.Length;i++)
        {
            for (int j = 0; j < masks[i].childCount; j++)
            {
                FunkbarMasksImg.Add(masks[i].GetChild(j).GetComponent<Image>());
            }

        }
    }

    // Change the visual of the Funkbar to the indicated percentage
    public void UpdateFunkBar(float funkPercentage)
    {
        float filledMasksIndex = funkPercentage  * FunkbarMasksImg.Count;

        for (int i = 0; i < filledMasksIndex; i++)
        {
            FunkbarMasksImg[i].color = funkBarGradient.Evaluate(funkPercentage);
        }
    }

    // ---------------------
    // CHARACTERS UI FUNCTIONS
    // ---------------------
    public void TESTUpdateMoves()
    {
        UpdateMovesUi(1, 1);
    }

    public void UpdateMovesUi(int movesReturning, int moveForTheTurn)
    {
        //Animation
        Sequence moveSeq = DOTween.Sequence();
        moveSeq.Append(moveLeftText.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f));
        moveSeq.Append(moveLeftText.transform.DORotate(new Vector3(0,0, 20), 0.1f));
        moveSeq.PrependInterval(0.1f);
        moveSeq.Append(moveLeftText.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        moveSeq.Append(moveLeftText.transform.DORotate(new Vector3(0, 0, 0), 0.1f));

        // Change the text
        moveLeftText.text = movesReturning + " / " + moveForTheTurn;
    }

    public void ShakePattern(int indexToShake)
    {


        PatternsbarElements[indexToShake].DOScale(1.4f,0.3f).OnComplete(()=>
        {
            PatternsbarElements[indexToShake].DOScale(1f, 0.3f);
        });
    }

    // ---------------------
    // ENDGAME SCREEN UI FUNCTIONS
    // ---------------------

    public void DisplayEndgameScreen(bool issue)
    {
        EndGameScreen.SetActive(true);

        if (issue)
        {
            EndGameScreen_winRect.DOAnchorPosY(0, 0.4f, false);
        }
        else
        {
            EndGameScreen_looseRect.DOAnchorPosY(0, 0.4f, false);
        }
    }

    
    // ---------------------
    // PAUSE MENUS UI FUNCTIONS
    // ---------------------

    // When the pause menu is open from the game
    public void OpenPauseMenu()
    {
        EnableOrDisablePauseMenu();
        PauseMenubackgroundImg.DOColor(new Color(0, 0, 0, 0.5f), 0.6f);
        PauseMainRect.DOAnchorPosY(0, 0.6f, false);
    }

    // When the settings menu is open from the pause menu
    public void OpenSettingsMenu()
    {
        PauseSettingsRect.rotation = Quaternion.Euler(0, 90, 0);
        PauseSettingsRect.DORotate(new Vector3(0, 0, 0), 0.5f);
        PauseSettingsRect.gameObject.SetActive(true);
        PauseMainRect.gameObject.SetActive(false);
    }

    // When the settings menu is closed and return to pause menu
    public void CloseSettingsMenu()
    {
        PauseMainRect.rotation = Quaternion.Euler(0, 90, 0);
        PauseMainRect.DORotate(new Vector3(0, 0, 0), 0.5f);
        PauseSettingsRect.gameObject.SetActive(false);
        PauseMainRect.gameObject.SetActive(true);
    }

    // When the quit confirm options appear
    public void OpenConfirm()
    {
        PauseQuitConfirmRect.rotation = Quaternion.Euler(0, 90, 0);
        PauseQuitConfirmRect.DORotate(new Vector3(0, 0, 0), 0.5f);
        PauseQuitConfirmRect.gameObject.SetActive(true);
        PauseMainRect.gameObject.SetActive(false);
    }

    // When the quit confirm options disappear
    public void CloseConfirm()
    {
        PauseMainRect.rotation = Quaternion.Euler(0, 90, 0);
        PauseMainRect.DORotate(new Vector3(0, 0, 0), 0.5f);
        PauseQuitConfirmRect.gameObject.SetActive(false);
        PauseMainRect.gameObject.SetActive(true);
    }

    // When the pause menu is closed and return to the game
    public void ClosePauseMenu()
    {
        PauseMenubackgroundImg.DOColor(new Color(0, 0, 0, 0), 0.6f);
        PauseMainRect.DOAnchorPosY(1200, 0.6f, false);
        Invoke("EnableOrDisablePauseMenu", 0.6f);
    }

    // System, enable or disable the pause menu
    private void EnableOrDisablePauseMenu()
    {
        if (PauseMenu.activeInHierarchy)
        {
            PauseMenu.SetActive(false);
        }
        else
        {
            PauseMenu.SetActive(true);
        }       
    }

    public void DisplayFX(int emplacement, int fxIndex)
    {
        PatternsbarElements[emplacement].GetComponent<Mb_PatternBarElement>().PlayFX(fxIndex);
    }

    public void DeployUi(Mb_PlayerCard uiToDeploy)
    {
        uiToDeploy.transform.DOLocalMoveX(uiToDeploy.cardTransform.localPosition.x+ 70, 0.2f);
        uiToDeploy.DeployName();
        uiToDeploy.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.2f);
    }

    public void CleanUi(Mb_PlayerCard uiToClean)
    {
        uiToClean.transform.DOLocalMoveX(uiToClean.cardTransform.localPosition.x - 70, 0.2f);
        uiToClean.CleanName();
        uiToClean.transform.DOScale(new Vector3(1, 1, 1), 0.2f);
    }

}
