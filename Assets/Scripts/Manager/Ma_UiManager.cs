using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Ma_UiManager : MonoBehaviour
{
    //   [SerializeField] Mb_PlayerCard[] allPlayerUi;
    [SerializeField] TextMeshProUGUI moveLeftText;

    [Header("Turnsbar elements")]
    public TMP_Text TurnsbarText;

    [Header("Patternsbar elements")]
    public List <RectTransform> PatternsbarElements;
    public Image[] PatternsbarIconsImg;
    public Image[] PatternsbarMultipliersImg;
    public Image[] PatternsbarCancelMarkersImg;
    public TMP_Text[] PatternsbarMultipliersTexts;

    [Header("Funkbar elements")]
    public Image FunkbarFillImg;
    public RectTransform FunkbarFillRect;
    public Image FunkbarCursorImg;
    public RectTransform FunkbarCursorRect;

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

    [Header("Loadscreen elements")]
    public GameObject Loadscreen;
    public RectTransform LoadscreenRect;

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
        FunkbarCursorImg = GameObject.Find("Funkbar_Cursor").GetComponent<Image>();
        FunkbarCursorRect = GameObject.Find("Funkbar_Cursor").GetComponent<RectTransform>();

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

        // Loadscreen elements
        Loadscreen = GameObject.Find("LoadScreen");
        LoadscreenRect = Loadscreen.GetComponent<RectTransform>();
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

    // Update the text of the Turnsbar to display current turn / Max turns
    public void UpdateTurnsbarText(int currentTurn, int maxTurn)
    {
        TurnsbarText.text = currentTurn + "/" + maxTurn;
    }

    // ---------------------
    // PATTERNSBAR FUNCTIONS
    // ---------------------

    public void RemovePattern(int emplacement)
    {
        StartCoroutine(RemovePatternAnimations(emplacement));
    }

    private IEnumerator RemovePatternAnimations(int emplacement)
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

    public void MovePatterns(int emplacement)
    {
        // Déplace le pattern concerné d'une case vers la gauche
        if(PatternsbarElements[emplacement].anchoredPosition.x >= -350)
        {
            // Déplacement du pattern
            PatternsbarElements[emplacement].transform.DOLocalMoveX(PatternsbarElements[emplacement].anchoredPosition.x - 200, 0.4f, false).SetEase(Ease.InOutQuart);

            // Couleur du background et scale, pour qu'il se reset après avoir été sur la case grise
            PatternsbarElements[emplacement].localScale = new Vector3(1, 1, 1);
            PatternsbarElements[emplacement].GetChild(0).GetComponent<Image>().color = Color.white;
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
        PatternsbarElements[emplacement].GetChild(0).GetComponent<Image>().color = Color.grey;

        // Réarrange la liste pour qu'elle match le nouvel ordre visuel
        RectTransform temp = PatternsbarElements[emplacement];
        PatternsbarElements.RemoveAt(emplacement);
        PatternsbarElements.Add(temp);
    }

    // Update the Icon of the patternsbar
    public void UpdatePatternsBarIcon(int emplacement,Sc_Pattern pattern)
    {
        PatternsbarElements[emplacement].GetChild(1).GetComponent<Image>().sprite = pattern.sprite;
    }

    // Update the multipliers visuals
    public void UpdateMultiplierIcon(int emplacement, Color color, string text)
    {
        PatternsbarMultipliersImg[emplacement].transform.localScale = new Vector3(0, 0, 0);
        PatternsbarMultipliersTexts[emplacement].transform.localScale = new Vector3(0, 0, 0);
        PatternsbarMultipliersImg[emplacement].transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutElastic);
        PatternsbarMultipliersTexts[emplacement].transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutElastic);
        PatternsbarMultipliersImg[emplacement].color = color;
        PatternsbarMultipliersTexts[emplacement].text = text;
        PatternsbarMultipliersTexts[emplacement].color = Color.black;
    }

    // Remove the multiplier visual
    public void RemoveMultiplierIcon(int emplacement)
    {
        UpdateMultiplierIcon(emplacement, Color.clear, "x1");
        PatternsbarMultipliersTexts[emplacement].color = Color.clear;
    }

    public void RemoveAllMultiplierIcon()
    {
       for(int i =0; i<PatternsbarMultipliersImg.Length; i++)
        {
            UpdateMultiplierIcon(i, Color.clear, "x1");
            PatternsbarMultipliersTexts[i].color = Color.clear;
        }
    }

    // Update the cancel marker visuals
    public void UpdateCancelMarkerIcon(int emplacement, bool active)
    {
        PatternsbarElements[emplacement].GetChild(2).GetComponent<Image>().color = active ? new Color(0.88f, 0.11f, 0.59f, 1.0f) : Color.clear;
    }

    // ---------------------
    // FUNKBAR FUNCTIONS
    // ---------------------

    // Change the visual of the Funkbar to the indicated percentage
    public void UpdateFunkBar(float funkPercentage)
    {
        FunkbarFillImg.fillAmount = funkPercentage;
        FunkbarCursorRect.anchoredPosition = new Vector2(FunkbarFillRect.sizeDelta.x * FunkbarFillImg.fillAmount, FunkbarCursorRect.anchoredPosition.y);
    }

    // ---------------------
    // CHARACTERS UI FUNCTIONS
    // ---------------------

    public void UpdateMovesUi(int movesReturning, int moveForTheTurn)
    {
        moveLeftText.text = movesReturning + " / " + moveForTheTurn;
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
    // LOADSCREEN FUNCTIONS
    // ---------------------

    public void DisplayLoadscreen()
    {
        EnableOrDisableLoadScreen();
        LoadscreenRect.anchoredPosition = new Vector2(-2225, 0);
        LoadscreenRect.DOAnchorPosX(0, 0.8f, false);
    }

    public void HideLoadscreen()
    {
        LoadscreenRect.DOAnchorPosX(2225, 0.8f, false);
        Invoke("EnableOrDisableLoadScreen", 0.8f);
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

    int fxIndexCount = -1;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            for (int i = 0; i < 5; i++)
                displayFX(i, (++fxIndexCount) % 5);
    }

    public void displayFX(int emplacement, int fxIndex)
    {
        PatternsbarElements[emplacement].GetComponent<Mb_PatternBarElement>().PlayFX(fxIndex);
    }
}
