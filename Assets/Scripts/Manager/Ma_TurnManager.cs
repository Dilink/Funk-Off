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

    [Header("Debug")]
    //[HideInInspector]
    public List<Mb_PlayerController> PlayersList; // A list of all the player characters

    public void BeginTurn()
    {
        // Reset all player characters move number
        for(int i =0; i < PlayersList.Count; i++)
        {
             PlayersList[i].MoveLeft = MaxMoves;
        }

        Debug.LogError("Turn " + CurrentTurn + " has begun");
    }

    public void EndTurn()
    {
        // Pass to the next turn
        if(CurrentTurn <= MaxTurn)
        {
            CurrentTurn++;
        }
        else // End the level
        {
            // End the level
        }

        Debug.LogError("Turn " + CurrentTurn + " has ended");
    }
}
