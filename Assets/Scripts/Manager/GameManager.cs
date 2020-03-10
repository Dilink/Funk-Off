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
            CheckGameEnd();
        }
    }

    [SerializeField] float funkDamagesEnemi;
    [SerializeField] float funkAddingPlayer;

    [InlineEditor]
    public Sc_LevelConfig levelConfig;

    public int currentRoundCountFinished = 0;
    [ReadOnly]
    public bool isGameFinished = false;

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
            if (currentPlayerSelectionned != null)
                currentPlayerSelectionned.OnDeselection();
            currentPlayerSelectionned = hit.collider.GetComponent<Mb_PlayerController>();
            currentPlayerSelectionned.OnSelection();

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

    public void OnPatternResolved(int indexInList, float otherMultiplier,int danceToTrigger)
    {
        //ANIM ET AUTRE FEEDBACKS DE COMPLETION
        foreach (Mb_PlayerController player in allPlayers)
            player.anim.SetTrigger("Dance"+ danceToTrigger);

        // VARIATION DU FUUUUUUUUUUUUNK

        Debug.Log("funkaddingPlayer " + funkAddingPlayer + " | multiplier value " + comboManager.getFunkMultiplier());
        FunkVariation((funkAddingPlayer + comboManager.getFunkMultiplier()) * otherMultiplier);
         
        if (!isGameFinished)
        {
            //DECOULEMENT DES PATTERNS
            patternManager.RotatePattern(indexInList);
        }
    }

    //FUNK adding
    public void FunkVariation(float funkToAdd)
    {
        funkAmount = Mathf.Clamp(funkAmount + funkToAdd, 0, 1);
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
        if (funkAmount <= 0.001f)
        {
            _funkAmount = 0.0f;
            uiManager.DisplayEndgameScreen(false);
            isGameFinished = true;
        }
        else if (funkAmount > 0.999f)
        {
            currentRoundCountFinished += 1;

            // If there is another round
            if (currentRoundCountFinished < levelConfig.rounds.Count)
            {
                _funkAmount = 0.5f;
                turnManager.OnNextRound();
            }
            else
            {
                _funkAmount = 1.0f;
                uiManager.DisplayEndgameScreen(true);
                isGameFinished = true;
            }
        }
    }

    [Button(ButtonSizes.Medium), GUIColor(0.89f, 0.14f, 0.14f)]
    private void UpdateReferences()
    {
        allPlayers = GameObject.FindObjectsOfType<Mb_PlayerController>();
        allTiles = GameObject.FindObjectsOfType<Mb_Tile>();

        GameObject.Find("MainUICanvas").GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
}
