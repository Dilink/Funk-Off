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

    private void Start()
    {
        ResetMove();
    }

    public void Move(Mb_Tile tileToMoveTo)
    {
        currentTile.avaible = true;
        currentTile.ResetOccupent();
        oldTile = currentTile;
        transform.DOMove(tileToMoveTo.transform.parent.position, 1,false);

        currentTile = tileToMoveTo;
        currentTile.setOccupent(this);
        currentTile.avaible = false;
        currentTile.OnMove();
       // GameManager.Instance.patternManager.CheckGridForPattern();
    } 

    public void CheckMovement(Mb_Tile tileToMoveTo)
    {
        if(moveLeft>= tileToMoveTo.tileProperties.cost &&
            tileToMoveTo.avaible == true &&
            Vector3.Distance(tileToMoveTo.transform.position, currentTile.transform.position)<1.2f)
        {
            moveLeft -= tileToMoveTo.tileProperties.cost;
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
        transform.position = tileToTp.transform.position;
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
