using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ma_UiManager : MonoBehaviour
{
    [SerializeField] Mb_PlayerCard[] allPlayerUi;

    [Header("Turnsbar elements")]
    public TMP_Text TurnsbarText;

    [Header("Funkbar elements")]
    public Image FunkbarFillImg;
    public RectTransform FunkbarFillRect;
    public Image FunkbarCursorImg;
    public RectTransform FunkbarCursorRect;

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
