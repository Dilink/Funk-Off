using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Ma_MainMenuManager : MonoBehaviour
{
    public static Ma_MainMenuManager Instance;

    public List<Sc_CharacterParameters> selectedCharacters;
    public Transform[] selectedCharactersEmplacements;
    public GameObject[] selectedEmplacementsGameobjects = new GameObject[3];
    public bool[] selectedEmplacementsStatus = new bool[3];
    public string levelToLoadName;

    [Header("Navigation elements")]

    public RectTransform currentScreen;
    [Space]
    public RectTransform MainMenuRect;
    public RectTransform OptionsRect;
    public RectTransform LevelRect;
    public RectTransform CharacterRect;
    public RectTransform ItemsUpgradeRect;

    [Header("MainMenu elements")]

    [Header("Options elements")]

    [Header("Levels elements")]

    public GameObject[] levelPlayButtonsGameobjects = new GameObject[3];
    public GameObject[] level1GradesGameobjects = new GameObject[5]; // image du grade
    public GameObject[] level2GradesGameobjects = new GameObject[5];
    public GameObject[] level3GradesGameobjects = new GameObject[5];
    public int currentLevel = 1;
    public int levelDone;
    public int gradeLevelDone;

    [Header("Items elements")]

    [Header("Team Builder elements")]
    public GridLayoutGroup cardsGrid;
    public Sc_TeamBuilderPlayerCards[] allCards;
    public RectTransform characterEmplacementOne;
    public RectTransform characterEmplacementTwo;
    public RectTransform characterEmplacementThree;
    public TMP_Text totalMovementText;

    public int totalMovement;

    [Header("Currency bar elements")]
    public TMP_Text currencyText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentScreen = MainMenuRect;
        allCards = FindObjectsOfType<Sc_TeamBuilderPlayerCards>();

        if (!PlayerPrefs.HasKey("00_JoeDinner"))
        {
            PlayerPrefs.SetInt("00_JoeDinner", 0);
        }
        else
        {
            GradeLevel(0, PlayerPrefs.GetInt("00_JoeDinner"));
        }

        if (!PlayerPrefs.HasKey("01_StarBowling"))
        {
            PlayerPrefs.SetInt("01_StarBowling", 0);
        }
        else
        {
            GradeLevel(0, PlayerPrefs.GetInt("01_StarBowling"));
        }

        if (!PlayerPrefs.HasKey("02_BrunoOffice"))
        {
            PlayerPrefs.SetInt("02_BrunoOffice", 0);
        }
        else
        {
            GradeLevel(0, PlayerPrefs.GetInt("02_BrunoOffice"));
        }
    }

    // -------------
    // NAVIGATION
    // -------------
    #region navigation
    // MAIN MENU
    public void DisplayMainMenuScreen(bool fromLeft)
    {
        if (fromLeft)
        {
            currentScreen.DOAnchorPos3DX(2000, 0.6f, false);
        }
        else
        {
            currentScreen.DOAnchorPos3DX(-2000, 0.6f, false);

        }

        currentScreen = MainMenuRect;

        if (fromLeft)
        {
            currentScreen.DOAnchorPos3DX(-2000, 0, false);
        }
        else
        {
            currentScreen.DOAnchorPos3DX(2000, 0, false);
        }

        currentScreen.DOAnchorPos3DX(0, 0.6f, false);
    }


    // OPTIONS
    public void DisplayOptionsScreen(bool fromLeft)
    {
        if (fromLeft)
        {
            currentScreen.DOAnchorPos3DX(2000, 0.6f, false);
        }
        else
        {
            currentScreen.DOAnchorPos3DX(-2000, 0.6f, false);
        }

        currentScreen = OptionsRect;

        if (fromLeft)
        {
            currentScreen.DOAnchorPos3DX(-2000, 0, false);
        }
        else
        {
            currentScreen.DOAnchorPos3DX(2000, 0, false);
        }

        currentScreen.DOAnchorPos3DX(0, 0.6f, false);
    }


    // LEVEL
    public void DisplayLevelScreen(bool fromLeft)
    {
        if (fromLeft)
        {
            currentScreen.DOAnchorPos3DX(2000, 0.6f, false);
        }
        else
        {
            currentScreen.DOAnchorPos3DX(-2000, 0.6f, false);
        }

        currentScreen = LevelRect;

        if (fromLeft)
        {
            currentScreen.DOAnchorPos3DX(-2000, 0, false);
        }
        else
        {
            currentScreen.DOAnchorPos3DX(2000, 0, false);
        }

        currentScreen.DOAnchorPos3DX(0, 0.6f, false);
    }


    // CHARACTERS
    public void DisplayCharacterScreen(bool fromLeft)
    {
        if (fromLeft)
        {
            currentScreen.DOAnchorPos3DX(2000, 0.6f, false);
        }
        else
        {
            currentScreen.DOAnchorPos3DX(-2000, 0.6f, false);

        }

        currentScreen = CharacterRect;

        if (fromLeft)
        {
            currentScreen.DOAnchorPos3DX(-2000, 0, false);
        }
        else
        {
            currentScreen.DOAnchorPos3DX(2000, 0, false);

        }

        currentScreen.DOAnchorPos3DX(0, 0.6f, false);
    }

    public void DisplayItemsUpgradeScreen()
    {
        ItemsUpgradeRect.DOAnchorPos3DY(0, 0.6f, false);
    }

    public void HideItemsDisplayScreen()
    {
        ItemsUpgradeRect.DOAnchorPos3DY(1200, 0.6f, false);
    }

    #endregion
    // -------------
    // OPTIONS
    // -------------
    #region options

    #endregion
    // -------------
    // LEVELS
    // -------------
    #region levels

    public void CheckLevelValidation(int levelClick)
    {
        //Check si on a débloqué le niveau que lequel on clique et si l'on a pas déja cliqué dessus
        if(currentLevel >= levelClick && levelPlayButtonsGameobjects[levelClick].activeInHierarchy == false)
        {
            for (int i = 0; i < levelPlayButtonsGameobjects.Length; i++)
            {
                if (levelPlayButtonsGameobjects[i].activeInHierarchy)
                {
                    levelPlayButtonsGameobjects[i].SetActive(false);
                    
                }
            }

            levelPlayButtonsGameobjects[levelClick].SetActive(true);
            
        }
    }
    public void LevelDoneBtn()
    {
        GradeLevel(levelDone, gradeLevelDone);
    }
    public void GradeLevel (int level, int grade)
    {
        // Joe's dinner
        if (level == 0)
        {
            if (PlayerPrefs.GetInt("00_JoeDinner") <= grade)
            {
                PlayerPrefs.SetInt("00_JoeDinner", grade);
            }
            else
            {
                return;
            }

            for (int i = 0; i < level1GradesGameobjects.Length; i++)
            {

                level1GradesGameobjects[i].SetActive(false);

            }

            level1GradesGameobjects[grade].SetActive(true);
        }

        // Star Bowling
        if (level == 1)
        {
            if (PlayerPrefs.GetInt("01_StarBowling") <= grade)
            {
                PlayerPrefs.SetInt("01_StarBowling", grade);
            }
            else
            {
                return;
            }

            for (int i = 0; i < level1GradesGameobjects.Length; i++)
            {

                level1GradesGameobjects[i].SetActive(false);

            }

            level1GradesGameobjects[grade].SetActive(true);
        }

        // Bruno's Office
        if (level == 2)
        {
            if (PlayerPrefs.GetInt("02_BrunoOffice") <= grade)
            {
                PlayerPrefs.SetInt("02_BrunoOffice", grade);
            }
            else
            {
                return;
            }

            for (int i = 0; i < level1GradesGameobjects.Length; i++)
            {

                level1GradesGameobjects[i].SetActive(false);

            }

            level1GradesGameobjects[grade].SetActive(true);
        }
    }
    #endregion
    // -------------
    // ITEMS UPGRADE
    // -------------
    #region items

    #endregion
    // -------------
    // TEAM BUILDER
    // -------------
    #region teambuilder


    public void GetSceneName(string sceneName)
    {
        levelToLoadName = sceneName;
    }

    public void StartButton()
    {
        Sc_LoadScreen.Instance.LoadThisScene(levelToLoadName);
    }

    // Add the card to the selected characters zone
    public void AddCardToSelectedEmplacements(GameObject card, int emplacement)
    {
        card.transform.DOMove(selectedCharactersEmplacements[emplacement].position, 0.2f, false);
        selectedEmplacementsStatus[emplacement] = true;
        selectedEmplacementsGameobjects[emplacement] = card;
    }

    // Remove the card to the selected characters zone
    public void RemoveCardFromSelectedEmplacement(GameObject card)
    {
        for(int i =0; i< selectedEmplacementsGameobjects.Length; i++)
        {
            if(selectedEmplacementsGameobjects[i] == card)
            {
                selectedEmplacementsStatus[i] = false;
                selectedEmplacementsGameobjects[i] = null;
            }
        }
    }

    public void UpdateMovementText( int move, bool sign)
    {
        if (sign)
            totalMovement += move;
        else
            totalMovement -= move;

        totalMovementText.text = totalMovement.ToString();
    }

    // Return all the scriptables objects of the cards in selected characters zone
    public void GetSelectedPlayerScriptablesList()
    {
        for(int k = 0; k < selectedEmplacementsGameobjects.Length; k++)
        {
            if(selectedEmplacementsGameobjects[k] != null)
                selectedCharacters.Add(selectedEmplacementsGameobjects[k].GetComponent<Sc_TeamBuilderPlayerCards>().characterParameters);
        }
    }

    public bool IsCharacterSelectionFull()
    {
        for(int i = 0; i < selectedEmplacementsStatus.Length; i++)
        {
            if (!selectedEmplacementsStatus[i])
            {
                return true;
            }
        }
        return false;
    }

    public bool checkForCard(GameObject card)
    {
        for (int i = 0; i< selectedEmplacementsGameobjects.Length; i++)
        {
            if(selectedEmplacementsGameobjects[i] == card)
            {
                return true;
            }
        }
        return false;
    }

    public void HighlightCard(int index)
    {
        allCards[index].transform.SetAsLastSibling();
        allCards[index].transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 1f);
    }

    public void UnhighlightCard(int index)
    {
        allCards[index].transform.SetAsLastSibling();
        allCards[index].transform.DOScale(new Vector3(1, 1, 1), 1f);
    }

    public void BrowsePlayerCardsLeft()
    {

    }

    public void BrowsePlayerCardsRight()
    {

    }
    #endregion
    // -------------
    // CURRENCY BAR
    // -------------
    #region currency

    public void UpdateCurrencyBar(float value)
    {
        currencyText.text = value.ToString();
    }

    #endregion
    // -------------
    // ANIMATIONS
    // -------------
    #region animations
    
    #endregion
}
