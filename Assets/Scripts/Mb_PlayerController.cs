using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_PlayerController : MonoBehaviour
{
    [SerializeField] int basicMoves = 3;
    private int moveLeft;

    private void Start()
    {
        ResetMove();
    }

    public void Move(Mb_Tile tileToMoveTo)
    {
        transform.DOMove(tileToMoveTo.transform.parent.position, 1,false);
       
    } 

    public void CheckMovement(Mb_Tile tileToMoveTo)
    {
        if(moveLeft>= tileToMoveTo.cost)
        {
            moveLeft -= tileToMoveTo.cost;
            Move(tileToMoveTo);
        }
    }

    public void ResetMove()
    {
        moveLeft = basicMoves;
        GameManager.Instance.uiManager.UpdateCharacterUi(this, moveLeft, basicMoves);
    }
}
