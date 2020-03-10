using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ma_AIManager : MonoBehaviour
{
    Sc_AIPattern oldPatternUsed = null;
    
    public void ChoosePattern()
    {
        TileModifier tileModifierExeption = (TileModifier.WalledDown | TileModifier.WalledLeft | TileModifier.WalledRight | TileModifier.WalledUp);
        List<Vector2> posOfEachPlayer = new List<Vector2>();

        foreach(Mb_PlayerController player in GameManager.Instance.allPlayers)
        {
            posOfEachPlayer.Add( new Vector2(player.currentTile.posX, player.currentTile.posZ));
        }

        List<Sc_AIPattern> temporaryList = GameManager.Instance.levelConfig.rounds[GameManager.Instance.currentRoundCountFinished].aiPatterns;
        foreach (Sc_AIPattern aiPotentialMove in temporaryList)
        {
            if (aiPotentialMove == oldPatternUsed)
            {
                temporaryList.Remove(aiPotentialMove);
            }
            foreach(Vector2 playerCoord in posOfEachPlayer)
            {
                if (((aiPotentialMove.Matrix.FromLocation((int )playerCoord.x , (int)playerCoord.y)) | (int) tileModifierExeption) != (int)tileModifierExeption)
                {
                    temporaryList.Remove(aiPotentialMove);
                    break;
                }
            }
        }       
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

