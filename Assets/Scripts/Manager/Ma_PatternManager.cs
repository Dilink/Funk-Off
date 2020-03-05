using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using Sirenix.OdinInspector;
using DG.Tweening;

public class Ma_PatternManager : MonoBehaviour
{
    //static int randomSeed=1;
    public List<Sc_Pattern> currentPatternsList = new List<Sc_Pattern>();
    [ReadOnly]
    public List<Sc_Pattern> availablePatternList = new List<Sc_Pattern>();
    
    [ReadOnly]
    public Sc_Pattern futurePattern;
    public readonly int patternCount = 5;

    public LevelConfig levelConfig;

    private void Awake()
    {
        LoadAvailablePatterns();
        GenerateStartPattern();
    }

    private void LoadAvailablePatterns()
    {
        availablePatternList.Clear();

        // Load all assets of type Sc_Pattern that are located in Assets/Patterns folder
        string[] guids2 = AssetDatabase.FindAssets("t:Sc_Pattern", new[] { "Assets/Patterns" });
        foreach (var i in guids2)
        {
            string path = AssetDatabase.GUIDToAssetPath(i);
            Sc_Pattern pattern = AssetDatabase.LoadAssetAtPath<Sc_Pattern>(path);

            pattern.Name = Path.GetFileName(path);
            pattern.Category = Directory.GetParent(path).Name;
            pattern.CategoryWeight = levelConfig.patternCategories[pattern.Category];

            availablePatternList.Add(pattern);
        }

        RandomizeList(ref availablePatternList);

        foreach (var item in availablePatternList)
        {
            Debug.Log("Weight: " + item.CategoryWeight);
        }
    }

    private void RandomizeList(ref List<Sc_Pattern> list)
    {
        System.Random rnd = new System.Random();
        list = list.OrderBy(x => rnd.Next()).ThenByDescending(x => x.CategoryWeight).ToList();
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

    public void CheckGridForPatternAndReact()
    {
        var res = JustCheckGridForPattern();
        if (res.HasValue)
        {
            GameManager.Instance.OnPatternResolved(res.Value.Item1, res.Value.Item2);
            return;
        }
    }

    private Optional<Tuple<int, Sc_Pattern>> JustCheckGridForPattern()
    {
        // take scene grid and check each pattern if currentPatternsList if it matches
        for (int i = 0; i < currentPatternsList.Count(); i++)
        {
            Sc_Pattern pattern = currentPatternsList[i];
            if (PatternValidation(GameManager.Instance.allTiles, pattern))
            {
                return new Optional<Tuple<int, Sc_Pattern>>(new Tuple<int, Sc_Pattern>(i, pattern));
            }
        }
        return new Optional<Tuple<int, Sc_Pattern>>();
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

        // Sort tiles by position
        return tiles.OrderBy(o => o.posX).ThenBy(o => o.posZ).ToArray();
    }

    bool PatternValidation(Mb_Tile[] allTiles, Sc_Pattern pattern)
    {
        Mb_Tile[] playerTiles = getAllTileWithPlayer(allTiles);
        int[] patternKeyPointsIndices = pattern.Matrix.GetTrueValuesIndices().OrderBy(i => pattern.Matrix.GetLocation(i).x).ThenBy(i => pattern.Matrix.GetLocation(i).y).ToArray();

        // Check for keypoints distance in patterns
        bool flagX1 = playerTiles[0].posX - playerTiles[1].posX == pattern.Matrix.GetLocation(patternKeyPointsIndices[0]).x - pattern.Matrix.GetLocation(patternKeyPointsIndices[1]).x;
        bool flagX2 = playerTiles[0].posX - playerTiles[2].posX == pattern.Matrix.GetLocation(patternKeyPointsIndices[0]).x - pattern.Matrix.GetLocation(patternKeyPointsIndices[2]).x;

        bool flagZ1 = playerTiles[0].posZ - playerTiles[1].posZ == pattern.Matrix.GetLocation(patternKeyPointsIndices[0]).y - pattern.Matrix.GetLocation(patternKeyPointsIndices[1]).y;
        bool flagZ2 = playerTiles[0].posZ - playerTiles[2].posZ == pattern.Matrix.GetLocation(patternKeyPointsIndices[0]).y - pattern.Matrix.GetLocation(patternKeyPointsIndices[2]).y;

        return flagX1 && flagX2 && flagZ1 && flagZ2;
    }
    
    void GenerateStartPattern()
    {
        // Load the first 5 patterns available
        /*int count = 0;
        foreach (var item in availablePatternList)
        {
            if (++count > patternCount + 1)
            {
                break;
            }
            // Debug.Log("Create pattern: " + item.Name);
            currentPatternsList.Add(item);
        }*/

        for (int i = 0; i < 5; i++)
        {
            currentPatternsList.Add(GetRandomPatternDifferentOfCurrents());
        }
        Debug.LogError("Go");
        futurePattern = GetRandomPatternDifferentOfCurrents();
        Debug.LogError(futurePattern);
        for (int i = 0; i < currentPatternsList.Count(); i++)
        {
            Sc_Pattern pattern = currentPatternsList[i];
            GameManager.Instance.uiManager.UpdatePatternsBarIcon(i, pattern);
        }
        GameManager.Instance.uiManager.UpdatePatternsBarIcon(currentPatternsList.Count(), futurePattern);
    }

    /*float random()
    {
        Vector2 st = new Vector2(randomSeed, randomSeed);
        randomSeed += 3;
        float f = (Mathf.Sin(Vector2.Dot(st, new Vector2(12.9898f, 78.233f))) * 43758.5453123f);
        return f - Mathf.Floor(f);
    }*/

    System.Random rnd = new System.Random();

    private Sc_Pattern GetRandomPatternDifferentOfCurrents()
    {
        List<Sc_Pattern> backupCurrentPatternsList = new List<Sc_Pattern>();
        List<Sc_Pattern> result = null;
        while (true)
        {
            //ADDITIONER TOUT LES POIDS POSSIBLE
            int total = 0;
            Dictionary<string, Tuple<int, List<Sc_Pattern>>> dic = new Dictionary<string, Tuple<int, List<Sc_Pattern>>>();
            foreach (var cat in levelConfig.patternCategories)
            {
                var lis = availablePatternList.Where(e => e.Category == cat.Key).ToList();
                var tup = new Tuple<int, List<Sc_Pattern>>(total + cat.Value - 1, lis);
                dic.Add(cat.Key, tup);
                total += cat.Value;
            }
            //TIRER UNE RANDOM ENTRE 0 ET CE POID
            total -= 1;
            //var pond = Mathf.RoundToInt(random()*total);
            var pond = rnd.Next(total);

            if (result != null)
                result.Clear();
            else
                result = new List<Sc_Pattern>();
            //int cumul = 0;

                //CHECKER LE DICT EN ATTENDANT QUE LE TOTAL SOIT < A LA VALEUR DE POID CUMULE
            foreach (var entry in levelConfig.patternCategories)
            {
                pond -= entry.Value;
                if ( pond  <=0 )
                {
                    Debug.LogError("BEFORE" +result);
                    result = dic[entry.Key].Item2;
                    
                    break;
                }
                              
            }

            //RETIRER LES PATTERNS DES PATTERNS PRESENTS ET DE LA PREVIEW DE LA LISTE POSSIBLE
           
            foreach (Sc_Pattern item in currentPatternsList)
            {
                result.Remove(item);
                backupCurrentPatternsList.Add(item);
            }
            result.Remove(futurePattern);

            if (!( result.Count() == 0))
            {
                break;
            }
        }
        
        

        //TIRER UNE RANDOM DANS LA LISTE QUI A ARRETE LA VALEUR DE POID
        Sc_Pattern stock = result[rnd.Next( result.Count())];

        //REMETTRE LES PATTERNS QU ON VIENT DE RETIRER
        result.AddRange(backupCurrentPatternsList);

        return stock;






        /*
        List<Sc_Pattern> copy = new List<Sc_Pattern>(availablePatternList);
        foreach (Sc_Pattern item in currentPatternsList)
        {
            foreach(Sc_Pattern comparedPattern in availablePatternList)
            {
                if (item == comparedPattern)
                    copy.Remove(item);

            }
            //copy.Remove(item);
        }
        copy.Remove(futurePattern);

        /*RandomizeList(ref copy);

        if (copy.Count() > 0)
        {
            return copy[0];
        }
        return null;*/
        /*
        int total = 0;
        Dictionary<string, Tuple<int, List<Sc_Pattern>>> dic = new Dictionary<string, Tuple<int, List<Sc_Pattern>>>();
        foreach (var cat in levelConfig.patternCategories)
        {
            var lis = copy.Where(e => e.Category == cat.Key).ToList();
            var tup = new Tuple<int, List<Sc_Pattern>>(total + cat.Value - 1, lis);
            dic.Add(cat.Key, tup);
            total += cat.Value;
        }*
        //Debug.Log(dic.Count());
        var rnd = new System.Random();
        var pond = rnd.Next(total);
        foreach (var i in dic)
        {
            if (pond < i.Value.Item1)
            {
                var l = i.Value.Item2;
                return l[rnd.Next(0, l.Count())];
            }
        }
        return null;
        */
    }

    public void RotatePattern(int indexInList)
    {
        currentPatternsList.RemoveAt(indexInList);
        currentPatternsList.Add(futurePattern);

        for (int i = 0; i < currentPatternsList.Count(); i++)
        {
            GameManager.Instance.uiManager.UpdatePatternsBarIcon(i, currentPatternsList[i]);
        }

        futurePattern = GetRandomPatternDifferentOfCurrents();
        GameManager.Instance.uiManager.UpdatePatternsBarIcon(currentPatternsList.Count(), futurePattern);
    }
}
