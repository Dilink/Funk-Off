using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternModifier
{

}

[CreateAssetMenu(fileName = "New Pattern", menuName = "Custom/Pattern")]
public class Sc_Pattern : ScriptableObject
{
    public string Title;
    [PreviewField(70, ObjectFieldAlignment.Left)]
    public Sprite sprite;
    public Matrix3x3Bool Matrix;
    public List<PatternModifier> modifiers;
    public DanceType danceToPlay;
    [ReadOnly]
    public string Name;
    [ReadOnly]
    public string Category;
}

public enum DanceType
{
    Dance1,Dance2,Dance3,Dance4,Dance5,Dance6
}
