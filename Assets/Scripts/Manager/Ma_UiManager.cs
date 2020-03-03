using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ma_UiManager : MonoBehaviour
{
    [SerializeField] Mb_PlayerCard[] allPlayerUi;

    public void UpdateFunkBar(float funkPercentage)
    {

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
