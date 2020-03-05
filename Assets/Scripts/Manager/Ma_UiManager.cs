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
    public Image[] PatternsbarIconsImg;
    public Image[] PatternsbarMultipliersImg;
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


    private void Reset()
    {
        // PlayerCards elements
        //allPlayerUi = FindObjectsOfType<Mb_PlayerCard>();

        // Turnsbar elements
        TurnsbarText = GameObject.Find("TurnsBar_TextTurnsCount").GetComponent<TMP_Text>();

        // Patternsbar elements
        PatternsbarIconsImg = GameObject.Find("PatternsBar_PatternsIcons").GetComponentsInChildren<Image>();
        PatternsbarMultipliersImg = GameObject.Find("PatternsBar_Multipliers").GetComponentsInChildren<Image>();
        PatternsbarMultipliersTexts = GameObject.Find("PatternsBar_MultipliersTexts").GetComponentsInChildren<TMP_Text>();

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

    // Update the Icon of the patternsbar
    public void UpdatePatternsBarIcon(int emplacement,Sc_Pattern pattern)
    {
        PatternsbarIconsImg[emplacement].sprite = pattern.sprite;
    }

    // Update the multipliers visuals
    public void UpdateMultiplierIcon(int emplacement, Color color, string text)
    {
        PatternsbarMultipliersImg[emplacement].color = color;
        PatternsbarMultipliersTexts[emplacement].text = text;
        PatternsbarMultipliersTexts[emplacement].color = Color.black;
    }

    // Remove the multiplier visual
    public void RemoveMultiplierIcon(int emplacement)
    {
        GameManager.Instance.uiManager.UpdateMultiplierIcon(emplacement, Color.clear, "x1");
        GameManager.Instance.uiManager.PatternsbarMultipliersTexts[emplacement].color = Color.clear;
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
    //OLD MOVEMENT SYSTEM
    /*
    public void UpdateCharacterUi(Mb_PlayerController playerConcerned, int MoveLeft, int MaxMove)
    {
        for (int i =0; i < allPlayerUi.Length; i++)
        {
            if (allPlayerUi[i].playerAssigned == playerConcerned)
            {
                //allPlayerUi[i].UpdateMoveLeftUi(MoveLeft, MaxMove);
            }
        }
    }

    public void UpdateCharacterIcons(Mb_PlayerController playerConcerned , Sprite icon, Sprite item, Sprite passive)
    {
        for (int i = 0; i < allPlayerUi.Length; i++)
        {
            if (allPlayerUi[i].playerAssigned == playerConcerned)
            {
                allPlayerUi[i].UpdateCardIcon(icon);
                allPlayerUi[i].UpdateCardItem(item);
                allPlayerUi[i].UpdateCardPassive(passive);
            }
        }
    }*/

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
}
