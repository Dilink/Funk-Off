using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_PlayerController : MonoBehaviour
{
    [SerializeField] int basicMoves = 3;
  //  private List<Mb_Tile> tileToGo = new List<Mb_Tile>();
    public Mb_Tile currentTile;
    public Mb_Tile oldTile;
    private int moveLeft;
    [HideInInspector] public Animator anim;

    private void Awake()
    {
        ResetMove();
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
        GameManager.Instance.patternManager.CheckGridForPattern();
    }

    public void CheckCostingMovement(Mb_Tile tileToMoveTo)
    {
        int distanceBetweenTiles = Mathf.Abs(currentTile.posX - tileToMoveTo.posX) + Mathf.Abs(currentTile.posZ - tileToMoveTo.posZ);

        if (moveLeft >= tileToMoveTo.tileProperties.cost &&
            tileToMoveTo.avaible == true &&
            distanceBetweenTiles <=1 &&
            GameManager.Instance.canAct==true)
        {
            print(tileToMoveTo);

            moveLeft -= tileToMoveTo.tileProperties.cost;
            GameManager.Instance.uiManager.UpdateCharacterUi(this,moveLeft,basicMoves);
            Move(tileToMoveTo);
        }
    }

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
        GameManager.Instance.patternManager.CheckGridForPattern();
    }
    /*  public void PreviewMove()
      {
          GameManager.Instance.SetPreviewLine(tileToGo,this);

      }*/

    public void ResetMove()
    {
        
        moveLeft = basicMoves;
        oldTile = currentTile;
        GameManager.Instance.uiManager.UpdateCharacterUi(this, moveLeft, basicMoves);
    }
}
