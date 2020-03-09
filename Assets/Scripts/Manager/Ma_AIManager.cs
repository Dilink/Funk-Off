using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ma_AIManager : MonoBehaviour
{

    /*
    void PrepAttack()
    {
        throw new NotImplementedException();
    }

    void CancelAttack()
    {
        throw new NotImplementedException();
    }

    void GridModification()
    {
        throw new NotImplementedException();
    }*/



    public void ChoosePattern()
    {
        List<Sc_AIPattern> temporaryList = GameManager.Instance.levelConfig.rounds[GameManager.Instance.currentRoundCountFinished].aiPatterns;
        int randomPattern = UnityEngine.Random.Range(0, temporaryList.Count - 1);
        ApplyPattern(temporaryList[randomPattern]);
    }

    public void ApplyPattern(Sc_AIPattern pattern)
    {
        for (int i = 0; i < GameManager.Instance.allTiles.Length; i++)
        {
            TileModifier newType = (TileModifier)pattern.Matrix[GameManager.Instance.allTiles[i].posX + 1, GameManager.Instance.allTiles[i].posZ + 1];
            GameManager.Instance.GetTile(GameManager.Instance.allTiles[i].posX, GameManager.Instance.allTiles[i].posZ).UpdateTileType(newType);
        }
    }
}

