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
        var tiles = GameManager.Instance.allTiles;

        for (int i = 0; i < tiles.Length; i++)
        {
            TileModifier newType = (TileModifier)pattern.Matrix.FromLocation(tiles[i].posX, tiles[i].posZ);
            GameManager.Instance.GetTile(tiles[i].posX, tiles[i].posZ).UpdateTileType(newType);
        }
    }
}

