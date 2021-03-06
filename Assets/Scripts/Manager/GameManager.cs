﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [Header("PLAYER PARAMETERS")]
    private int movePerTurn;
    [HideInInspector] public int maxMovesPerTurn=9;
    private int moveLeft;
    int maxMovesReseted = 9;
    [HideInInspector] public bool isTheFirstMove = true;

    public Mb_PlayerController currentPlayerSelectionned;
    public Mb_PlayerController[] allPlayers;

    [Header("GRID PARAMETERS")]
    public Mb_Tile[] allTiles;
    public Sc_GridFeedBackRule gridFeedbackRules;
    [SerializeField] MeshRenderer feedbackAutourGrid;
    

    [Header("MANAGERS")]
    public Ma_UiManager uiManager;
    public Ma_PatternManager patternManager;
    public Ma_ComboManager comboManager;
    public Ma_TurnManager turnManager;
    public Ma_AIManager aiManager;
    public Ma_SoundManager soundManager;
    public Ma_MusicManager musicManager;
    public Ma_SceneLoader sceneManager;

    [Header("BrunoPart")]
    [SerializeField] Animator animBruno;
    
    [Header("FunkRule")]
    private float _funkAmount = 0f;
    private Mb_Tile lastTileMousedOver;

    private float funkAmount
    {
        get => _funkAmount;
        set
        {
            if (value < _funkAmount)
            {
                soundManager.PlaySound(GameSound.S_GrooveBarDown);
            }
            else if (value > _funkAmount)
            {
                soundManager.PlaySound(GameSound.S_GrooveBarUp);
            }
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

    public float timeBetweenTurns = 1.0f;

    private void Start()
    {
        DefineMaxMoves();
        EnableActing(); 
        ResetMove();
        UpdateFeedBackAutourGrid(0);
        uiManager.UpdateMaxMoveUi(maxMovesPerTurn);
        StartCoroutine(OnGameStartPost());
    }

    //ACTING
    #region

    public bool canAct = false;
    public bool canActForced = true;

    public void EnableActing()
    {
        if (!canActForced)
        {
            canAct = true;
        }
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

        if (currentPlayerSelectionned != null)
            CheckingPatternPreview();
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
            if (currentPlayerSelectionned != null && currentPlayerSelectionned != hit.collider.GetComponent<Mb_PlayerController>())
                currentPlayerSelectionned.OnDeselection();

            currentPlayerSelectionned = hit.collider.GetComponent<Mb_PlayerController>();
            currentPlayerSelectionned.OnSelection();

            soundManager.PlaySound(GameSound.S_CharacterSelection);
        }

    }

    void CastRayTile()
    {
        RaycastHit hit;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {
            currentPlayerSelectionned.CheckCostingMovement( hit.collider.GetComponent<Mb_Tile>());
            
        }
    }
    #endregion

    //MOVEPART
    //definir la limte de début


    public int moveLeftForTurn()
    {
        return moveLeft;
    }

    public void DecreaseMovesLeft(int toDecrease)
    {
        moveLeft -= toDecrease;

        uiManager.UpdateMovesUi(moveLeft);

        if (moveLeft == 0)
        {
            uiManager.DeployEndTurnButton();
        }
    }

    public void IncreaseMovesLeft(int toDecrease)
    {
        moveLeft += toDecrease;
        uiManager.UpdateMovesUi(moveLeft);
    }

    void DefineMaxMoves()
    {
        int tempInt=0;
        foreach(Mb_PlayerController player in allPlayers)
        {
            tempInt += player.characterBaseCharacteristics.movementGiven;
        }
        maxMovesPerTurn = tempInt;
    }

    public void ResetMove()
    {
        isTheFirstMove = true;

        moveLeft = maxMovesPerTurn;

        uiManager.UpdateMovesUi(moveLeft);
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
            if (Mathf.Clamp(allTiles[i].posX,-2,2) == x && Mathf.Clamp(allTiles[i].posZ,-2,2) == z)
            {
                return allTiles[i];
                
            }
        }
        return null;
    }

    public void OnPatternResolved(int indexInList, int danceToTrigger, CharacterSkills allCharacterSkills)
    {
        //ANIM ET AUTRE FEEDBACKS DE COMPLETION
        foreach (Mb_PlayerController player in allPlayers)
        {
            player.anim.SetTrigger("Dance" + danceToTrigger);
            player.particleFeedback.Play();
            player.currentTile.OnPatternCompleteFeedback();
        }

        soundManager.PlaySound(GameSound.S_DanceMoveValidation1);

        // VARIATION DU FUUUUUUUUUUUUNK

        FunkVariation(funkAddingPlayer + comboManager.getFunkMultiplier());
        
        if ((allCharacterSkills & CharacterSkills.FinisherMove) == CharacterSkills.FinisherMove)
        {
            IncreaseMovesLeft(1);
        }
        if (!isGameFinished)
        {
            //DECOULEMENT DES PATTERNS
            patternManager.RotatePattern(indexInList);
        }

        foreach(Mb_Tile tile in allTiles)
        {
            tile.DesctivateCanWalkFeedBack();
        }
    }

    //FUNK adding
    public void FunkVariation(float funkToAdd)
    {
        if (funkToAdd > 0)
            FunkAddingAnim();
        else
            DealDamageAnim();

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
        if (funkAmount > 0.98f)
        {
            currentRoundCountFinished += 1;

            // If there is another round
            if (currentRoundCountFinished < levelConfig.rounds.Count)
            {
                _funkAmount = 0f;
                uiManager.UpdateFunkBar(0);
                OnNextRoundPre();
            }
            else
            {
                _funkAmount = 1.0f;
                uiManager.UpdateFunkBar(1);

                uiManager.DisplayEndgameScreen(true);
                isGameFinished = true;
            }
        }
        else if (turnManager.IsLastRoundFinished())
        {
            // Set value to 0 to re-check if game end
            _funkAmount = 0.0f;
            //uiManager.UpdateFunkBar(0);
            uiManager.DisplayEndgameScreen(false);
            isGameFinished = true;

        }
    }

    [Button(ButtonSizes.Medium), GUIColor(0.89f, 0.14f, 0.14f)]
    private void UpdateReferences()
    {
        allPlayers = GameObject.FindObjectsOfType<Mb_PlayerController>();
        allTiles = GameObject.FindObjectsOfType<Mb_Tile>();

        GameObject cameraGo = GameObject.Find("Main Camera");
        if (!cameraGo)
        {
            cameraGo = Camera.main.gameObject;
        }
        if (cameraGo)
        {
            GameObject.Find("MainUICanvas").GetComponent<Canvas>().worldCamera = cameraGo.GetComponent<Camera>();
        }
    }

    //FEEDBACK BRUNO
    void FunkAddingAnim()
    {
        animBruno.SetTrigger("Damaged");
    }

    void DealDamageAnim()
    {
        animBruno.SetTrigger("Attack");
    }

    void ActingAnim()
    {
        animBruno.SetTrigger("Acting");
    }

    //PATTERN COMPLETION PREVIEW
    void CheckingPatternPreview()
    {
        RaycastHit hit;
        Ray ray;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
   

        // SI LA SOURIS EST AU DESSUS D UNE TILE
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
        {
            List<Mb_Tile> tilesToCheck = new List<Mb_Tile>();

            foreach(Mb_PlayerController players in allPlayers)
            {
                tilesToCheck.Add(players.currentTile);
            }
            tilesToCheck.Remove(currentPlayerSelectionned.currentTile);

            Mb_Tile tileOverlaped = hit.collider.GetComponent<Mb_Tile>();

            if (tileOverlaped.canWalkOn == true)
            {
                tilesToCheck.Add(tileOverlaped);

                var patternToBeAccomplished = patternManager.JustCheckGridForPattern(tilesToCheck.ToArray(), true);

                if (tileOverlaped != lastTileMousedOver)
                {
                    for (int i = 0; i < tilesToCheck.Count(); i++)
                    {
                        tilesToCheck[i].PrecompletionFeedback(false);
                    }
                }

                //A CORRIGER

                if (patternToBeAccomplished.HasValue)
                {
                    if (lastTileMousedOver == null)
                    {
                        lastTileMousedOver = tileOverlaped;
                    }

                    for (int i = 0; i < tilesToCheck.Count(); i++)
                    {
                         tilesToCheck[i].PrecompletionFeedback(true);
                    }
                    uiManager.GetPatternElement(patternToBeAccomplished.Value.Item1).ShakePattern();
                }
                else
                {
                    for (int i = 0; i < tilesToCheck.Count(); i++)
                    {
                        tilesToCheck[i].PrecompletionFeedback(false);
                    }
                }
            }
            /*
            if (lastTileMousedOver != tileOverlaped)
            {
                if(lastTileMousedOver != null)
                    lastTileMousedOver.PrecompletionFeedback(false);

                lastTileMousedOver = tileOverlaped;
            }*/
        }
        else
        {
            foreach(Mb_Tile tiles in allTiles)
            {
                tiles.PrecompletionFeedback(false);
            }
        }


    }

    //feeddabck autourGrid
    public void UpdateFeedBackAutourGrid(int comboLevel)
    {
        comboLevel = Mathf.Clamp(comboLevel, 0, 5);

        switch(comboLevel)
        {
            case 0:
                feedbackAutourGrid.material = gridFeedbackRules.calmGrid;
                break;

            case 1:
                gridFeedbackRules.excitedGrid.DOVector(new Vector4(0, 0.05f, 0, 0),"_Speed", 1f);
                feedbackAutourGrid.material = gridFeedbackRules.excitedGrid;
                break;

            case 2:
                gridFeedbackRules.excitedGrid.DOVector(new Vector4(0, 0.15f, 0, 0), "_Speed", 1f);
                feedbackAutourGrid.material = gridFeedbackRules.excitedGrid;
                break;

            case 3:
                gridFeedbackRules.excitedGrid.DOVector(new Vector4(0, 0.35f, 0, 0), "_Speed", 1f);
                feedbackAutourGrid.material = gridFeedbackRules.excitedGrid;
                break;

            case 4:
                gridFeedbackRules.excitedGrid.DOVector(new Vector4(0, 0.55f, 0, 0), "_Speed", 1f);
                feedbackAutourGrid.material = gridFeedbackRules.excitedGrid;
                break;

            case 5:
                gridFeedbackRules.excitedGrid.DOVector(new Vector4(0, 0.85f, 0, 0), "_Speed", 1f);
                feedbackAutourGrid.material = gridFeedbackRules.excitedGrid;
                break;

        }
    }

    

    public void OnTurnEndPre()
    {
        uiManager.ClearAllMultiplierUi();
        uiManager.DeployEndTurnButton();
        OnTurnEndPost(turnManager.IsLastRoundFinished());
    }

    public void OnTurnEndPost(bool isLevelFinished)
    {
        uiManager.ClearAllMultiplierUi();
        patternManager.OnTurnEnd(isLevelFinished: isLevelFinished);
        if (!isLevelFinished)
        {
            uiManager.UpdateTurnsbarText(turnManager.GetCurrentTurn(), turnManager.GetMaxTurn());
        }
    }

    public void OnNextRoundPre()
    {
        turnManager.OnNextRound();
        UpdateFeedBackAutourGrid(0);
        uiManager.ClearAllMultiplierUi();
        OnNextRoundPost();
        ResetMove();
    }

    public void OnNextRoundPost()
    {
        patternManager.OnTurnEnd(isLevelFinished: true, ignoreDamages: true);
        uiManager.UpdateTurnsbarText(turnManager.GetCurrentTurn(), turnManager.GetMaxTurn());

        comboManager.ResetMultiplier();
    }

    public void UpdatePatternsBarIcon(int indexInList, Sc_Pattern pattern)
    {
        uiManager.GetPatternElement(indexInList).UpdatePatternsBarIcon(pattern);
    }

    public void OnMovePatterns(int indexInList)
    {
        uiManager.GetPatternElement(indexInList).MovePatterns(false,indexInList);
    }

    public void OnRemovePattern(int indexInList, bool isPatternDestroyed = false)
    {
        uiManager.GetPatternElement(indexInList).RemovePattern(indexInList,isPatternDestroyed);
    }



    public void OnRespawnPattern(Mb_PatternBarElement element)
    {
        uiManager.RespawnPattern(element);
    }

    public IEnumerator OnGameStartPost()
    {
        yield return new WaitForSeconds(0.1f);

        uiManager.UpdateTurnsbarText(turnManager.GetCurrentTurn(), turnManager.GetMaxTurn());
        uiManager.ClearAllMultiplierUi();
        
        StartCoroutine(turnManager.PreventPlayerFromActing());
    }

    public void OnNewTurnPre(int indexOfPatern, bool isPatternDestroyed = false)
    {
        uiManager.RemoveAllMultiplierIcon();
    }

    public void OnMultiplierAppear(int indexOfPatern, Color color, Color colorbkg, string text, int multiplierIndex)
    {
        uiManager.GetPatternElement(indexOfPatern).PlayFX(multiplierIndex);
        uiManager.GetPatternElement(indexOfPatern).UpdateMultiplierIcon(Color.white, Color.red, text);

        musicManager.PlayLayer(multiplierIndex + 1);
        soundManager.PlaySound(GameSound.S_MultiplierAppear);
    }

    public void OnUpdateCancelMarker(int index, bool flag = false)
    {
        uiManager.GetPatternElement(index).UpdateCancelMarkerIcon(flag);
    }
}
