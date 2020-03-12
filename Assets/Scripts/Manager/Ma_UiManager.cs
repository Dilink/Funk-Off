using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;
using System.Linq;

[System.Serializable]
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
    [Header("PARAMETERS")]
    public float FunkBarFillSpeed = 0.5f;
    public Gradient funkBarGradient;
    public Animator[] funkBarKey;

    [Space]
    [Header("Turnsbar elements")]
    public TMP_Text TurnsbarText;

    [Header("Movebar elements")]
    [SerializeField] TextMeshProUGUI moveLeftText;

    [Header("Patternsbar elements")]
    [ReadOnly] [ShowInInspector] [SerializeField] private List<PatternItem> patternItems = new List<PatternItem>();
    [ReadOnly] [ShowInInspector] [SerializeField] private List<PatternMultiplier> patternMultipliers = new List<PatternMultiplier>();

    private bool isPaternShaking;
    private static System.Random rand = new System.Random();

    [Header("Funkbar elements")]
    public Renderer funkbarRend;
    private Material funkbarMatBase;
    private Material funkbarMatInstance;
    public MeshRenderer[] allSquare;


    [Header("Endturn Button elements")]
    public Button endturnButton;

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
        // Turnsbar elements
        TurnsbarText = GameObject.Find("TurnsBar_TextTurnsCount").GetComponent<TMP_Text>();

        // Funkbar elements

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
        funkbarMatBase = funkbarRend.material;
        funkbarMatInstance = new Material(funkbarMatBase);

        for(int i=0; i < allSquare.Length; i++)
        {
            allSquare[i].material = funkbarMatInstance;
        }
    }

    // ---------------------
    // TURNSBAR FUNCTIONS
    // ---------------------

    public void ClearAllMultiplierUi()
    {
        for (int i = 0; i < patternItems.Count; i++)
        {
            UpdateMultiplierIcon(i, Color.clear, GameManager.Instance.comboManager.colorNone, "");
        }
    }

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
        moveSeq.Append(TurnsbarText.transform.DOLocalRotate(new Vector3(0, 0, 20), 0.1f));
        moveSeq.PrependInterval(0.1f);
        moveSeq.Append(TurnsbarText.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        moveSeq.Append(TurnsbarText.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f));

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
            PatternItem pattern = patternItems[emplacement];
            Image multiplierImg = patternMultipliers[emplacement].multiplierImg;
            TMP_Text multiplierText = patternMultipliers[emplacement].multiplierText;

            multiplierImg.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.2f).SetEase(Ease.OutCirc);
            multiplierImg.transform.DOLocalMoveY(pattern.transform.anchoredPosition.y + 85, 0.2f, false).SetEase(Ease.OutCirc);
            multiplierText.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.2f).SetEase(Ease.OutCirc);
            multiplierText.transform.DOLocalMoveY(pattern.transform.anchoredPosition.y + 85, 0.2f, false).SetEase(Ease.OutCirc);

            pattern.transform.transform.DOScale(new Vector3(1.6f, 1.6f, 1.6f), 0.2f).SetEase(Ease.OutCirc);
            pattern.transform.transform.DOLocalMoveY(pattern.transform.anchoredPosition.y + 75, 0.2f, false).SetEase(Ease.OutCirc);

            yield return new WaitForSeconds(0.2f);

            //multiplierImg.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.OutElastic);
            //multiplierText.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.OutElastic);

            pattern.transform.transform.DOScale(new Vector3(1.8f, 1.8f, 1.8f), 0.5f).SetEase(Ease.OutElastic);

            yield return new WaitForSeconds(0.3f);

            multiplierImg.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.InCubic);
            multiplierImg.transform.DOLocalMoveY(pattern.transform.anchoredPosition.y - 85, 0.18f, false).SetEase(Ease.InCubic);
            multiplierText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f).SetEase(Ease.InCubic);
            multiplierText.transform.DOLocalMoveY(pattern.transform.anchoredPosition.y - 85, 0.18f, false).SetEase(Ease.InCubic);

            pattern.transform.transform.DOLocalMoveY(pattern.transform.anchoredPosition.y - 275, 0.25f, false).SetEase(Ease.InCubic);
        }
        else
        {
            Debug.LogError("TODO: animation cancel");
        }
    }

    public void MovePatterns(int emplacement)
    {
        PatternItem pattern = patternItems[emplacement];

        // Déplace le pattern concerné d'une case vers la gauche
        if (pattern.transform.anchoredPosition.x >= -350)
        {
            // Déplacement du pattern
            pattern.transform.transform.DOLocalMoveX(pattern.transform.anchoredPosition.x - 200, 0.4f, false).SetEase(Ease.InOutQuart);

            // Couleur du background et scale, pour qu'il se reset après avoir été sur la case grise
            pattern.transform.localScale = new Vector3(1, 1, 1);
            pattern.background.color = Color.white;
        }
        else // Si le pattern concerné est le plus à gauche, le renvoit sur la case grise
        {
            RespawnPattern(emplacement);
        }
    }

    public void RespawnPattern(int emplacement)
    {
        PatternItem pattern = patternItems[emplacement];

        // Remet le pattern sur la case grise
        pattern.transform.anchoredPosition = new Vector2(600, 0);

        // Change le background et le scale pour qu'il s'adapte à la case grise 
        pattern.transform.localScale = new Vector3(0, 0, 0);
        pattern.transform.transform.DOScale(new Vector3(0.8f, 0.8f, 0.8f), 0.4f).SetEase(Ease.OutBack);
        pattern.background.color = Color.grey;

        // Réarrange la liste pour qu'elle match le nouvel ordre visuel
        patternItems.RemoveAt(emplacement);
        patternItems.Add(pattern);
    }

    // Update the Icon of the patternsbar
    public void UpdatePatternsBarIcon(int emplacement,Sc_Pattern pattern)
    {
        PatternItem item = patternItems[emplacement];

        item.icon.sprite = pattern.sprite;
    }

    // MULTIPLIERS

    // Update the multipliers visuals
    public void UpdateMultiplierIcon(int emplacement, Color color, Color colorbkg, string text)
    {
        PatternItem item = patternItems[emplacement];
        Image img = patternMultipliers[emplacement].multiplierImg;
        TMP_Text tex = patternMultipliers[emplacement].multiplierText;
        // Animation
        img.transform.localScale = new Vector3(0, 0, 0);
        tex.transform.localScale = new Vector3(0, 0, 0);
        img.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutElastic);
        tex.transform.DOScale(new Vector3(1f, 1f, 1f), 0.8f).SetEase(Ease.OutElastic);

        // Color and text
        img.color = color;
        item.color.color = colorbkg;
        tex.text = text;
        tex.color = Color.black;
    }

    // Remove the multiplier visual
    public void RemoveMultiplierIcon(int emplacement)
    {
        UpdateMultiplierIcon(emplacement, Color.clear, GameManager.Instance.comboManager.colorNone , "x1");
        patternMultipliers[emplacement].multiplierText.color = Color.clear;
    }

    public void RemoveAllMultiplierIcon()
    {
        GameManager.Instance.UpdateFeedBackAutourGrid(0);

        for (int i = 0; i < patternItems.Count; i++)
        {
            UpdateMultiplierIcon(i, Color.clear, GameManager.Instance.comboManager.colorNone, "x1");
            patternMultipliers[i].multiplierText.color = Color.clear;
        }
    }

    // Update the cancel marker visuals
    public void UpdateCancelMarkerIcon(int emplacement, bool active)
    {
       // PatternsbarElements[emplacement].GetChild(3).GetComponent<Image>().color = active ? new Color(0.88f, 0.11f, 0.59f, 1.0f) : Color.clear;
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
        if (funkPercentage >= funkBarShader.GetFloat("_STEP"))
        { 
            funkBarShader.DOFloat(funkPercentage, "_STEP", FunkBarFillSpeed).SetEase(Ease.OutQuint);
            funkBarShader.DOColor(funkBarGradient.Evaluate(funkPercentage), "_COLO", FunkBarFillSpeed);

            yield return new WaitForSeconds(FunkBarFillSpeed);

            for (int i = 0; i < funkBarKey.Length; i++)
            {
                yield return new WaitForSeconds(0.008f);
                funkBarKey[i].SetTrigger("isGoingUp");
            }
        }
        else if (funkPercentage < funkBarShader.GetFloat("_STEP"))
        {
            Animator[] funkBarKeyClone = new Animator[funkBarKey.Length];
            funkBarKey.CopyTo(funkBarKeyClone, 0);

            RandomizeArray(ref funkBarKeyClone);

            for (int i = 0; i < funkBarKeyClone.Length; i+=2)
            {
                yield return new WaitForSeconds(0.005f);
                funkBarKeyClone[i].SetTrigger("isGoingDown");
                funkBarKeyClone[i+1].SetTrigger("isGoingDown");
            }
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
        moveSeq.Append(moveLeftText.transform.DOLocalRotate(new Vector3(0,0, 20), 0.1f));
        moveSeq.PrependInterval(0.1f);
        moveSeq.Append(moveLeftText.transform.DOScale(new Vector3(1, 1, 1), 0.1f));
        moveSeq.Append(moveLeftText.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.1f));

        // Change the text
        moveLeftText.text = movesReturning + " / " + moveForTheTurn;
    }

    public void ShakePattern(int indexToShake)
    {
        PatternItem item = patternItems[indexToShake];

        item.transform.DOScale(1.4f,0.3f).OnComplete(()=>
        {
            item.transform.DOScale(1f, 0.3f);
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
    // ENDTURN BUTTON UI FUNCTIONS
    // ---------------------

    public void EnableDisableEndturnButton(bool status)
    {
        Debug.Log("Endturn button = " + status);
        endturnButton.interactable = status;
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
        PatternItem item = patternItems[emplacement];
        item.element.PlayFX(fxIndex);
    }

    public void DeployUi(Mb_PlayerCard uiToDeploy)
    {
        uiToDeploy.transform.DOLocalMoveX(uiToDeploy.cardTransform.localPosition.x+ 70, 0.2f).SetEase(Ease.OutQuint);
        uiToDeploy.DeployName();
    }

    public void CleanUi(Mb_PlayerCard uiToClean)
    {
        uiToClean.transform.DOLocalMoveX(uiToClean.cardTransform.localPosition.x - 70, 0.2f).SetEase(Ease.OutQuint);
        uiToClean.CleanName();
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
}
