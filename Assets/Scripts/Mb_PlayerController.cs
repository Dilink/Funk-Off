using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Mb_PlayerController : MonoBehaviour
{
    public int MoveLeft = 3;
    Mb_Tile tileOn;

    public void Move(Mb_Tile tileToMoveTo)
    {
        transform.DOMove(tileToMoveTo.transform.parent.position, 1,false);
        
        tileOn = tileToMoveTo;
    }

    public void CheckMovement(Mb_Tile tileToMoveTo)
    {
        if(MoveLeft>= tileToMoveTo.cost)
        {
            MoveLeft -= tileToMoveTo.cost;
            Move(tileToMoveTo);
        }
    }
}
