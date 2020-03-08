using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Ma_MainMenuManager : Singleton<Ma_MainMenuManager>
{
    public List<Sc_CharacterParameters> selectedCharacters;
    public Transform[] selectedCharactersEmplacements;
    private GameObject[] selectedEmplacementsGameobjects = new GameObject[3];
    private bool[] selectedEmplacementsStatus = new bool[3];

    [Header("Navigation elements")]

    public RectTransform currentScreen;
    [Space]
    public RectTransform MainMenuRect;
    public RectTransform OptionsRect;
    public RectTransform LevelRect;
    public RectTransform CharacterRect;
    public RectTransform ItemsUpgradeRect;

    [Header("Team Builder elements")]
    public GridLayoutGroup cardsGrid;
    public RectTransform selectedCharactersEmplacementsRect;

    private void Start()
    {
        currentScreen = MainMenuRect;
    }

    // -------------
    // NAVIGATION
    // -------------

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

    // -------------
    // TEAM BUILDER
    // -------------

    // Add the card to the selected characters zone
    public void AddCardToSelectedEmplacements(GameObject card)
    {
        for(int i = 0; i < selectedEmplacementsStatus.Length; i++)
        {
            if (!selectedEmplacementsStatus[i])
            {
                card.transform.DOMove(selectedCharactersEmplacements[i].position, 0.2f, false);
                selectedEmplacementsStatus[i] = true;
                selectedEmplacementsGameobjects[i] = card;
                return;
            }
        }
    }

    // Remove the card to the selected characters zone
    public void RemoveCardFromSelectedEmplacement(GameObject card)
    {
        for(int j = 0; j < selectedEmplacementsGameobjects.Length; j++)
        {
            if(selectedEmplacementsGameobjects[j] == card)
            {
                selectedEmplacementsStatus[j] = false;
                selectedEmplacementsGameobjects[j] = null;
                return;
            }
        }
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
}
