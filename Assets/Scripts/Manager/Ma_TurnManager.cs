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
    }

    public void EndTurn()
    {
        GameManager.Instance.UpdateFeedBackAutourGrid(0);

        GameManager.Instance.OnTurnEndPre();
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
                GameManager.Instance.ResetMove();
                GameManager.Instance.comboManager.ResetMultiplier();

                //AI PART A CHANGER
                GameManager.Instance.aiManager.ChoosePattern();

                StartCoroutine(PreventPlayerFromActing());
            }
            GameManager.Instance.OnTurnEndPost(isLevelFinished: true);
        }
    }

    public IEnumerator PreventPlayerFromActing()
    {
        GameManager game = GameManager.Instance;
        game.uiManager.DisplayRoundIntermediateScreen(game.currentRoundCountFinished);
        game.canActForced = true;
        game.DisableActing();
        //yield return new WaitForSeconds(game.timeBetweenTurns);
        yield return new WaitForSeconds(1.0f);
        game.canActForced = false;
        game.EnableActing();
    }

    public void OnNextRound() {
        GameManager.Instance.FunkVariation(-1);
        CurrentTurn = 1;
        MaxTurn = GameManager.Instance.levelConfig.rounds[GameManager.Instance.currentRoundCountFinished].turnLimit;
    }

    public bool IsLastRoundFinished()
    {
        return CurrentTurn > MaxTurn;
    }

    public int GetMaxTurn()
    {
        return MaxTurn;
    }

    public int GetCurrentTurn()
    {
        return CurrentTurn;
    }
}
