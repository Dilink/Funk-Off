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



    //ANIM ET FEEDBACKS
    [HideInInspector] public Animator anim;

    private void Awake()
    {
       // ResetMove();

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

        //bouger le joueur                                               //declenchement parametre de la tuile
        transform.DOMove(tileToMoveTo.transform.position + new Vector3(0,.5f,0), .33f,false).OnComplete(OnMoveCallBack);
    } 

    void OnMoveCallBack()
    {
        currentTile.OnMove(false);
        GameManager.Instance.EnableActing();
        if((characterBaseCharacteristics.characterSkills & CharacterSkills.Finisher) == CharacterSkills.Finisher)
            GameManager.Instance.patternManager.CheckGridForPatternAndReact(1.5f);
        else
            GameManager.Instance.patternManager.CheckGridForPatternAndReact(1);

    }

    //MOUVEMENT PAYANT
    public void CheckCostingMovement(Mb_Tile tileToMoveTo)
    {
        int distanceBetweenTilesX = Mathf.Abs(currentTile.posX - tileToMoveTo.posX);
        int distanceBetweenTilesZ = Mathf.Abs(currentTile.posZ - tileToMoveTo.posZ);
        int distanceBetweenTilesXZ = Mathf.Abs(currentTile.posX - tileToMoveTo.posX) + Mathf.Abs(currentTile.posZ - tileToMoveTo.posZ);

        if(tileToMoveTo.avaible == false)
        {
            int directionX = tileToMoveTo.posX - currentTile.posX ;
            int directionZ= tileToMoveTo.posZ - currentTile.posZ ;
          
            if ((characterBaseCharacteristics.characterSkills & CharacterSkills.JumpOff) == CharacterSkills.JumpOff &&
                GameManager.Instance.GetTile(currentTile.posX + directionX * 2, currentTile.posZ + directionZ * 2).avaible == true &&
                GameManager.Instance.moveLeftForTurn() >= tileToMoveTo.tileProperties.cost)
            {
                    GameManager.Instance.DecreaseMovesLeft(tileToMoveTo.tileProperties.cost);

                    Move(GameManager.Instance.GetTile(currentTile.posX + directionX * 2, currentTile.posZ + directionZ * 2));
                
                }
        }
        else
        {
            if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Swift) == CharacterSkills.Swift)
            {
                if (GameManager.Instance.moveLeftForTurn() >= tileToMoveTo.tileProperties.cost &&
                distanceBetweenTilesX <= 1 &&
                distanceBetweenTilesZ <= 1 &&
                GameManager.Instance.canAct == true)
                {
                    GameManager.Instance.DecreaseMovesLeft(tileToMoveTo.tileProperties.cost);
                    //GameManager.Instance.uiManager.UpdateCharacterUi(this,moveLeft,basicMoves);
                    Move(tileToMoveTo);
                }
            }
            else if (GameManager.Instance.moveLeftForTurn() >= tileToMoveTo.tileProperties.cost &&
                tileToMoveTo.avaible == true &&
                distanceBetweenTilesXZ <= 1 &&
                GameManager.Instance.canAct == true)
            {
                print(tileToMoveTo);

                GameManager.Instance.DecreaseMovesLeft(tileToMoveTo.tileProperties.cost);
                //GameManager.Instance.uiManager.UpdateCharacterUi(this,moveLeft,basicMoves);
                Move(tileToMoveTo);
            }
        }
       
    }
  
    //MOUVEMENT GRATUIT
    public void CheckFreeMovement(Mb_Tile tileToMoveTo)
    {
        if (tileToMoveTo.avaible == true)
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

    void Tp(Mb_Tile tileToTp)
    {
        GameManager.Instance.DisableActing();
        currentTile.avaible = true;
        currentTile.ResetOccupent();
        oldTile = currentTile;

        currentTile = tileToTp;
        currentTile.setOccupent(this);
        currentTile.avaible = false;

        transform.position = tileToTp.transform.parent.position;
        OnTpCallBack();
    }

    void OnTpCallBack()
    {
        currentTile.OnMove(true);
        GameManager.Instance.EnableActing();

        if ((characterBaseCharacteristics.characterSkills & CharacterSkills.Finisher) == CharacterSkills.Finisher)
            GameManager.Instance.patternManager.CheckGridForPatternAndReact(1.5f);
        else
            GameManager.Instance.patternManager.CheckGridForPatternAndReact(1);
    }

        
}

