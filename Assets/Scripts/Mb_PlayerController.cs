using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Mb_PlayerController : MonoBehaviour
{
    //  private List<Mb_Tile> tileToGo = new List<Mb_Tile>();
    [Header("GRID")]
    public Mb_Tile currentTile;
    public Mb_Tile oldTile;
    public Sc_CharacterParameters characterBaseCharacteristics;

    private float customMultiplier = 1;

    // OLD MOVEMENT SYSTEM
    //[SerializeField] int basicMoves = 3;
    // private int moveLeft;
    int velX = 0, velZ = 0;
    [SerializeField] SkinnedMeshRenderer characterRenderer;
    private Material meshMaterial;
    [Header("feedBacks")]

    public Animator anim;


    public ParticleSystem particleFeedback;
    private bool isSelected = false;
    [SerializeField] Mb_PlayerCard UiAssociated;

    //ANIM ET FEEDBACKS

    private void Awake()
    {
        // ResetMove();
        oldTile = currentTile;
        meshMaterial = characterRenderer.material;
        Material materialInstance = new Material(meshMaterial);
        meshMaterial = materialInstance;
        ResetOutline();
        if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Foresight) == CharacterSkills.Foresight)
        {
            GameManager.Instance.patternManager.canForesight = true;
        }

    }

    private void Move(Mb_Tile tileToMoveTo)
    {
        DesactivateAllTiles();
        GameManager.Instance.DisableActing();

        //reset de la vieille tuile
        oldTile = currentTile;

        switch (characterBaseCharacteristics.characterName.ToLower())
        {
            case "richard":
                GameManager.Instance.soundManager.PlaySound(GameSound.S_RichardMove);
                break;
            case "caesar":
            case "ceasar":
                GameManager.Instance.soundManager.PlaySound(GameSound.S_CaesarMove);
                break;
            case "dave":
                GameManager.Instance.soundManager.PlaySound(GameSound.S_DaveMove);
                break;
        }


        //set de la nouvelle tuile
        currentTile = tileToMoveTo;
        currentTile.setOccupent(this);
        currentTile.avaible = false;
        UpdateVelocity();
        //bouger le joueur    
        //declenchement parametre de la tuile
        GameManager.Instance.patternManager.SetLastPlayerMove(this);
        transform.DOMove(tileToMoveTo.transform.position + new Vector3(0, .5f, 0), .33f, false).OnComplete(OnMoveCallBack);
    }

    void OnMoveCallBack()
    {
        oldTile.ResetOccupent();

        TileModifier allTileModifierButWalls = (TileModifier.Destroyer | TileModifier.Ice | TileModifier.Slow | TileModifier.Tp);

        if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Absorber) == CharacterSkills.Absorber &&
            (currentTile.tileProperties.type & allTileModifierButWalls) != 0)
        {
            if (currentTile.tileProperties.cost >= 2)
            {
                GameManager.Instance.IncreaseMovesLeft(currentTile.tileProperties.cost - 1);
            }
            currentTile.RestBaseTileButWalls();
            GameManager.Instance.IncreaseMovesLeft(1);
        }
        currentTile.OnMove(false);
        GameManager.Instance.EnableActing();
        CheckPatternCallBack();

        if ((characterBaseCharacteristics.characterSkills & CharacterSkills.RandomizerFirstMove) == CharacterSkills.RandomizerFirstMove)
        {
            if (GameManager.Instance.isTheFirstMove == true)
            {
                GameManager.Instance.patternManager.RandomizeCurrentPatterns();
                GameManager.Instance.isTheFirstMove = false;

            }
            else
                GameManager.Instance.isTheFirstMove = false;

        }

        if(GameManager.Instance.currentPlayerSelectionned == this)
        {
            UpdatePreview(currentTile);
        }
    }

    //MOUVEMENT PAYANT
    public void CheckCostingMovement(Mb_Tile tileToMoveTo)
    {
        bool isIn =false;

        foreach (Mb_Tile tileAvaible in allTilesAvaibleToWalkOn())
        {
            if (tileAvaible == tileToMoveTo)
            {
                isIn = true;
                break;
            }
        }
        if(GameManager.Instance.canAct == true && isIn==true)
        {
            if (tileToMoveTo.avaible == false && (characterBaseCharacteristics.characterSkills & CharacterSkills.JumpOff) == CharacterSkills.JumpOff)
            {
                int directionX = tileToMoveTo.posX- currentTile.posX;
                int directionZ = tileToMoveTo.posZ - currentTile.posZ;
                print(currentTile.posX + (directionX * 2));
                print(currentTile.posZ + (directionZ * 2));

                GameManager.Instance.DecreaseMovesLeft(tileToMoveTo.tileProperties.cost);
                Move(GameManager.Instance.GetTile(currentTile.posX + (directionX * 2), currentTile.posZ + (directionZ * 2)));
            }
            else
            {
                GameManager.Instance.DecreaseMovesLeft(tileToMoveTo.tileProperties.cost);
                Move(tileToMoveTo);
            }
    
        }

        #region
        /*
        if (tileToMoveTo.avaible == false && (characterBaseCharacteristics.characterSkills & CharacterSkills.JumpOff) == CharacterSkills.JumpOff &&
            distanceBetweenTilesXZ == 1)
        {
            if (GameManager.Instance.GetTile(currentTile.posX + directionX * 2, currentTile.posZ + directionZ * 2).avaible == true &&
                GameManager.Instance.moveLeftForTurn() >= tileToMoveTo.tileProperties.cost)
            {
                GameManager.Instance.DecreaseMovesLeft(tileToMoveTo.tileProperties.cost);
                Move(GameManager.Instance.GetTile(currentTile.posX + directionX * 2, currentTile.posZ + directionZ * 2));
            }
        }

        else
        {
            if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Swift) == CharacterSkills.Swift &&
                distanceBetweenTilesX == 1 &&
                distanceBetweenTilesZ == 1)
            {
                if (GameManager.Instance.moveLeftForTurn() >= tileToMoveTo.tileProperties.cost &&
                tileToMoveTo.avaible == true &&
                GameManager.Instance.canAct == true &&
                IsNotWalled(tileToMoveTo, directionX, directionZ))
                {
                    GameManager.Instance.DecreaseMovesLeft(tileToMoveTo.tileProperties.cost);
                    Move(tileToMoveTo);
                }
            }
            else if (GameManager.Instance.moveLeftForTurn() >= tileToMoveTo.tileProperties.cost &&
                tileToMoveTo.avaible == true &&
                distanceBetweenTilesXZ <= 1 &&
                GameManager.Instance.canAct == true &&
                IsNotWalled(tileToMoveTo, directionX, directionZ))
            {
                GameManager.Instance.DecreaseMovesLeft(tileToMoveTo.tileProperties.cost);
                //GameManager.Instance.uiManager.UpdateCharacterUi(this,moveLeft,basicMoves);
                Move(tileToMoveTo);
            }
        }*/
        #endregion
    }

    bool IsNotWalled(Mb_Tile tileToCheck, int directionX, int directionZ)
    {
        bool temporaryBool = true;

        if (Mathf.Abs(directionX) + Mathf.Abs(directionZ) == 2)
        {
            int directionToCheckX = tileToCheck.posX - currentTile.posX;
            int directionToCheckZ = tileToCheck.posZ - currentTile.posZ;

            TileModifier entireModifierX = currentTile.tileProperties.type | GameManager.Instance.GetTile(currentTile.posX + directionToCheckX, currentTile.posZ).tileProperties.type;
            TileModifier entireModifierZ = currentTile.tileProperties.type | GameManager.Instance.GetTile(currentTile.posX, currentTile.posZ + directionToCheckZ).tileProperties.type;

            //GAUCHE EN X
            if (directionToCheckX <= -1)
            {
                if ((GameManager.Instance.GetTile(currentTile.posX + directionToCheckX, currentTile.posZ).tileProperties.type & (TileModifier.WalledLeft)) != 0)
                {

                    if (directionToCheckZ <= -1)
                    {
                        if ((GameManager.Instance.GetTile(currentTile.posX, currentTile.posZ + directionToCheckZ).tileProperties.type & (TileModifier.WalledUp | TileModifier.WalledRight)) != 0)
                        {
                            temporaryBool = false;
                            if (temporaryBool == false)
                                return false;
                        }
                    }
                    else if (directionToCheckZ >= 1)
                    {
                        if ((GameManager.Instance.GetTile(currentTile.posX, currentTile.posZ + directionToCheckZ).tileProperties.type & (TileModifier.WalledDown | TileModifier.WalledLeft)) != 0)
                        {
                            temporaryBool = false;
                            if (temporaryBool == false)
                                return false;
                        }
                    }
                }

            }
            //DROITE EN X
            else if (directionToCheckX >= 1)
            {
                if ((GameManager.Instance.GetTile(currentTile.posX + directionToCheckX, currentTile.posZ).tileProperties.type & (TileModifier.WalledRight)) != 0)
                {
                    if (directionToCheckZ <= -1)
                    {
                        if ((GameManager.Instance.GetTile(currentTile.posX, currentTile.posZ + directionToCheckZ).tileProperties.type & (TileModifier.WalledDown | TileModifier.WalledLeft)) != 0)
                        {
                            temporaryBool = false;
                            if (temporaryBool == false)
                                return false;
                        }
                    }
                    else
                    {

                        if ((GameManager.Instance.GetTile(currentTile.posX, currentTile.posZ + directionToCheckZ).tileProperties.type & (TileModifier.WalledDown | TileModifier.WalledRight)) != 0)
                        {
                            temporaryBool = false;
                            if (temporaryBool == false)
                                return false;
                        }
                    }

                }
            }
        }

        else if (Mathf.Abs(directionX) + Mathf.Abs(directionZ) == 1)
        {
            if (directionX == -1)
            {
                if ((currentTile.tileProperties.type & TileModifier.WalledLeft) != 0)
                {
                    temporaryBool = false;
                }
            }
            else if (directionX == 1)
            {
                if ((currentTile.tileProperties.type & TileModifier.WalledRight) != 0)
                {
                    temporaryBool = false;
                }
            }
            else if (directionZ == -1)
            {
                if ((currentTile.tileProperties.type & TileModifier.WalledDown) != 0)
                {
                    temporaryBool = false;
                }

            }
            else if (directionZ == 1)
            {
                if ((currentTile.tileProperties.type & TileModifier.WalledUp) != 0)
                {
                    temporaryBool = false;
                }
            }


        }

        return temporaryBool;
    }



    //MOUVEMENT GRATUIT
    public void CheckFreeMovement(Mb_Tile tileToMoveTo)
    {
        if (tileToMoveTo == null)
            return;

        int directionX = tileToMoveTo.posX - currentTile.posX;
 
        int directionZ = tileToMoveTo.posZ - currentTile.posZ;


        if (tileToMoveTo.avaible == true &&
           IsNotWalled(tileToMoveTo, directionX, directionZ))
        {
            Move(tileToMoveTo);
        }
    }

    public void CheckTp(Mb_Tile tileToTp)
    {
        if (tileToTp.avaible == true)
        {
            Tp(tileToTp);
        }
    }

    public void Drift()
    {
        int z = Mathf.Clamp(currentTile.posZ + velZ, -2, 2);
        int x = Mathf.Clamp(currentTile.posX + velX, -2, 2);

        if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Swift) != 0)
        {
            if (currentTile.posZ + velZ <= 2 && currentTile.posZ + velZ >= -2 &&
                currentTile.posX + velX <= 2 && currentTile.posX + velX >= -2)
                CheckFreeMovement(GameManager.Instance.GetTile(x, z));
        }


        else
            CheckFreeMovement(GameManager.Instance.GetTile(x, z));
    }

    void Tp(Mb_Tile tileToTp)
    {
        GameManager.Instance.DisableActing();
        currentTile.avaible = true;
        currentTile.ResetOccupent();
        oldTile = currentTile;

        currentTile = tileToTp;
        currentTile.setOccupent(this);
        currentTile.avaible = false;

        transform.position = tileToTp.transform.position + new Vector3(0, .5f, 0);
        OnTpCallBack();
    }

    void OnTpCallBack()
    {
        currentTile.OnMove(true);
        GameManager.Instance.EnableActing();
        CheckPatternCallBack();

    }

    void CheckPatternCallBack()
    {
        GameManager.Instance.patternManager.CheckGridForPatternAndReact();
    }

    void UpdateVelocity()
    {
        velX = currentTile.posX - oldTile.posX;
        velZ = currentTile.posZ - oldTile.posZ;
    }

    //FEEDBACK
    public void OnSelection()
    {
        Debug.Log(name);
        if (isSelected == false)
        {
            SetOutline();
           // GameManager.Instance.uiManager.DeployUi(UiAssociated);
        }

        UpdatePreview(currentTile);

        anim.SetTrigger("OnPick");
        isSelected = true;
    }

    public void UpdatePreview(Mb_Tile tileToPreviewFrom)
    {
        foreach (Mb_Tile tile in GameManager.Instance.allTiles)
        {
            tile.DesctivateCanWalkFeedBack();
        }

        foreach (Mb_Tile tile in allTilesAvaibleToWalkOn())
        {
            tile.canWalkOn = true;
            tile.ActivateAvaibleFeedback();
        }
    }

    void SetOutline()
    {

        meshMaterial.SetFloat("_OUTLINE", .0002f);
    }

    void ResetOutline()
    {
        meshMaterial.SetFloat("_OUTLINE", 0);
    }

    public void OnDeselection()
    {
        if (isSelected == true)
        {
            GameManager.Instance.uiManager.CleanUi(UiAssociated);
            ResetOutline();
        }
        isSelected = false;

        DesactivateAllTiles();
    }

    public Mb_Tile[] allTilesAvaibleToWalkOn()
    {
        List<Mb_Tile> temporaryList = new List<Mb_Tile>();
        foreach(Mb_Tile tile in GameManager.Instance.allTiles)
        {
            if(tile.tileProperties.cost <= GameManager.Instance.moveLeftForTurn())
            {
                if (tile.avaible == true)
                {
                    if (DistanceInX(tile) + DistanceInZ(tile) <= 1 &&
                        IsNotWalled(tile, DirectionX(tile), DirectionZ(tile)))
                    {
                        temporaryList.Add(tile);
                    }

                    if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Swift) == CharacterSkills.Swift)
                    {
                        if (DistanceInX(tile) <= 1 &&
                            DistanceInZ(tile) <= 1 &&
                            IsNotWalled(tile, DirectionX(tile), DirectionZ(tile)))
                        {
                            temporaryList.Add(tile);
                        }
                    }
                }
                else if ((characterBaseCharacteristics.characterSkills & CharacterSkills.JumpOff) == CharacterSkills.JumpOff)
                {
                    if (GameManager.Instance.GetTile(tile.posX + DirectionX(tile), tile.posZ + DirectionZ(tile))!= null)
                    { 
                        if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Swift) == CharacterSkills.Swift)
                        {
                            if (GameManager.Instance.GetTile(tile.posX + DirectionX(tile), tile.posZ + DirectionZ(tile)).avaible == true)
                            {
                                temporaryList.Add(tile);
                            }
                        }
                        else
                        {
                            if (GameManager.Instance.GetTile(tile.posX + DirectionX(tile), tile.posZ + DirectionZ(tile)).avaible == true &&
                                Mathf.Abs(DirectionX(tile))+ Mathf.Abs(DirectionZ(tile)) <= 1)
                            {
                                temporaryList.Add(tile);
                            }
                        }
                    }

                }
            }
         
        }
        return temporaryList.ToArray();
    }

    void DesactivateAllTiles()
    {
        foreach (Mb_Tile tile in GameManager.Instance.allTiles)
        {
            tile.canWalkOn = false;
            tile.DesctivateCanWalkFeedBack();
        }
    }

    int DistanceInX(Mb_Tile tileToCompareTo)
    {
        return Mathf.Abs(DirectionX(tileToCompareTo));
    }

    int DirectionX(Mb_Tile tileToCompareTo)
    {
        return tileToCompareTo.posX - currentTile.posX;
    }

    int DistanceInZ(Mb_Tile tileToCompareTo)
    {
        return Mathf.Abs(DirectionZ(tileToCompareTo));
    }

    int DirectionZ(Mb_Tile tileToCompareTo)
    {
        return tileToCompareTo.posZ - currentTile.posZ;
    }
}

