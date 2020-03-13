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

        List<Sc_AIPattern> temporaryList = new List<Sc_AIPattern>(GameManager.Instance.levelConfig.rounds[GameManager.Instance.currentRoundCountFinished].aiPatterns);

        for (int i=0; i < temporaryList.Count; i++)
        {
            if (temporaryList[i] == oldPatternUsed)
            {
                temporaryList.Remove(temporaryList[i]);
            }

            foreach(Vector2 playerCoord in posOfEachPlayer)
            {
                if (((temporaryList[i].Matrix.FromLocation((int )playerCoord.x , (int)playerCoord.y)) | (int) tileModifierExeption) != (int)tileModifierExeption)
                {
                    temporaryList.Remove(temporaryList[i]);
                    break;
                }
            }
        }    
        
        if(temporaryList.Count ==0)
        {
            temporaryList = GameManager.Instance.levelConfig.rounds[GameManager.Instance.currentRoundCountFinished].aiPatterns;
        }
        int randomPattern = UnityEngine.Random.Range(0, temporaryList.Count - 1);
        ApplyPattern(temporaryList[randomPattern]);
    }

    public void ApplyPattern(Sc_AIPattern pattern)
    {
        oldPatternUsed = pattern;

        var tiles = GameManager.Instance.allTiles;

        for (int i = 0; i < tiles.Length; i++)
        {
            TileModifier newType = (TileModifier)pattern.Matrix.FromLocation(tiles[i].posX, tiles[i].posZ);
            GameManager.Instance.GetTile(tiles[i].posX, tiles[i].posZ).UpdateTileType(newType);
        }
    }
}

