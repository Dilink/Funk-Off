using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("PLAYER PARAMETERS")]
    private int movePerTurn;
    [SerializeField] bool canStore;
    [SerializeField] int maxMovesPerTurn;
    private int moveLeft;
    int totalMoveReseted = 0;

    public Mb_PlayerController currentPlayerSelectionned;
    public Mb_PlayerController[] allPlayers;

    [Header("GRID PARAMETERS")]
    public Mb_Tile[] allTiles;
    public Sc_GridFeedBackRule gridFeedbackRules;

    [Header("MANAGERS")]
    public Ma_UiManager uiManager;
    public Ma_PatternManager patternManager;
    public Ma_ComboManager comboManager;
    public Ma_TurnManager turnManager;
    public Ma_AIManager aiManager;
    
    [Header("FunkRule")]
    private float _funkAmount = 0.5f;

    private float funkAmount
    {
        get => _funkAmount;
        set
        {
            _funkAmount = value;
            uiManager.UpdateFunkBar(funkAmount);
            //CheckGameEnd();
        }
    }

    [SerializeField] float funkDamagesEnemi;
    [SerializeField] float funkAddingPlayer;

    [InlineEditor]
    public Sc_LevelConfig levelConfig;

    public int currentRoundCountFinished = 0;

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
        if (canStore)
            moveLeft = Mathf.Clamp(totalMoveReseted + reservedMoves, 0, maxMovesPerTurn);
        else
            moveLeft = totalMoveReseted;
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
            if (Mathf.Clamp(allTiles[i].posX,-1,1) == x && Mathf.Clamp(allTiles[i].posZ,-1,1) == z)
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

        // VARIATION DU FUUUUUUUUUUUUNK

        //Debug.Log("funkaddingPlayer " + funkAddingPlayer + " | multiplier value " + comboManager.getFunkMultiplier());
        FunkVariation((funkAddingPlayer + comboManager.getFunkMultiplier()) * otherMultiplier);
         

        //DECOULEMENT DES PATTERNS
        patternManager.RotatePattern(indexInList);

    }

    //FUNK adding
    public void FunkVariation(float funkToAdd)
    {
        float funkToAddTotal = funkToAdd * comboManager.getFunkMultiplier();
        funkAmount += ( funkToAdd * comboManager.getFunkMultiplier());
        funkAmount = Mathf.Clamp(funkAmount, 0, 1);
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

    public void CheckGameEnd()
    {
        //Debug.LogError("funkAmount="+ funkAmount);
        if (funkAmount <= 0.0f)
        {
            uiManager.DisplayEndgameScreen(false);
        }
        else if (funkAmount >= 1.0f)
        {
            currentRoundCountFinished += 1;
            _funkAmount = 0.5f;
            //Debug.LogError("OnNextRound 1");
            turnManager.OnNextRound();
            //Debug.LogError("OnNextRound");

            if (currentRoundCountFinished >= levelConfig.roundCount)
            {
                //Debug.LogError("Finished");
                uiManager.DisplayEndgameScreen(true);
            }
        }
    }

    [Button(ButtonSizes.Medium), GUIColor(0.89f, 0.14f, 0.14f)]
    private void UpdateReferences()
    {
        allPlayers = GameObject.FindObjectsOfType<Mb_PlayerController>();
        allTiles = GameObject.FindObjectsOfType<Mb_Tile>();
    }
}
