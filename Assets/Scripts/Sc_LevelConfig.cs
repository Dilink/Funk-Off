using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "New Level Config", menuName = "Custom/Level Configuration")]
public class Sc_LevelConfig : SerializedScriptableObject
{
    // Key: Pattern category type
    // Value: Pattern category weight
    public Dictionary<string, int> patternCategories = new Dictionary<string, int>() {
        { "2x2", 1 },
        { "2x3", 1 },
        { "3x1", 1 },
        { "3x2", 1 },
        { "3x3", 1 },
    };
}
