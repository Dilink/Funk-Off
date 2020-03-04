using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ma_TurnManager : MonoBehaviour
{
    public static Ma_TurnManager instance; // Static instance

    [Header("Turns stats")]
    public int MaxTurn; // Max number of turns for this level
    public int CurrentTurn; // Current turn number

    [Header("Moves stats")]
    public int MaxMoves; // Max moves per character and per turn

    public void BeginTurn()
    {
        // Reset all player characters move number
        for(int i =0; i < GameManager.Instance.allPlayers.Length; i++)
        {
            GameManager.Instance.allPlayers[i].ResetMove();
        }

        Debug.LogWarning("Turn " + CurrentTurn + " has begun");
    }

    public void EndTurn()
    {
        // Pass to the next turn
        if(CurrentTurn <= MaxTurn)
        {
            CurrentTurn++;
            for(int i =0; i < GameManager.Instance.allPlayers.Length;i++)
            {
                GameManager.Instance.allPlayers[i].ResetMove();
            }
        }
        else // End the level
        {
            // End the level
        }
        
    }
}
