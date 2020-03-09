using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_PlayerController : MonoBehaviour
{
  //  private List<Mb_Tile> tileToGo = new List<Mb_Tile>();
    public Mb_Tile currentTile;
    public Mb_Tile oldTile;
    public Sc_CharacterParameters characterBaseCharacteristics;

    private float customMultiplier =1;

    // OLD MOVEMENT SYSTEM
    //[SerializeField] int basicMoves = 3;
    // private int moveLeft;
    int velX=0, velZ=0;


//ANIM ET FEEDBACKS
[HideInInspector] public Animator anim;


    private void Awake()
    {
        // ResetMove();
        oldTile = currentTile;
         anim = GetComponent<Animator>();
    }

    private void Move(Mb_Tile tileToMoveTo)
    {
        GameManager.Instance.DisableActing();
        //reset de la vieille tuile
        currentTile.avaible = true;
        currentTile.ResetOccupent();

        oldTile = currentTile;
      
        //set de la nouvelle tuile
        currentTile = tileToMoveTo;
        currentTile.setOccupent(this);
        currentTile.avaible = false;
        UpdateVelocity();
        //bouger le joueur                                               //declenchement parametre de la tuile
        transform.DOMove(tileToMoveTo.transform.position + new Vector3(0,.5f,0), .33f,false).OnComplete(OnMoveCallBack);
    } 

    void OnMoveCallBack()
    {
        currentTile.OnMove(false);
        GameManager.Instance.EnableActing();
        CheckPatternCallBack();

    }

    //MOUVEMENT PAYANT
    public void CheckCostingMovement(Mb_Tile tileToMoveTo)
    {
        int directionX = Mathf.Clamp(tileToMoveTo.posX - currentTile.posX,-1,1);
        int directionZ = Mathf.Clamp(tileToMoveTo.posZ - currentTile.posZ,-1,1);

        int distanceBetweenTilesX = Mathf.Abs(directionX);
        int distanceBetweenTilesZ = Mathf.Abs(directionZ);
        int distanceBetweenTilesXZ = Mathf.Abs(currentTile.posX - tileToMoveTo.posX) + Mathf.Abs(currentTile.posZ - tileToMoveTo.posZ);

        if(tileToMoveTo.avaible == false && (characterBaseCharacteristics.characterSkills & CharacterSkills.JumpOff) == CharacterSkills.JumpOff)
        {
       

            if (GameManager.Instance.GetTile(Mathf.Clamp(currentTile.posX + directionX * 2, -1,1), Mathf.Clamp(currentTile.posZ + directionZ * 2,-1,1)).avaible == true &&
                GameManager.Instance.moveLeftForTurn() >= tileToMoveTo.tileProperties.cost &&
                 IsNotWalled(GameManager.Instance.GetTile(Mathf.Clamp(currentTile.posX + directionX * 2, -1, 1), Mathf.Clamp(currentTile.posZ + directionZ * 2, -1, 1)), directionX, directionZ))
            {
                    GameManager.Instance.DecreaseMovesLeft(tileToMoveTo.tileProperties.cost);
                    Move(GameManager.Instance.GetTile(currentTile.posX + directionX * 2, currentTile.posZ + directionZ * 2));  
            }
        }
        else
        {
            if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Swift) == CharacterSkills.Swift && 
                distanceBetweenTilesX <= 1 &&
                distanceBetweenTilesZ <= 1)
            {
                if (GameManager.Instance.moveLeftForTurn() >= tileToMoveTo.tileProperties.cost &&
                distanceBetweenTilesX <= 1 && 
                distanceBetweenTilesZ <= 1 &&
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
        }
       
    }

    bool IsNotWalled(Mb_Tile tileToCheck, int directionX, int directionZ)
    {
        bool temporaryBool = true;

        print("DirectionX" + directionX);
        if (Mathf.Abs(directionX) + Mathf.Abs(directionZ) == 2)
        {
            int directionToCheckX = tileToCheck.posX - currentTile.posX;
            int directionToCheckZ = tileToCheck.posZ - currentTile.posZ;

            TileModifier entireModifierX = currentTile.tileProperties.type | GameManager.Instance.GetTile(currentTile.posX + directionToCheckX, currentTile.posZ).tileProperties.type;
            TileModifier entireModifierZ = currentTile.tileProperties.type | GameManager.Instance.GetTile(currentTile.posX, currentTile.posZ + directionToCheckZ).tileProperties.type;

            //GAUCHE EN X
            if (directionToCheckX <= -1)
            {
                if ((GameManager.Instance.GetTile(currentTile.posX + directionToCheckX, currentTile.posZ).tileProperties.type & (TileModifier.WalledUp | TileModifier.WalledRight)) != 0)
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
                if ((GameManager.Instance.GetTile(currentTile.posX + directionToCheckX, currentTile.posZ).tileProperties.type & (TileModifier.WalledUp | TileModifier.WalledLeft)) != 0)
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
                if ((currentTile.tileProperties.type & TileModifier.WalledUp) != 0)
                {
                    temporaryBool = false;
                }

            }
            else if (directionZ == 1)
            {
                if ((currentTile.tileProperties.type & TileModifier.WalledDown) != 0)
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
        int z = Mathf.Clamp(currentTile.posZ + velZ, -1,1);
        int x = Mathf.Clamp(currentTile.posX + velX, - 1, 1); 
        if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Swift) != 0)
        {
            (Mathf.Abs(z) - Mathf.Abs(x) != 0)
                  CheckFreeMovement(GameManager.Instance.GetTile(x, z));
        }
          

        else if (Mathf.Abs(z)- Mathf.Abs(x) !=0)
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
        if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Finisher) == CharacterSkills.Finisher)
            GameManager.Instance.patternManager.CheckGridForPatternAndReact(1.5f);
        else
            GameManager.Instance.patternManager.CheckGridForPatternAndReact(1);
    }

    void UpdateVelocity()
    {
        velX = currentTile.posX - oldTile.posX;
        velZ = currentTile.posZ - oldTile.posZ;
    }

}

