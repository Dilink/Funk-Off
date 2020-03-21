using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using System.Linq;

/*[System.Serializable]
public struct PatternItem
{
    public RectTransform transform;
    public Image color;
    public Image background;
    public Image icon;
    public Mb_PatternBarElement element;
}

[System.Serializable]
public struct PatternMultiplier
{
    public Image multiplierImg;
    public TMP_Text multiplierText;
}

public class Ma_UiManager : MonoBehaviour
{
    [Space]

    [SerializeField] TextMeshProUGUI maxMoveText;

    [Header("Patternsbar elements")]
    [ReadOnly] [ShowInInspector] [SerializeField] private List<PatternItem> patternItems = new List<PatternItem>();
    [ReadOnly] [ShowInInspector] [SerializeField] private List<PatternMultiplier> patternMultipliers = new List<PatternMultiplier>();

    private bool isPaternShaking;


    //public MeshRenderer[] allSquare;
    
    //[Header("UiCharacter")]

    private void Reset()
    {
        // Turnsbar elements

        // Funkbar elements

        // PlayerStateBar elements
    }

    
        /*
        maxMoveText.text = GameManager.Instance.maxMovesPerTurn.ToString();
       // RemoveAllMultiplierIcon();
        funkbarMatBase = funkbarRend.material;
       
        UpdateFunkBar(0);*
    }

    // ---------------------
    // TURNSBAR FUNCTIONS
    // ---------------------

    public void TESTUpdateTurns()
    {
        UpdateTurnsbarText(1, 1);
    }

    

    // ---------------------
    // PATTERNSBAR FUNCTIONS
    // ---------------------

    

    

    // MULTIPLIERS

    // Remove the multiplier visual
    public void RemoveMultiplierIcon(int emplacement)
    {
        UpdateMultiplierIcon(emplacement, Color.clear, GameManager.Instance.comboManager.colorNone , "x1");
        patternMultipliers[emplacement].multiplierText.color = Color.clear;
    }

    public void CancelAllMarkerIcon()
    {
         foreach(PatternItem markerToCancel in patternItems)
        {
            markerToCancel.background.GetComponent<Animator>().SetTrigger("DesactivateColor");
        }
    }

    
    

    

#if UNITY_EDITOR
    [Button(ButtonSizes.Medium), GUIColor(0.89f, 0.14f, 0.14f)]
    private void Populate()
    {
        Transform container = transform.Find("Canvas/PatternsBar");

        patternItems.Clear();
        patternMultipliers.Clear();

        Transform elements = container.Find("PatternsBar_elements");
        Transform multipliers = container.Find("PatternsBar_Multipliers");

        for (int i = 0; i < 6; i++)
        {
            RectTransform elmtTransform = elements.GetChild(i).GetComponent<RectTransform>();
            RectTransform multTransform = multipliers.GetChild(i).GetComponent<RectTransform>();

            PatternItem item = new PatternItem();
            item.transform = elmtTransform;
            item.color = elmtTransform.GetChild(0).GetComponent<Image>();
            item.background = elmtTransform.GetChild(1).GetComponent<Image>();
            item.icon = elmtTransform.GetChild(2).GetComponent<Image>();
            item.element = elmtTransform.GetComponent<Mb_PatternBarElement>();
            patternItems.Add(item);

            PatternMultiplier mult = new PatternMultiplier();
            mult.multiplierImg = multTransform.GetChild(0).GetComponent<Image>();
            mult.multiplierText = multTransform.GetChild(1).GetComponent<TMP_Text>();
            patternMultipliers.Add(mult);
        }
    }
#endif
}*/

public class Ma_UiManager : MonoBehaviour
{
    [Header("Patternsbar elements")]
    [ReadOnly] [ShowInInspector] [SerializeField] private List<Mb_PatternBarElement> patternElements = new List<Mb_PatternBarElement>();

    [ReadOnly]
    public List<RectTransform> patternAnchors = new List<RectTransform>();

    [Header("Endturn Button elements")]
    [SerializeField] Button endturnButton;

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

    [Header("Funkbar elements")]
    public Renderer funkbarRend;
    private Material funkbarMatBase;
    private Material funkbarMatInstance;
    [SerializeField] MeshRenderer[] allBars;

    [Header("PARAMETERS")]
    public float FunkBarFillSpeed = 0.5f;
    public Gradient funkBarGradient;
    public Animator[] funkBarKey;

    [Header("Turnsbar elements")]
    public TMP_Text TurnsbarText;

    [Header("Movebar elements")]
    [SerializeField] TextMeshProUGUI moveLeftText;
    [SerializeField] TextMeshProUGUI moveMaxText;

    private static System.Random rand = new System.Random();

    public Mb_PatternBarElement GetPatternElement(int index)
    {
        if (index < 0 || index >= patternElements.Count)
        {
            throw new IndexOutOfRangeException("Invalid pattern index.");
        }

        return patternElements[index];
    }

    private void Start()
    {
        funkbarMatInstance = new Material(funkbarRend.material);
        for (int i = 0; i < allBars.Length; i++)
        {
            allBars[i].material = funkbarMatInstance;
        }

        funkbarMatInstance.SetFloat("_STEP", 0);
    }
    // ---------------------
    // ENDGAME SCREEN UI FUNCTIONS
    // ---------------------

    public void DisplayEndgameScreen(bool issue)
    {
        EndGameScreen.SetActive(true);
        (issue ? EndGameScreen_winRect : EndGameScreen_looseRect).DOAnchorPosY(0, 0.4f, false);
    }

    // ---------------------
    // ENDTURN BUTTON UI FUNCTIONS
    // ---------------------

    public void EnableDisableEndturnButton(bool status)
    {
     // endturnButton.interactable = status;
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
        PauseSettingsRect.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
        PauseSettingsRect.gameObject.SetActive(true);
        PauseMainRect.gameObject.SetActive(false);
    }

    // When the settings menu is closed and return to pause menu
    public void CloseSettingsMenu()
    {
        PauseMainRect.rotation = Quaternion.Euler(0, 90, 0);
        PauseMainRect.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
        PauseSettingsRect.gameObject.SetActive(false);
        PauseMainRect.gameObject.SetActive(true);
    }

    // When the quit confirm options appear
    public void OpenConfirm()
    {
        PauseQuitConfirmRect.rotation = Quaternion.Euler(0, 90, 0);
        PauseQuitConfirmRect.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
        PauseQuitConfirmRect.gameObject.SetActive(true);
        PauseMainRect.gameObject.SetActive(false);
    }

    // When the quit confirm options disappear
    public void CloseConfirm()
    {
        PauseMainRect.rotation = Quaternion.Euler(0, 90, 0);
        PauseMainRect.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
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

    public void RestartLevel()
    {
        Sc_LoadScreen.Instance.LoadThisScene(SceneManager.GetActiveScene().name);
    }

    // System, enable or disable the pause menu
    private void EnableOrDisablePauseMenu()
    {
        PauseMenu.SetActive(!PauseMenu.activeInHierarchy);
    }

    // ---------------------
    // FUNKBAR FUNCTIONS
    // ---------------------

    // Change the visual of the Funkbar to the indicated percentage
    public void UpdateFunkBar(float funkPercentage)
    {
        StartCoroutine(UpdateFunkBarCoroutine(funkPercentage));
    }

    private void RandomizeArray(ref Animator[] Array)
    {
        Array = Array.OrderBy(x => rand.Next()).ToArray();
    }

    private IEnumerator UpdateFunkBarCoroutine(float funkPercentage)
    {
        funkbarMatInstance.DOFloat(funkPercentage, "_STEP", FunkBarFillSpeed).SetEase(Ease.OutQuint);
        funkbarMatInstance.DOColor(funkBarGradient.Evaluate(funkPercentage), "_COLO", FunkBarFillSpeed);

        if (funkPercentage >= funkbarMatInstance.GetFloat("_STEP"))
        {

            yield return new WaitForSeconds(FunkBarFillSpeed);

            for (int i = 0; i < funkBarKey.Length; i++)
            {
                yield return new WaitForSeconds(0.008f);
                funkBarKey[i].SetTrigger("isGoingUp");
            }
        }
        else
        {
            Animator[] funkBarKeyClone = new Animator[funkBarKey.Length];
            funkBarKey.CopyTo(funkBarKeyClone, 0);

            RandomizeArray(ref funkBarKeyClone);

            for (int i = 0; i < funkBarKeyClone.Length - 1; i += 2)
            {
                yield return new WaitForSeconds(0.005f);
                funkBarKeyClone[i].SetTrigger("isGoingDown");
                funkBarKeyClone[i + 1].SetTrigger("isGoingDown");
            }
        }
    }

    public void UpdateMaxMoveUi(int MaxMoves)
    {
        moveMaxText.text = MaxMoves.ToString();
    }
    
    public void UpdateMovesUi(int moveForTheTurn)
    {
        //Animation
        Sequence moveSeq = DOTween.Sequence();
        moveSeq.Append(moveLeftText.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f));
        moveSeq.Append(moveLeftText.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.1f));
        moveSeq.PrependInterval(0.1f);
        moveSeq.Append(moveLeftText.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        moveSeq.Append(moveLeftText.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f));

        // Change the text
        moveLeftText.text = moveForTheTurn.ToString();
    }

    // Update the text of the Turnsbar to display current turn / Max turns
    public void UpdateTurnsbarText(int currentTurn, int maxTurn)
    {
        //Animation
        Sequence moveSeq = DOTween.Sequence();
        moveSeq.Append(TurnsbarText.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f));
        moveSeq.Append(TurnsbarText.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.1f));
        moveSeq.PrependInterval(0.1f);
        moveSeq.Append(TurnsbarText.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        moveSeq.Append(TurnsbarText.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f));

        TurnsbarText.text = (maxTurn - currentTurn).ToString();
    }

    public void RemoveAllMultiplierIcon()
    {
        GameManager.Instance.UpdateFeedBackAutourGrid(0);

        for (int i = 0; i < patternElements.Count; i++)
            patternElements[i].RemoveMultiplierIcon();
    }

    public void ClearAllMultiplierUi()
    {
        foreach (Mb_PatternBarElement patternElement in patternElements)
        {
            patternElement.UpdateMultiplierIcon(Color.clear, GameManager.Instance.comboManager.colorNone, "");
        }
    }

    public void RespawnPattern(Mb_PatternBarElement element)
    {
        int emplacement = patternElements.IndexOf(element);
        // Réarrange la liste pour qu'elle match le nouvel ordre visuel
        patternElements.RemoveAt(emplacement);
        patternElements.Add(element);
    }

    public void RespawnPattern(int index)
    {
        Mb_PatternBarElement element = patternElements[index];
        // Réarrange la liste pour qu'elle match le nouvel ordre visuel
        patternElements.RemoveAt(index);
        patternElements.Add(element);
    }

    public void DeployUi(Mb_PlayerCard uiToDeploy)
    {
        uiToDeploy.transform.DOLocalMoveX(uiToDeploy.cardTransform.localPosition.x + 70, 0.2f).SetEase(Ease.OutQuint);
        uiToDeploy.DeployName();
    }

    public void CleanUi(Mb_PlayerCard uiToClean)
    {
        uiToClean.transform.DOLocalMoveX(uiToClean.cardTransform.localPosition.x - 70, 0.2f).SetEase(Ease.OutQuint);
        uiToClean.CleanName();
    }


    public void MovePatterns(int FinishedPattern)
    {
        for (int i = FinishedPattern; i < patternElements.Count(); i ++)
        {
            if (i == FinishedPattern)
            {
                patternElements[FinishedPattern].RespawnAnimation();
            }
           else
           {
                patternElements[i].rectTransform.DOScale(1, .4f);
                patternElements[i].rectTransform.DOMove(patternAnchors[i - 1].transform.position, .5f).SetEase(Ease.InOutQuart);
           }
        }
    }

#if UNITY_EDITOR
    [Button(ButtonSizes.Medium), GUIColor(0.89f, 0.14f, 0.14f)]
    private void Populate()
    {
        patternElements.Clear();
        patternAnchors.Clear();

        Transform patternElementsContainer = GameObject.Find("PatternsBar_elements").transform;
        Transform patternAnchorsContainer = GameObject.Find("PatternsBar_anchors").transform;

        for (int i = 0; i < patternElementsContainer.childCount; i++)
        {
            Mb_PatternBarElement pattern = patternElementsContainer.GetChild(i).GetComponent<Mb_PatternBarElement>();
            patternElements.Add(pattern);

            RectTransform rect = patternAnchorsContainer.GetChild(i).GetComponent<RectTransform>();
            patternAnchors.Add(rect);
        }

        // Pausemenu elements
        PauseMenu = gameObject.transform.Find("Canvas/PauseMenu").gameObject;
        Transform pauseMenuCtnr = PauseMenu.transform;
        PauseMenubackgroundImg = pauseMenuCtnr.Find("PauseMenu_BackgroundBlack").GetComponent<Image>();
        PauseMainRect = pauseMenuCtnr.Find("PauseMain").GetComponent<RectTransform>();
        PauseSettingsRect = pauseMenuCtnr.Find("PauseSettings").GetComponent<RectTransform>();
        PauseQuitConfirmRect = pauseMenuCtnr.Find("QuitConfirm").GetComponent<RectTransform>();

        // EndgameScreen elements
        Transform mainUiCanvas = gameObject.transform.Find("MainUICanvas");
        EndGameScreen = mainUiCanvas.Find("EndGameScreen").gameObject;
        Transform endGameScreenCtnr = EndGameScreen.transform;
        EndGameScreen_winRect = endGameScreenCtnr.Find("EndGameScreen_Win").GetComponent<RectTransform>();
        EndGameScreen_looseRect = endGameScreenCtnr.Find("EndGameScreen_Loose").GetComponent<RectTransform>();

        TurnsbarText = mainUiCanvas.Find("Image/TurnsBar_TextTurnsCount").GetComponent<TMP_Text>();
        endturnButton = mainUiCanvas.Find("EndTurnButtonPart/EndTurnButton").GetComponent<Button>();
    }
#endif
}
