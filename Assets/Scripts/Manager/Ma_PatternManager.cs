using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class Ma_PatternManager : MonoBehaviour
{
    public List<Sc_Pattern> currentPatternsList = new List<Sc_Pattern>();
    [InlineEditor]
    public Sc_Pattern obj;
    public List<Sc_Pattern> availablePatternList = new List<Sc_Pattern>();

    private void Awake()
    {
        GenerateStartPattern();
    }

    /*private Tuple<int, int> getSizeOfPattern(Sc_Pattern pattern)
    {
        int minW = int.MaxValue;
        int maxW = int.MinValue;
        int minH = int.MaxValue;
        int maxH = int.MinValue;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (pattern.Matrix[i, j])
                {
                    minW = Math.Min(minW, i + 1);
                    maxW = Math.Max(maxW, i + 1);

                    minH = Math.Min(minH, j + 1);
                    maxH = Math.Max(maxH, j + 1);
                }
            }
        }

        return new Tuple<int, int>(maxW - minW + 1, maxH - minH + 1);
    }*/

    public void CheckGridForPattern()
    {
        // take scene grid and check each pattern if currentPatternsList if it matches
        foreach (var pattern in currentPatternsList)
        {
            if (PatternValidation(GameManager.Instance.allTiles, pattern))
            {
                Debug.Log("Pattern matched: " + pattern.Name);
                GameManager.Instance.ResolvePattern(pattern);
                break;
            }
        }
        Debug.Log("No pattern matched.");
    }

    [Button]
    void test()
    {
        CheckGridForPattern();
    }

    private Mb_Tile[] getAllTileWithPlayer(Mb_Tile[] allTiles)
    {
        List<Mb_Tile> tiles = new List<Mb_Tile>();

        for (int i = 0; i < allTiles.Length; i++)
        {
            if (allTiles[i].playerOnTile)
            {
                tiles.Add(allTiles[i]);
            }
        }

        return tiles.OrderBy(o => o.posX).ThenBy(o => o.posZ).ToArray();
    }

    bool PatternValidation(Mb_Tile[] allTiles, Sc_Pattern pattern)
    {
        Mb_Tile[] playerTiles = getAllTileWithPlayer(allTiles);
        int[] patternKeyPointsIndices = pattern.Matrix.GetTrueValuesIndices().OrderBy(i => pattern.Matrix.GetLocation(i).x).ThenBy(i => pattern.Matrix.GetLocation(i).y).ToArray();


        bool flagX1 = playerTiles[0].posX - playerTiles[1].posX == pattern.Matrix.GetLocation(patternKeyPointsIndices[0]).x - pattern.Matrix.GetLocation(patternKeyPointsIndices[1]).x;
        bool flagX2 = playerTiles[0].posX - playerTiles[2].posX == pattern.Matrix.GetLocation(patternKeyPointsIndices[0]).x - pattern.Matrix.GetLocation(patternKeyPointsIndices[2]).x;

        bool flagZ1 = playerTiles[0].posZ - playerTiles[1].posZ == pattern.Matrix.GetLocation(patternKeyPointsIndices[0]).y - pattern.Matrix.GetLocation(patternKeyPointsIndices[1]).y;
        bool flagZ2 = playerTiles[0].posZ - playerTiles[2].posZ == pattern.Matrix.GetLocation(patternKeyPointsIndices[0]).y - pattern.Matrix.GetLocation(patternKeyPointsIndices[2]).y;

        return flagX1 && flagX2 && flagZ1 && flagZ2;
    }
    
    void GenerateStartPattern()
    {
        int count = 0;
        foreach (var item in availablePatternList)
        {
            if (++count > 5)
            {
                break;
            }
            Debug.Log("Create pattern: " + item.Name);
            currentPatternsList.Add(item);
        }
    }
}
