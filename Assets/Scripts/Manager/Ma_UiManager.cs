using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ma_UiManager : MonoBehaviour
{
    public static Ma_UiManager instance;

    [SerializeField] Mb_PlayerCard[] allPlayerUi;

    [Header("Turnsbar elements")]
    public TMP_Text TurnsbarText;

    [Header("Funkbar elements")]
    public Image FunkbarFillImg;
    public RectTransform FunkbarFillRect;
    public Image FunkbarCursorImg;
    public RectTransform FunkbarCursorRect;

    private void Reset()
    {
        // Turnsbar elements
        TurnsbarText = GameObject.Find("TurnsBar_TextTurnsCount").GetComponent<TMP_Text>();

        // Funkbar elements
        FunkbarFillImg = GameObject.Find("Funkbar_Fill").GetComponent<Image>();
        FunkbarFillRect = GameObject.Find("Funkbar_Fill").GetComponent<RectTransform>();
        FunkbarCursorImg = GameObject.Find("Funkbar_Cursor").GetComponent<Image>();
        FunkbarCursorRect = GameObject.Find("Funkbar_Cursor").GetComponent<RectTransform>();
    }

    // Update the text of the Turnsbar to display current turn / Max turns
    public void UpdateTurnsbarText()
    {
        TurnsbarText.text = Ma_TurnManager.instance.CurrentTurn.ToString() + "/" + Ma_TurnManager.instance.MaxTurn.ToString();
    }

    // Change the visual of the Funkbar to the indicated percentage
    public void UpdateFunkBar(float funkPercentage)
    {
        FunkbarFillImg.fillAmount = funkPercentage / 100;
        FunkbarCursorRect.anchoredPosition = new Vector2(FunkbarFillRect.sizeDelta.x * FunkbarFillImg.fillAmount, FunkbarCursorRect.anchoredPosition.y);
    }

    public void UpdateCharacterUi(Mb_PlayerController playerConcerned, int MoveLeft, int MaxMove)
    {
        for (int i =0; i < allPlayerUi.Length; i++)
        {
            if (allPlayerUi[i].playerAssigned == playerConcerned)
            {
                allPlayerUi[i].UpdateMoveLeftUi(MoveLeft, MaxMove);
            }
        }
    }
}
