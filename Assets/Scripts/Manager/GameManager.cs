using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("PLAYER PARAMETERS")]
    private int movePerTurn;
    [SerializeField] int maxMovesPerTurn;
    private int moveLeft;
    int totalMoveReseted = 0;

    public Mb_PlayerController currentPlayerSelectionned;
    public Mb_PlayerController[] allPlayers;

    [Header("GRID PARAMETERS")]
    public Mb_Tile[] allTiles;

    [Header("MANAGERS")]
    public Ma_UiManager uiManager;
    public Ma_PatternManager patternManager;
    public Ma_ComboManager comboManager;
    
    [Header("FunkRule")]
    private float funkMultiplier=1;
    private float funkAmount = 0.5f;

    [SerializeField] float funkDamagesEnemi;
    [SerializeField] float funkAddingPlayer;

    private void Start()
    {
      
        uiManager.UpdateFunkBar(funkAmount);
        SetupMovementLimit();
        EnableActing();
        ResetMove();
    }
    //ACTING
    #region

    public bool canAct=true;

    public void EnableActing()
    {
        canAct = true;
    }

    public void DisableActing()
    {
        canAct = false;
    }
    #endregion

    private void Update()
    {
        //INPUTSOURIS
        if (Input.GetMouseButtonDown(0))
        {
            CastRayPlayer();
        }
        else if (currentPlayerSelectionned != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                CastRayTile();
            }
        }
    }

    //SELECTION
    #region
    void CastRayPlayer()
    {
        RaycastHit hit;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit,Mathf.Infinity,1 << 9))
        {
            currentPlayerSelectionned = hit.collider.GetComponent<Mb_PlayerController>();
        }
        else
            currentPlayerSelectionned = null;

    }

    void CastRayTile()
    {
        RaycastHit hit;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {
            currentPlayerSelectionned.CheckCostingMovement(hit.collider.GetComponent<Mb_Tile>());
            
        }
    }
    #endregion

    //MOVEPART
    //definir la limte de début
    private void SetupMovementLimit()
    {
       
        foreach (Mb_PlayerController player in allPlayers)
        {
            totalMoveReseted += player.characterBaseCharacteristics.movementGiven;
        }
        totalMoveReseted = Mathf.Clamp(totalMoveReseted, totalMoveReseted, maxMovesPerTurn);              
    }



    public int moveLeftForTurn()
    {
        return moveLeft;
    }

    public void DecreaseMovesLeft(int toDecrease)
    {
        moveLeft -= toDecrease;
        uiManager.UpdateMovesUi(moveLeft, movePerTurn);
    }

    public void ResetMove()
    {
        int reservedMoves = moveLeft;
        moveLeft = Mathf.Clamp(totalMoveReseted + reservedMoves, 0, maxMovesPerTurn);              
        uiManager.UpdateMovesUi(moveLeft, maxMovesPerTurn);
    }
    //PREVIEW
    /*
    public void SetPreviewLine(List<Mb_Tile> allTilesToMove, Mb_PlayerController currentPlayer)
    {
        linePreview.gameObject.SetActive(true);
        linePreview.positionCount = allTilesToMove.Count + 1;
        linePreview.SetPosition(0,currentPlayer.currentTile.transform.position);

        for (int i = 1; i < allTilesToMove.Count; i++)
        {
            linePreview.SetPosition(i,allTilesToMove[i].transform.position);
        }
    }

    public void EndPreviewLine()
    {
        linePreview.gameObject.SetActive(false);
        linePreview.positionCount = 0;

    }*/

    //CHOPPER LA TILE QUE L ON VEUT
    public Mb_Tile GetTile(int x, int z)
    {
        for (int i =0; i < allTiles.Length; i++)
        {
            if (allTiles[i].posX == x && allTiles[i].posZ == z)
            {
                return allTiles[i];
                
            }
        }
        return null;
    }

    public void OnPatternResolved(int indexInList, float otherMultiplier)
    {
        //ANIM ET AUTRE FEEDBACKS DE COMPLETION
        foreach (Mb_PlayerController player in allPlayers)
            player.anim.SetTrigger("Dance");

        //DECOULEMENT DES PATTERNS
;        patternManager.RotatePattern(indexInList);

        //INCREMENTATION DU MULTIPLIER ICI
        comboManager.RotateMultipliers(indexInList);

        // VARIATION DU FUUUUUUUUUUUUNK
        FunkVariation(funkAddingPlayer * funkMultiplier * otherMultiplier);
    }

    //FUNK adding
    public void FunkVariation(float funkToAdd)
    {
        funkAmount += funkToAdd * funkMultiplier;
        funkAmount = Mathf.Clamp(funkAmount, 0, 1);
        uiManager.UpdateFunkBar(funkAmount);
    }

    //FUNK MULTIPLIER SET
    public void SetFunkMultiplier(float newModifier)
    {
        funkMultiplier = newModifier;
    }

    //DAMAGES PART
    public void SetFunkDamages(float newDamages)
    {
        funkDamagesEnemi = newDamages;
    }

    public float funkDamagesToDeal()
    {
        return -funkDamagesEnemi;
    }

    //TILE SPE
    public Mb_Tile TpTile(Mb_Tile currentTpUsed)
    {
        for (int i = 0; i < allTiles.Length; i++)
        {
            if ((allTiles[i].tileProperties.type & TileModifier.Tp) == TileModifier.Tp && currentTpUsed!= allTiles[i])
            {
                return allTiles[i];
            }
        }
        return null;
    }
}
