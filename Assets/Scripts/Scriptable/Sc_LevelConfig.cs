using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct PatternCategory
{
    public string Name;
    public int Weight;

    public PatternCategory(string Name, int Weight)
    {
        this.Name = Name;
        this.Weight = Weight;
    }
}

[System.Serializable]
public struct Round
{
    public List<PatternCategory> patternCategories;
    public List<Sc_AIPattern> aiPatterns;
    public int turnLimit;

    /*public PatternCategories()
    {
        patternCategories = new List<PatternCategory>()
        {
            new PatternCategory("2x2", 1),
            new PatternCategory("2x3", 1),
            new PatternCategory("3x1", 1),
            new PatternCategory("3x2", 1),
            new PatternCategory("3x3", 1),
        };
    }*/
}

[CreateAssetMenu(fileName = "New Level Config", menuName = "Custom/Level Configuration")]
public class Sc_LevelConfig : ScriptableObject
{
    public List<Round> rounds = new List<Round>();

    [Range(0, 4)]
    public int minPatternsToCancelAttack = 1;
    [Range(1, 5)]
    public int maxPatternsToCancelAttack = 3;

    [Min(1)]
    public int roundCount = 1;
}
