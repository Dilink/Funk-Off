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

    [Header("Team Builder elements")]
    public GridLayoutGroup cardsGrid;
    public RectTransform selectedCharactersEmplacementsRect;


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

    public void GetSelectedPlayerScriptablesList()
    {
        for(int k = 0; k < selectedEmplacementsGameobjects.Length; k++)
        {
            if(selectedEmplacementsGameobjects[k] != null)
                selectedCharacters.Add(selectedEmplacementsGameobjects[k].GetComponent<Sc_TeamBuilderPlayerCards>().characterParameters);
        }
    }
}
