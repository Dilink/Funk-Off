using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ma_TurnManager : MonoBehaviour
{
    public static Ma_TurnManager instance; // Static instance

    [Header("Turns stats")]
    [SerializeField] int MaxTurn; // Max number of turns for this level
    private int CurrentTurn = 1; // Current turn number

    private void Start()
    {
        MaxTurn = GameManager.Instance.levelConfig.rounds[0].turnLimit;
        GameManager.Instance.uiManager.UpdateTurnsbarText(CurrentTurn, MaxTurn);
    }

    public void BeginTurn()
    {
        /*
        // Reset all player characters move number
        for(int i =0; i < GameManager.Instance.allPlayers.Length; i++)
        {
            GameManager.Instance.allPlayers[i]..ResetMove();
        }
        */
        Debug.LogWarning("Turn " + CurrentTurn + " has begun");
    }

    public void EndTurn()
    {
        // Pass to the next turn
        if (CurrentTurn <= MaxTurn)
        {
            CurrentTurn++;

            //old deplacement System
            /*
            for (int i =0; i < GameManager.Instance.allPlayers.Length;i++)
            {
                GameManager.Instance.allPlayers[i].ResetMove();
            }*/
            GameManager.Instance.patternManager.OnTurnEnd();
            GameManager.Instance.uiManager.UpdateTurnsbarText(CurrentTurn, MaxTurn);
            GameManager.Instance.ResetMove();
            GameManager.Instance.comboManager.RemoveAllMultipliers();
        }
        else // End the level
        {
            GameManager.Instance.CheckGameEnd();
        }

    }

    public void OnNextRound() {
        CurrentTurn = 1;
        MaxTurn = GameManager.Instance.levelConfig.rounds[GameManager.Instance.currentRoundCountFinished].turnLimit;
        GameManager.Instance.patternManager.OnTurnEnd(true, true);
        GameManager.Instance.uiManager.UpdateTurnsbarText(CurrentTurn, MaxTurn);
        GameManager.Instance.ResetMove();
        GameManager.Instance.comboManager.RemoveAllMultipliers();
    }
}
