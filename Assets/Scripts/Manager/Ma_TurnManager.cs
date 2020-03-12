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
        //AI PART A CHANGER
        GameManager.Instance.aiManager.ChoosePattern();
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
        GameManager.Instance.UpdateFeedBackAutourGrid(0);

        GameManager.Instance.uiManager.ClearAllMultiplierUi();
        GameManager.Instance.uiManager.EnableDisableEndturnButton(false);
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

            // If not game end
            if (CurrentTurn <= MaxTurn)
            {
                GameManager.Instance.patternManager.OnTurnEnd();
                GameManager.Instance.uiManager.UpdateTurnsbarText(CurrentTurn, MaxTurn);
                GameManager.Instance.ResetMove();
                GameManager.Instance.comboManager.ResetMultiplier();

                //AI PART A CHANGER
                GameManager.Instance.aiManager.ChoosePattern();
            }
            else
            {
                GameManager.Instance.patternManager.OnTurnEnd(true);
            }
        }
    }

    public void OnNextRound() {
        CurrentTurn = 1;
        MaxTurn = GameManager.Instance.levelConfig.rounds[GameManager.Instance.currentRoundCountFinished].turnLimit;
        GameManager.Instance.UpdateFeedBackAutourGrid(0);

        GameManager.Instance.patternManager.OnTurnEnd(true, true);
        GameManager.Instance.uiManager.UpdateTurnsbarText(CurrentTurn, MaxTurn);
        GameManager.Instance.ResetMove();
        GameManager.Instance.comboManager.ResetMultiplier();
    }

    public bool IsLastRoundFinished()
    {
        Debug.LogError("IsLastRoundFinished() " + CurrentTurn + ">" + MaxTurn);
        return CurrentTurn > MaxTurn;
    }
}
